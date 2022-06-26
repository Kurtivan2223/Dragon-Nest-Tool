using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace PathCreate
{
	public class Form1 : Form
	{
		private string FilePatch = "";

		private IContainer components;

		private Label label1;

		private Panel panel1;

		private Label md5label;

		private Label label4;

		private Label label5;

		private Label FileCountLabel;

		private Label label7;

		private Label label6;

		private Label FileNameLabel;
        private Label label3;
        private Label label8;

		public Form1()
		{
			InitializeComponent();
			panel1.AllowDrop = true;
			panel1.DragEnter += panel1_DragEnter;
			panel1.DragDrop += panel1_DragDrop;
            md5label.Text = "NULL";
			FileCountLabel.Text = "0";
            FileNameLabel.Text = "NULL";
		}

		protected string GetMD5HashFromFile(string fileName)
		{
			using MD5 mD = MD5.Create();
			using FileStream inputStream = File.OpenRead(fileName);
			return BitConverter.ToString(mD.ComputeHash(inputStream)).Replace("-", string.Empty);
		}

		public void fileSave(string fileName, string text)
		{
			FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate);
			StreamWriter streamWriter = new StreamWriter(fileStream);
			fileStream.Seek(0L, SeekOrigin.End);
			streamWriter.WriteLine(text);
			streamWriter.Close();
		}

		private void panel1_DragDrop(object sender, DragEventArgs e)
		{
			string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
			FilePatch = "";
			for (int i = 0; i < array.Length; i++)
			{
				FilePatch += array[i];
				if (string.Equals(Path.GetExtension(array[i]), ".pak", StringComparison.InvariantCultureIgnoreCase))
				{
					CreatePatch(FilePatch);
				}
			}
		}

		private void CreatePatch(string PatchInput)
		{
			string text = GetMD5HashFromFile(PatchInput).ToLower();
			string fileName = Path.GetFileName(PatchInput);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(PatchInput);
			string text2 = Path.GetDirectoryName(PatchInput) + "\\" + fileNameWithoutExtension;

			md5label.Text = text;
			FileNameLabel.Text = fileName;

			using FileStream fileStream = File.OpenRead(PatchInput);
			HEADER hEADER = fileStream.ReadStruct<HEADER>();
			FileCountLabel.Text = Convert.ToString(hEADER.FileCounts);

			if (File.Exists(text2 + ".pak.md5"))
			{
				File.Delete(text2 + ".pak.md5");
			}

			fileSave(text2 + ".pak.md5", text);

			if (File.Exists(text2 + ".txt"))
			{
				File.Delete(text2 + ".txt");
			}

			fileStream.Position = hEADER.TableOffset;

			for (int i = 0; i <= hEADER.FileCounts; i++)
			{
				string text3 = ByteToString(fileStream.ReadStruct<FILELIST>().NameFile);

                /*
                if the file inside pak endswith extension from array
                D means delete from both physical and .pak directory
                C means overwrite in client

                Base on Sea Launcher

                */

                

                /*if (text3.EndsWith(".exe") || text3.EndsWith(".dll") || text3.EndsWith(".xem"))
                    fileSave(text2 + ".txt", "C " + text3);
                else
                    fileSave(text2 + ".txt", "D " + text3);
                */

                //You can remove this and write a blank txt file if you don't want to delete something
                if (text3.EndsWith(".exe") || text3.EndsWith(".dll") || text3.EndsWith(".xem"))
                    fileSave(text2 + ".txt", "C " + text3);
            }
		}

		public string ByteToString(byte[] bfiles)
		{
			_ = string.Empty;
			return Encoding.ASCII.GetString(bfiles).Trim(default(char)).Split(default(char))
				.First()
				.Remove(0, 1);
		}

		private void panel1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop) && (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.FileCountLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.md5label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "md5";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.FileNameLabel);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.FileCountLabel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.md5label);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 120);
            this.panel1.TabIndex = 4;
            this.panel1.DragDrop += new System.Windows.Forms.DragEventHandler(this.panel1_DragDrop);
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(292, 26);
            this.label8.TabIndex = 12;
            this.label8.Text = "Drag .pak File here to Generate Text File patch list and Md5.\r\nGenerating both fi" +
    "les varries on how big the patch.\r\n";
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Location = new System.Drawing.Point(77, 56);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(0, 13);
            this.FileNameLabel.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(62, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = ":";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "File Name";
            // 
            // FileCountLabel
            // 
            this.FileCountLabel.AutoSize = true;
            this.FileCountLabel.Location = new System.Drawing.Point(77, 33);
            this.FileCountLabel.Name = "FileCountLabel";
            this.FileCountLabel.Size = new System.Drawing.Size(0, 13);
            this.FileCountLabel.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = ":";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "File Count";
            // 
            // md5label
            // 
            this.md5label.AutoSize = true;
            this.md5label.Location = new System.Drawing.Point(77, 12);
            this.md5label.Name = "md5label";
            this.md5label.Size = new System.Drawing.Size(0, 13);
            this.md5label.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = ":";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 121);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patch TxT&MD5 Generator";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
	}
}
