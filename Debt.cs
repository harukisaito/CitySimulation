using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CitySimulation
{
    class Debt
    {
        private float debtAmount;
        private int tempDay;

        public float DebtAmount
        {
            get {return debtAmount;}
            set {debtAmount = value;}
        }

        public int TempDay
        {
            get {return tempDay;}
            set {tempDay = value;}
        }
    }
}
