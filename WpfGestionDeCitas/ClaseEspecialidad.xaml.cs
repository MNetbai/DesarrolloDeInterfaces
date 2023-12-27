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
    /// Lógica de interacción para Especialidad.xaml
    /// </summary>
    public partial class ClaseEspecialidad : Window
    {
        public ClaseEspecialidad()
        {
            InitializeComponent();
        }

        private void NuevaEspecialidad_Click(object sender, RoutedEventArgs e)
        {
            NuevaEspecialidad nuevaEspecialidadWindow = new NuevaEspecialidad();
            nuevaEspecialidadWindow.Show();
        }

        private void ModificarEspecialidad_Click(object sender, RoutedEventArgs e)
        {
            ModificarEspecialidad modificarEspecialidadWindow = new ModificarEspecialidad();
            modificarEspecialidadWindow.Show();
        }
    }
}
