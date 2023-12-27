using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfGestionDeCitas
{
    /// <summary>
    /// Lógica de interacción para NuevoPaciente.xaml
    /// </summary>
    public partial class NuevoPaciente : Window
    {
        //notificar a NuevaCita que se ha creado un nuevo paciente
        public event EventHandler PacienteCreado;

        public NuevoPaciente()
        {
            InitializeComponent();
        }

        private void btnAltaPaciente_Click(object sender, RoutedEventArgs e)
        {
            if (ConexionBD.GuardarPaciente(txtNombre.Text, txtApellidos.Text, txtDireccion.Text, txtDni.Text, txtTelefono.Text, (Int32)int.Parse(txtIdCompania.Text), txtEmail.Text))
            {
                MessageBox.Show("Paciente dado de alta con éxito");

                //invocamos el evento PacienteCreado para notificar a NuevaCita
                OnPacienteCreado();
            }
            else
            {
                MessageBox.Show("Paciente no registrado");
            }
            this.Close();
        }

        //para invocar el evento PacienteCreado
        protected virtual void OnPacienteCreado()
        {
            PacienteCreado?.Invoke(this, EventArgs.Empty);
        }
    }
}
