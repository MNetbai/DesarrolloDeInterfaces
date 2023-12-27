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
    /// Lógica de interacción para ClasePaciente.xaml
    /// </summary>
    public partial class ClasePaciente : Window
    {
        public ClasePaciente()
        {
            InitializeComponent();
        }

        //Para crear y modificar paciente
        private void NuevoPaciente_Click(object sender, RoutedEventArgs e)
        {
            NuevoPaciente nuevoPacienteWindow = new NuevoPaciente();
            nuevoPacienteWindow.Show();
        }

        private void ModificarPaciente_Click(object sender, RoutedEventArgs e)
        {
            ModificarPaciente modificarPacienteWindow = new ModificarPaciente();
            modificarPacienteWindow.Show();
        }
    }
}
