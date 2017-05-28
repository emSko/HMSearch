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

        public Plotting_Form1()
        {
            InitializeComponent();
        }

        // Initial plot setup, modify this as needed
        private void ilPanel1_Load(object sender, EventArgs e)
        {
            ILArray<float> X = new float[] { 0, 0, 1, 1, 2.5F, -2.5F, 5, 9, 1, 38 };
            ILArray<float> Y = new float[] { 1, 0, 1, 0, 1.5F, 0.5F, 5, 9, 1, 39 };
            ILArray<float> Z = new float[] { 0, 0, 1, 1, 0.4F, -0.2F, 5, 9, 1, 39 };
            X = X.Reshape(2, 5);
            Y = Y.Reshape(2, 5);
            Z = Z.Reshape(2, 5);
            // setup the plot (modify as needed)
            ilPanel1.Scene.Add(new ILPlotCube(twoDMode: false) {
                    new ILSurface(Z,

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
