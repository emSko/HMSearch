using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonySearchAlg
{
    public class ObjFunctionParser
    {
        private string function;
        char[] separatingChars = { '+', '-', '*', '/', '^'};

        public ObjFunctionParser(string function)
        {
            this.function = function;
        }

        //funckja zwraca listę zawierającą wszytskie zmienne decyzyjne
        public List<string> getDesignVariables()
        {
            List<string> variables = new List<string>();
            string[] words = function.Split(separatingChars, 
                             System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string w in words)
            {
                if (w.StartsWith("x") && !variables.Contains(w))
                    variables.Add(w);
            }
            return variables;
        }


        public string replacePowOperator()
        {
            string result = "";
            /*string[] words = function.Split('^');
            for (int i=0; i<(words.Length-1); ++i)
            {
                string[] w1 = words[i].Split(separatingChars,
                             System.StringSplitOptions.RemoveEmptyEntries);
                words[i] = words[i].Substring(0, words[i].Length - w1[w1.Length - 1].Length);
                if (result != "")
                  result=result.Substring(0, result.Length-w1[0].Length-1);

                result = result + words[i];
                result = result + "Pow("+ w1[w1.Length - 1]+",";
                string[] w2 = words[i+1].Split(separatingChars,
                            System.StringSplitOptions.RemoveEmptyEntries);
                words[i + 1] = words[i + 1].Substring(w2[0].Length);
                result = result + w2[0] + ")" + words[i + 1];
            }*/
            string[] pieces = function.Split('^');
            for(int i = 0;i<(pieces.Length-1);++i)
            {
                result = result + pieces[i];

                string[] p1 = pieces[i].Split(separatingChars,
                            System.StringSplitOptions.RemoveEmptyEntries);
                string [] p2 = pieces[i + 1].Split(separatingChars,
                            System.StringSplitOptions.RemoveEmptyEntries);
                string number = p1[p1.Length - 1];
                for(int j=0; j<Convert.ToInt32(p2[0])-1; j++)
                {
                    result = result + "*" + number;
                }
                pieces[i + 1] = pieces[i + 1].Substring(p2[0].Length);
            }
            result = result + pieces[pieces.Length - 1];
            return result;
        }


        //funkcja podstawia za zmienne  
        public string getFilledObjFuntion(Dictionary<string,double> varValues)
        {
            string filledFunction = this.function;
            List<string> vars = getDesignVariables();
            foreach(string v in vars)
            {
                filledFunction = filledFunction.Replace(v, varValues[v].ToString());
            }
            return filledFunction;
        }
    }
}
