using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DatabaseHelper;

namespace BusinessObjects
{
    public class OrderDetail
    {
        #region Private Members
        private int _OrderID = 0;
        private String _ProductName = string.Empty;
        private Decimal _UnitPrice = 0;
        private int _Quantity = 0;
        private decimal _SubTotal = 0;
        private decimal _DiscountSubTotal = 0;
        private decimal _Discount = 0;
        #endregion

        #region Public Properties
        public String ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        public Decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }
        public int OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public Decimal SubTotal
        {
            get { return _SubTotal; }
            set { _SubTotal = value; }
        }
        public Decimal DiscountSubTotal
        {
            get { return _DiscountSubTotal; }
            set { _DiscountSubTotal = value; }
        }

        public Decimal Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void InitializeBusinessData(DataRow dr)
        {
            _ProductName      =          dr["ProductName"].ToString();
            _OrderID          = (int)    dr["OrderID"];
            _Quantity         = (int)    dr["Quantity"];
            _UnitPrice        = (decimal)dr["UnitPrice"];
            _SubTotal         = (decimal)dr["Subtotal"];
            _DiscountSubTotal = (decimal)dr["DiscountSubTotal"];
            _Discount         = (decimal)dr["Discount"];
        }

        #endregion

        #region Public Events

        #endregion

        #region Public Event Handlers

        #endregion

        #region Construction
        public OrderDetail()
        {

        }
        #endregion
    }
}