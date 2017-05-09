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

        double[,] hsMemory; // pamieć Harmony Search
        List<double> bounds; // minimum i maksimum zmienności


        public Algorithm(string objectiveFunction, int numberOfDesignVar, int numberOfRunds=50000, int HMMatrixSize=20,
                            double HMCR=0.9,double PAR=0.35)
        {
            this.objectiveFunction = objectiveFunction;
            this.numberOfRunds = numberOfRunds;
            hsMemory = new double[HMMatrixSize, numberOfDesignVar];
            bounds = new List<double>();
            this.HMMatrixSize = HMMatrixSize;
            this.HMCR = HMCR;
            this.PAR = PAR;
            functionParser = new ObjFunctionParser(this.objectiveFunction);
        }

        private void Initialize()
        {
            // inicjalizacja struktur
           
            Random rnd = new Random();

            for (int r = 0; r < HMMatrixSize; r++)	// wypełnienie pamięci losowymi wartościami
                for (int c = 0; c < numberOfDesignVar; c++)
                    hsMemory[r, c] = rnd.Next( /*zakres zmiennosci*/);
        }

        public double computeObjectiveFunction(Dictionary<string, double> varValues)
        {
            DataTable dt = new DataTable();

            var resultOfFunction = dt.Compute(functionParser.getFilledObjFuntion(varValues),"");
            return Convert.ToDouble(resultOfFunction);
        }

    }
}
