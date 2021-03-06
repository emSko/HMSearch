﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonySearchAlg
{
    public class ObjFunctionParser
    {
        private string function;
        char[] separatingChars = { '+', '-', '*', '/', '^', ')', '(' };
        char[] separatingCharsForDeVar = { '+', '-', '*', '/', '^', ')', '(' ,','};

        public ObjFunctionParser(string function)
        {
            this.function = function;
        }

        public string parseFunction()
        {
            this.function = replaceExpFunction();
            this.function = replaceAbsFunction();
            this.function = replaceTrigFunctions();
            this.function = replaceLogarithmsWithValues();
            this.function = removePow();
            this.function = removeEandPi();
            return this.function;
        }

        public string removePow()
        {
            if (this.function.Contains('^'))
                this.function = replacePowOperator();
            return this.function;
        }

        public string removeEandPi()
        {
            if (this.function.Contains('e'))
            {
                this.function = replaceESign();
            }

            if (this.function.Contains("pi"))
            {
                this.function = replacePiSign();
            }

            return this.function;
        }

        public string replaceESign()
        {
            string result = this.function;
            result = result.Replace("e", "2.718281828459");
            return result;
        }

        public string replacePiSign()
        {
            string result = this.function;
            result = result.Replace("pi", "3.14159265359");
            return result;
        }

        //funckja zwraca listę zawierającą wszytskie zmienne decyzyjne
        public List<string> getDesignVariables()
        {
            List<string> variables = new List<string>();
            string[] words = function.Split(separatingCharsForDeVar, 
                             System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string w in words)
            {
                if (w.StartsWith("x") && !variables.Contains(w))
                    variables.Add(w);
            }
            return variables;
        }

        //metoda zwraca listę zawierającą wyrażenia logarytmiczne
        public List<string> getLogarithms()
        {
            char[] separators = { '+', '-', '*', '/', '^', ',' };
            List<string> logarithm = new List<string>();
            string[] words = function.Split(separators,
                             System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string w in words)
            {
                if (w.StartsWith("l") && !logarithm.Contains(w))
                    logarithm.Add(w);
            }
            return logarithm;
        }

        // metoda podstawia math.logarithm
        public string replaceLogarithmsWithValues()
        {
            string filledFunction = this.function;
            List<string> logarithms = getLogarithms();

            int begin;
            int end;
            string number;

            foreach (string log in logarithms)
            {

                if (log.Contains("log"))
                {
                    begin = 4;
                    end = log.LastIndexOf(")");
                    number = log.Substring(begin, end - begin);
                    var numberString = number.ToString().Replace(",", ".");
                    filledFunction = filledFunction.Replace(log, "Log10(" + numberString + ")");
                }

                else
                    if(log.Contains("ln"))
                {
                    begin = 3;
                    end = log.LastIndexOf(")");
                    number = log.Substring(begin, end - begin);
                    var numberString = number.ToString().Replace(",", ".");
                    filledFunction = filledFunction.Replace(log, "Log(2.718281828459," + numberString + ")");
                }

            }
            return filledFunction;
        }


        private string replacePowOperator()
        {
            string result = "";
            string[] pieces = function.Split('^');

            for(int i = 0;i<(pieces.Length-1);++i)
            {
                result = result + pieces[i];
                if (result[result.Length - 1] == ')')
                {
                    string f;
                    int j = 0;
                    int lInd = result.Length-1;
                    int p = 1;
                    int c = p - j;
                    while (c!=0)
                    {
                        lInd = result.Substring(0,lInd).LastIndexOf('(');
                        p = result.Substring(lInd).Count(z => z == ')');
                        j++;
                        c = p - j;
                    }
                    if (lInd != 0)
                    {
                        f = result.Substring(lInd);
                        result = result.Substring(0,lInd);
                    }
                    else
                    {
                        f = result;
                        result = "";
                    }
                    result = result + "Pow(" + f + ",";
                    //string f=""; int position;
                    //string temp = result;
                    //string x;
                    //do
                    //{
                    //    position = temp.LastIndexOf('(');
                    //    if (position == 0)
                    //    {
                    //        f = result;
                    //        result = "";
                    //        break;
                    //    }
                    //    if (position < 3)
                    //        break;
                    //    temp = temp.Substring(0, position);
                    //    int[] signIndex = { temp.LastIndexOf("+"),
                    //    temp.LastIndexOf("-"),temp.LastIndexOf("/"),temp.LastIndexOf("*"),temp.LastIndexOf("(")};
                    //    int op = signIndex.Max();
                    //    x = temp.Substring(op+1);
                    //} while (x == "Pow"|| x=="Log"|| x=="Sin"|| x == "Cos"|| x == "Asin"||
                    //x == "Acos"|| x == "Tan"|| x == "Atan" || x == "Exp");
                    //if (position != 0)
                    //{
                    //    f = result.Substring(position);
                    //    result = temp;
                    //}
                    //result = result + "Pow(" + f + ",";
                }
                else
                {

                    string[] p1 = pieces[i].Split(separatingChars,
                              System.StringSplitOptions.RemoveEmptyEntries);

                    result = result.Substring(0, result.Length - p1[p1.Length - 1].Length);
                    result = result + "Pow(" + p1[p1.Length - 1]+",";
                }
                string[] p2 = pieces[i + 1].Split(separatingChars,
                            System.StringSplitOptions.RemoveEmptyEntries);
                result = result + p2[0] + ")";
                pieces[i + 1] = pieces[i + 1].Substring(p2[0].Length);

            }
            result = result + pieces[pieces.Length - 1];

            /*
            pieces = result.Split('(');
            result = "";
            for(int i = 0; i < (pieces.Length); ++i)
            {
                if (!pieces[i].Contains(")"))
                    result = result + pieces[i];
                else
                {
                    string[] p1= pieces[i].Split(')');
                    if (p1[p1.Length-1].Substring(0,1)=="^")
                    {
                        string[] p2 = p1[p1.Length - 1].Split(separatingChars,
                                System.StringSplitOptions.RemoveEmptyEntries);
                        p1[0] = "(" + pieces[0] + ")";
                        result = result + p1[0];
                        for (int j=0; j<Convert.ToInt32(p2[0]);++j )
                        {
                            result = result + "*" + p1[0];
                        }
                    }
                    else
                    {
                        result = result + "(" + pieces[i];
                    }

                }
            }*/

            return result;
        }

        public string replaceTrigFunctions()
        {
            string result=this.function;
            List<string> trig = new List<string>(new string[] { "cos", "sin", "arccos",
            "arcsin", "tan", "arctan", "arcCos", "arcSin"}); ;


            Dictionary<string, string> trigFunc =
            new Dictionary<string, string>();

            trigFunc.Add(trig[0],"Cos");
            trigFunc.Add(trig[1],"Sin");
            trigFunc.Add(trig[2],"Acos");
            trigFunc.Add(trig[3], "Asin");
            trigFunc.Add(trig[4], "Tan");
            trigFunc.Add(trig[5], "Atan");
            trigFunc.Add(trig[6], "Acos");
            trigFunc.Add(trig[7], "Asin");

            foreach (string v in trig)
            {
                result = result.Replace(v,trigFunc[v]);
            }
            return result;
        }

        public string replaceExpFunction()
        {
            string result = this.function;
            result = result.Replace("exp", "Exp");
            return result;
        }

        public string replaceAbsFunction()
        {
            string result = this.function;
            if (result.Contains("|"))
            {
                int i=0;
                bool isFront = true;
                while ((i = result.IndexOf('|', i)) != -1)
                {
                    // Print out the substring.
                    if (isFront)
                    {
                        if(i!=0)
                        {
                            string x = result.Substring(0, i);
                            result = x + "Abs(" + result.Substring(i + 1);
                        }
                        else 
                            result ="Abs(" + result.Substring(i + 1);
                    }
                    else
                    {
                        string x = result.Substring(0, i) + ")";
                        if (i != result.Length - 1)
                        
                            result = x + result.Substring(i + 1);
                        else
                            result = x;
                           
                    }
                    isFront = !isFront;
                }

            }

            return result;
        }


        //funkcja podstawia za zmienne  
        public string getFilledObjFuntion(Dictionary<string,double> varValues)
        {
            string filledFunction = this.function;
            List<string> vars = getDesignVariables();
            foreach(string v in vars)
            {
                string number = varValues[v].ToString();
                filledFunction = filledFunction.Replace(v, number.Replace(',','.'));
            }
          //  filledFunction = filledFunction.Replace(',', '.');
            return filledFunction;
        }
    }
}
