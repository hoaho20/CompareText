using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using DiffPlex.WindowsForms.Controls;

namespace CompareText
{
    public partial class Form1 : Form
    {
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private Button btnSelectFile1;
        private Button btnSelectFile2;
        private Button btnCompare;
        private string file1Path = "";
        private string file2Path = "";

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Create and configure the first file selection button
            btnSelectFile1 = new Button
            {
                Text = "Select File 1",
                Location = new Point(10, 10),
                Size = new Size(100, 30)
            };
            btnSelectFile1.Click += BtnSelectFile1_Click;
            this.Controls.Add(btnSelectFile1);

            // Create and configure the second file selection button
            btnSelectFile2 = new Button
            {
                Text = "Select File 2",
                Location = new Point(120, 10),
                Size = new Size(100, 30)
            };
            btnSelectFile2.Click += BtnSelectFile2_Click;
            this.Controls.Add(btnSelectFile2);

            // Create and configure the compare button
            btnCompare = new Button
            {
                Text = "Compare",
                Location = new Point(230, 10),
                Size = new Size(100, 30)
            };
            btnCompare.Click += BtnCompare_Click;
            this.Controls.Add(btnCompare);

            // Create and configure the first RichTextBox
            richTextBox1 = new RichTextBox
            {
                Location = new Point(10, 50),
                Size = new Size(400, 400),
                Font = new Font("Consolas", 10),
                WordWrap = false
            };
            this.Controls.Add(richTextBox1);

            // Create and configure the second RichTextBox
            richTextBox2 = new RichTextBox
            {
                Location = new Point(420, 50),
                Size = new Size(400, 400),
                Font = new Font("Consolas", 10),
                WordWrap = false,
            };
            this.Controls.Add(richTextBox2);

            // Set form properties
            this.Text = "Code Comparison Tool";
            this.Size = new Size(850, 500);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void BtnSelectFile1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    file1Path = openFileDialog.FileName;
                    richTextBox1.Text = File.ReadAllText(file1Path);
                }
            }
        }

        private void BtnSelectFile2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    file2Path = openFileDialog.FileName;
                    richTextBox2.Text = File.ReadAllText(file2Path);
                }
            }
        }

        private void BtnCompare_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text) || string.IsNullOrEmpty(richTextBox2.Text))
            {
                MessageBox.Show("Please load or enter text in both boxes first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a new form for the diff view
            Form diffForm = new Form
            {
                Text = "Code Comparison",
                Size = new Size(1000, 700),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.Sizable,
                MinimizeBox = true,
                MaximizeBox = true
            };

            // Create and configure the diff viewer
            var diffView = new DiffViewer
            {
                Margin = new Padding(0),
                Dock = DockStyle.Fill,
                OldText = richTextBox1.Text,
                NewText = richTextBox2.Text
            };

            // Add the diff viewer to the form
            diffForm.Controls.Add(diffView);

            // Show the form as a dialog
            diffForm.ShowDialog(this);
        }
    }
}
