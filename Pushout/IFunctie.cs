using System;
using System.Collections.Generic;
using System.Text;

namespace Pushout
{
    public interface IFunctie<TD,TC>
    {
        TC Calcul(TD intrare);
    }
}
