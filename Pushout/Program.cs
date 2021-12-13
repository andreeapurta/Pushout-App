using System;
using System.Collections.Generic;

namespace Pushout
{
    public class Program
    {
        private static void Main(string[] args)
        {
            PushoutImplementation<string, string> pushout = new PushoutImplementation<string, string>();
            //definim multimile
            HashSet<string> X = new HashSet<string>();
            HashSet<string> W = new HashSet<string>();
            HashSet<string> Y = new HashSet<string>();
            X.Add("x1");
            X.Add("x2");
            X.Add("x3");
            W.Add("w1");
            W.Add("w2");
            W.Add("w3");
            W.Add("w4");
            Y.Add("y1");
            Y.Add("y2");

            var result = pushout.GetPushout(new F(), X, new G(), Y, W);
            Console.Write("Pushout: ");
            foreach (var item in result)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            HashSet<string> extensie = new HashSet<string>();
            //la extensie codomeniul este reuniunea multimilor x si y
            extensie.Add("x1");
            extensie.Add("x2");
            extensie.Add("x3");
            extensie.Add("y1");
            extensie.Add("y2");
            Console.Write("Pushout Extensie: ");
            var resultExtensie = extensie.Pushout(W, new F(), new G());
            foreach (var item in resultExtensie)
            {
                Console.Write(item + " ");
            }
        }
    }
}