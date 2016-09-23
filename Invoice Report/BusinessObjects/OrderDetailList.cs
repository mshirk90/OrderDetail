using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class OrderDetailList 
    {
        #region Private Members
        private BindingList<OrderDetail> _List;
        
        #endregion

        #region Public Properties
        public BindingList<OrderDetail> List
        {
            get { return _List; }
        }
     
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods
        public OrderDetailList GetByOrderId(int orderId)
        {
            Database database = new Database("Northwind");
            database.Command.Parameters.Clear();
            database.Command.CommandType = CommandType.StoredProcedure;
            database.Command.CommandText = "OrderDetailsByOrderId";
            database.Command.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderId;
            
            DataTable dt = database.ExecuteQuery();
            foreach (DataRow dr in dt.Rows)
            {
                OrderDetail od = new OrderDetail();
                od.InitializeBusinessData(dr);               
                _List.Add(od);
            }

            return this;
        }

        #endregion

        #region Public Events
   
        #endregion

        #region Public Event Handlers

        #endregion

        #region Construction
        public OrderDetailList()
        {
            _List = new BindingList<OrderDetail>();

        }
        #endregion
    }
}