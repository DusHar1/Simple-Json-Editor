Imports System
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class JsonFileService

    Public Function Load(path As String) As JsonNode
        If Not File.Exists(path) Then
            Throw New FileNotFoundException("File not found.", path)
        End If

        Dim json As String = File.ReadAllText(path)
        Dim token As JToken = JToken.Parse(json)
        Dim root As JsonNode = JsonNode.FromJToken("$", token, Nothing)
        Return root
    End Function

    Public Sub Save(path As String, root As JsonNode)
        If String.IsNullOrWhiteSpace(path) Then
            Throw New ArgumentException("Path is required.", NameOf(path))
        End If

        Dim token As JToken = root.ToJToken()
        Dim json As String = token.ToString(Formatting.Indented)
        File.WriteAllText(path, json)
    End Sub

End Class
