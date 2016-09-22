using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Event
    {
        public delegate void SavableEventHandler(SavableEventArgs e);

        public event SavableEventHandler Savable;

        public void RaiseEvent(SavableEventArgs e)
        {
            SavableEventHandler evt = Savable;
            if (evt != null)
            {
                evt(e);
            }
        }
    }
}