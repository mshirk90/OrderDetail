using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessObjects
{
    public class BrokenRule
    {
        #region Private Members
        private String _Rule = String.Empty;
        #endregion

        #region Public Properties
        public String Rule
        {
            get { return _Rule; }
            set { _Rule = value; }
        }
        #endregion

        #region Construction
        public BrokenRule(String rule)
        {
            _Rule = rule;
        }
        #endregion
    }
}