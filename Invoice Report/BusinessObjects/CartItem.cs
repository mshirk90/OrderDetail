using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessObjects
{
    public class CartItem
    {
        #region Private Members
        private Guid _Id = Guid.Empty;
        private String _Name = String.Empty;
        private Decimal _Price = 0;
        private Int32 _Quantity = 0;
        private Decimal _Subtotal = 0;
        #endregion

        #region Public Properties
        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public Decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        public Int32 Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        public Decimal Subtotal
        {
            get { return _Subtotal; }
            set { _Subtotal = value; }
        }
        #endregion

        #region Construction
        public CartItem(String productId, String name, String price)
        {
            _Id = new Guid(productId);
            _Name = name;
            _Price = Convert.ToDecimal(price);
            _Quantity = 1;
        }
        public CartItem()
        {
            _Quantity = 1;
        }
        #endregion
    }
}