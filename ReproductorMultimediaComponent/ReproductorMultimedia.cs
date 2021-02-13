using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReproductorMultimediaComponent
{
    [
       DefaultProperty("Estado"),
       DefaultEvent("Click")
   ]
    public partial class ReproductorMultimedia : UserControl
    {

        [Category("Design")]
        [Description("Estado del botón que determina el símbolo asociado")]

        private bool estado;
        public bool Estado { 
            set 
            {
                estado = value;
                btnControl.Image = estado ? Properties.Resources.pause : Properties.Resources.play;
            } 
            get 
            {
                return estado;
            } 
        }

        [Category("Design")]
        [Description("Propiedad numérica entre 0 y 99")]
        private int xx;
        public int XX
        {
            set
            {
                if(value > 99)
                {
                    xx = 0;
                }
                else
                {
                    xx = value;
                }
                Refresh();
            }
            get
            {
                return xx;
            }
        }

        [Category("Design")]
        [Description("Propiedad numérica entre 0 y 59")]
        private int yy;
        public int YY
        {
            set
            {
                if(value <= 59)
                {
                    yy = value;
                }
                else
                {
                    yy = value % 60;
                    DesbordaTiempo?.Invoke(this, EventArgs.Empty);
                }
                Refresh();
            }
            get
            {
                return yy;
            }
        }

        [Category("Design")]
        [Description("Texto de label tiempo")]
        public string txtlblTiempo
        {
            get
            {
                return txtlblTiempo;
            }
        }

        [Category("Action")]
        [Description("Se lanza cuando yy supera 59")]
        public event System.EventHandler DesbordaTiempo;

       
        private void btnControl_Click(object sender, EventArgs e)
        {
            Estado = !Estado;
            this.OnClick(e);    
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            lblTiempo.Text = string.Format("{0:00}:{1:00}", XX, YY);

        }

        public ReproductorMultimedia()
        {
            InitializeComponent();
        }

    }
}
