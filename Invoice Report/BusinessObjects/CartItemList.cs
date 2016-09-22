using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace BusinessObjects
{
    public class CartItemList
    {
        #region Private Members
        private BindingList<CartItem> _List = new BindingList<CartItem>();
        private Decimal _Total = 0;
        private Int32 _TotalItems = 0;
        #endregion

        #region Public Properties
        public BindingList<CartItem> List
        {
            get { return _List; }
        }
        public Decimal Total
        {
            get { return _Total; }
        }
        public Int32 TotalItems
        {
            get
            {
                _TotalItems = 0;
                foreach(CartItem ci in _List)
                {
                    _TotalItems += ci.Quantity;
                }
                return _TotalItems;
            }
        }
        #endregion

        #region Public Methods
        public void Calculate()
        {

        }
        public Boolean Exists(Guid productId)
        {
            Boolean result = false;
            foreach(CartItem item in _List)
            {
                if(item.Id == productId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void IncrementQuantity(Guid productId)
        {
            foreach (CartItem item in _List)
            {
                if (item.Id == productId)
                {
                    item.Quantity++;
                    break;
                }
            }
        }
        #endregion
    }
}