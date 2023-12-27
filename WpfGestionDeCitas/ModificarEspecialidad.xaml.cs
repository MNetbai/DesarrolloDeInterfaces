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
    /// Lógica de interacción para ModificarEspecialidad.xaml
    /// </summary>
    public partial class ModificarEspecialidad : Window

    {
        //Lista de especialidades y médicos para obtenerlas de la base de datos y así modificarlas
        List<Especialidad> especialidad;
        List<Medico> medicos = new List<Medico>();

        public ModificarEspecialidad()
        {
            InitializeComponent();
            //Inicialización de los campos
            txtNombreEspec.IsEnabled = false;
            txtDescripcionEspec.IsEnabled = false;
            btnModificarEspec.IsEnabled = false;

            especialidad = ConexionBD.LeerDatosEspecialidad();
            foreach (var esp in especialidad)
            {
                if (esp.Baja == 0)
                    cmbEspecialidad.Items.Add(esp.Nombre);
            }
        }

        private void cmbEspecialidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbEspecialidad.SelectedIndex != -1)
            {
                Especialidad especialidadSeleccionada = especialidad[cmbEspecialidad.SelectedIndex];

                //Activamos los elementos de modificación y cargamos la información de la especialidad seleccionada
                txtDescripcionEspec.IsEnabled = true;
                btnModificarEspec.IsEnabled = true;

                txtNombreEspec.Text = especialidadSeleccionada.Nombre;
                txtDescripcionEspec.Text = especialidadSeleccionada.Descripcion;

                //se habilita el CheckBox cuando se selecciona una especialidad
                chkDarDeBaja.IsEnabled = true;
            }
            else
            {
                //Si no hay una especialidad seleccionada, desactivamos los elementos de modificación y deshabilitamos el CheckBox
                txtNombreEspec.IsEnabled = false;
                txtDescripcionEspec.IsEnabled = false;
                btnModificarEspec.IsEnabled = false;

                //limpiar los TextBox
                txtNombreEspec.Text = string.Empty;
                txtDescripcionEspec.Text = string.Empty;

                //Deshabilitamos y desmarcamos el CheckBox cuando no hay especialidad seleccionada
                chkDarDeBaja.IsEnabled = false;
                chkDarDeBaja.IsChecked = false;
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbEspecialidad.SelectedIndex != -1)
            {
                int selectedEspecialidadId = especialidad[cmbEspecialidad.SelectedIndex].Id;

                if (ConexionBD.ModificarDescripcionEspecialidad(txtDescripcionEspec.Text, selectedEspecialidadId))
                {
                    MessageBox.Show("Descripción de la especialidad modificada con éxito");

                    //Verifico si el CheckBox está marcado
                    if (chkDarDeBaja.IsChecked == true)
                    {
                        //Damos de baja la especialidad y los médicos (si es necesario)
                        if (DarDeBajaEspecialidadConMedicos(selectedEspecialidadId))
                        {
                            MessageBox.Show("Especialidad y médicos dados de baja con éxito");
                        }
                        else
                        {
                            MessageBox.Show("No se pudieron dar de baja la especialidad y los médicos");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No ha sido posible modificar la descripción de la especialidad");
                }
            }
            else
            {
                MessageBox.Show("Selecciona una especialidad antes de modificar");
            }
        }

        private bool DarDeBajaEspecialidadConMedicos(int especialidadId)
        {
            try
            {
                //Dar de baja la especialidad
                if (ConexionBD.DarDeBajaEspecialidad(especialidadId))
                {
                    //Obtener la lista de médicos de esa especialidad
                    medicos = ConexionBD.LeerDatosMedicoEspecialidadBaja(especialidadId);

                    //Dar de baja a los médicos
                    foreach (Medico medico in medicos)
                    {
                        ConexionBD.DarDeBajaMedico(medico.Id);  
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
