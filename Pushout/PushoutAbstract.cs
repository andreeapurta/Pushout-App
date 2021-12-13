using System.Collections.Generic;

namespace Pushout
{
    public abstract class PushoutAbstract<T1, T2>
    {
        public abstract HashSet<T2> GetPushout(
            IFunctie<T1, T2> fi, 
            HashSet<T2> codomfi, 
            IFunctie<T1, T2> gi, 
            HashSet<T2> codomgi, 
            HashSet<T1> domi
            );
    }
}