using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Lógica de interacción para ModificarPaciente.xaml
    /// </summary>
    public partial class ModificarPaciente : Window
    {
        public ModificarPaciente()
        {
            InitializeComponent();
        }

        //modificamos paciente con solo las opciones que nos pide
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            txtTelefono.IsEnabled = true;
            txtIdCompania.IsEnabled = true;
            txtDireccion.IsEnabled = true;
            txtTelefono.IsEnabled = false;
            txtDni.IsEnabled = false;
            btnModificarPaciente.IsEnabled = true;
            List<Paciente>? paciente = new List<Paciente>();
            paciente = ConexionBD.LeerDatosPaciente();
            for (int i = 0; i < paciente.Count; i++)
            {
                if (paciente[i].Apellidos == txtApellidos.Text || paciente[i].Dni == txtDni.Text)
                {
                    txtNombre.Text = paciente[i].Nombre;
                    txtApellidos.Text = paciente[i].Apellidos;
                    txtDireccion.Text = paciente[i].Direccion;
                    txtDni.Text = paciente[i].Dni;
                    txtTelefono.Text = paciente[i].Telefono;
                    txtIdCompania.Text = Convert.ToString(paciente[i].IdCompania);
                    txtEmail.Text = paciente[i].Email;
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow? mainWindow = new MainWindow();
            if (txtApellidos != null && txtDni != null)
            {
                if (ConexionBD.ModificarPaciente(txtNombre.Text, txtApellidos.Text, txtDireccion.Text, txtDni.Text, txtTelefono.Text, Convert.ToInt32(txtIdCompania.Text), txtEmail.Text))
                {
                    MessageBox.Show("Paciente modificado correctamente");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No ha sido posible modificar el paciente");
                    this.Close();
                }
            }

        }
    }
}
