using System.Collections.Generic;
using System.Linq;

namespace Pushout
{
    public static class Extensions
    {
        public static HashSet<T2> Pushout<T1, T2>(
            //metoda de extensie are un singur codomeniu care este reuniunea multimilor X si Y
            this IEnumerable<T2> codomeniu,
            HashSet<T1> domeniu,
            IFunctie<T1, T2> fi,
            IFunctie<T1, T2> gi)
        {
            List<List<int>> matricePushout = new List<List<int>>();

            //transformam din hash set in lista ca sa putem indexa in domeniu
            List<T2> reuniune = new List<T2>();
            foreach (var item in codomeniu)
            {
                reuniune.Add(item);
            }

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
            foreach (var w in domeniu)
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