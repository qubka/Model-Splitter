using ICSharpCode.SharpZipLib.BZip2;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Model_Splitter
{
	public partial class FormSplitter : Form
	{
		string select = "";
		string target = "";
		bool compress = true;

		public FormSplitter()
		{
			InitializeComponent();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{

		}

		private void buttonSelectFolder_Click(object sender, EventArgs e)
		{
			select = "";

			using (FolderBrowserDialog fbd = new FolderBrowserDialog())
			{
				fbd.RootFolder = Environment.SpecialFolder.Desktop;
				fbd.Description = "Select the folder or model:";

				fbd.ShowNewFolderButton = false;

				if (fbd.ShowDialog() == DialogResult.OK)
				{
					string path = fbd.SelectedPath;

					if (Directory.Exists(path))
					{
						select = path;
						textBoxSelect.Text = path;
						textBoxCount.Text = Directory.GetFiles(path, "*.mdl", SearchOption.AllDirectories).Length.ToString();
					}
				}
			}
		}

		private void buttonSelectFile_Click(object sender, EventArgs e)
		{
			select = "";

			using (OpenFileDialog fbd = new OpenFileDialog())
			{
				fbd.InitialDirectory = "c:\\";
				fbd.Filter = "mdl files (*.mdl)|*.mdl";
				fbd.FilterIndex = 2;
				fbd.RestoreDirectory = true;

				if (fbd.ShowDialog() == DialogResult.OK)
				{
					string path = fbd.FileName;

					if (File.Exists(path) && Path.GetExtension(path) == ".mdl")
					{
						select = path;
						textBoxSelect.Text = path;
						textBoxCount.Text = "1";
					}
					else
					{
						MessageBox.Show("You did not enter a valid path.", "Error Detected in Input");
					}
				}
			}
		}

		private void buttonTarget_Click(object sender, EventArgs e)
		{
			target = "";

			using (FolderBrowserDialog fbd = new FolderBrowserDialog())
			{
				fbd.RootFolder = Environment.SpecialFolder.Desktop;
				fbd.Description = "Select the destination folder:";

				fbd.ShowNewFolderButton = false;

				if (fbd.ShowDialog() == DialogResult.OK)
				{
					string path = fbd.SelectedPath;

					if (path == select)
					{
						MessageBox.Show("You did not enter a valid destination path.", "Error Detected in Input");
					}
					else if (Directory.Exists(path))
					{
						target = path;
						textBoxTarget.Text = path;
					}
				}
			}
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			if (File.Exists(select))
            {
				progressBar.Maximum = 100;
				progressBar.Value = 0;

				ProcessFile(select);

				progressBar.Value = 100;
            }
            else if (Directory.Exists(select))
			{
				string[] list = Directory.GetFiles(select, "*.mdl", SearchOption.AllDirectories);

				progressBar.Maximum = list.Length;
				progressBar.Value = 0;

				foreach (string file in list)
                {
					ProcessFile(file);
					progressBar.Increment(1);
				}
			}
			else
			{
				MessageBox.Show("You did not enter a valid path.", "Error Detected in Input");
				return;
			}

			textBoxTarget.Text = "";
			textBoxSelect.Text = "";
			MessageBox.Show("Successfully finish the process!", "Success");
		}

        private void ProcessFile(string path)
        {
            (string root, ArrayList list) data = Decryptor.ReadFile(path);
            String dest = target + '/' + Path.GetFileNameWithoutExtension((string)data.list[0]);
            int size = data.root.Length; uint counter = 1;

            while (Directory.Exists(dest))
            {
                dest += counter.ToString();
                counter++;
            }

            Directory.CreateDirectory(dest);

            String srv = dest + "/srv-side";
            Directory.CreateDirectory(srv);

			String fdl = dest + "/fdl-side";
			Directory.CreateDirectory(fdl);

			using (StreamWriter file = new StreamWriter(dest + "/paths.txt", false))
            {
                foreach (string full in data.list)
                {
					if (!File.Exists(full))
                    {
						continue;
                    }

                    string cut = full.Substring(size);
                    file.WriteLine(cut.Replace("\\", "/"));

                    dest = srv + cut;
                    Directory.CreateDirectory(Path.GetDirectoryName(dest));
					File.Copy(full, dest, true);

					if (compress)
                    {
						dest = fdl + cut;
						string bzip = dest + ".bz2";
						if (File.Exists(bzip))
                        {
							continue;
						}

						Directory.CreateDirectory(Path.GetDirectoryName(bzip));
						
						using (FileStream stream = new FileStream(full, FileMode.Open, FileAccess.Read))
                        using (FileStream zipped = new FileStream(bzip, FileMode.CreateNew))
                        {
                            try
                            {
                                BZip2.Compress(stream, zipped, true, 9);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void textBoxSelect_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBoxTarget_TextChanged(object sender, EventArgs e)
		{

		}

		private void labelCount_Click(object sender, EventArgs e)
		{

		}

		private void checkBoxCompress_CheckedChanged(object sender, EventArgs e)
		{
			compress = !compress;
		}
	}
}
