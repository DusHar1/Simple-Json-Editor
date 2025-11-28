Imports System
Imports System.Collections.Generic
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Enum JsonNodeKind
    ObjectNode
    ArrayNode
    ValueNode
End Enum


Public Class JsonNode

    Public Property Name As String
    Public Property Kind As JsonNodeKind
    Public Property ValueType As TypeCode
    Public Property Value As String
    Public Property Children As List(Of JsonNode)
    Public Property Parent As JsonNode


    Public Sub New()
        Children = New List(Of JsonNode)()
        ValueType = TypeCode.String
        Value = String.Empty
    End Sub

    Public Sub New(name As String, kind As JsonNodeKind, Optional parent As JsonNode = Nothing)
        Me.New()
        Me.Name = name
        Me.Kind = kind
        Me.Parent = parent
    End Sub

    Public Shared Function FromJToken(name As String, token As JToken, Optional parent As JsonNode = Nothing) As JsonNode
        Dim node As New JsonNode(name, JsonNodeKind.ValueNode, parent)

        Select Case token.Type
            Case JTokenType.[Object]
                node.Kind = JsonNodeKind.ObjectNode
                For Each prop As JProperty In DirectCast(token, JObject).Properties()
                    Dim child = FromJToken(prop.Name, prop.Value, node)
                    node.Children.Add(child)
                Next

            Case JTokenType.Array
                node.Kind = JsonNodeKind.ArrayNode
                Dim arr = DirectCast(token, JArray)
                For i As Integer = 0 To arr.Count - 1
                    Dim child = FromJToken(i.ToString(), arr(i), node)
                    node.Children.Add(child)
                Next

            Case Else
                node.Kind = JsonNodeKind.ValueNode
                Dim jv As JValue = DirectCast(token, JValue)
                If jv.Value Is Nothing Then
                    node.ValueType = TypeCode.Empty
                    node.Value = String.Empty
                ElseIf TypeOf jv.Value Is Boolean Then
                    node.ValueType = TypeCode.Boolean
                    node.Value = CBool(jv.Value).ToString().ToLowerInvariant()
                ElseIf TypeOf jv.Value Is Integer OrElse TypeOf jv.Value Is Long OrElse TypeOf jv.Value Is Short Then
                    node.ValueType = TypeCode.Int64
                    node.Value = Convert.ToInt64(jv.Value).ToString()
                ElseIf TypeOf jv.Value Is Double OrElse TypeOf jv.Value Is Single OrElse TypeOf jv.Value Is Decimal Then
                    node.ValueType = TypeCode.Double
                    node.Value = Convert.ToDouble(jv.Value).ToString(System.Globalization.CultureInfo.InvariantCulture)
                Else
                    node.ValueType = TypeCode.String
                    node.Value = jv.Value.ToString()
                End If
        End Select

        Return node
    End Function

    Public Function ToJToken() As JToken
        Select Case Kind
            Case JsonNodeKind.ObjectNode
                Dim obj As New JObject()
                For Each child In Children
                    Dim childToken = child.ToJToken()
                    Dim propName = If(String.IsNullOrEmpty(child.Name), "item", child.Name)
                    obj(propName) = childToken
                Next
                Return obj

            Case JsonNodeKind.ArrayNode
                Dim arr As New JArray()
                For Each child In Children
                    arr.Add(child.ToJToken())
                Next
                Return arr

            Case JsonNodeKind.ValueNode
                Return CreateJValue()
            Case Else
                Return JValue.CreateNull()
        End Select
    End Function

    Private Function CreateJValue() As JValue
        Select Case ValueType
            Case TypeCode.Empty
                Return JValue.CreateNull()

            Case TypeCode.Boolean
                Dim b As Boolean
                If Boolean.TryParse(Value, b) Then
                    Return New JValue(b)
                Else
                    Return New JValue(False)
                End If

            Case TypeCode.Int32, TypeCode.Int64, TypeCode.Int16
                Dim l As Long
                If Long.TryParse(Value, l) Then
                    Return New JValue(l)
                Else
                    Return New JValue(Value)
                End If

            Case TypeCode.Double, TypeCode.Decimal, TypeCode.Single
                Dim d As Double
                If Double.TryParse(Value, System.Globalization.NumberStyles.Any,
                                   System.Globalization.CultureInfo.InvariantCulture, d) Then
                    Return New JValue(d)
                Else
                    Return New JValue(Value)
                End If

            Case Else
                Return New JValue(Value)
        End Select
    End Function

    Public Shared Function CreateValueNode(parent As JsonNode, name As String, typeCode As TypeCode, value As String) As JsonNode
        Dim n As New JsonNode(name, JsonNodeKind.ValueNode, parent)
        n.ValueType = typeCode
        n.Value = value
        Return n
    End Function

    Public Function CloneDeep(Optional newParent As JsonNode = Nothing) As JsonNode
        Dim copy As New JsonNode(Me.Name, Me.Kind, newParent)
        copy.ValueType = Me.ValueType
        copy.Value = Me.Value
        For Each child In Me.Children
            Dim childCopy = child.CloneDeep(copy)
            copy.Children.Add(childCopy)
        Next
        Return copy
    End Function

End Class
