using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarmonySearchAlg;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HarmonySearchAlgTests
{
    [TestClass]
    public class Plotting_Form1Test
    {
        //[TestMethod()]
        //public void DrawingTest()
        //{
        //    string function = "x1+x2";

        //    string excepted = "100*Pow((x2-Pow(x1,2)),2)+Pow((1-x1),2)";
        //    ObjFunctionParser sut = new ObjFunctionParser(function);

        //    sut.parseFunction();

        //    Plotting_Form1 plot = new Plotting_Form1(ref sut);

        //    Dictionary<string, double> min = new Dictionary<string, double>();
        //    Dictionary<string, double> max = new Dictionary<string, double>();
        //    min.Add("x1", 1);
        //    min.Add("x2", 1);
        //    max.Add("x1", 5);
        //    max.Add("x2", 5);
        //    List<string> vars = new List<string>();
        //    vars.Add("x1");
        //    vars.Add("x2");

        //    plot.drawSurfacePlot(min, max, vars);

        //    Assert.AreEqual(excepted, 4);
        //}
    }
}
