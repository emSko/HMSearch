using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HarmonySearchAlg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            HideControls();
        }

        private void buttonEnterFunction_Click(object sender, EventArgs e)
        {
            HideControls();
            ObjFunctionParser obj = new ObjFunctionParser(textBoxFunction.Text);
            var variables = obj.getDesignVariables();
            int amountOfVariables = variables.Count();

            Dictionary<string, double> minValues = new Dictionary<string, double>();
            Dictionary<string, double> maxValues = new Dictionary<string, double>();
            minValues.Add("x1", 123);
            maxValues.Add("x1", 3454);
            minValues.Add("x2", 666);
            maxValues.Add("x2", 999);

            Algorithm algorithm = new Algorithm(textBoxFunction.Text, amountOfVariables, minValues, maxValues);

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
        }
    }
}
