using System;
using System.Collections.Generic;
using System.Text;

namespace Pushout
{
    public class G : IFunctie<string, string>
    {
        public string Calcul(string intrare)
        {
            switch (intrare)
            {
                case "w1":
                    return "y1";

                case "w2":
                    return "y2";

                case "w3":
                    return "y2";

                case "w4":
                    return "y2";
            }
            throw new NotSupportedException();
        }
    }
}