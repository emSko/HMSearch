using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using NCalc;

namespace HarmonySearchAlg
{
    public partial class Plotting_Form1 : Form
    {

        ObjFunctionParser functionParser;
        List<double> xValues;
        List<double> yValues;
        List<double> zValues;

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

            var xRange = (maxX - minX) / 100;
            var yRange = (maxY - minY) / 100;

            Dictionary<string, double> values = new Dictionary<string, double>();
            xValues = new List<double>();
            yValues = new List<double>();
            zValues = new List<double>();

            var actualX = maxX;
            var actualY = maxY;

            xValues.Add(actualX);
            yValues.Add(actualY);
            values.Add(vars[0], actualX);
            values.Add(vars[1], actualY);
            zValues.Add(computeObjectiveFunction(values));

            for (int i = 1; i < 100; i++)
            {
                actualX -= xRange;
                actualY -= xRange;
                xValues.Add(actualX);
                yValues.Add(actualY);
                values[vars[0]] = actualX;
                values[vars[1]] = actualY;
                zValues.Add(computeObjectiveFunction(values));
            }
        }

        // Initial plot setup, modify this as needed
        private void ilPanel1_Load(object sender, EventArgs e)
        {
            ILArray<float> X = new float[] { 0, 0, 1, 2, 3, 4, 5, 9, 1, 38 };
            ILArray<float> Y = new float[] { 1, 0, 1, 5, 3, 4, 15, 9, 1, 39 };
            ILArray<float> Z = new float[] { 10, 0, 1, 2, 3, 4, 5, 9, 1, 39 };
            X = X.Reshape(2, 5);
            Y = Y.Reshape(2, 5);
            Z = Z.Reshape(2, 5);
            // setup the plot (modify as needed)
            ilPanel1.Scene.Add(new ILPlotCube(twoDMode: false) {
                    new ILSurface(Z,X,Y,

      ///////////////////////////////////////////////////// tutaj wzór funkcji
      //(x, y) => (float)(Math.Pow(x,2)-y*Math.Log10(y)),

      //////////////////////////////////////////////////// tutaj zakres zmiennych
      // xmin: -2, xmax: 1,
      // ymin: -1, ymax: 3,
       colormap: Colormaps.ILNumerics) {
          new ILColorbar()
    }
        });

        }
    }
}
