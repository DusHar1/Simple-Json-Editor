# 🧰 Simple Json Editor
A Windows tool for visually building and editing JSON configuration files.

## 📖 Overview
Config JSON Utility is a Windows Forms application for creating, editing, validating, and organizing JSON configuration files through a visual, tree-based interface. It eliminates the need to manually write or debug raw JSON by providing real-time preview, structured editing tools, error highlighting, and productivity-focused workflow enhancements
<img width="778" height="592" alt="Example" src="https://github.com/user-attachments/assets/e8c69938-9d25-433b-9437-764289f327ed" />

## ✨ Features
### 🔧 JSON Structure Editor
* Tree-based JSON builder
* Supports objects, arrays, and value nodes
* Edit keys, values, and JSON data types
* Automatic array index assignment
* Rename, move, or repurpose nodes on the fly

### 📋 Node Actions
* Copy / Paste entire sections or values
* Multi-level Undo support
* Add object, array, or value nodes
* Delete and restructure hierarchies
* Re-parenting: move nodes anywhere in the tree

### 🔍 Search Tools
* Search by key and/or value
* Jump to the next match
* Expand/collapse all nodes for quick navigation

### 🧾 Real-Time JSON Preview
* Pretty-printed JSON output
* Line number gutter
* Maintains scroll and cursor position
* Updates automatically with every change

### 🛑 Error Handling
* Displays raw JSON when opening files
* Highlights JSON syntax errors with exact line/column
* Alerts with descriptive error messages
* Safe fallback even when input is malformed

<img width="782" height="589" alt="ErrorMsg" src="https://github.com/user-attachments/assets/b843cdc4-4926-4871-87a5-85fa561ebeae" />
<img width="499" height="219" alt="ErrorHighlight" src="https://github.com/user-attachments/assets/eb277ec1-1087-4a46-98e1-2c1742e65f1e" />


### 📁 File I/O
* Open and edit any .json file
* Save with formatted output
* Create new configuration from scratch
* Confirmation prompts when discarding changes

### 🛠 Build Instructions
Requirements:
* Visual Studio 2019 or later
* .NET Framework 4.5.2
* NuGet: Newtonsoft.Json (13.0.3)
