using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LkDicomViewTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lkDicomViewer1.LoadDicomDirectory("D:\\Patients\\LKDS-0001");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lkDicomViewer1.SetWindowLevel(-500, 1500);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lkDicomViewer1.SetWindowLevel(40, 400);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                lkDicomViewer1.ImageSpace.CurrentAnnType = LkDicomView.AnnObjects.Enums.AnnObjectType.Ruler;
            }
        }
    }
}
