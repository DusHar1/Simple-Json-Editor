<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormConfigEditor
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormConfigEditor))
        Me.btnNewConfig = New System.Windows.Forms.Button()
        Me.btnOpenConfig = New System.Windows.Forms.Button()
        Me.btnSaveConfig = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnUndo = New System.Windows.Forms.Button()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.btnPaste = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.chkSearchValues = New System.Windows.Forms.CheckBox()
        Me.btnSearchNext = New System.Windows.Forms.Button()
        Me.splitMain = New System.Windows.Forms.SplitContainer()
        Me.tvConfig = New System.Windows.Forms.TreeView()
        Me.cmsTree = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuAddSection = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAddSetting = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAddArrayItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuCopyNode = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPasteNode = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuDeleteNode = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuExpandAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCollapseAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.splitRight = New System.Windows.Forms.SplitContainer()
        Me.grpNodeDetails = New System.Windows.Forms.GroupBox()
        Me.cmbParent = New System.Windows.Forms.ComboBox()
        Me.lblParent = New System.Windows.Forms.Label()
        Me.btnDeleteNode = New System.Windows.Forms.Button()
        Me.btnAddArrayItem = New System.Windows.Forms.Button()
        Me.btnAddSetting = New System.Windows.Forms.Button()
        Me.btnAddSection = New System.Windows.Forms.Button()
        Me.btnApplyNode = New System.Windows.Forms.Button()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.lblValue = New System.Windows.Forms.Label()
        Me.cmbValueType = New System.Windows.Forms.ComboBox()
        Me.lblValueType = New System.Windows.Forms.Label()
        Me.cmbKind = New System.Windows.Forms.ComboBox()
        Me.lblKind = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.pnlEditor = New System.Windows.Forms.Panel()
        Me.rtbJsonPreview = New System.Windows.Forms.RichTextBox()
        Me.pnlLineNumbers = New System.Windows.Forms.Panel()
        Me.ofdConfig = New System.Windows.Forms.OpenFileDialog()
        Me.sfdConfig = New System.Windows.Forms.SaveFileDialog()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        Me.cmsTree.SuspendLayout()
        CType(Me.splitRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitRight.Panel1.SuspendLayout()
        Me.splitRight.Panel2.SuspendLayout()
        Me.splitRight.SuspendLayout()
        Me.grpNodeDetails.SuspendLayout()
        Me.pnlEditor.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnNewConfig
        '
        Me.btnNewConfig.Location = New System.Drawing.Point(12, 12)
        Me.btnNewConfig.Name = "btnNewConfig"
        Me.btnNewConfig.Size = New System.Drawing.Size(90, 27)
        Me.btnNewConfig.TabIndex = 0
        Me.btnNewConfig.Text = "New"
        Me.btnNewConfig.UseVisualStyleBackColor = True
        '
        'btnOpenConfig
        '
        Me.btnOpenConfig.Location = New System.Drawing.Point(108, 12)
        Me.btnOpenConfig.Name = "btnOpenConfig"
        Me.btnOpenConfig.Size = New System.Drawing.Size(75, 27)
        Me.btnOpenConfig.TabIndex = 1
        Me.btnOpenConfig.Text = "Open"
        Me.btnOpenConfig.UseVisualStyleBackColor = True
        '
        'btnSaveConfig
        '
        Me.btnSaveConfig.Location = New System.Drawing.Point(189, 12)
        Me.btnSaveConfig.Name = "btnSaveConfig"
        Me.btnSaveConfig.Size = New System.Drawing.Size(75, 27)
        Me.btnSaveConfig.TabIndex = 2
        Me.btnSaveConfig.Text = "Save"
        Me.btnSaveConfig.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(697, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 27)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnUndo
        '
        Me.btnUndo.Location = New System.Drawing.Point(270, 12)
        Me.btnUndo.Name = "btnUndo"
        Me.btnUndo.Size = New System.Drawing.Size(60, 27)
        Me.btnUndo.TabIndex = 4
        Me.btnUndo.Text = "Undo"
        Me.btnUndo.UseVisualStyleBackColor = True
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(336, 12)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(60, 27)
        Me.btnCopy.TabIndex = 5
        Me.btnCopy.Text = "Copy"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnPaste
        '
        Me.btnPaste.Location = New System.Drawing.Point(402, 12)
        Me.btnPaste.Name = "btnPaste"
        Me.btnPaste.Size = New System.Drawing.Size(60, 27)
        Me.btnPaste.TabIndex = 6
        Me.btnPaste.Text = "Paste"
        Me.btnPaste.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(548, 16)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(70, 20)
        Me.txtSearch.TabIndex = 7
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(498, 19)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(44, 13)
        Me.lblSearch.TabIndex = 8
        Me.lblSearch.Text = "Search:"
        '
        'chkSearchValues
        '
        Me.chkSearchValues.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkSearchValues.AutoSize = True
        Me.chkSearchValues.Location = New System.Drawing.Point(622, 42)
        Me.chkSearchValues.Name = "chkSearchValues"
        Me.chkSearchValues.Size = New System.Drawing.Size(94, 17)
        Me.chkSearchValues.TabIndex = 9
        Me.chkSearchValues.Text = "Search values"
        Me.chkSearchValues.UseVisualStyleBackColor = True
        '
        'btnSearchNext
        '
        Me.btnSearchNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchNext.Location = New System.Drawing.Point(624, 12)
        Me.btnSearchNext.Name = "btnSearchNext"
        Me.btnSearchNext.Size = New System.Drawing.Size(67, 27)
        Me.btnSearchNext.TabIndex = 10
        Me.btnSearchNext.Text = "Find"
        Me.btnSearchNext.UseVisualStyleBackColor = True
        '
        'splitMain
        '
        Me.splitMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splitMain.Location = New System.Drawing.Point(12, 65)
        Me.splitMain.Name = "splitMain"
        '
        'splitMain.Panel1
        '
        Me.splitMain.Panel1.Controls.Add(Me.tvConfig)
        '
        'splitMain.Panel2
        '
        Me.splitMain.Panel2.Controls.Add(Me.splitRight)
        Me.splitMain.Size = New System.Drawing.Size(760, 484)
        Me.splitMain.SplitterDistance = 250
        Me.splitMain.TabIndex = 11
        '
        'tvConfig
        '
        Me.tvConfig.ContextMenuStrip = Me.cmsTree
        Me.tvConfig.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvConfig.HideSelection = False
        Me.tvConfig.Location = New System.Drawing.Point(0, 0)
        Me.tvConfig.Name = "tvConfig"
        Me.tvConfig.Size = New System.Drawing.Size(250, 484)
        Me.tvConfig.TabIndex = 0
        '
        'cmsTree
        '
        Me.cmsTree.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAddSection, Me.mnuAddSetting, Me.mnuAddArrayItem, Me.ToolStripSeparator1, Me.mnuCopyNode, Me.mnuPasteNode, Me.ToolStripSeparator3, Me.mnuDeleteNode, Me.ToolStripSeparator2, Me.mnuExpandAll, Me.mnuCollapseAll})
        Me.cmsTree.Name = "cmsTree"
        Me.cmsTree.Size = New System.Drawing.Size(155, 198)
        '
        'mnuAddSection
        '
        Me.mnuAddSection.Name = "mnuAddSection"
        Me.mnuAddSection.Size = New System.Drawing.Size(154, 22)
        Me.mnuAddSection.Text = "Add Section"
        '
        'mnuAddSetting
        '
        Me.mnuAddSetting.Name = "mnuAddSetting"
        Me.mnuAddSetting.Size = New System.Drawing.Size(154, 22)
        Me.mnuAddSetting.Text = "Add Setting"
        '
        'mnuAddArrayItem
        '
        Me.mnuAddArrayItem.Name = "mnuAddArrayItem"
        Me.mnuAddArrayItem.Size = New System.Drawing.Size(154, 22)
        Me.mnuAddArrayItem.Text = "Add Array Item"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(151, 6)
        '
        'mnuCopyNode
        '
        Me.mnuCopyNode.Name = "mnuCopyNode"
        Me.mnuCopyNode.Size = New System.Drawing.Size(154, 22)
        Me.mnuCopyNode.Text = "Copy"
        '
        'mnuPasteNode
        '
        Me.mnuPasteNode.Name = "mnuPasteNode"
        Me.mnuPasteNode.Size = New System.Drawing.Size(154, 22)
        Me.mnuPasteNode.Text = "Paste"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(151, 6)
        '
        'mnuDeleteNode
        '
        Me.mnuDeleteNode.Name = "mnuDeleteNode"
        Me.mnuDeleteNode.Size = New System.Drawing.Size(154, 22)
        Me.mnuDeleteNode.Text = "Delete Node"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(151, 6)
        '
        'mnuExpandAll
        '
        Me.mnuExpandAll.Name = "mnuExpandAll"
        Me.mnuExpandAll.Size = New System.Drawing.Size(154, 22)
        Me.mnuExpandAll.Text = "Expand All"
        '
        'mnuCollapseAll
        '
        Me.mnuCollapseAll.Name = "mnuCollapseAll"
        Me.mnuCollapseAll.Size = New System.Drawing.Size(154, 22)
        Me.mnuCollapseAll.Text = "Collapse All"
        '
        'splitRight
        '
        Me.splitRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitRight.Location = New System.Drawing.Point(0, 0)
        Me.splitRight.Name = "splitRight"
        Me.splitRight.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitRight.Panel1
        '
        Me.splitRight.Panel1.Controls.Add(Me.grpNodeDetails)
        '
        'splitRight.Panel2
        '
        Me.splitRight.Panel2.Controls.Add(Me.pnlEditor)
        Me.splitRight.Size = New System.Drawing.Size(506, 484)
        Me.splitRight.SplitterDistance = 190
        Me.splitRight.TabIndex = 0
        '
        'grpNodeDetails
        '
        Me.grpNodeDetails.Controls.Add(Me.cmbParent)
        Me.grpNodeDetails.Controls.Add(Me.lblParent)
        Me.grpNodeDetails.Controls.Add(Me.btnDeleteNode)
        Me.grpNodeDetails.Controls.Add(Me.btnAddArrayItem)
        Me.grpNodeDetails.Controls.Add(Me.btnAddSetting)
        Me.grpNodeDetails.Controls.Add(Me.btnAddSection)
        Me.grpNodeDetails.Controls.Add(Me.btnApplyNode)
        Me.grpNodeDetails.Controls.Add(Me.txtValue)
        Me.grpNodeDetails.Controls.Add(Me.lblValue)
        Me.grpNodeDetails.Controls.Add(Me.cmbValueType)
        Me.grpNodeDetails.Controls.Add(Me.lblValueType)
        Me.grpNodeDetails.Controls.Add(Me.cmbKind)
        Me.grpNodeDetails.Controls.Add(Me.lblKind)
        Me.grpNodeDetails.Controls.Add(Me.txtName)
        Me.grpNodeDetails.Controls.Add(Me.lblName)
        Me.grpNodeDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpNodeDetails.Location = New System.Drawing.Point(0, 0)
        Me.grpNodeDetails.Name = "grpNodeDetails"
        Me.grpNodeDetails.Size = New System.Drawing.Size(506, 190)
        Me.grpNodeDetails.TabIndex = 0
        Me.grpNodeDetails.TabStop = False
        Me.grpNodeDetails.Text = "Node Details"
        '
        'cmbParent
        '
        Me.cmbParent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbParent.FormattingEnabled = True
        Me.cmbParent.Location = New System.Drawing.Point(83, 71)
        Me.cmbParent.Name = "cmbParent"
        Me.cmbParent.Size = New System.Drawing.Size(408, 21)
        Me.cmbParent.TabIndex = 14
        '
        'lblParent
        '
        Me.lblParent.AutoSize = True
        Me.lblParent.Location = New System.Drawing.Point(13, 74)
        Me.lblParent.Name = "lblParent"
        Me.lblParent.Size = New System.Drawing.Size(41, 13)
        Me.lblParent.TabIndex = 13
        Me.lblParent.Text = "Parent:"
        '
        'btnDeleteNode
        '
        Me.btnDeleteNode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteNode.Location = New System.Drawing.Point(416, 145)
        Me.btnDeleteNode.Name = "btnDeleteNode"
        Me.btnDeleteNode.Size = New System.Drawing.Size(75, 27)
        Me.btnDeleteNode.TabIndex = 12
        Me.btnDeleteNode.Text = "Delete"
        Me.btnDeleteNode.UseVisualStyleBackColor = True
        '
        'btnAddArrayItem
        '
        Me.btnAddArrayItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddArrayItem.Location = New System.Drawing.Point(335, 145)
        Me.btnAddArrayItem.Name = "btnAddArrayItem"
        Me.btnAddArrayItem.Size = New System.Drawing.Size(75, 27)
        Me.btnAddArrayItem.TabIndex = 11
        Me.btnAddArrayItem.Text = "Add Array"
        Me.btnAddArrayItem.UseVisualStyleBackColor = True
        '
        'btnAddSetting
        '
        Me.btnAddSetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddSetting.Location = New System.Drawing.Point(254, 145)
        Me.btnAddSetting.Name = "btnAddSetting"
        Me.btnAddSetting.Size = New System.Drawing.Size(75, 27)
        Me.btnAddSetting.TabIndex = 10
        Me.btnAddSetting.Text = "Add Setting"
        Me.btnAddSetting.UseVisualStyleBackColor = True
        '
        'btnAddSection
        '
        Me.btnAddSection.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddSection.Location = New System.Drawing.Point(173, 145)
        Me.btnAddSection.Name = "btnAddSection"
        Me.btnAddSection.Size = New System.Drawing.Size(75, 27)
        Me.btnAddSection.TabIndex = 9
        Me.btnAddSection.Text = "Add Section"
        Me.btnAddSection.UseVisualStyleBackColor = True
        '
        'btnApplyNode
        '
        Me.btnApplyNode.Location = New System.Drawing.Point(16, 145)
        Me.btnApplyNode.Name = "btnApplyNode"
        Me.btnApplyNode.Size = New System.Drawing.Size(111, 27)
        Me.btnApplyNode.TabIndex = 8
        Me.btnApplyNode.Text = "Apply Changes"
        Me.btnApplyNode.UseVisualStyleBackColor = True
        '
        'txtValue
        '
        Me.txtValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtValue.Location = New System.Drawing.Point(83, 108)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(408, 20)
        Me.txtValue.TabIndex = 7
        '
        'lblValue
        '
        Me.lblValue.AutoSize = True
        Me.lblValue.Location = New System.Drawing.Point(13, 111)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(37, 13)
        Me.lblValue.TabIndex = 6
        Me.lblValue.Text = "Value:"
        '
        'cmbValueType
        '
        Me.cmbValueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbValueType.FormattingEnabled = True
        Me.cmbValueType.Location = New System.Drawing.Point(363, 71)
        Me.cmbValueType.Name = "cmbValueType"
        Me.cmbValueType.Size = New System.Drawing.Size(128, 21)
        Me.cmbValueType.TabIndex = 5
        '
        'lblValueType
        '
        Me.lblValueType.AutoSize = True
        Me.lblValueType.Location = New System.Drawing.Point(294, 55)
        Me.lblValueType.Name = "lblValueType"
        Me.lblValueType.Size = New System.Drawing.Size(64, 13)
        Me.lblValueType.TabIndex = 4
        Me.lblValueType.Text = "Value Type:"
        '
        'cmbKind
        '
        Me.cmbKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKind.FormattingEnabled = True
        Me.cmbKind.Location = New System.Drawing.Point(83, 44)
        Me.cmbKind.Name = "cmbKind"
        Me.cmbKind.Size = New System.Drawing.Size(189, 21)
        Me.cmbKind.TabIndex = 3
        '
        'lblKind
        '
        Me.lblKind.AutoSize = True
        Me.lblKind.Location = New System.Drawing.Point(13, 47)
        Me.lblKind.Name = "lblKind"
        Me.lblKind.Size = New System.Drawing.Size(60, 13)
        Me.lblKind.TabIndex = 2
        Me.lblKind.Text = "Node Kind:"
        '
        'txtName
        '
        Me.txtName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtName.Location = New System.Drawing.Point(83, 18)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(408, 20)
        Me.txtName.TabIndex = 1
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(13, 21)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(67, 13)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "Name / Key:"
        '
        'pnlEditor
        '
        Me.pnlEditor.Controls.Add(Me.rtbJsonPreview)
        Me.pnlEditor.Controls.Add(Me.pnlLineNumbers)
        Me.pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlEditor.Location = New System.Drawing.Point(0, 0)
        Me.pnlEditor.Name = "pnlEditor"
        Me.pnlEditor.Size = New System.Drawing.Size(506, 290)
        Me.pnlEditor.TabIndex = 0
        '
        'rtbJsonPreview
        '
        Me.rtbJsonPreview.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbJsonPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbJsonPreview.Font = New System.Drawing.Font("Consolas", 9.0!)
        Me.rtbJsonPreview.Location = New System.Drawing.Point(40, 0)
        Me.rtbJsonPreview.Name = "rtbJsonPreview"
        Me.rtbJsonPreview.ReadOnly = True
        Me.rtbJsonPreview.Size = New System.Drawing.Size(466, 290)
        Me.rtbJsonPreview.TabIndex = 1
        Me.rtbJsonPreview.Text = ""
        Me.rtbJsonPreview.WordWrap = False
        '
        'pnlLineNumbers
        '
        Me.pnlLineNumbers.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLineNumbers.Location = New System.Drawing.Point(0, 0)
        Me.pnlLineNumbers.Name = "pnlLineNumbers"
        Me.pnlLineNumbers.Size = New System.Drawing.Size(40, 290)
        Me.pnlLineNumbers.TabIndex = 0
        '
        'FormConfigEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.btnSearchNext)
        Me.Controls.Add(Me.chkSearchValues)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.btnPaste)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.btnUndo)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSaveConfig)
        Me.Controls.Add(Me.btnOpenConfig)
        Me.Controls.Add(Me.btnNewConfig)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormConfigEditor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Config JSON Utility"
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        Me.cmsTree.ResumeLayout(False)
        Me.splitRight.Panel1.ResumeLayout(False)
        Me.splitRight.Panel2.ResumeLayout(False)
        CType(Me.splitRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitRight.ResumeLayout(False)
        Me.grpNodeDetails.ResumeLayout(False)
        Me.grpNodeDetails.PerformLayout()
        Me.pnlEditor.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnNewConfig As System.Windows.Forms.Button
    Friend WithEvents btnOpenConfig As System.Windows.Forms.Button
    Friend WithEvents btnSaveConfig As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnUndo As System.Windows.Forms.Button
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents btnPaste As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents chkSearchValues As System.Windows.Forms.CheckBox
    Friend WithEvents btnSearchNext As System.Windows.Forms.Button
    Friend WithEvents splitMain As System.Windows.Forms.SplitContainer
    Friend WithEvents tvConfig As System.Windows.Forms.TreeView
    Friend WithEvents splitRight As System.Windows.Forms.SplitContainer
    Friend WithEvents grpNodeDetails As System.Windows.Forms.GroupBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents cmbKind As System.Windows.Forms.ComboBox
    Friend WithEvents lblKind As System.Windows.Forms.Label
    Friend WithEvents cmbValueType As System.Windows.Forms.ComboBox
    Friend WithEvents lblValueType As System.Windows.Forms.Label
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents btnApplyNode As System.Windows.Forms.Button
    Friend WithEvents btnAddArrayItem As System.Windows.Forms.Button
    Friend WithEvents btnAddSetting As System.Windows.Forms.Button
    Friend WithEvents btnAddSection As System.Windows.Forms.Button
    Friend WithEvents btnDeleteNode As System.Windows.Forms.Button
    Friend WithEvents pnlEditor As System.Windows.Forms.Panel
    Friend WithEvents rtbJsonPreview As System.Windows.Forms.RichTextBox
    Friend WithEvents pnlLineNumbers As System.Windows.Forms.Panel
    Friend WithEvents cmsTree As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuAddSection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAddSetting As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAddArrayItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuCopyNode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPasteNode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuDeleteNode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuExpandAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCollapseAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ofdConfig As System.Windows.Forms.OpenFileDialog
    Friend WithEvents sfdConfig As System.Windows.Forms.SaveFileDialog
    Friend WithEvents cmbParent As System.Windows.Forms.ComboBox
    Friend WithEvents lblParent As System.Windows.Forms.Label
End Class
