using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class CategoryList : Event
    {
        #region Private Members
        private BindingList<Category> _List;
        #endregion

        #region Public Properties
        public BindingList<Category> List
        {
            get { return _List; }
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods
        public CategoryList GetAll()
        {
            Database database = new Database("Customer");

            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "tblCategoryGetAll";
            DataTable dt = database.ExecuteQuery();

            Category select = new Category();
            select.Id = Guid.Empty;
            select.Name = "-- Please Select a Category --";
            _List.Add(select);

            foreach (DataRow dr in dt.Rows)
            {
                Category ca = new Category();
                ca.Initialize(dr);
                ca.InitializeBusinessData(dr);
                ca.IsNew = false;
                ca.IsDirty = false;
                ca.Savable += Category_Savable;
                _List.Add(ca);
            }

            return this;
        }
        public CategoryList Save()
        {
            foreach (Category ca in _List)
            {
                if (ca.IsSavable() == true)
                {
                    ca.Save();
                }
            }

            return this;
        }
        #endregion

        #region Public Events
        private void Category_Savable(SavableEventArgs e)
        {
            RaiseEvent(e);
        }
        #endregion

        #region Public Event Handlers
        private void _List_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new Product();
            Product category = (Product)e.NewObject;
            category.Savable += Category_Savable;
        }
        #endregion

        #region Construction
        public CategoryList()
        {
            _List = new BindingList<Category>();
            _List.AddingNew += _List_AddingNew;
        }
        #endregion
    }
}