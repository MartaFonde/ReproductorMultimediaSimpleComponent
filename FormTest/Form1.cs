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

namespace FormTest
{
    public partial class Form1 : Form
    {
        DirectoryInfo dirInfo;
        FileInfo[] files;
        List<string> pathImagenes;
        int cont;
        string titulo = "Visor de imágenes";

        public Form1()
        {
            InitializeComponent();

            pathImagenes = new List<string>();
            this.Text = titulo;

            reprMultim.XX = 0;
            reprMultim.YY = 0;
            reprMultim.Estado = false;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            DialogResult result = fb.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fb.SelectedPath))
            {                
                this.Text = fb.SelectedPath;
                dirInfo = new DirectoryInfo(fb.SelectedPath);
                files = dirInfo.GetFiles();
                filtrarImagenes();
                cont = 0;
            }
            else
            {
                dirInfo = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (reprMultim.Estado)
            {
                reprMultim.YY++;
                cambiaImagen();
                
            }            
        }

        private void reprMultim_Click(object sender, EventArgs e)
        {
            if (reprMultim.Estado)
            {
                if (!timer1.Enabled)
                {
                    timer1.Enabled = true;
                }

                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }

        private void reprMultim_DesbordaTiempo(object sender, EventArgs e)
        {
            reprMultim.XX++;
        }

        private void cambiaImagen()
        {
            if (dirInfo != null && pathImagenes.Count > 0)
            {
                if (cont >= pathImagenes.Count)
                {
                    cont = 0;                    
                }                
                                   
                verImagen();
                cont++;
                
            }
        }


        private void verImagen()
        {
            try
            {
                pictureBox1.Image = Image.FromFile(pathImagenes[cont]);    
            }
            catch (Exception ex) when (ex is ArgumentException || ex is OutOfMemoryException || ex is FileNotFoundException)
            {
                //Non quero mandar ningunha excepcion
            }
        }

        private void filtrarImagenes()
        {
            pathImagenes.Clear();

            for (int i = 0; i < files.Length; i++)
            {
                try
                {                                        
                    Image.FromFile(files[i].FullName);
                    pathImagenes.Add(files[i].FullName);    //se non salta excep gardamos path
                }
                catch (Exception ex) when (ex is ArgumentException || ex is OutOfMemoryException || ex is FileNotFoundException)
                {
                    //Non quero mandar ningunha excepcion
                }
            }

            if(pathImagenes.Count == 0)
            {
                MessageBox.Show("No se encuentran imágenes en el directorio indicado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Text = titulo;                
            }
           
        }
    }
}
