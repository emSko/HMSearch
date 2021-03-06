﻿using NCalc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarmonySearchAlg
{
    public class Algorithm
    {
        Random rnd;

        private ObjFunctionParser functionParser;

        private int numberOfRunds; //kryterium przerwania algorytmu - ilość rund

        //parametry macierzy pamięci harmonii
        private int numberOfDesignVar; // ilość zmiennych decyzyjnych
        private int HMMatrixSize; //rozmiar macierzy pamieci harmonii

        //parametry decydujące o niedeterministyczności rozwiązania, do improwizacji
        private double HMCR; //harmony memory consideration ratio, 
                             //współczynnik wyboru tonu z pamięci
        private double PAR;//Pitch adjust rate, współczynnik dostosowania tonu
        private double bw;
        //opis problemu
        private string objectiveFunction;//funkja celu

        List<Dictionary<string, double>> hsMemory;

        //lista zmiennych decyzyjnych
        List<string> vars;

        // minimum i maksimum zmienności
        Dictionary<string, double> minValues;
        Dictionary<string, double> maxValues;

        public Algorithm(string objectiveFunction, int numberOfDesignVar, Dictionary<string, double> minValues, 
            Dictionary<string, double> maxValues, int numberOfRunds=50000, int HMMatrixSize=20,
                            double HMCR=0.9,double PAR=0.35, double bw=3)
        {
            this.numberOfDesignVar = numberOfDesignVar;
            this.objectiveFunction = objectiveFunction;
            this.numberOfRunds = numberOfRunds;
            hsMemory = new List<Dictionary<string, double>>();
            this.HMMatrixSize = HMMatrixSize;
            this.HMCR = HMCR;
            this.PAR = PAR;
            this.minValues = new Dictionary<string, double>(minValues);
            this.maxValues = new Dictionary<string, double>(maxValues);
            this.bw = bw;

            functionParser = new ObjFunctionParser(this.objectiveFunction);
            functionParser.parseFunction();

            rnd = new Random();
        }

        public void drawSurfacePlot()
        {
            Plotting_Form1 plot = new Plotting_Form1(ref functionParser);
            plot.drawSurfacePlot(minValues, maxValues, functionParser.getDesignVariables());
            plot.Show();
        }

        public Dictionary<string, double> runAlgorithm()
        {
            InitializeHSM();
            for (int i = 0; i < numberOfRunds; ++i)
                improviseNewSolution();
            return hsMemory[0];
        }

        public void improviseNewSolution()
        {
            Dictionary<string, double> newSolution = newValueSelection();
            newSolution.Add("functionVal", computeObjectiveFunction(newSolution));

            if(newSolution["functionVal"]<hsMemory[HMMatrixSize-1]["functionVal"])
            {
                hsMemory[HMMatrixSize - 1] = newSolution;
                sortHSMemory();
            }
        }

        public Dictionary<string, double> newValueSelection()
        {
            Dictionary<string, double> harmony=new Dictionary<string, double>();
            foreach (var v in vars)
            {
                double probOfHistoric = rnd.NextDouble();
                double value;
                //wartosc ze znajdujacych sie juz w macierzy
                if (probOfHistoric <= HMCR)
                {
                    int index = rnd.Next(0, HMMatrixSize);
                    value = hsMemory[index][v];
                    double probPitchAdjust = rnd.NextDouble();

                    // dostrajanie pitch adjustment
                    if (probOfHistoric<=PAR)
                    {
                        value = value + valueRandomSelection(-1, 1) * bw*(maxValues[v]-minValues[v]);
                        if (value < minValues[v])
                            value = minValues[v];
                        if (value > maxValues[v])
                            value = maxValues[v];
                    }
                }
                else
                    //wartosc z calego przedzialu
                    value = valueRandomSelection(minValues[v], maxValues[v]);

                harmony.Add(v, value);
            }
            return harmony;
        }


        public void sortHSMemory()
        {
            hsMemory.Sort((x, y) => ((double)x["functionVal"]).CompareTo((double)y["functionVal"]));
        }

        private double valueRandomSelection(double lb, double ub)
        {
            double value = rnd.NextDouble() * (ub - lb) + lb;
            return value;
        }

        public void InitializeHSM()
        {
            // inicjalizacja struktur
            vars = functionParser.getDesignVariables();

            for(int r = 0; r<HMMatrixSize;++r)
            {
                hsMemory.Add(new Dictionary<string, double>());
                foreach (var v in vars)
                {
                    //double value = rnd.NextDouble() * (maxValues[v] - minValues[v]) + minValues[v];
                    hsMemory[r].Add(v, valueRandomSelection(maxValues[v], minValues[v]));
                }
                double valObjectiveFunc = computeObjectiveFunction(hsMemory[r]);
                hsMemory[r].Add("functionVal", valObjectiveFunc);
            }

            sortHSMemory();
        }

        public double computeObjectiveFunction(Dictionary<string, double> varValues)
        {
                var resultOfFunction = new Expression(functionParser.getFilledObjFuntion(varValues)).Evaluate();
                return Convert.ToDouble(resultOfFunction);
        }

    }
}
