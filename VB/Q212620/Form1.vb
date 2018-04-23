Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports Northwind

Namespace Q212620
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			XpoDefault.ConnectionString = AccessConnectionProvider.GetConnectionString("..\..\nwind.mdb")
			InitializeComponent()
			Dim data As New XPVirtualTreeListDataBase(session1)
			data.SetChildren(New XPCollection(session1, GetType(Categories)))
			treeList1.DataSource = data
		End Sub
	End Class
End Namespace