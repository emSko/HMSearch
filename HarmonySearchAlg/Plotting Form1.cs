﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using NCalc;
using ILNumerics.Toolboxes;

namespace HarmonySearchAlg
{
    public partial class Plotting_Form1 : Form
    {

        ObjFunctionParser functionParser;
        List<double> xValues;
        List<double> yValues;
        List<double> zValues;
        int range = 10;

        public Plotting_Form1(ref ObjFunctionParser functionParser)
        {
            this.functionParser = functionParser;
            InitializeComponent();
        }

        public double computeObjectiveFunction(Dictionary<string, double> varValues)
        {
            var resultOfFunction = new Expression(functionParser.getFilledObjFuntion(varValues)).Evaluate();
            return Convert.ToDouble(resultOfFunction);
        }

        public void drawSurfacePlot(Dictionary<string, double> minValues, Dictionary<string, double> maxValues, List<string> vars)
        {
            var maxX = maxValues[vars[0]];
            var minX = minValues[vars[0]];

            var maxY = maxValues[vars[1]];
            var minY = minValues[vars[1]];

            var xRange = (maxX - minX) / range;
            var yRange = (maxY - minY) / range;

            Dictionary<string, double> values = new Dictionary<string, double>();
            xValues = new List<double>();
            yValues = new List<double>();
            zValues = new List<double>();

            var actualX = maxX+xRange;
            var actualY = maxY+yRange;

            for (int i = 0; i < range; i++)
            {
                actualX -= xRange;

                for (int j=0; j< range; j++)
                {
                    actualY -= yRange;
                    values[vars[0]] = actualX;
                    values[vars[1]] = actualY;
                    xValues.Add(actualX);
                    yValues.Add(actualY);
                    zValues.Add(computeObjectiveFunction(values));
                }
                actualY= maxY + yRange;
            }
        }

        // Initial plot setup, modify this as needed
        private void ilPanel1_Load(object sender, EventArgs e)
        {
            ILArray<double> X = xValues.ToArray();
            ILArray<double> Y = yValues.ToArray();
            ILArray<double> Z = zValues.ToArray();
            X = X.Reshape(2, (range * range) / 2);
            Y = Y.Reshape(2, (range * range) / 2);
            Z = Z.Reshape(2, (range * range) / 2);

            //var asd = new Expression("Math.Pow(x,2)-y*Math.Log10(y)").Evaluate();
            //var a = Convert.ToDouble(asd);

            // setup the plot (modify as needed)
            ilPanel1.Scene.Add(new ILPlotCube(twoDMode: false) {
                    new ILSurface(//Z,X,Y,
      ///////////////////////////////////////////////////// tutaj wzór funkcji
      (x, y) => (float)(-20*Math.Exp(-0.2*Math.Pow((0.5*(Math.Pow(x,2)+Math.Pow(y,2))),0.5))-Math.Exp(0.5*(Math.Cos(2*3.14159265359*x)+Math.Cos(2*3.14159265359*y)))+2.718281828459+20),
      
      //////////////////////////////////////////////////// tutaj zakres zmiennych
       xmin: -5, xmax: 5,
       ymin: -5, ymax: 5,
       colormap: Colormaps.ILNumerics) {
          new ILColorbar()
    }
        } );
            //var p2 = ilPanel1.Scene.Camera.Add(new ILPoints
            //{
            //    Tag = "myPoints2",
            //    Size = 10,
            //    Color = Color.Black
            //});
        }
    }
}
