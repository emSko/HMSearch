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


            Algorithm sut = new Algorithm(objFunction, numberOfVar, 
                new Dictionary<string, double>(), new Dictionary<string, double>());
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

            Algorithm sut = new Algorithm(objFunction, numberOfVar,
                new Dictionary<string, double>(), new Dictionary<string, double>());
            double actual = sut.computeObjectiveFunction(dic);
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void computeObjectiveFunctionCorrectWithPowTest()
        {
            string objFunction = "3*x1+2*x2^2";
            int x1 = 2, x2 = 3;
            int numberOfVar = 2;

            Dictionary<string, double> dic = new Dictionary<string, double>();
            dic.Add("x1", 2);
            dic.Add("x2", 3);

            double excepted = 3 * x1 + 2 * Math.Pow(x2,2);

            Algorithm sut = new Algorithm(objFunction, numberOfVar,
                new Dictionary<string, double>(), new Dictionary<string, double>());
            double actual = sut.computeObjectiveFunction(dic);
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void computeObjectiveFunctionCorrectWithPowInDenominatorTest()
        {
            string objFunction = "3*x1+2*x2^2/x1^2";
            double x1 = 0.5, x2 = 2.1;
            int numberOfVar = 2;

            Dictionary<string, double> dic = new Dictionary<string, double>();
            dic.Add("x1", x1);
            dic.Add("x2", x2);

            double excepted = 3 * x1 + 2 * Math.Pow(x2, 2)/ Math.Pow(x1, 2);

            Algorithm sut = new Algorithm(objFunction, numberOfVar,
                new Dictionary<string, double>(), new Dictionary<string, double>());
            double actual = sut.computeObjectiveFunction(dic);
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void computeObjectiveFunctionCorrectWithNestedPowTest()
        {
            string objFunction = "(4+x1^3)^2";
            double x1 = 0.5, x2 = 2.1;
            int numberOfVar = 2;

            Dictionary<string, double> dic = new Dictionary<string, double>();
            dic.Add("x1", x1);
            dic.Add("x2", x2);

            double excepted = Math.Pow((4 + Math.Pow(x1, 3)), 2);

            Algorithm sut = new Algorithm(objFunction, numberOfVar,
                new Dictionary<string, double>(), new Dictionary<string, double>());
            double actual = sut.computeObjectiveFunction(dic);
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void computeRodenbrockFunction()
        {
            string objFunction = "100*(x2-x1^2)^2+(1-x1)^2";
            int numberOfVar = 2;


            Dictionary<string, double> minValues = new Dictionary<string, double>();
            minValues.Add("x1", -10);
            minValues.Add("x2", -10);


            Dictionary<string, double> maxValues = new Dictionary<string, double>();
            maxValues.Add("x1", 10);
            maxValues.Add("x2", 10);

            double excepted = 0;

            Algorithm sut = new Algorithm(objFunction, numberOfVar,
                minValues, maxValues);
            Dictionary<string, double> actual = sut.runAlgorithm();
            Assert.AreNotEqual(excepted, actual["functionVal"]);
        }


        [TestMethod()]
        public void computeLeviFunction()
        {
            string objFunction = "(sin(3*pi*x1))^2+(x1-1)^2*(1+(sin(3*pi*x2))^2)+(x2-1)^2*(1+(sin(2*pi*x2))^2)";
            int numberOfVar = 2;

            Dictionary<string, double> minValues = new Dictionary<string, double>();
            minValues.Add("x1", -10);
            minValues.Add("x2", -10);


            Dictionary<string, double> maxValues = new Dictionary<string, double>();
            maxValues.Add("x1", 10);
            maxValues.Add("x2", 10);

            double excepted = 0;

            Algorithm sut = new Algorithm(objFunction, numberOfVar,
                minValues, maxValues,1000);
            Dictionary<string, double> actual = sut.runAlgorithm();
            Assert.AreNotEqual(excepted, actual["functionVal"]);
        }


        [TestMethod()]
        public void computeExp()
        {
            string objFunction = "Exp(2)";
            int numberOfVar = 2;

            double excepted = Math.Exp(2);


            Algorithm sut = new Algorithm(objFunction, numberOfVar,
                new Dictionary<string, double>(), new Dictionary<string, double>());
            double actual = sut.computeObjectiveFunction(new Dictionary<string, double>());
            Assert.AreEqual(excepted, actual);
        }


        [TestMethod()]
        public void computeAbs()
        {
            string objFunction = "Abs(2)";
            int numberOfVar = 2;

            double excepted = Math.Abs(2);


            Algorithm sut = new Algorithm(objFunction, numberOfVar,
                new Dictionary<string, double>(), new Dictionary<string, double>());
            double actual = sut.computeObjectiveFunction(new Dictionary<string, double>());
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void computeEwasFunction()
        {
            string objFunction = "x1*exp(-x1^2-x2^2)";
            int numberOfVar = 2;

            Dictionary<string, double> minValues = new Dictionary<string, double>();
            minValues.Add("x1", -5);
            minValues.Add("x2", -5);


            Dictionary<string, double> maxValues = new Dictionary<string, double>();
            maxValues.Add("x1", 5);
            maxValues.Add("x2", 5);

            double excepted = -0.42888194248;

            Algorithm sut = new Algorithm(objFunction, numberOfVar,
                minValues, maxValues, 10000);
            Dictionary<string, double> actual = sut.runAlgorithm();
            Assert.AreNotEqual(excepted, actual["functionVal"]);
        }

    }
}