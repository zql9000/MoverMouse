using System;
using System.Drawing;
using System.Windows.Forms;

namespace MoverMouse
{
    public partial class Form1 : Form
    {
        private enum eAcciones
        {
            Detener = 0,
            Iniciar = 1
        }
        eAcciones Accion;
        private enum eSentidos
        {
            Derecha = 0,
            Izquierda = 1
        }
        eSentidos Sentido;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(250);
                this.Hide();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = (int)txtFrecuencia.Value * 1000;
            Accion = eAcciones.Detener;
            btnAccion.Text = eAcciones.Detener.ToString();
            notifyIcon1.BalloonTipText = string.Format("Se moverá el mouse cada {0} segundos.", txtFrecuencia.Value);
            this.WindowState = FormWindowState.Minimized;
            timer1.Start();
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            if (Accion == eAcciones.Detener)
            {
                Accion = eAcciones.Iniciar;
                btnAccion.Text = eAcciones.Iniciar.ToString();
                notifyIcon1.BalloonTipText = "El mouse no se moverá porque se encuentra detenido.";
                timer1.Stop();
            }
            else
            {
                Accion = eAcciones.Detener;
                btnAccion.Text = eAcciones.Detener.ToString();
                notifyIcon1.BalloonTipText = string.Format("Se moverá el mouse cada {0} segundos.", timer1.Interval / 1000);
                timer1.Start();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
        }

        private void txtFrecuencia_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)txtFrecuencia.Value * 1000;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Sentido == eSentidos.Derecha)
            {
                Point actual = Cursor.Position;
                actual.Offset(10, 0);
                Cursor.Position = actual;

                if (actual.X >= Screen.PrimaryScreen.WorkingArea.Right)
                {
                    Sentido = eSentidos.Izquierda;
                }
            }
            else
            {
                Point actual = Cursor.Position;
                actual.Offset(-10, 0);
                Cursor.Position = actual;

                if (actual.X <= Screen.PrimaryScreen.WorkingArea.Left)
                {
                    Sentido = eSentidos.Derecha;
                }
            }
        }
    }
}
