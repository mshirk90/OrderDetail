using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class ProductList : Event
    {
        #region Private Members
        private BindingList<Product> _List;
        private String _path = String.Empty;
        #endregion

        #region Public Properties
        public BindingList<Product> List
        {
            get { return _List; }
        }
        public String Path
        {
            get { return _path; }
            set { _path = value; }
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods
        public ProductList GetByCategoryId(Guid categoryId)
        {
            Database database = new Database("Customer");

            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblProductGetByCategoryID";
            database.Command.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = categoryId;
            
            DataTable dt = database.ExecuteQuery();
            foreach (DataRow dr in dt.Rows)
            {
                Product pr = new Product();
                pr.FilePath = _path;
                pr.Initialize(dr);
                pr.InitializeBusinessData(dr);
                pr.IsNew = false;
                pr.IsDirty = false;
                pr.Savable += Product_Savable;
                _List.Add(pr);
            }

            return this;
        }
        public ProductList Save()
        {
            foreach (Product pr in _List)
            {
                if (pr.IsSavable() == true)
                {
                    pr.Save();
                }
            }

            return this;
        }
        public Boolean IsSavable()
        {
            Boolean result = false;

            foreach (Product pr in _List)
            {
                if (pr.IsSavable() == true)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        public ProductList GetAll()
        {
            Database database = new Database("Customer");

            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblProductGetAll";

            DataTable dt = database.ExecuteQuery();
            Product blank = new Product();
            blank.Name = "- Please Select -";
            _List.Add(blank);

            foreach (DataRow dr in dt.Rows)
            {
                Product pr = new Product();
                pr.Initialize(dr);
                pr.InitializeBusinessData(dr);
                pr.IsNew = false;
                pr.IsDirty = false;
                pr.Savable += Product_Savable;
                _List.Add(pr);
            }

            return this;
        }
        #endregion

        #region Public Events
        private void Product_Savable(SavableEventArgs e)
        {
            RaiseEvent(e);
        }
        #endregion

        #region Public Event Handlers
        private void _List_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new Product();
            Product product = (Product)e.NewObject;
            product.Savable += Product_Savable;
        }
        #endregion

        #region Construction
        public ProductList()
        {
            _List = new BindingList<Product>();
            _List.AddingNew += _List_AddingNew;
        }
        #endregion
    }
}