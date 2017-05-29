using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HarmonySearchAlg
{
    public partial class Form1 : Form
    {
        int amountOfVariables;
        List<string> variables;
        Algorithm algorithm;

        public Form1()
        {
            InitializeComponent();
            HideControls();

        }

        private void buttonEnterFunction_Click(object sender, EventArgs e)
        {
            HideControls();
            ObjFunctionParser obj = new ObjFunctionParser(textBoxFunction.Text);
            variables = obj.getDesignVariables();
            variables.Sort();
            amountOfVariables = variables.Count();

            for (int i=1; i<=variables.Count; i++)
            {
                ((Label)Form1.ActiveForm.Controls.Find("label" + i, true)[0]).Text = variables[i - 1];
                ((Label)Form1.ActiveForm.Controls.Find("label" + i, true)[0]).Show();
                ((TextBox)Form1.ActiveForm.Controls.Find("textBox" + i, true)[0]).Show();
                ((TextBox)Form1.ActiveForm.Controls.Find("textBox" + i + i, true)[0]).Show();
            }

            buttonMinMax.Show();
            labelMax.Show();
            labelMin.Show();
        }

        private void HideControls()
        {
            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
            label10.Hide();

            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            textBox4.Hide();
            textBox5.Hide();
            textBox6.Hide();
            textBox7.Hide();
            textBox8.Hide();
            textBox9.Hide();
            textBox10.Hide();

            textBox11.Hide();
            textBox22.Hide();
            textBox33.Hide();
            textBox44.Hide();
            textBox55.Hide();
            textBox66.Hide();
            textBox77.Hide();
            textBox88.Hide();
            textBox99.Hide();
            textBox1010.Hide();

            buttonMinMax.Hide();

            labelMax.Hide();
            labelMin.Hide();
            button1.Hide();
        }

        private void buttonMinMax_Click(object sender, EventArgs e)
        {
            Dictionary<string, double> minValues = new Dictionary<string, double>();
            Dictionary<string, double> maxValues = new Dictionary<string, double>();

            for (int i=1; i<=amountOfVariables; i++)
            {
                var minValue = ((TextBox)Form1.ActiveForm.Controls.Find("textBox" + i, true)[0]).Text;
                var maxValue = ((TextBox)Form1.ActiveForm.Controls.Find("textBox" + i + i, true)[0]).Text;
                minValues.Add(variables[i - 1],Convert.ToDouble(minValue));
                maxValues.Add(variables[i - 1], Convert.ToDouble(maxValue));
            }

            algorithm = new Algorithm(textBoxFunction.Text, amountOfVariables, minValues, maxValues);
            algorithm.InitializeHSM();
            if(variables.Count()==2)
            {
                button1.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            algorithm.drawSurfacePlot();
        }
    }
}
