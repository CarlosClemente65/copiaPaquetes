using System;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace copiaPaquetes
{
    public partial class frmInicio : Form
    {
        variables variable = new variables();

        public frmInicio()
        {
            InitializeComponent();
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            //Lee el fichero de configuracion para cargar las variables
            variable.CargarConfiguracion();

            //Rellena los textBox con los valores cargados en las variables desde el fichero de configuracion
            lbl_destino.Text = variable.destino;
            ActualizaTextBox();
        }

        public void ActualizaTextBox()
        {
            txtRutaPi.Text = variable.rutaPi;
            txtRutanoPi.Text = variable.rutanoPi;
            txtRutaGestion.Text = variable.rutaGestion;
            txtRutaGasoleos.Text = variable.rutaGasoleos;
            txtDestinoPi.Text = variable.destinoPi;
            txtDestinonoPi.Text = variable.destinonoPi;
            txtDestinoLocal.Text = variable.destinoLocal;
            txtDestinoPasesPi.Text = variable.destinoPasesPi;
            txtDestinoPasesnoPi.Text = variable.destinoPasesnoPi;
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            //Lanza el proceso de copia segun los checkBox marcados en los programas
            foreach (System.Windows.Forms.Control groupBox in tabControl1.TabPages["tabPage1"].Controls)
            {
                if (groupBox is System.Windows.Forms.GroupBox && groupBox.Name == "groupBox1")
                {
                    foreach (System.Windows.Forms.Control checkBoxControl in groupBox.Controls)
                    {
                        if (checkBoxControl is System.Windows.Forms.CheckBox checkBox && checkBox.Checked)
                        {
                            switch (checkBoxControl)
                            {
                                case System.Windows.Forms.CheckBox cbx when cbx == cbx_ipcont08:
                                    string fichero = @"\ipcont08\pcont08z.tgz";
                                    LanzaCopia(fichero);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public void btnGuardarConfiguracion_MouseClick(object sender, MouseEventArgs e)
        {
            //Graba en el fichero de configuracion las variables
            variable.GuardarConfiguracion();
        }


        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar si la pestaña seleccionada es tabPage5
            if (tabControl1.SelectedTab == tabPage5)
            {
                // Ejecutar el método cuando se selecciona tabPage5
                variable.CargarConfiguracion();
                ActualizaTextBox();
            }
        }

        #region control marcas programas
        private void cbx_ipcont08_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbx_ipcont08.Checked)
            {
                variable.ipcont08 = $@"{variable.rutaPi}\ipcont08\pcont08z.tgz";
            }
            else
            {
                variable.ipcont08 = string.Empty;
            }
        }

        private void cbx_ipbasica_CheckStateChanged(object sender, EventArgs e)
        {
            variable.ipbasica = cbx_ipbasica.Checked;
        }

        private void cbx_ipmodelo_CheckStateChanged(object sender, EventArgs e)
        {
            variable.ipmodelo = cbx_ipmodelo.Checked;
        }

        private void cbx_ipintegr_CheckStateChanged(object sender, EventArgs e)
        {
            variable.ipintegr = cbx_ipintegr.Checked;
        }

        private void cbx_dsarchi_CheckStateChanged(object sender, EventArgs e)
        {
            variable.dsarchi = cbx_dsarchi.Checked;
        }

        #endregion


        private void LanzaCopia(string fichero)
        {
            //Nota Esta parte no la tengo probada
            string origenCopia = variable.origen + fichero;
            string destinoCopia = variable.destino + fichero;

            // Configura la información del proceso
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = @"C:\Users\oficina\AppData\Local\Programs\WinSCP\WinSCP.com",
                Arguments = $"/ini=nul /command \"open sftp://centos@172.31.5.149/ -hostkey=\"\"ssh-ed25519 255 ypCFfhJskB3YSCzQzF5iHV0eaWxlBIvMeM5kRl4N46o=\"\" -privatekey=\"\"C:\\Oficina_ds\\Diagram\\Accesos portatil\\conexiones VPN\\Credenciales SSH\\aws_diagram_irlanda.ppk\"\" -rawsettings AgentFwd=1\" \"put {origenCopia} {destinoCopia}\" \"exit\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Crea y comienza el proceso
            using (Process process = new Process { StartInfo = processStartInfo })
            {
                process.OutputDataReceived += (sender, e) =>
                {
                    // Muestra el progreso en un TextBox (suponiendo que tienes un TextBox llamado textBoxOutput)
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        ResultadoCopia(e.Data);
                    }
                };

                // Inicia la redirección de la salida estándar
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.Start();

                // Inicia la recepción de datos de salida de forma asincrónica
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();
            }
        }

        private void ResultadoCopia(string resultado)
        {
            //Este metodo se supone que escribe en el textBox el progreso de la copia, pero hay que probarlo
            // Asegúrate de que este método se ejecute en el hilo de la interfaz de usuario (UI)
            if (InvokeRequired)
            {
                Invoke(new Action<string>(ResultadoCopia), resultado);
            }
            else
            {
                // Mostrar el resultado en el TextBox ProgesoCopia
                txtProgresoCopia.AppendText(resultado + Environment.NewLine);
            }
        }

        #region Mouse Hover
        private void btnRutaPi_MouseHover(object sender, EventArgs e)
        {
            if (txtRutaPi.Enabled == false)
            {
                btnRutaPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar_hover;
            }
        }

        private void btnRutanoPi_MouseHover(object sender, EventArgs e)
        {
            if (txtRutanoPi.Enabled == false)
            {
                btnRutanoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar_hover;
            }
        }

        private void btnRutaGestion_MouseHover(object sender, EventArgs e)
        {
            if (txtRutaGestion.Enabled == false)
            {
                btnRutaGestion.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar_hover;
            }
        }

        private void btnRutaGasoleos_MouseHover(object sender, EventArgs e)
        {
            if (txtRutaGasoleos.Enabled == false)
            {
                btnRutaGasoleos.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar_hover;
            }
        }

        private void btnDestinoPi_MouseHover(object sender, EventArgs e)
        {
            if (txtDestinoPi.Enabled == false)
            {
                btnDestinoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar_hover;
            }
        }

        private void btnDestinonoPi_MouseHover(object sender, EventArgs e)
        {
            if (txtDestinonoPi.Enabled == false)
            {
                btnDestinonoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar_hover;
            }
        }

        private void btnDestinoLocal_MouseHover(object sender, EventArgs e)
        {
            if (txtDestinoLocal.Enabled == false)
            {
                btnDestinoLocal.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar_hover;
            }
        }

        private void btnDestinoPasesPi_MouseHover(object sender, EventArgs e)
        {
            if (txtDestinoPasesPi.Enabled == false)
            {
                btnDestinoPasesPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar_hover;
            }
        }

        private void btnDestinoPasesnoPi_MouseHover(object sender, EventArgs e)
        {
            if (txtDestinoPasesnoPi.Enabled == false)
            {
                btnDestinoPasesnoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar_hover;
            }
        }

        #endregion

        #region Mouse Leave
        private void btnRutaPi_MouseLeave(object sender, EventArgs e)
        {
            if (txtRutaPi.Enabled == false)
            {
                btnRutaPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnRutanoPi_MouseLeave(object sender, EventArgs e)
        {
            if (txtRutanoPi.Enabled == false)
            {
                btnRutanoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnRutaGestion_MouseLeave(object sender, EventArgs e)
        {
            if (txtRutaGestion.Enabled == false)
            {
                btnRutaGestion.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnRutaGasoleos_MouseLeave(object sender, EventArgs e)
        {
            if (txtRutaGasoleos.Enabled == false)
            {
                btnRutaGasoleos.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnDestinoPi_MouseLeave(object sender, EventArgs e)
        {
            if (txtDestinoPi.Enabled == false)
            {
                btnDestinoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnDestinonoPi_MouseLeave(object sender, EventArgs e)
        {
            if (txtDestinonoPi.Enabled == false)
            {
                btnDestinonoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnDestinoLocal_MouseLeave(object sender, EventArgs e)
        {
            if (txtDestinoLocal.Enabled == false)
            {
                btnDestinoLocal.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnDestinoPasesPi_MouseLeave(object sender, EventArgs e)
        {
            if (txtDestinoPasesPi.Enabled == false)
            {
                btnDestinoPasesPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnDestinoPasesnoPi_MouseLeave(object sender, EventArgs e)
        {
            if (txtDestinoPasesnoPi.Enabled == false)
            {
                btnDestinoPasesnoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        #endregion


        #region MouseClick
        private void btnRutaPi_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtRutaPi.Enabled == false)
            {
                txtRutaPi.Enabled = true;
                txtRutaPi.Focus();
                txtRutaPi.SelectionStart = txtRutaPi.TextLength;
                btnRutaPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.guardar;
            }
            else
            {
                variable.rutaPi = txtRutaPi.Text;
                txtRutaPi.Enabled = false;
                btnRutaPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnRutanoPi_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtRutanoPi.Enabled == false)
            {
                txtRutanoPi.Enabled = true;
                txtRutanoPi.Focus();
                txtRutanoPi.SelectionStart = txtRutanoPi.TextLength;
                btnRutanoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.guardar;
            }
            else
            {
                variable.rutanoPi = txtRutanoPi.Text;
                txtRutanoPi.Enabled = false;
                btnRutanoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnRutaGestion_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtRutaGestion.Enabled == false)
            {
                txtRutaGestion.Enabled = true;
                txtRutaGestion.Focus();
                txtRutaGestion.SelectionStart = txtRutaGestion.TextLength;
                btnRutaGestion.BackgroundImage = global::copiaPaquetes.Properties.Resources.guardar;
            }
            else
            {
                variable.rutaGestion = txtRutaGestion.Text;
                txtRutaGestion.Enabled = false;
                btnRutaGestion.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnRutaGasoleos_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtRutaGasoleos.Enabled == false)
            {
                txtRutaGasoleos.Enabled = true;
                txtRutaGasoleos.Focus();
                txtRutaGasoleos.SelectionStart = txtRutaGasoleos.TextLength;
                btnRutaGasoleos.BackgroundImage = global::copiaPaquetes.Properties.Resources.guardar;
            }
            else
            {
                variable.rutaGasoleos = txtRutaPi.Text;
                txtRutaGasoleos.Enabled = false;
                btnRutaGasoleos.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnDestinoPi_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtDestinoPi.Enabled == false)
            {
                txtDestinoPi.Enabled = true;
                txtDestinoPi.Focus();
                txtDestinoPi.SelectionStart = txtDestinoPi.TextLength;
                btnDestinoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.guardar;
            }
            else
            {
                variable.destinoPi = txtDestinoPi.Text;
                txtDestinoPi.Enabled = false;
                btnDestinoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnDestinonoPi_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtDestinonoPi.Enabled == false)
            {
                txtDestinonoPi.Enabled = true;
                txtDestinonoPi.Focus();
                txtDestinonoPi.SelectionStart = txtDestinonoPi.TextLength;
                btnDestinonoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.guardar;
            }
            else
            {
                variable.destinonoPi = txtDestinonoPi.Text;
                txtDestinonoPi.Enabled = false;
                btnDestinonoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnDestinoLocal_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtDestinoLocal.Enabled == false)
            {
                txtDestinoLocal.Enabled = true;
                txtDestinoLocal.Focus();
                txtDestinoLocal.SelectionStart = txtDestinoLocal.TextLength;
                btnDestinoLocal.BackgroundImage = global::copiaPaquetes.Properties.Resources.guardar;
            }
            else
            {
                variable.destinoLocal = txtDestinoLocal.Text;
                txtDestinoLocal.Enabled = false;
                btnDestinoLocal.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnDestinoPasesPi_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtDestinoPasesPi.Enabled == false)
            {
                txtDestinoPasesPi.Enabled = true;
                txtDestinoPasesPi.Focus();
                txtDestinoPasesPi.SelectionStart = txtDestinoPasesPi.TextLength;
                btnDestinoPasesPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.guardar;
            }
            else
            {
                variable.destinoPasesPi = txtDestinoPasesPi.Text;
                txtDestinoPasesPi.Enabled = false;
                btnDestinoPasesPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }

        private void btnDestinoPasesnoPi_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtDestinoPasesnoPi.Enabled == false)
            {
                txtDestinoPasesnoPi.Enabled = true;
                txtDestinoPasesnoPi.Focus();
                txtDestinoPasesnoPi.SelectionStart = txtDestinoPasesnoPi.TextLength;
                btnDestinoPasesnoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.guardar;
            }
            else
            {
                variable.destinoPasesnoPi = txtDestinoPasesnoPi.Text;
                txtDestinoPasesnoPi.Enabled = false;
                btnDestinoPasesnoPi.BackgroundImage = global::copiaPaquetes.Properties.Resources.editar;
            }
        }


        #endregion

        
    }


}
