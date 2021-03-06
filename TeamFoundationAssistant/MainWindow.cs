﻿using GanttProjectDotNet.IO;
using GanttProjectDotNet.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamFoundationAssistant.Forms;

namespace TeamFoundationAssistant
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void testGanttProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            GanttProject project = new GanttProject();

            var xmlsXaver = new GanttXMLSaver(null, null, null, null,null);
            xmlsXaver.Save(null);
        }

        private void btnDeploy_Click(object sender, EventArgs e)
        {
            DeployForm deployForm = new DeployForm();
            deployForm.Show();
        }
    }
}
