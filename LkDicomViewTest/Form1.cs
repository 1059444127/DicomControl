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
            lkDicomViewer1.ImageSpace.CurrentAnnType = LkDicomView.AnnObjects.Enums.AnnObjectType.None;
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

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            lkDicomViewer1.ImageSpace.CurrentAnnType = LkDicomView.AnnObjects.Enums.AnnObjectType.Eraser;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            lkDicomViewer1.ImageSpace.CurrentAnnType = LkDicomView.AnnObjects.Enums.AnnObjectType.Rectangle;

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            lkDicomViewer1.ImageSpace.CurrentAnnType = LkDicomView.AnnObjects.Enums.AnnObjectType.Ellipse;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            lkDicomViewer1.ImageSpace.AnnObjectContainer.SaveAnnObjects("D:\\2.json");
            lkDicomViewer1.SaveImage(true, "D:\\1.jpg");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lkDicomViewer1.ImageSpace.AnnObjectContainer.LoadAnnObjects("D:\\2.json");
        }
    }
}
