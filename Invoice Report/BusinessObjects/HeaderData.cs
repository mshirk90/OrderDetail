using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DatabaseHelper;
using System.Data;

namespace BusinessObjects
{
    public class HeaderData 
    {
        #region Private Members
        private Int32 _ID = 0;
        #endregion

        #region Public Properties
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods
        public void Initialize(DataRow dr)
        {
            _ID = (Int32)dr["ID"];
            
        }
        #endregion

        #region Public Events

        #endregion

        #region Public Event Handlers

        #endregion

        #region Construction

        #endregion
    }
}