using System;
using System.Collections.Generic;
using System.Linq;

namespace Pushout
{
    public class PushoutImplementation<T1, T2> : PushoutAbstract<T1, T2>
    {
        public override HashSet<T2> GetPushout(
            IFunctie<T1, T2> fi,
            HashSet<T2> codomfi,//x
            IFunctie<T1, T2> gi,
            HashSet<T2> codomgi, //y
            HashSet<T1> domi) //w
        {
            List<List<int>> matricePushout = new List<List<int>>();

            //xUy
            List<T2> reuniune = new List<T2>();
            foreach (var item in codomfi)
                reuniune.Add(item);
            foreach (var item in codomgi)
                reuniune.Add(item);

            //initializam matrice pushout cu 0
            for (int i = 0; i < reuniune.Count; i++)
            {
                matricePushout.Add(new List<int>());
                for (int j = 0; j < reuniune.Count; j++)
                {
                    matricePushout[i].Add(0);
                }
            }

            //Relatia initiala
            foreach (var w in domi)
            {
                var linie = fi.Calcul(w);
                var coloana = gi.Calcul(w);
                var indexLinie = reuniune.IndexOf(linie);
                var indexColoana = reuniune.IndexOf(coloana);

                matricePushout[indexLinie][indexColoana] = 1;
            }

            //Reflexivitatea

            for (int i = 0; i < reuniune.Count; i++)
            {
                matricePushout[i][i] = 1;
            }

            //Simetria
            for (int i = 0; i < reuniune.Count; i++)
            {
                for (int j = 0; j < reuniune.Count; j++)
                {
                    if (matricePushout[i][j] == 1)
                    {
                        matricePushout[j][i] = matricePushout[i][j];
                    }
                }
            }

            //Calcul Partitii
            Dictionary<T2, int> partitii = new Dictionary<T2, int>();

            int s = 1;
            int oldi = 0; //pentru ca dupa ca gat cu valorile de -1, continui unde am ramas
            for (int i = 0; i < reuniune.Count; i++) //parcurgere reuniune
            {
                //verificat casutele initiale din partitie, daca e ceva pe ele sau nu
                var item = reuniune[i];
                if (!partitii.ContainsKey(item)) //adaug 1 pe pozitia elementului curent ( de ex x1 , x1)
                {
                    partitii.Add(item, s);
                }
                else //daca exista itemul deja ma uit daca e negativ
                {
                    if (partitii[item] < 0)
                    {
                        partitii.Remove(item); //il scot
                        partitii.Add(item, s); //il inloocuiesc cu nr pozitiv ( aici diferenta de la P2 la P3  ex -1 => 1)
                    }
                }
                // indexul elementul curent la care sunt
                var indexItem = reuniune.IndexOf(item);
                for (int j = 0; j < matricePushout[indexItem].Count; j++)
                {
                    if (j != indexItem) //nu ma mai uit pe diagonala ca deja l-am adaugat
                    {
                        if (!partitii.ContainsKey(reuniune[j]))
                        {
                            if (matricePushout[indexItem][j] != 0)
                            {
                                partitii.Add(reuniune[j], -1); //daca nu exista elementul deja adauga -1 //daca nu se face reuniune cu el insusi, punem -1 in matrice
                                oldi = i;
                                i = reuniune.IndexOf(reuniune[j]) - 1; //urmatorul element devine cel nou adaugat
                            }
                        }
                    }
                }
                //daca nu mai exista nici un -1 pe partitii restaurez i ca sa trec mai departe
                if (!partitii.ContainsValue(-1) && (partitii.Count != reuniune.Count))
                {
                    i = oldi;
                    s++;
                }
            }
            //pe baza partitiilor se intoarce intr-un hashset valoarea pushout ului
            HashSet<T2> result = new HashSet<T2>();
            for (int i = 0; i < reuniune.Count; i++)
            {
                if (partitii.ContainsValue(i))
                {
                    result.Add(partitii.First(x => x.Value == i).Key);
                }
            }
            return result;
        }
    }
}