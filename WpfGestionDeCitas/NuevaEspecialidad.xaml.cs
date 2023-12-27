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
    /// Lógica de interacción para NuevaEspecialidad.xaml
    /// </summary>
    public partial class NuevaEspecialidad : Window
    {
        public NuevaEspecialidad()
        {
            InitializeComponent();
        }

        private void btnAltaEspecialidad_Click(object sender, RoutedEventArgs e)
        {
            if (ConexionBD.GuardarEspecialidad(txtNombre.Text, txtDescripcion.Text))
            {
                MessageBox.Show("Especialidad dada de alta con éxito");
            }
            else
            {
                MessageBox.Show("Especialidad no registrada");
            }
            this.Close();
        }
    }
}
