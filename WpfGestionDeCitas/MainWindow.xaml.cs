using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGestionDeCitas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Cita_Click(object sender, RoutedEventArgs e)
        {
            //Abrimos la ventana de Cita
            ClaseCita claseCitaWindow = new ClaseCita();
            claseCitaWindow.Show();
            
        }

        private void Paciente_Click(object sender, RoutedEventArgs e)
        {
            //Abrimos la ventana de Paciente
            ClasePaciente clasePacienteWindow = new ClasePaciente();
            clasePacienteWindow.Show();
        }

        private void Especialidad_Click(object sender, RoutedEventArgs e)
        {
            //Abrimos la ventana de Especialidad
            ClaseEspecialidad claseEspecialidadWindow = new ClaseEspecialidad();
            claseEspecialidadWindow.Show();

        }
    }
}
