﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using HarmonySearchAlg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonySearchAlg.Tests
{
    [TestClass()]
    public class ObjFunctionParserTests
    {
        [TestMethod()]
        public void getTheSameVariablesList()
        {
            string function = "x1+4*10+x2^2+x3-4/x4";
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
            string function = "x1+4*10+x2^2";
            string excepted = "2+4*10+5^2";
            ObjFunctionParser sut = new ObjFunctionParser(function);
            Dictionary<string, double> dic = new Dictionary<string, double>();
            dic.Add("x1",2);
            dic.Add("x2",5);
            string actual = sut.getFilledObjFuntion(dic);
            Assert.AreEqual(excepted, actual);
        }
    }
}