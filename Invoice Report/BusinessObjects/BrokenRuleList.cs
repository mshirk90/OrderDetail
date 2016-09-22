using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessObjects
{
    public class BrokenRuleList
    {
        #region Private Members
        private List<BrokenRule> _List = new List<BrokenRule>();
        #endregion

        #region Public Properties
        public List<BrokenRule> List
        {
            get { return _List; }
        }
        public Int32 Count
        {
            get { return _List.Count; }
        }
        #endregion
    }
}