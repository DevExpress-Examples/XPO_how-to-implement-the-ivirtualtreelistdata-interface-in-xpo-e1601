Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports Northwind

Namespace Q212620

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            XpoDefault.ConnectionString = AccessConnectionProvider.GetConnectionString("..\..\nwind.mdb")
            InitializeComponent()
            Dim data As XPVirtualTreeListDataBase = New XPVirtualTreeListDataBase(session1)
            data.SetChildren(New XPCollection(session1, GetType(Categories)))
            treeList1.DataSource = data
        End Sub
    End Class
End Namespace
