using System;
using System.Collections.Generic;
using System.Globalization;
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
using Google.Protobuf;
using MySql.Data.MySqlClient;

namespace WpfGestionDeCitas
{
    public partial class NuevaCita : Window
    {
        MySqlConnection conexionBD;
        List<Especialidad> especialidad;
        List<Paciente> paciente;
        List<Medico> medico;
        List<string> horaCita = new List<string>();

        int idEspecialidad = 0;
        int idPaciente = 0;
        int idMedico = 0;
        string diaCitaElegida = "";
        string horaCitaElegida = "";
        
        public NuevaCita()
        {
            InitializeComponent();
            //muestro en el DataGrid los datos obtenidos de la tabla especialidad y paciente
            especialidad = ConexionBD.LeerDatosEspecialidad();
            paciente = ConexionBD.LeerDatosPaciente();
            for (int i = 0; i < especialidad.Count; i++)
            {
                if (especialidad[i].Baja == 0)
                    cmbEspecialidad.Items.Add(especialidad[i].Nombre.ToString());
            }
        }

        //Cojo la lista de especialidades
        private void cmbEspecialidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Para cuando seleccione una especialidad en concreto que solo muestre el médico
            //asociado a esa especialidad 
            if (cmbEspecialidad.SelectedIndex > -1)
            {
                cmbMedico.Items.Clear();
                cmbMedico.Items.Refresh();
                medico = ConexionBD.LeerDatosMedicoEspecialidadBaja(cmbEspecialidad.SelectedIndex + 1);
                for (int i = 0; i < medico.Count; i++)
                {
                    if (medico[i].Baja == 0)
                        cmbMedico.Items.Add(medico[i].Nombre.ToString());
                }
                idEspecialidad = especialidad[cmbEspecialidad.SelectedIndex].Id;
            }
        }

        //Cojo la lista de médicos
        private void cmbMedico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMedico.SelectedIndex > -1)
            {
                if (cmbMedico.Items.Count > 0)
                {
                    for (int i = 0; i < medico.Count; i++)
                    {
                        if (medico[i].Nombre.Equals(cmbMedico.SelectedItem.ToString()))
                        {
                            idMedico = (int)medico[i].Id;
                        }
                    }
                }
            }
        }

        //Cojo las horas
        private void cmbHoraCita_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbHoraCita.SelectedIndex > -1)
            {
                horaCitaElegida = (string)cmbHoraCita.Items[cmbHoraCita.SelectedIndex];
            }
        }

        //creo la cita para paciente existente o nuevo
        public bool crearCitaPaciente()
        {
            conexionBD = ConexionBD.GetConexion();
            Cita citaPaciente = new Cita();
            citaPaciente.Id = 0;
            citaPaciente.IdEspecialidad = idEspecialidad;
            citaPaciente.IdPaciente = idPaciente;
            citaPaciente.IdMedico = idMedico;
            citaPaciente.Fecha = DateTime.ParseExact(diaCitaElegida, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            citaPaciente.Hora = horaCitaElegida;
            citaPaciente.Anulada = 0;

            //si ya hay una cita para la misma hora, médico y especialidad
            if (ConexionBD.ExisteCitaParaFechaHoraMedicoEspecialidad(
                DateTime.ParseExact(diaCitaElegida, "yyyy/MM/dd", CultureInfo.InvariantCulture),
                horaCitaElegida, idMedico, idEspecialidad))
            {
                MessageBox.Show("Ya existe una cita para esa hora, médico y especialidad. Por favor, elige otra hora");
                return false; //No crea la cita y devuelve false
            }

            ConexionBD.GuardarCita(0, idEspecialidad, idPaciente, idMedico, DateTime.ParseExact(diaCitaElegida, "yyyy/MM/dd", CultureInfo.InvariantCulture), horaCitaElegida, 0);

            return true; //La cita se creó correctamente
        }

        //Creo la cita
        private void btnCrearCita_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDniApellidos.Text))
                MessageBox.Show("Debes introducir dni o apellidos para continuar");
            else if (cmbEspecialidad.SelectedIndex == -1)
                MessageBox.Show("Tienes que escoger una especialidad para continuar");
            else if (cmbMedico.SelectedIndex == -1)
                MessageBox.Show("Elige el médico para continuar");
            else if (string.IsNullOrEmpty(fechaDatePicker.Text))
                MessageBox.Show("Selecciona la fecha para continuar");
            else if (cmbHoraCita.SelectedIndex == -1)
                MessageBox.Show("Selecciona la hora para continuar");
            else
            {
                try
                {
                    //Se intenta crear la cita y se verifica si se creó correctamente
                    if (crearCitaPaciente())
                    {
                        //Si la cita se creó correctamente, muestra el mensaje de éxito
                        MessageBox.Show("Cita creada con éxito");

                        //Limpia los controles y variables
                        cmbHoraCita.SelectedIndex = -1;
                        cmbHoraCita.ItemsSource = null;
                        cmbHoraCita.Items.Clear();
                        cmbHoraCita.Items.Refresh();
                        cmbHoraCita.Items.Clear();
                        fechaDatePicker.Text = "";
                        txtDniApellidos.Text = string.Empty;
                        cmbMedico.SelectedIndex = -1;
                        cmbEspecialidad.SelectedIndex = -1;
                    }
                    //Si no se creó la cita (porque ya existe), no hace nada más
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al crear la cita: " + ex.Message);
                }
            }
        }


        //Creo la cita con la fecha elegida
        private void fechaDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime start = DateTime.Parse("9:00 AM");
            DateTime end = DateTime.Parse("15:00 PM");

            if (fechaDatePicker.SelectedDate.HasValue && cmbEspecialidad.SelectedIndex > -1 && cmbMedico.SelectedIndex > -1)
            {
                diaCitaElegida = fechaDatePicker.SelectedDate.Value.ToString("yyyy/MM/dd");

                //Obtener citas ocupadas para la fecha, especialidad y médico seleccionados
                List<string> citasOcupadas = ConexionBD.LeerCitasPorFechaEspecialidadMedico(
                    DateTime.ParseExact(diaCitaElegida, "yyyy/MM/dd", CultureInfo.InvariantCulture),
                    idPaciente, idEspecialidad, idMedico);

                horaCita.Clear();

                while (start <= end)
                {
                    string horaActual = start.AddHours(0).ToString("H:mm");

                    //Solo agrega la hora si no está en la lista de citas ocupadas
                    if (!citasOcupadas.Contains(horaActual))
                    {
                        horaCita.Add(horaActual);
                    }
                    start = start.AddMinutes(15);
                }
                cmbHoraCita.ItemsSource = horaCita;
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            paciente = ConexionBD.LeerDatosPaciente();
            List<Paciente> pacienteEncontrado = new List<Paciente>();

            for (int i = 0; i < paciente.Count; i++)
            {
                if (paciente[i].Apellidos.Equals(txtDniApellidos.Text) || paciente[i].Dni.Equals(txtDniApellidos.Text))
                {
                    pacienteEncontrado.Add(paciente[i]);
                    idPaciente = paciente[i].Id;
                }
            }

            if (pacienteEncontrado.Count == 0)
            {
                MessageBoxResult result = MessageBox.Show("Ese paciente no existe, ¿crear nuevo?", "Ok", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    NuevoPaciente nuevoPaciente = new NuevoPaciente();
                    nuevoPaciente.Show();
                }
            }
            else
            {
                //Asignamos la lista de pacientes encontrados al origen de datos del DataGrid
                dataGridDatosPaciente.ItemsSource = pacienteEncontrado;
            }
        }

        private void NuevoPaciente_PacienteCreado(object sender, EventArgs e)
        {
            //obtenemos todos los pacientes después de crear uno nuevo
            paciente = ConexionBD.LeerDatosPaciente();

            //se limpia el DataGrid antes de agregar nuevos pacientes
            dataGridDatosPaciente.Items.Clear();

            //se añaden todos los pacientes al DataGrid
            foreach (var p in paciente)
            {
                dataGridDatosPaciente.Items.Add(new { Nombre = p.Nombre, Apellidos = p.Apellidos, Direccion = p.Direccion, Dni = p.Dni, Telefono = p.Telefono, IdCompania = p.IdCompania, Email = p.Email });
            }
        }

        private void dataGridDatosPaciente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Solo muestra info
        }
    }
}

