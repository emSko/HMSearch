using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarmonySearchAlg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonySearchAlg.Tests
{
    [TestClass()]
    public class AlgorithmTests
    {
        [TestMethod()]
        public void computeObjectiveFunctionCorrectTest()
        {
            string objFunction = "3*x1+2*x2*x2";
            int x1 = 2, x2 = 3;
            int numberOfVar = 2;

            Dictionary<string, double> dic = new Dictionary<string, double>();
            dic.Add("x1", 2);
            dic.Add("x2", 3);

            double excepted = 3*x1+2*Math.Pow(x2,2);

            Algorithm sut = new Algorithm(objFunction, numberOfVar);
            double actual=sut.computeObjectiveFunction(dic);
            Assert.AreEqual(excepted,actual);
        }
        [TestMethod()]
        public void computeObjectiveFunctionCorrectWithoutPowTest()
        {
            string objFunction = "3*x1+2*x2";
            int x1 = 2, x2 = 3;
            int numberOfVar = 2;

            Dictionary<string, double> dic = new Dictionary<string, double>();
            dic.Add("x1", 2);
            dic.Add("x2", 3);

            double excepted = 3 * x1 + 2 * x2;

            Algorithm sut = new Algorithm(objFunction, numberOfVar);
            double actual = sut.computeObjectiveFunction(dic);
            Assert.AreEqual(excepted, actual);
        }
    }
}