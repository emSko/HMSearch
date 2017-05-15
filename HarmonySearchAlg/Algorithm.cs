﻿using NCalc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonySearchAlg
{
    public class Algorithm
    {
        private ObjFunctionParser functionParser;

        private int numberOfRunds; //kryterium przerwania algorytmu - ilość rund

        //parametry macierzy pamięci harmonii
        private int numberOfDesignVar; // ilość zmiennych decyzyjnych
        private int HMMatrixSize; //rozmiar macierzy pamieci harmonii

        //parametry decydujące o niedeterministyczności rozwiązania, do improwizacji
        private double HMCR; //harmony memory consideration ratio, 
                             //współczynnik wyboru tonu z pamięci
        private double PAR;//Pitch adjust rate, współczynnik dostosowania tonu

        //opis problemu
        private string objectiveFunction;//funkja celu

        List<Dictionary<string, double>> hsMemory;
        //double[,] hsMemory; // pamieć Harmony Search

        // minimum i maksimum zmienności
        Dictionary<string, double> minValues;
        Dictionary<string, double> maxValues;


        public Algorithm(string objectiveFunction, int numberOfDesignVar, Dictionary<string, double> minValues, Dictionary<string, double> maxValues, int numberOfRunds=50000, int HMMatrixSize=20,
                            double HMCR=0.9,double PAR=0.35)
        {
            this.numberOfDesignVar = numberOfDesignVar;
            this.objectiveFunction = objectiveFunction;
            this.numberOfRunds = numberOfRunds;
            hsMemory = new List<Dictionary<string, double>>();
           // hsMemory = new double[HMMatrixSize, numberOfDesignVar+1]; //ilos kolumn to iolsc zmiennych decyzyjnych + 1 kolumna na wynik funkcji celu
            this.HMMatrixSize = HMMatrixSize;
            this.HMCR = HMCR;
            this.PAR = PAR;
            this.minValues = new Dictionary<string, double>(minValues);
            this.maxValues = new Dictionary<string, double>(maxValues);
            functionParser = new ObjFunctionParser(this.objectiveFunction);
        }

        public void sortHSMemory()
        {
            hsMemory.Sort((x, y) => ((double)x["functionVal"]).CompareTo((double)y["functionVal"]));
        }


        public void InitializeHSM()
        {
            // inicjalizacja struktur
           
            Random rnd = new Random();
            List<string> vars = functionParser.getDesignVariables();

            int c = 0; //collumn - kazda kolumna repreezentuje inna zmienna decyzyjna

            for(int r = 0; r<HMMatrixSize;++r)
            {
                hsMemory.Add(new Dictionary<string, double>());
                foreach (var v in vars)
                {
                    double value = rnd.NextDouble() * (maxValues[v] - minValues[v]) + minValues[v];
                    hsMemory[r].Add(v, value);
                }
                double valObjectiveFunc = computeObjectiveFunction(hsMemory[r]);
                hsMemory[r].Add("functionVal", valObjectiveFunc);
            }

            sortHSMemory();
           /* foreach(var v in vars)
            {
                for(int r=0; r<HMMatrixSize;++r) //r-row kazdy wiersz to inne rozwiazanie
                {
                    hsMemory[r][c] = rnd.NextDouble() * (maxValues[v] - minValues[v]) + minValues[v];
                }
                ++c;
            }*/

            //obliczenie wartosci funckji celu dla kazdego rozwiazania

           /* for (int r = 0; r < HMMatrixSize; r++)	// wypełnienie pamięci losowymi wartościami
                for (int c = 0; c < numberOfDesignVar; c++)
                    hsMemory[r, c] = rnd.Next( /*zakres zmiennosci);*/
        }

        public double computeObjectiveFunction(Dictionary<string, double> varValues)
        {
             var resultOfFunction = new Expression(functionParser.getFilledObjFuntion(varValues)).Evaluate();
            return Convert.ToDouble(resultOfFunction);
        }

    }
}
