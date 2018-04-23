using System;
using DevExpress.Xpo;
using DevExpress.XtraTreeList;
using System.Collections.Generic;
using System.Collections;
namespace Northwind {

    public class Categories : XPVirtualTreeListDataBase {
        int fCategoryID;
        [Key(true)]
        public int CategoryID {
            get { return fCategoryID; }
            set { SetPropertyValue<int>("CategoryID", ref fCategoryID, value); }
        }
        string fCategoryName;
        [Size(15)]
        public string CategoryName {
            get { return fCategoryName; }
            set { SetPropertyValue<string>("CategoryName", ref fCategoryName, value); }
        }
        [Association("Category-Products", typeof(Products))]
        public XPCollection<Products> Products { get { return GetCollection<Products>("Products"); } }
        protected override string ChildrenCollectionName { get { return "Products"; } }
        public Categories(Session session) : base(session) { }
        public Categories() : this(Session.DefaultSession) { }
    }

    public class Products : XPVirtualTreeListDataBase {
        int fProductID;
        [Key(true)]
        public int ProductID {
            get { return fProductID; }
            set { SetPropertyValue<int>("ProductID", ref fProductID, value); }
        }
        string fProductName;
        [Size(40)]
        public string ProductName {
            get { return fProductName; }
            set { SetPropertyValue<string>("ProductName", ref fProductName, value); }
        }
        Categories fCategoryID;
        [Association("Category-Products")]
        public Categories CategoryID {
            get { return fCategoryID; }
            set { SetPropertyValue<Categories>("CategoryID", ref fCategoryID, value); }
        }
        public Products(Session session) : base(session) {
            fieldMap.Add("CategoryName", "ProductName");
        }
        public Products() : this(Session.DefaultSession) { }
    }

    [NonPersistent]
    public class XPVirtualTreeListDataBase : XPLiteObject, TreeList.IVirtualTreeListData {
        protected XPCollection childrenCore;
        protected Dictionary<string, string> fieldMap = new Dictionary<string, string>();

        public XPVirtualTreeListDataBase() : base() { }

        public XPVirtualTreeListDataBase(Session session) : base(session) { }

        protected virtual string ChildrenCollectionName { get { return string.Empty; } }

        private string GetTreeListDataFieldName(string fieldName) {
            return fieldMap.ContainsKey(fieldName) ? fieldMap[fieldName] : fieldName;
        }

        public void SetChildren(XPCollection children) {
            childrenCore = children;
        }

        #region IVirtualTreeListData Members

        public void VirtualTreeGetCellValue(VirtualTreeGetCellValueInfo info) {
            string fieldName = GetTreeListDataFieldName(info.Column.FieldName);
            info.CellData = ClassInfo.GetMember(fieldName).GetValue(this);
        }

        public void VirtualTreeGetChildNodes(VirtualTreeGetChildNodesInfo info) {
            if (string.IsNullOrEmpty(ChildrenCollectionName)) info.Children = childrenCore;
            else info.Children = (IList)ClassInfo.GetMember(ChildrenCollectionName).GetValue(this);
        }

        public void VirtualTreeSetCellValue(VirtualTreeSetCellValueInfo info) {
            string fieldName = GetTreeListDataFieldName(info.Column.FieldName);
            ClassInfo.GetMember(fieldName).SetValue(this, info.NewCellData);
        }

        #endregion
    }
}
