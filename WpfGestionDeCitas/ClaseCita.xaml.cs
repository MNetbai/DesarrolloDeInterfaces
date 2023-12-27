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
    /// Lógica de interacción para Cita.xaml
    /// </summary>
    public partial class ClaseCita : Window
    {
        public ClaseCita()
        {
            InitializeComponent();
        }
        //Abrimos las ventanas correspondientes

        private void NuevaCita_Click(object sender, RoutedEventArgs e)
        {
            NuevaCita nuevaCitaWindow = new NuevaCita();
            nuevaCitaWindow.Show();
        }

        private void ModificarCita_Click(object sender, RoutedEventArgs e)
        {
            ModificarCita modificarCitaWindow = new ModificarCita();
            modificarCitaWindow.Show();
        }

        private void CancelarCita_Click(object sender, RoutedEventArgs e)
        {
            CancelarCita cancelarCitaWindow = new CancelarCita();
            cancelarCitaWindow.Show();
        }
    }
}
