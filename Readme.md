<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1601)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/Q212620/Form1.cs) (VB: [Form1.vb](./VB/Q212620/Form1.vb))
* [Northwind.cs](./CS/Q212620/Northwind.cs) (VB: [Northwind.vb](./VB/Q212620/Northwind.vb))
<!-- default file list end -->
# How to implement the IVirtualTreeListData interface in XPO


<p>This example demonstrates how to implement a base persistent class exposing a virtual tree structure, and allowing its descendants to be represented in a TreeList control.</p>


<h3>Description</h3>

<p>XPVirtualTreeListDataBase is a base class that implements the TreeList.IVirtualTreeListData interface. The NonPersistentAttribute is applied to this class. So, it won&#39;t be saved to a database, and a table won&#39;t be created for this class. It is only used for binding purposes and as a base class for other objects that are used to retrieve data from the database. </p><p>The <strong>XPVirtualTreeListDataBase.VirtualTreeGetChildNodes</strong> method is invoked by the TreeList and should return a list of objects inherited from the XPVirtualTreeListDataBase. These objects will be displayed in the TreeList as a child nodes of a node representing the current object. In my implementation, this method simply returns objects from the collection property specified by the childrenCollectionName property. You can provide your own logic here. Please  note that for a base class, representing the root of a tree, the collection of first level nodes should be manually populated with XPVirtualTreeListDataBase descendants.</p><code lang='cs'>
XPVirtualTreeListDataBase data = new XPVirtualTreeListDataBase(session1);
data.SetChildren(new XPCollection(session1, typeof(Categories)));
</code><p>The <strong>XPVirtualTreeListDataBase.VirtualTreeGetCellValue</strong> method is invoked by the TreeList and should return values that will be represented within the TreeList in a specified column. In the sample, I simple get the value of a corresponding property from the current instance of the persistent object. To accomplish this task, it&#39;s necessary to populate the fieldMap dictionary to associate TreeList columns with a corresponding properties. </p><p>The <strong>XPVirtualTreeListDataBase.VirtualTreeSetCellValue</strong> method is invoked by the TreeList and should provide a functionality to save values from the TreeList to a corresponding data table. This method works in a similar way as the XPVirtualTreeListDataBase.VirtualTreeGetCellValue method does. </p><p>Classes inherited from the  XPVirtualTreeListDataBase are used to work with data in an ordinary way, and to provide the TreeList.IVirtualTreeListData implementation inherited from the base class.</p>

<br/>


