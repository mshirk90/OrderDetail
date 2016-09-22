using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class SavableEventArgs
    {
        #region Private Member
        private Boolean _Savable = false;
        #endregion

        #region Public Property
        public Boolean Savable
        {
            get { return _Savable; }
            set { _Savable = value; }
        }
        #endregion

        #region Construction
        public SavableEventArgs(Boolean Savable)
        {
            _Savable = Savable;
        }
        #endregion
    }
}