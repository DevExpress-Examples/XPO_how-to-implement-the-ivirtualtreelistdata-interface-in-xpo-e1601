Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo
Imports DevExpress.XtraTreeList
Imports System.Collections.Generic
Imports System.Collections
Namespace Northwind

	Public Class Categories
		Inherits XPVirtualTreeListDataBase
		Private fCategoryID As Integer
		<Key(True)> _
		Public Property CategoryID() As Integer
			Get
				Return fCategoryID
			End Get
			Set(ByVal value As Integer)
				SetPropertyValue(Of Integer)("CategoryID", fCategoryID, value)
			End Set
		End Property
		Private fCategoryName As String
		<Size(15)> _
		Public Property CategoryName() As String
			Get
				Return fCategoryName
			End Get
			Set(ByVal value As String)
				SetPropertyValue(Of String)("CategoryName", fCategoryName, value)
			End Set
		End Property
		<Association("Category-Products", GetType(Products))> _
		Public ReadOnly Property Products() As XPCollection(Of Products)
			Get
				Return GetCollection(Of Products)("Products")
			End Get
		End Property
		Protected Overrides ReadOnly Property ChildrenCollectionName() As String
			Get
				Return "Products"
			End Get
		End Property
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Public Sub New()
			Me.New(Session.DefaultSession)
		End Sub
	End Class

	Public Class Products
		Inherits XPVirtualTreeListDataBase
		Private fProductID As Integer
		<Key(True)> _
		Public Property ProductID() As Integer
			Get
				Return fProductID
			End Get
			Set(ByVal value As Integer)
				SetPropertyValue(Of Integer)("ProductID", fProductID, value)
			End Set
		End Property
		Private fProductName As String
		<Size(40)> _
		Public Property ProductName() As String
			Get
				Return fProductName
			End Get
			Set(ByVal value As String)
				SetPropertyValue(Of String)("ProductName", fProductName, value)
			End Set
		End Property
		Private fCategoryID As Categories
		<Association("Category-Products")> _
		Public Property CategoryID() As Categories
			Get
				Return fCategoryID
			End Get
			Set(ByVal value As Categories)
				SetPropertyValue(Of Categories)("CategoryID", fCategoryID, value)
			End Set
		End Property
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
			fieldMap.Add("CategoryName", "ProductName")
		End Sub
		Public Sub New()
			Me.New(Session.DefaultSession)
		End Sub
	End Class

	<NonPersistent> _
	Public Class XPVirtualTreeListDataBase
		Inherits XPLiteObject
		Implements TreeList.IVirtualTreeListData
		Protected childrenCore As XPCollection
		Protected fieldMap As New Dictionary(Of String, String)()

		Public Sub New()
			MyBase.New()
		End Sub

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub

		Protected Overridable ReadOnly Property ChildrenCollectionName() As String
			Get
				Return String.Empty
			End Get
		End Property

		Private Function GetTreeListDataFieldName(ByVal fieldName As String) As String
			If fieldMap.ContainsKey(fieldName) Then
				Return fieldMap(fieldName)
			Else
				Return fieldName
			End If
		End Function

		Public Sub SetChildren(ByVal children As XPCollection)
			childrenCore = children
		End Sub

		#Region "IVirtualTreeListData Members"

        Public Sub VirtualTreeGetCellValue(ByVal info As VirtualTreeGetCellValueInfo) _
            Implements TreeList.IVirtualTreeListData.VirtualTreeGetCellValue
            Dim fieldName As String = GetTreeListDataFieldName(info.Column.FieldName)
            info.CellData = ClassInfo.GetMember(fieldName).GetValue(Me)
        End Sub

        Public Sub VirtualTreeGetChildNodes(ByVal info As VirtualTreeGetChildNodesInfo) _
                  Implements TreeList.IVirtualTreeListData.VirtualTreeGetChildNodes
            If String.IsNullOrEmpty(ChildrenCollectionName) Then
                info.Children = childrenCore
            Else
                info.Children = CType(ClassInfo.GetMember(ChildrenCollectionName).GetValue(Me), IList)
            End If
        End Sub

        Public Sub VirtualTreeSetCellValue(ByVal info As VirtualTreeSetCellValueInfo) _
                  Implements TreeList.IVirtualTreeListData.VirtualTreeSetCellValue
            Dim fieldName As String = GetTreeListDataFieldName(info.Column.FieldName)
            ClassInfo.GetMember(fieldName).SetValue(Me, info.NewCellData)
        End Sub

		#End Region
	End Class
End Namespace
