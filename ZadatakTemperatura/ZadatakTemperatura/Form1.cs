using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security;

namespace ZadatakTemperatura
{
    public partial class Form1 : Form
    {
        private String[] temperatures;

        public Form1()
        {
            InitializeComponent();
            button1.Click += new EventHandler(SelectButton_Click);
        }

        //open file dialog
        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var sr = new StreamReader(openFileDialog1.FileName);
                    String text = sr.ReadToEnd();
                    this.temperatures = text.Split(',');
                    foreach (String temperature in temperatures)
                    {
                        listView1.Items.Add(temperature);
                    }
                    label1.Text = "Datoteka " + openFileDialog1.FileName;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show("Security error.\n\nError message: {ex.Message}\n\n" +
                    "Details:\n\n{ex.StackTrace}");
                }
            }
        }

        //najtopliji dan
        private void button3_Click(object sender, EventArgs e)
        {
            int indexOfHottestDay = 0;
            double degreesOfHottestDay = Convert.ToDouble(this.temperatures[0]);

            for (int i = 1; i < this.temperatures.Length; i++)
            {
                double degrees = Convert.ToDouble(this.temperatures[i]);
                if (degrees > degreesOfHottestDay)
                {
                    indexOfHottestDay = i;
                    degreesOfHottestDay = degrees;
                }
            }

            ++indexOfHottestDay; //zbog korisnika, da ne krece od nule vec od 1

            label3.Text = "Najtopliji je " + indexOfHottestDay + ". dan sa " + degreesOfHottestDay + " stepeni";
        }

        //prosecna temperature
        private void button2_Click(object sender, EventArgs e)
        {
            double sumOfTemperatures = 0;
            int countOfTemperatures = this.temperatures.Length;
            double averageTemperature = 0.0;

            foreach (String temperature in temperatures)
            {
                sumOfTemperatures += Convert.ToDouble(temperature);
            }

            averageTemperature = (double) sumOfTemperatures / countOfTemperatures;
            label2.Text = "Prosecno je " + averageTemperature + " stepena";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        
    }
}
