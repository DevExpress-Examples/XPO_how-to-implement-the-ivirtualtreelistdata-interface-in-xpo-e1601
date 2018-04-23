using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Northwind;

namespace Q212620 {
    public partial class Form1 : Form {
        public Form1() {
            XpoDefault.ConnectionString = AccessConnectionProvider.GetConnectionString(@"..\..\nwind.mdb");
            InitializeComponent();
            XPVirtualTreeListDataBase data = new XPVirtualTreeListDataBase(session1);
            data.SetChildren(new XPCollection(session1, typeof(Categories)));
            treeList1.DataSource = data;
        }
    }
}