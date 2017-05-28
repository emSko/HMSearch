using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HarmonySearchAlg.Tests
{
    [TestClass()]
    public class ObjFunctionParserTests
    {
        [TestMethod()]
        public void getTheSameVariablesList()
        {
            string function = "x1+4*10+x2^2+x3-4/x4+x1";
            List<string> excepted = new List<string>(new string[] { "x1", "x2", "x3","x4"});
            ObjFunctionParser sut = new ObjFunctionParser(function);
            List<string> actual = sut.getDesignVariables();
            CollectionAssert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void getDifferentVariablesListButTheSameLength()
        {
            string function = "x1+4*10+x2^2+x3-4/x4";
            List<string> excepted = new List<string>(new string[] { "x1", "x2", "x3", "x5" });
            ObjFunctionParser sut = new ObjFunctionParser(function);
            List<string> actual = sut.getDesignVariables();
            CollectionAssert.AreNotEqual(excepted, actual);
        }

        [TestMethod()]
        public void getDifferentVariablesList()
        {
            string function = "x1+4*10+x2^2+x3-4/x4";
            List<string> excepted = new List<string>(new string[] { "x1", "x2", "x5" });
            ObjFunctionParser sut = new ObjFunctionParser(function);
            List<string> actual = sut.getDesignVariables();
            CollectionAssert.AreNotEqual(excepted, actual);
        }


        [TestMethod()]
        public void substitiutFunctionVariables()
        {
            string function = "x1+4*10+(x2*x2)";
            string excepted = "2+4*10+(5*5)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            Dictionary<string, double> dic = new Dictionary<string, double>();
            dic.Add("x1",2);
            dic.Add("x2",5);
            string actual = sut.getFilledObjFuntion(dic);
            Assert.AreEqual(excepted, actual);
        }


        [TestMethod()]
        public void substitiutFunctionVariablesWithMultiply()
        {
            string function = "x1+4*10+x2+3*x2";
            string excepted = "2+4*10+5+3*5";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            Dictionary<string, double> dic = new Dictionary<string, double>();
            dic.Add("x1", 2);
            dic.Add("x2", 5);
            string actual = sut.getFilledObjFuntion(dic);
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void replacePowOperatorForOneVariable()
        {
            string function = "x1^2";
            string excepted = "Pow(x1,2)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.removePow();
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void getListOfLogarithms()
        {
            string function = "x1^2+log(x1)*3+ln(25)";
            string excepted = "x1^2+Log10(x1)*3+Log(2.718281828459,25)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.replaceLogarithmsWithValues();
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void removeEandPi()
        {
            string function = "x1^2+3*e-pi";
            string excepted = "x1^2+3*2.718281828459-3.14159265359";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.removeEandPi();
            Assert.AreEqual(excepted, actual);
        }


        [TestMethod()]
        public void replacePowOperatorForFunction()
        {
            string function = "x1+4*10+x2^2";
            string excepted = "x1+4*10+Pow(x2,2)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.removePow();
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void replacePowOperatorForComplicatedFunction()
        {
            string function = "x1+4*10+x2^2+x3^4/9+x1^3/x2^2";
            string excepted = "x1+4*10+Pow(x2,2)+Pow(x3,4)/9+Pow(x1,3)/Pow(x2,2)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.removePow();
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void replacePowOperatorForFunctionWithBrackets()
        {
            string function = "(x1+2)^2";
            string excepted = "Pow((x1+2),2)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.removePow();
            Assert.AreEqual(excepted, actual);
        }


        [TestMethod()]
        public void replacePowOperatorForFunctionWithBracketsWithNestedPow()
        {
            string function = "(x1^3+2)^2";
            string excepted = "Pow((Pow(x1,3)+2),2)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.removePow();
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void replacePowOperatorForFunctionWithNestedPowVer2()
        {
            string function = "(4+x1^3)^2";
            string excepted = "Pow((4+Pow(x1,3)),2)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.removePow();
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void replacePowOperatorForFunctionWithManyNestedPowVer2()
        {
            string function = "(4+x1^3+x2^4)^2-x2^2";
            string excepted = "Pow((4+Pow(x1,3)+Pow(x2,4)),2)-Pow(x2,2)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.removePow();
            Assert.AreEqual(excepted, actual);
        }


        [TestMethod()]
        public void replaceTrigFuntions()
        {
            string function = "cos(2*x1)-arcsin(3*pi)";
            string excepted = "Cos(2*x1)-Asin(3*pi)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.replaceTrigFunctions();
            Assert.AreEqual(excepted, actual);
        }


        [TestMethod()]
        public void parseEasyFunction()
        {
            string function = "cos(2*x1)-arcsin(3*pi)-x2^2";
            string excepted = "Cos(2*x1)-Asin(3*3.14159265359)-Pow(x2,2)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.parseFunction();
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void parseTrigFunctionNested()
        {
            string function = "(cos(2*x1)-arcsin(3*pi)-x2^2)^2";
            string excepted = "Pow((Cos(2*x1)-Asin(3*3.14159265359)-Pow(x2,2)),2)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.parseFunction();
            Assert.AreEqual(excepted, actual);
        }
        [TestMethod()]
        public void parseTrigFunctionPower()
        {
            string function = "(cos(2*x1))^2";
            string excepted = "Pow((Cos(2*x1)),2)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.parseFunction();
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void RosenbrockFunctionParse()
        {
            string function = "100*(x2-x1^2)^2+(1-x1)^2";

            string excepted = "100*Pow((x2-Pow(x1,2)),2)+Pow((1-x1),2)";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            string actual = sut.parseFunction();
            Assert.AreEqual(excepted, actual);
        }
    }
}