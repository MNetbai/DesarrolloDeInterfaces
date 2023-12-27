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
    /// Lógica de interacción para CancelarCita.xaml
    /// </summary>
    public partial class CancelarCita : Window
    {
        List<Cita> listadoCitas;

        public CancelarCita()
        {
            InitializeComponent();
        }

        private void btnBuscarCita_Click(object sender, RoutedEventArgs e)
        {
            //obtenemos el texto del TextBox
            string textoBusqueda = txtBuscar.Text;

            //llamamos al método para obtener las citas
            listadoCitas = ConexionBD.LeerDatosCitasDNI(textoBusqueda);

            //mostramos las citas en el DataGrid
            dataGridCancelarCita.ItemsSource = listadoCitas;
        }

        private void btnCancelarCita_Click(object sender, RoutedEventArgs e)
        {
            //verifico si hay alguna cita seleccionada en el DataGrid
            if (dataGridCancelarCita.SelectedItem != null)
            {
                //obtenemos la cita seleccionada
                Cita citaSeleccionada = (Cita)dataGridCancelarCita.SelectedItem;

                //intentamos anular la cita
                bool anulacionExitosa = ConexionBD.AnularCita(citaSeleccionada.Id);

                //verificamos si la anulación fue exitosa
                if (anulacionExitosa)
                {
                    //mostramos mensaje de éxito
                    MessageBox.Show("La cita ha sido anulada con éxito");

                    //actualizamos la lista de citas después de la cancelación
                    string textoBusqueda = txtBuscar.Text;
                    listadoCitas = ConexionBD.LeerDatosCitasDNI(textoBusqueda);

                    //mostramos las citas actualizadas en el DataGrid
                    dataGridCancelarCita.ItemsSource = listadoCitas;
                }
                else
                {
                    //mostramos mensaje de error
                    MessageBox.Show("Error al intentar anular la cita");
                }
            }
            else
            {
                //Si no hay ninguna cita seleccionada, muestra un mensaje
                MessageBox.Show("Primero seleccione una cita");
            }
        }

        private void dataGridCancelarCita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
        }

        
    }
}
