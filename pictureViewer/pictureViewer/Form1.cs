using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace pictureViewer
{
    public partial class Form1 : Form
    {

        string currentDir = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var fb = new FolderBrowserDialog();
                if(fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    currentDir = fb.SelectedPath; // user chon file anh
                    //Hien thi duong dan 
                    textBoxDicrectory.Text = currentDir;
                    //lay toan bo anh tu Directory
                    var dirInfo = new DirectoryInfo(currentDir); //tao duong dan den directory
                                                                 // lay files anh 
                    var files = dirInfo.GetFiles().Where(c => (c.Extension.Equals(".jpg") || c.Extension.Equals(".png") || c.Extension.Equals(".jpeg") || c.Extension.Equals(".bmp")));
                   foreach(var image in files)
                    {
                        // add anh vao listbox
                        listBoxImages.Items.Add(image.Name);
                    }

                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Không thể mở: " + ex.Message + " " + ex.Source);
            }
        }

        private void listBoxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedImage = listBoxImages.SelectedItems[0].ToString();
                if ( !string.IsNullOrEmpty( selectedImage ) && !string.IsNullOrEmpty(currentDir))
                {
                    var fullPath = Path.Combine( currentDir, selectedImage );
                    pictureBoxImagePreview.Image = Image.FromFile( fullPath );
                }
            }
            catch
            {

            }
        }
    }

}
