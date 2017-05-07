using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonySearchAlg
{
    class Algorithm
    {
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

        public Algorithm(string objectiveFunction, int numberOfRunds=50000, int HMMatrixSize=20,
                            double HMCR=0.9,double PAR=0.35)
        {
            this.objectiveFunction = objectiveFunction;
            this.numberOfRunds = numberOfRunds;
        }
    }
}
