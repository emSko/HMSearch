using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

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
            // setup the plot (modify as needed)
            ilPanel1.Scene.Add(new ILPlotCube(twoDMode: false) {
                    new ILSurface(

      /////////////////////////////////////////////////// tutaj wzór funkcji
      (x, y) => (float)(Math.Pow(x,2)-y*Math.Log10(y)),

      ////////////////////////////////////////////////// tutaj zakres zmiennych
                  xmin: -2, xmax: 10,
            ymin: -1, ymax: 3,
       colormap: Colormaps.ILNumerics) {
          new ILColorbar()
    }
        });

        }
    }
}
