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
    /// Lógica de interacción para ModificarCita.xaml
    /// </summary>
    public partial class ModificarCita : Window
    {
        List<Cita> listadoCitas;
        public ModificarCita()
        {
            InitializeComponent();
        }

        private void btnBuscarCita_Click(object sender, RoutedEventArgs e)
        {
            //obtenemos el texto del TextBox
            string textoBusqueda = txtBuscarPaciente.Text;

            //llamamos al método para obtener las citas
            listadoCitas = ConexionBD.LeerDatosCitasDNI(textoBusqueda);

            //Habilito el CheckBox después de cargar las citas
            cbxAnularCita.IsEnabled = true;

            //Habilito los controles de fecha y hora si se encontró una cita
            bool citaEncontrada = (listadoCitas != null && listadoCitas.Count > 0);
            fechaDatePicker.IsEnabled = citaEncontrada;
            cmbHoraCita.IsEnabled = citaEncontrada;
            lblNuevaFecha.IsEnabled = citaEncontrada;
            lblNuevaHora.IsEnabled = citaEncontrada;

            //Mostramos las citas en el DataGrid
            dataGridModificarCita.ItemsSource = listadoCitas;

            //Si se encontró alguna cita, inicializamos el combo de horas
            if (citaEncontrada)
            {
                //Tomamos la primera cita como referencia
                Cita primeraCitaEncontrada = listadoCitas.First();
                InicializarComboHoras(primeraCitaEncontrada.Fecha, primeraCitaEncontrada.IdMedico, primeraCitaEncontrada.IdEspecialidad, primeraCitaEncontrada.Id);
            }

            //Habilitamos el CheckBox después de cargar los datos
            cbxAnularCita.IsEnabled = true;
        }


        private void cbxAnularCita_Checked(object sender, RoutedEventArgs e)
        {
            //verifico si hay alguna cita seleccionada en el DataGrid
            if (dataGridModificarCita.SelectedItem != null)
            {
                //obtenemos la cita seleccionada
                Cita citaSeleccionada = (Cita)dataGridModificarCita.SelectedItem;

                //intentamos anular la cita
                bool anulacionExitosa = ConexionBD.AnularCita(citaSeleccionada.Id);

                //verificamos si la anulación fue exitosa
                if (anulacionExitosa)
                {
                    //mostramos mensaje de éxito
                    MessageBox.Show("La cita ha sido anulada con éxito");

                    //actualizamos la lista de citas después de la cancelación
                    string textoBusqueda = txtBuscarPaciente.Text;
                    listadoCitas = ConexionBD.LeerDatosCitasDNI(textoBusqueda);

                    //mostramos las citas actualizadas en el DataGrid
                    dataGridModificarCita.ItemsSource = listadoCitas;
                }
                else
                {
                    //mostramos mensaje de error
                    MessageBox.Show("Error al intentar anular la cita");
                }

                //desmarcamos el CheckBox después de anular la cita
                cbxAnularCita.IsChecked = false;
            }
            else
            {
                //Si no hay ninguna cita seleccionada, muestra un mensaje
                MessageBox.Show("Primero seleccione una cita");
                //desmarcammos el CheckBox ya que no se pudo anular ninguna cita
                cbxAnularCita.IsChecked = false;
            }
        }

        private void cbxAnularCita_Loaded(object sender, RoutedEventArgs e)
        {
            //deshabilito el CheckBox al cargar la ventana
            CheckBox checkBox = (CheckBox)sender;
            checkBox.IsEnabled = false;
        }

        private void dataGridModificarCita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //compruebo si hay alguna cita seleccionada
            if (dataGridModificarCita.SelectedItem != null)
            {
                //habilito controles para modificar cita
                HabilitarControlesModificarCita();
            }
        }

        private void HabilitarControlesModificarCita()
        {
            //Habilitamos controles para modificar cita
            fechaDatePicker.IsEnabled = true;
            lblNuevaFecha.IsEnabled = true;
            lblNuevaHora.IsEnabled = true;

            //Obtenemos la cita seleccionada
            Cita citaSeleccionada = (Cita)dataGridModificarCita.SelectedItem;

            //Obtenemos las horas ocupadas para la misma fecha, médico, etc.
            List<string> horasOcupadas = ConexionBD.LeerHorasOcupadasPorFechaMedico(
                citaSeleccionada.Fecha, citaSeleccionada.IdMedico);

            //Definimos el rango de horas disponibles
            DateTime start = DateTime.Parse("9:00 AM");
            DateTime end = DateTime.Parse("15:00 PM");

            //Limpiamos y volvemos a llenar el ComboBox con el rango de horas disponibles excluyendo las ocupadas
            cmbHoraCita.Items.Clear();
            while (start <= end)
            {
                string horaActual = start.ToString("h:mm tt");

                //se agrega la hora si no está en la lista de horas ocupadas
                if (!horasOcupadas.Contains(horaActual))
                {
                    cmbHoraCita.Items.Add(horaActual);
                }
                start = start.AddMinutes(15);
            }
        }

        private void cmbHoraCita_Loaded(object sender, RoutedEventArgs e)
        {
            //deshabilito el ComboBox al cargar la ventana
            ComboBox comboBox = (ComboBox)sender;
            comboBox.IsEnabled = false;
        }

        private void btnModificarCita_Click(object sender, RoutedEventArgs e)
        {
            //verifico si hay una cita seleccionada
            if (dataGridModificarCita.SelectedItem != null)
            {
                //obtengo la cita seleccionada
                Cita citaSeleccionada = (Cita)dataGridModificarCita.SelectedItem;

                //obtengo la nueva fecha y hora
                DateTime nuevaFecha = citaSeleccionada.Fecha; //Usar la fecha de la cita seleccionada
                string nuevaHora = cmbHoraCita.SelectedValue?.ToString() ?? string.Empty;

                //modifico la cita
                ModificandoCita(citaSeleccionada, nuevaFecha, nuevaHora);
            }
        }

        private void ModificandoCita(Cita cita, DateTime nuevaFecha, string nuevaHora)
        {
            //modificar la cita en la base de datos
            bool modificacionExitosa = ConexionBD.ModificarCita(cita.Id, nuevaFecha, nuevaHora);

            //verifica si la modificación fue exitosa
            if (modificacionExitosa)
            {
                //mensaje de éxito
                MessageBox.Show("La cita ha sido modificada con éxito");

                //Actualizo el DataGrid con las citas actualizadas
                CargarCitas();
            }
            else
            {
                //mensaje de error
                MessageBox.Show("Error al intentar modificar la cita");
            }
        }

        private void CargarCitas()
        {
            //obtener las citas actualizadas
            listadoCitas = ConexionBD.LeerDatosCitasDNI(txtBuscarPaciente.Text);

            //mostrar las citas en el DataGrid
            dataGridModificarCita.ItemsSource = listadoCitas;
        }

        private void InicializarComboHoras(DateTime fecha, int idMedico, int idEspecialidad, int idCita)
        {
            cmbHoraCita.Items.Clear(); //Limpia las horas anteriores

            //definimos el rango de horas disponibles
            DateTime start = DateTime.Parse("9:00 AM");
            DateTime end = DateTime.Parse("15:00 PM");

            //se obtienen las horas ocupadas para la fecha, médico, especialidad y cita específica
            List<string> horasOcupadas = ConexionBD.LeerHorasOcupadasPorFechaMedico(fecha, idMedico);

            //se llena el ComboBox con el rango de horas que no están ocupadas
            while (start <= end)
            {
                string hora = start.ToString("h:mm tt");
                if (!horasOcupadas.Contains(hora))
                {
                    cmbHoraCita.Items.Add(hora);
                }
                start = start.AddMinutes(15);
            }
        }



        private void cmbHoraCita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbHoraCita.SelectedItem != null)
            {
                //obtengo la hora seleccionada
                string horaSeleccionada = cmbHoraCita.SelectedItem.ToString();
            }
        }
    }
}
