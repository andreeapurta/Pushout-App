using System;
using System.Collections.Generic;
using System.Text;

namespace Pushout
{
    public class F : IFunctie<string, string>
    {
        public string Calcul(string intrare)
        {
            switch (intrare)
            {
                case "w1":
                    return "x1";
                case "w2":
                    return "x2";
                case "w3":
                    return "x3";
                case "w4":
                    return "x3";
            }
            throw new NotSupportedException();
        }
    }
}
