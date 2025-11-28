Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Drawing
Imports System.Collections.Generic
Imports ConfigJsonUtility.JsonNode


Public Class FormConfigEditor

    Private _root As JsonNode
    Private ReadOnly _service As New JsonFileService()
    Private _currentFilePath As String = Nothing
    Private _isUpdatingUi As Boolean = False

    Private _lastSearchText As String = ""
    Private _lastSearchNode As TreeNode = Nothing

    Private _clipboardNode As JsonNode = Nothing
    Private _undoStack As New Stack(Of JToken)()

    Private Sub FormConfigEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeCombos()
        CreateNewConfig()
        AddHandler rtbJsonPreview.VScroll, AddressOf rtbJsonPreview_Scrolled
        AddHandler rtbJsonPreview.TextChanged, AddressOf rtbJsonPreview_TextChanged
    End Sub

    Private Sub InitializeCombos()
        cmbKind.Items.Clear()
        cmbKind.Items.Add(JsonNodeKind.ObjectNode.ToString())
        cmbKind.Items.Add(JsonNodeKind.ArrayNode.ToString())
        cmbKind.Items.Add(JsonNodeKind.ValueNode.ToString())

        cmbValueType.Items.Clear()
        cmbValueType.Items.Add("String")
        cmbValueType.Items.Add("Int64")
        cmbValueType.Items.Add("Double")
        cmbValueType.Items.Add("Boolean")
        cmbValueType.Items.Add("Null")

        cmbKind.SelectedIndex = 0
        cmbValueType.SelectedIndex = 0
    End Sub

    Private Sub CreateNewConfig()
        _root = New JsonNode("$", JsonNodeKind.ObjectNode)
        _currentFilePath = Nothing
        _undoStack.Clear()
        RefreshTree()
        RefreshPreview()
        Me.Text = "Config JSON Utility - New Config"
    End Sub

    Private Sub PushUndoSnapshot()
        If _root IsNot Nothing Then
            Dim snap As JToken = _root.ToJToken().DeepClone()
            _undoStack.Push(snap)
        End If
    End Sub

    Private Sub RefreshTree()
        tvConfig.BeginUpdate()
        tvConfig.Nodes.Clear()

        If _root IsNot Nothing Then
            Dim rootNode As TreeNode = CreateTreeNodeFromJsonNode(_root)
            tvConfig.Nodes.Add(rootNode)
            rootNode.Expand()
        End If

        tvConfig.EndUpdate()
    End Sub

    Private Function CreateTreeNodeFromJsonNode(node As JsonNode) As TreeNode
        Dim text As String = GetDisplayText(node)
        Dim tvNode As New TreeNode(text)
        tvNode.Tag = node

        For Each child In node.Children
            tvNode.Nodes.Add(CreateTreeNodeFromJsonNode(child))
        Next

        Return tvNode
    End Function

    Private Function GetDisplayText(node As JsonNode) As String
        Dim namePart As String = node.Name
        If node.Name = "$" Then namePart = "(root config)"

        Select Case node.Kind
            Case JsonNodeKind.ObjectNode
                Return namePart & " {section}"
            Case JsonNodeKind.ArrayNode
                Return namePart & " [array]"
            Case JsonNodeKind.ValueNode
                Dim typeStr As String = node.ValueType.ToString()
                Dim val As String = node.Value
                Return namePart & " = " & val & " (" & typeStr & ")"
            Case Else
                Return namePart
        End Select
    End Function

    Private Sub tvConfig_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvConfig.AfterSelect
        Dim node As JsonNode = TryCast(e.Node.Tag, JsonNode)
        If node Is Nothing Then Return

        _isUpdatingUi = True
        Try
            txtName.Text = node.Name
            cmbKind.SelectedItem = node.Kind.ToString()

            ' Value editor behavior:
            ' Object: value controls disabled
            ' Array & Value: value controls enabled
            If node.Kind = JsonNodeKind.ObjectNode Then
                txtValue.Enabled = False
                cmbValueType.Enabled = False
                txtValue.Text = ""
            Else
                txtValue.Enabled = True
                cmbValueType.Enabled = True

                Select Case node.ValueType
                    Case TypeCode.Int64
                        cmbValueType.SelectedItem = "Int64"
                    Case TypeCode.Double
                        cmbValueType.SelectedItem = "Double"
                    Case TypeCode.Boolean
                        cmbValueType.SelectedItem = "Boolean"
                    Case TypeCode.Empty
                        cmbValueType.SelectedItem = "Null"
                    Case Else
                        cmbValueType.SelectedItem = "String"
                End Select

                txtValue.Text = node.Value
            End If

            PopulateParentCombo(node)
        Finally
            _isUpdatingUi = False
        End Try
    End Sub

    Private Sub PopulateParentCombo(selected As JsonNode)
        cmbParent.Items.Clear()

        If _root Is Nothing OrElse selected Is Nothing Then Return

        Dim candidates As New List(Of JsonNode)()
        CollectParentCandidates(_root, selected, candidates)

        For Each n In candidates
            cmbParent.Items.Add(New ParentOption(GetNodePath(n), n))
        Next

        ' Select current parent if possible
        If selected.Parent IsNot Nothing Then
            For i As Integer = 0 To cmbParent.Items.Count - 1
                Dim opt = TryCast(cmbParent.Items(i), ParentOption)
                If opt IsNot Nothing AndAlso opt.Node Is selected.Parent Then
                    cmbParent.SelectedIndex = i
                    Exit For
                End If
            Next
        Else
            cmbParent.SelectedIndex = -1
        End If
    End Sub

    Private Sub CollectParentCandidates(current As JsonNode, selected As JsonNode, list As List(Of JsonNode))
        If current IsNot selected AndAlso current.Kind <> JsonNodeKind.ValueNode Then
            ' Don't allow moving into own descendants
            If Not IsDescendantOf(current, selected) Then
                list.Add(current)
            End If
        End If

        For Each child In current.Children
            CollectParentCandidates(child, selected, list)
        Next
    End Sub

    Private Function IsDescendantOf(candidate As JsonNode, potentialAncestor As JsonNode) As Boolean
        Dim cur As JsonNode = candidate.Parent
        While cur IsNot Nothing
            If cur Is potentialAncestor Then
                Return True
            End If
            cur = cur.Parent
        End While
        Return False
    End Function

    Private Function GetNodePath(node As JsonNode) As String
        Dim parts As New List(Of String)()
        Dim cur As JsonNode = node
        While cur IsNot Nothing
            parts.Insert(0, If(cur.Name, "?"))
            cur = cur.Parent
        End While
        Return String.Join(" -> ", parts.ToArray())
    End Function

    Private Sub btnApplyNode_Click(sender As Object, e As EventArgs) Handles btnApplyNode.Click
        Dim tvNode As TreeNode = tvConfig.SelectedNode
        If tvNode Is Nothing Then Return

        Dim node As JsonNode = TryCast(tvNode.Tag, JsonNode)
        If node Is Nothing Then Return

        PushUndoSnapshot()

        ' Name change (except root)
        If node IsNot _root Then
            node.Name = txtName.Text
        End If

        ' Kind change
        Dim newKind As JsonNodeKind = node.Kind
        If cmbKind.SelectedItem IsNot Nothing Then
            newKind = DirectCast([Enum].Parse(GetType(JsonNodeKind),
                                              cmbKind.SelectedItem.ToString()), JsonNodeKind)
        End If

        If node Is _root AndAlso newKind <> JsonNodeKind.ObjectNode Then
            MessageBox.Show("Root config node must be an object.", "Not Allowed",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbKind.SelectedItem = JsonNodeKind.ObjectNode.ToString()
            Return
        End If

        If newKind <> node.Kind Then
            node.Kind = newKind
            Select Case node.Kind
                Case JsonNodeKind.ObjectNode
                    node.Value = String.Empty
                    node.ValueType = TypeCode.String
                Case JsonNodeKind.ArrayNode
                    If node.ValueType = TypeCode.Empty Then
                        node.ValueType = TypeCode.String
                    End If
                Case JsonNodeKind.ValueNode
                    node.Children.Clear()
                    node.ValueType = TypeCode.String
                    node.Value = txtValue.Text
            End Select
        End If

        ' Value / type for arrays and values
        If node.Kind = JsonNodeKind.ArrayNode OrElse node.Kind = JsonNodeKind.ValueNode Then
            Dim typeCode As TypeCode = TypeCode.String
            Select Case cmbValueType.SelectedItem?.ToString()
                Case "Int64"
                    typeCode = TypeCode.Int64
                Case "Double"
                    typeCode = TypeCode.Double
                Case "Boolean"
                    typeCode = TypeCode.Boolean
                Case "Null"
                    typeCode = TypeCode.Empty
                Case Else
                    typeCode = TypeCode.String
            End Select
            node.ValueType = typeCode
            node.Value = txtValue.Text
        End If

        ' Re-parenting if parent combo changed
        Dim selectedParent As JsonNode = node.Parent
        Dim optParent As ParentOption = TryCast(cmbParent.SelectedItem, ParentOption)
        If optParent IsNot Nothing Then
            selectedParent = optParent.Node
        End If

        If selectedParent IsNot node.Parent AndAlso selectedParent IsNot Nothing Then
            ' Remove from old parent
            If node.Parent IsNot Nothing Then
                node.Parent.Children.Remove(node)
            End If

            ' Add to new parent
            node.Parent = selectedParent

            If selectedParent.Kind = JsonNodeKind.ArrayNode Then
                node.Name = selectedParent.Children.Count.ToString()
            End If
            selectedParent.Children.Add(node)

            ' Move TreeNode visually
            Dim newParentTree As TreeNode = FindTreeNodeByJsonNode(selectedParent)
            If newParentTree IsNot Nothing Then
                tvNode.Remove()
                newParentTree.Nodes.Add(tvNode)
                newParentTree.Expand()
            End If
        End If

        tvNode.Text = GetDisplayText(node)
        RefreshPreview()
    End Sub

    Private Function FindTreeNodeByJsonNode(target As JsonNode) As TreeNode
        If tvConfig.Nodes.Count = 0 OrElse target Is Nothing Then Return Nothing
        Return FindTreeNodeRecursive(tvConfig.Nodes(0), target)
    End Function

    Private Function FindTreeNodeRecursive(node As TreeNode, target As JsonNode) As TreeNode
        Dim jNode As JsonNode = TryCast(node.Tag, JsonNode)
        If jNode Is target Then
            Return node
        End If

        For Each child As TreeNode In node.Nodes
            Dim found = FindTreeNodeRecursive(child, target)
            If found IsNot Nothing Then Return found
        Next

        Return Nothing
    End Function

    Private Sub btnAddSection_Click(sender As Object, e As EventArgs) Handles btnAddSection.Click, mnuAddSection.Click
        Dim tvNode As TreeNode = tvConfig.SelectedNode
        If tvNode Is Nothing Then Return

        Dim parentNode As JsonNode = TryCast(tvNode.Tag, JsonNode)
        If parentNode Is Nothing Then Return

        If parentNode.Kind <> JsonNodeKind.ObjectNode Then
            MessageBox.Show("Sections can only be added under object/section nodes.",
                            "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        PushUndoSnapshot()

        Dim sectionName As String = "NewSection"
        Dim child As New JsonNode(sectionName, JsonNodeKind.ObjectNode, parentNode)
        parentNode.Children.Add(child)

        Dim childTvNode As TreeNode = CreateTreeNodeFromJsonNode(child)
        tvNode.Nodes.Add(childTvNode)
        tvNode.Expand()
        tvConfig.SelectedNode = childTvNode

        RefreshPreview()
    End Sub

    Private Sub btnAddSetting_Click(sender As Object, e As EventArgs) Handles btnAddSetting.Click, mnuAddSetting.Click
        Dim tvNode As TreeNode = tvConfig.SelectedNode
        If tvNode Is Nothing Then Return

        Dim parentNode As JsonNode = TryCast(tvNode.Tag, JsonNode)
        If parentNode Is Nothing Then Return

        If parentNode.Kind <> JsonNodeKind.ObjectNode Then
            MessageBox.Show("Settings can only be added under object/section nodes.",
                            "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        PushUndoSnapshot()

        Dim settingName As String = "NewSetting"
        Dim child As JsonNode = JsonNode.CreateValueNode(parentNode, settingName, TypeCode.String, "")
        parentNode.Children.Add(child)

        Dim childTvNode As TreeNode = CreateTreeNodeFromJsonNode(child)
        tvNode.Nodes.Add(childTvNode)
        tvNode.Expand()
        tvConfig.SelectedNode = childTvNode

        RefreshPreview()
    End Sub

    Private Sub btnAddArrayItem_Click(sender As Object, e As EventArgs) Handles btnAddArrayItem.Click, mnuAddArrayItem.Click
        Dim tvNode As TreeNode = tvConfig.SelectedNode
        If tvNode Is Nothing Then Return

        Dim parentNode As JsonNode = TryCast(tvNode.Tag, JsonNode)
        If parentNode Is Nothing Then Return

        PushUndoSnapshot()

        If parentNode.Kind = JsonNodeKind.ArrayNode Then
            Dim idx As String = parentNode.Children.Count.ToString()
            Dim child As JsonNode = JsonNode.CreateValueNode(parentNode, idx, TypeCode.String, "")
            parentNode.Children.Add(child)

            Dim childTvNode As TreeNode = CreateTreeNodeFromJsonNode(child)
            tvNode.Nodes.Add(childTvNode)
            tvNode.Expand()
            tvConfig.SelectedNode = childTvNode
        ElseIf parentNode.Kind = JsonNodeKind.ObjectNode Then
            Dim arrayName As String = "NewArray"
            Dim arrayNode As New JsonNode(arrayName, JsonNodeKind.ArrayNode, parentNode)
            parentNode.Children.Add(arrayNode)

            Dim idx As String = "0"
            Dim item As JsonNode = JsonNode.CreateValueNode(arrayNode, idx, TypeCode.String, "")
            arrayNode.Children.Add(item)

            Dim arrayTvNode As TreeNode = CreateTreeNodeFromJsonNode(arrayNode)
            tvNode.Nodes.Add(arrayTvNode)
            arrayTvNode.Expand()
            tvConfig.SelectedNode = arrayTvNode.FirstNode
        Else
            MessageBox.Show("Array items can be added to array nodes or object/section nodes.",
                            "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        RefreshPreview()
    End Sub

    Private Sub btnDeleteNode_Click(sender As Object, e As EventArgs) Handles btnDeleteNode.Click, mnuDeleteNode.Click
        Dim tvNode As TreeNode = tvConfig.SelectedNode
        If tvNode Is Nothing Then Return

        Dim node As JsonNode = TryCast(tvNode.Tag, JsonNode)
        If node Is Nothing Then Return

        If node Is _root Then
            MessageBox.Show("Cannot delete the root config node.", "Not Allowed",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        PushUndoSnapshot()

        If node.Parent IsNot Nothing Then
            node.Parent.Children.Remove(node)
        End If

        tvNode.Remove()
        RefreshPreview()
    End Sub

    Private Sub mnuExpandAll_Click(sender As Object, e As EventArgs) Handles mnuExpandAll.Click
        tvConfig.BeginUpdate()
        tvConfig.ExpandAll()
        tvConfig.EndUpdate()
    End Sub

    Private Sub mnuCollapseAll_Click(sender As Object, e As EventArgs) Handles mnuCollapseAll.Click
        tvConfig.BeginUpdate()
        tvConfig.CollapseAll()
        tvConfig.EndUpdate()
    End Sub

    Private Sub mnuCopyNode_Click(sender As Object, e As EventArgs) Handles mnuCopyNode.Click, btnCopy.Click
        Dim tvNode As TreeNode = tvConfig.SelectedNode
        If tvNode Is Nothing Then Return

        Dim node As JsonNode = TryCast(tvNode.Tag, JsonNode)
        If node Is Nothing Then Return

        _clipboardNode = node.CloneDeep(Nothing)
    End Sub

    Private Sub mnuPasteNode_Click(sender As Object, e As EventArgs) Handles mnuPasteNode.Click, btnPaste.Click
        If _clipboardNode Is Nothing Then Return

        Dim tvNode As TreeNode = tvConfig.SelectedNode
        If tvNode Is Nothing Then Return

        Dim targetNode As JsonNode = TryCast(tvNode.Tag, JsonNode)
        If targetNode Is Nothing Then Return

        PushUndoSnapshot()

        Dim parent As JsonNode = Nothing
        Dim parentTree As TreeNode = Nothing

        If targetNode.Kind = JsonNodeKind.ObjectNode OrElse targetNode.Kind = JsonNodeKind.ArrayNode Then
            parent = targetNode
            parentTree = tvNode
        Else
            parent = targetNode.Parent
            parentTree = tvNode.Parent
        End If

        If parent Is Nothing OrElse parentTree Is Nothing Then Return

        If parent.Kind <> JsonNodeKind.ObjectNode AndAlso parent.Kind <> JsonNodeKind.ArrayNode Then
            MessageBox.Show("Cannot paste into this node type.", "Paste",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim newNode As JsonNode = _clipboardNode.CloneDeep(parent)
        If parent.Kind = JsonNodeKind.ArrayNode Then
            newNode.Name = parent.Children.Count.ToString()
        End If

        parent.Children.Add(newNode)

        Dim newTreeNode As TreeNode = CreateTreeNodeFromJsonNode(newNode)
        parentTree.Nodes.Add(newTreeNode)
        parentTree.Expand()
        tvConfig.SelectedNode = newTreeNode

        RefreshPreview()
    End Sub

    Private Sub btnUndo_Click(sender As Object, e As EventArgs) Handles btnUndo.Click
        If _undoStack.Count = 0 Then
            MessageBox.Show("Nothing to undo.", "Undo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim snap As JToken = _undoStack.Pop()
        _root = JsonNode.FromJToken("$", snap, Nothing)
        RefreshTree()
        RefreshPreview()
    End Sub

    Private Sub btnNewConfig_Click(sender As Object, e As EventArgs) Handles btnNewConfig.Click
        If ConfirmLoseChanges() Then
            CreateNewConfig()
        End If
    End Sub

    Private Sub btnOpenConfig_Click(sender As Object, e As EventArgs) Handles btnOpenConfig.Click
        If Not ConfirmLoseChanges() Then Return

        ofdConfig.Filter = "JSON config files (*.json)|*.json|All files (*.*)|*.*"
        ofdConfig.Title = "Open Config JSON"

        If ofdConfig.ShowDialog(Me) = DialogResult.OK Then
            Try
                ' Show raw JSON first in the preview
                Dim rawJson As String = File.ReadAllText(ofdConfig.FileName)
                rtbJsonPreview.Text = rawJson
                pnlLineNumbers.Invalidate()

                ' Try to parse via service
                _root = _service.Load(ofdConfig.FileName)

                If _root.Kind <> JsonNodeKind.ObjectNode Then
                    MessageBox.Show("Config root should be an object. File will still be loaded.",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

                _currentFilePath = ofdConfig.FileName
                _undoStack.Clear()
                RefreshTree()
                RefreshPreview()
                Me.Text = "Config JSON Utility - " & _currentFilePath

            Catch ex As JsonReaderException
                HighlightJsonError(ex.LineNumber, ex.LinePosition)
                MessageBox.Show("JSON error while loading file:" & Environment.NewLine &
                                ex.Message, "Invalid JSON",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show("Error opening config:" & Environment.NewLine & ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnSaveConfig_Click(sender As Object, e As EventArgs) Handles btnSaveConfig.Click
        Try
            If _root Is Nothing Then Return

            If String.IsNullOrWhiteSpace(_currentFilePath) Then
                sfdConfig.Filter = "JSON config files (*.json)|*.json|All files (*.*)|*.*"
                sfdConfig.Title = "Save Config JSON"
                sfdConfig.DefaultExt = "json"

                If sfdConfig.ShowDialog(Me) <> DialogResult.OK Then
                    Return
                End If

                _currentFilePath = sfdConfig.FileName
            End If

            _service.Save(_currentFilePath, _root)
            RefreshPreview()
            Me.Text = "Config JSON Utility - " & _currentFilePath
            MessageBox.Show("Config saved successfully.", "Saved",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error saving config:" & Environment.NewLine & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Function ConfirmLoseChanges() As Boolean
        Dim result = MessageBox.Show("Any unsaved changes will be lost. Continue?",
                                     "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        Return result = DialogResult.Yes
    End Function

    Private Sub RefreshPreview()
        If _root Is Nothing Then
            rtbJsonPreview.Clear()
            Return
        End If

        Dim token As JToken = _root.ToJToken()
        Dim json As String = token.ToString(Formatting.Indented)

        Dim selStart As Integer = rtbJsonPreview.SelectionStart
        Dim selLen As Integer = rtbJsonPreview.SelectionLength

        rtbJsonPreview.SuspendLayout()
        rtbJsonPreview.Text = json
        rtbJsonPreview.SelectionStart = selStart
        rtbJsonPreview.SelectionLength = selLen
        rtbJsonPreview.ResumeLayout()

        pnlLineNumbers.Invalidate()
    End Sub

    Private Sub HighlightJsonError(line As Integer, position As Integer)
        Try
            If line < 1 Then line = 1
            If position < 1 Then position = 1

            Dim text As String = rtbJsonPreview.Text
            If String.IsNullOrEmpty(text) Then Return

            Dim lines() As String = text.Split(New String() {Environment.NewLine, "\n"}, StringSplitOptions.None)

            Dim idx As Integer = 0

            ' Sum length of all previous lines
            For i As Integer = 0 To Math.Min(lines.Length - 1, line - 2)
                idx += lines(i).Length + Environment.NewLine.Length
            Next

            ' Add column position
            idx += Math.Max(0, position - 1)

            ' Bounds check
            If idx >= text.Length Then idx = text.Length - 1
            If idx < 0 Then idx = 0

            ' Highlight
            rtbJsonPreview.SelectionStart = idx
            rtbJsonPreview.SelectionLength = 1
            rtbJsonPreview.SelectionBackColor = Color.Yellow
            rtbJsonPreview.SelectionColor = Color.Red
            rtbJsonPreview.ScrollToCaret()

        Catch
            ' Fallback: highlight entire text
            rtbJsonPreview.SelectionStart = 0
            rtbJsonPreview.SelectionLength = rtbJsonPreview.TextLength
            rtbJsonPreview.SelectionBackColor = Color.Yellow
            rtbJsonPreview.SelectionColor = Color.Red
        End Try
    End Sub


    Private Sub rtbJsonPreview_Scrolled(sender As Object, e As EventArgs)
        pnlLineNumbers.Invalidate()
    End Sub

    Private Sub rtbJsonPreview_TextChanged(sender As Object, e As EventArgs)
        pnlLineNumbers.Invalidate()
    End Sub

    Private Sub pnlLineNumbers_Paint(sender As Object, e As PaintEventArgs) Handles pnlLineNumbers.Paint
        Dim font As Font = rtbJsonPreview.Font
        Dim firstIndex As Integer = rtbJsonPreview.GetCharIndexFromPosition(New Point(0, 0))
        Dim firstLine As Integer = rtbJsonPreview.GetLineFromCharIndex(firstIndex)

        Dim lastIndex As Integer = rtbJsonPreview.GetCharIndexFromPosition(New Point(0, rtbJsonPreview.ClientSize.Height))
        Dim lastLine As Integer = rtbJsonPreview.GetLineFromCharIndex(lastIndex)

        Dim lineHeight As Integer = TextRenderer.MeasureText("X", font).Height

        e.Graphics.Clear(SystemColors.Control)

        Dim y As Integer = -(rtbJsonPreview.GetPositionFromCharIndex(firstIndex).Y Mod lineHeight)

        For line As Integer = firstLine To lastLine
            Dim lineNumberText As String = (line + 1).ToString()
            Dim rect As New Rectangle(0, y, pnlLineNumbers.Width - 2, lineHeight)
            TextRenderer.DrawText(e.Graphics, lineNumberText, font, rect, Color.Gray,
                                  TextFormatFlags.Right Or TextFormatFlags.VerticalCenter)
            y += lineHeight
        Next
    End Sub

    Private Sub btnSearchNext_Click(sender As Object, e As EventArgs) Handles btnSearchNext.Click
        Dim text As String = txtSearch.Text
        If String.IsNullOrWhiteSpace(text) Then Return

        Dim searchValues As Boolean = chkSearchValues.Checked

        If _lastSearchText <> text Then
            _lastSearchText = text
            _lastSearchNode = Nothing
        End If

        Dim startNode As TreeNode
        If _lastSearchNode Is Nothing Then
            If tvConfig.Nodes.Count = 0 Then Return
            startNode = tvConfig.Nodes(0)
        Else
            startNode = GetNextNode(_lastSearchNode)
        End If

        Dim found As TreeNode = FindNode(startNode, text, searchValues)
        If found IsNot Nothing Then
            _lastSearchNode = found
            tvConfig.SelectedNode = found
            tvConfig.Focus()
        Else
            MessageBox.Show("No more matches.", "Search",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            _lastSearchNode = Nothing
        End If
    End Sub

    Private Function GetNextNode(node As TreeNode) As TreeNode
        If node Is Nothing Then Return Nothing
        If node.FirstNode IsNot Nothing Then
            Return node.FirstNode
        End If
        While node IsNot Nothing
            If node.NextNode IsNot Nothing Then
                Return node.NextNode
            End If
            node = node.Parent
        End While
        Return Nothing
    End Function

    Private Function FindNode(startNode As TreeNode, text As String, searchValues As Boolean) As TreeNode
        Dim node As TreeNode = startNode
        While node IsNot Nothing
            Dim jNode As JsonNode = TryCast(node.Tag, JsonNode)
            If jNode IsNot Nothing Then
                Dim hit As Boolean = False
                If (jNode.Name IsNot Nothing AndAlso jNode.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) Then
                    hit = True
                ElseIf searchValues AndAlso jNode.Kind = JsonNodeKind.ValueNode AndAlso
                       jNode.Value IsNot Nothing AndAlso
                       jNode.Value.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0 Then
                    hit = True
                End If

                If hit Then
                    Return node
                End If
            End If

            node = GetNextNode(node)
        End While

        Return Nothing
    End Function

    Private Class ParentOption
        Public ReadOnly Property Text As String
        Public ReadOnly Property Node As JsonNode

        Public Sub New(text As String, node As JsonNode)
            Me.Text = text
            Me.Node = node
        End Sub

        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

End Class