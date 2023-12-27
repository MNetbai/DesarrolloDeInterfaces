using MySql.Data.MySqlClient;
using System.Windows;

namespace WpfGestionDeCitas
{
    internal static class ConexionBD
    {
        static string servidor = "localhost"; //Nombre o ip del servidor de MySQL
        static string bd = "centromedico"; //Nombre de la base de datos
        static string usuario = "root"; //Usuario de acceso a MySQL
        static string password = "Adinutsa96."; //Contraseña de usuario de acceso a MySQL

        public static MySqlConnection GetConexion()
        {
            //Crearemos la cadena de conexión concatenando las variables
            string cadenaConexion = "Database=" + bd + "; Data Source=" + servidor + "; User Id=" + usuario + "; Password=" + password + "";

            //Instancia para conexión a MySQL, recibe la cadena de conexión
            MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);
            return conexionBD;
        }

        public static bool GuardarPaciente(String nombre, String apellidos, String direccion, String dni, String telefono, Int32 idCompania, String email)
        {
            MySqlConnection conn = ConexionBD.GetConexion();
            bool result = true;
            try
            {
                conn.Open();
                string sql = "INSERT INTO paciente (Nombre, Apellidos, Direccion, dni, Telefono, idCompañia, email) VALUES (?nombre,?apellidos,?direccion,?dni,?telefono,?idCompania,?email)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = nombre;
                cmd.Parameters.Add("?apellidos", MySqlDbType.VarChar).Value = apellidos;
                cmd.Parameters.Add("?direccion", MySqlDbType.VarChar).Value = direccion;
                cmd.Parameters.Add("?dni", MySqlDbType.VarChar).Value = dni;
                cmd.Parameters.Add("?telefono", MySqlDbType.VarChar).Value = telefono;
                cmd.Parameters.Add("?idCompania", MySqlDbType.Int32).Value = idCompania;
                cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = email;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = false;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static bool ModificarPaciente(String nombre, String apellidos, String direccion, String dni, String telefono, int idCompania, String email)
        {
            MySqlConnection conn = ConexionBD.GetConexion();
            bool result = true;
            try
            {
                conn.Open();
                string sql = "UPDATE paciente SET Nombre=?nombre, Direccion=?direccion, Telefono=?telefono, idCompañia=?idCompania, Email=?email WHERE DNI = ?dni or Apellidos=?apellido";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = nombre;
                cmd.Parameters.Add("?apellido", MySqlDbType.VarChar).Value = apellidos;
                cmd.Parameters.Add("?direccion", MySqlDbType.VarChar).Value = direccion;
                cmd.Parameters.Add("?dni", MySqlDbType.VarChar).Value = dni;
                cmd.Parameters.Add("?telefono", MySqlDbType.Int32).Value = telefono;
                cmd.Parameters.Add("?idCompania", MySqlDbType.VarChar).Value = idCompania;
                cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = email;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = false;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static List<Paciente> LeerDatosPaciente()
        {

            MySqlDataReader? reader = null;
            List<Paciente> datos = new List<Paciente>();
            MySqlConnection conexionBD = GetConexion();
            try
            {
                string consulta = "SELECT * FROM paciente";
                MySqlCommand comando = new MySqlCommand(consulta);
                comando.Connection = conexionBD;
                conexionBD.Open();
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    int id = 0;
                    if (!reader.IsDBNull(0))
                    {
                        id = reader.GetInt32(0);
                    }
                    String nombre = "";
                    if (!reader.IsDBNull(1))
                    {
                        nombre = reader.GetString(1);
                    }
                    String apellidos = "";
                    if (!reader.IsDBNull(2))
                    {
                        apellidos = reader.GetString(2);
                    }
                    String direccion = "";
                    if (!reader.IsDBNull(3))
                    {
                        direccion = reader.GetString(3);
                    }
                    String dni = "";
                    if (!reader.IsDBNull(4))
                    {
                        dni = reader.GetString(4);
                    }
                    String telefono = "";
                    if (!reader.IsDBNull(5))
                    {
                        telefono = reader.GetString(5);
                    }
                    int idCompania = 0;
                    if (!reader.IsDBNull(6))
                    {
                        idCompania = reader.GetInt32(6);
                    }
                    String email = "";
                    if (!reader.IsDBNull(7))
                    {
                        email = reader.GetString(7);
                    }
                    Paciente a = new Paciente(id, nombre, apellidos, direccion, dni, telefono, idCompania, email);
                    datos.Add(a);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
            return datos;
        }

        public static List<Cita> LeerDatosCita()
        {

            MySqlDataReader? reader = null; //Variable para leer el resultado de la consulta
            List<Cita> datos = new List<Cita>(); //Variable para almacenar el resultado
            MySqlConnection conexionBD = GetConexion();
            //Agregamos try-catch para capturar posibles errores de conexión o sintaxis.
            try
            {
                string consulta = "SELECT * FROM CITA where anulada=0"; //Consulta a MySQL (Muestra las bases de datos que tiene el servidor)
                MySqlCommand comando = new MySqlCommand(consulta); //Declaración SQL para ejecutar contra una base de datos MySQL
                comando.Connection = conexionBD; //Establece la MySqlConnection utilizada por esta instancia de MySqlCommand
                conexionBD.Open(); //Abre la conexión
                reader = comando.ExecuteReader(); //Ejecuta la consulta y crea un MySqlDataReader

                while (reader.Read()) //Avanza MySqlDataReader al siguiente registro
                {
                    int id = 0;
                    if (!reader.IsDBNull(0))
                        id = reader.GetInt32(0);
                    int idEspecialidad = 0;
                    if (!reader.IsDBNull(1))
                        idEspecialidad = reader.GetInt32(1);
                    int idPaciente = 0;
                    if (!reader.IsDBNull(2))
                        idPaciente = reader.GetInt32(2);
                    int idMedico = 0;
                    if (!reader.IsDBNull(3))
                        idMedico = reader.GetInt32(3);
                    DateTime fecha = DateTime.Now;
                    if (!reader.IsDBNull(4))
                        fecha = reader.GetDateTime(4);
                    string hora = "";
                    if (!reader.IsDBNull(5))
                        hora = reader.GetString(5);
                    int anulada = 0;
                    if (!reader.IsDBNull(6))
                        anulada = reader.GetInt32(6);
                    //Almacena cada registro en la clase 
                    Cita cita = new Cita(id, idEspecialidad, idPaciente, idMedico, fecha, hora, anulada);
                    datos.Add(cita);
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message); //Si existe un error aquí muestra el mensaje
            }
            finally
            {
                conexionBD.Close(); //Cierra la conexión a MySQL
            }
            return datos;
        }

        public static List<string> LeerCitasPorFecha(DateTime fechaP, int idPaciente)
        {
            MySqlDataReader reader = null; //Variable para leer el resultado de la consulta
            List<string> datos = new List<String>(); //Variable para almacenar el resultado
            MySqlConnection conexionBD = GetConexion();
            //Agregamos try-catch para capturar posibles errores de conexión o sintaxis.
            try
            {
                string consulta = "SELECT HORA FROM CITA,paciente where cita.idPaciente=paciente.id and cita.FECHA = @fecha and idPaciente=@idPaciente"; //Consulta a MySQL (Muestra las bases de datos que tiene el servidor)
                MySqlCommand comando = new MySqlCommand(consulta); //Declaración SQL para ejecutar contra una base de datos MySQL
                comando.Parameters.AddWithValue("@fecha", fechaP);
                comando.Parameters.AddWithValue("@idPaciente", idPaciente);

                comando.Connection = conexionBD; //Establece la MySqlConnection utilizada por esta instancia de MySqlCommand
                conexionBD.Open(); //Abre la conexión
                reader = comando.ExecuteReader(); //Ejecuta la consulta y crea un MySqlDataReader
                while (reader.Read()) //Avanza MySqlDataReader al siguiente registro
                {
                    //Almacena cada registro en la clase 
                    string hora = reader.GetString(0);
                    datos.Add(hora);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message); //Si existe un error aquí muestra el mensaje
            }
            finally
            {
                conexionBD.Close(); //Cierra la conexión a MySQL
            }
            return datos;
        }

        public static List<string> LeerHorasDisponiblesPorFechaMedicoEspecialidad(DateTime fecha, int idMedico, int idEspecialidad)
        {
            MySqlConnection conexionBD = GetConexion();
            List<string> horasDisponibles = new List<string>();

            try
            {
                string consulta = "SELECT DISTINCT HORA FROM HORARIO_MEDICO WHERE IdMedico = @idMedico";
                MySqlCommand comando = new MySqlCommand(consulta);
                comando.Parameters.AddWithValue("@idMedico", idMedico);

                comando.Connection = conexionBD;
                conexionBD.Open();

                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    string hora = reader.GetString(0);

                    //verificamos si la hora está disponible para la fecha, médico y especialidad específicos
                    if (!ExisteCitaParaFechaHoraMedicoEspecialidad(fecha, hora, idMedico, idEspecialidad))
                    {
                        horasDisponibles.Add(hora);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return horasDisponibles;
        }

        public static List<string> LeerHorasOcupadasPorFechaMedico(DateTime fecha, int idMedico)
        {
            MySqlDataReader reader = null;
            List<string> datos = new List<string>();
            MySqlConnection conexionBD = GetConexion();

            try
            {
                string consulta = "SELECT HORA FROM CITA WHERE FECHA = @fecha AND IdMedico = @idMedico";
                MySqlCommand comando = new MySqlCommand(consulta);
                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@idMedico", idMedico);

                comando.Connection = conexionBD;
                conexionBD.Open();
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    string hora = reader.GetString(0);
                    datos.Add(hora);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                conexionBD.Close();
            }

            return datos;
        }

        public static List<string> LeerCitasPorFechaEspecialidadMedico(DateTime fecha, int idPaciente, int idEspecialidad, int idMedico)
        {
            MySqlDataReader reader = null;
            List<string> datos = new List<string>();
            MySqlConnection conexionBD = GetConexion();

            try
            {
                string consulta = "SELECT HORA FROM CITA WHERE FECHA = @fecha AND IdPaciente = @idPaciente AND IdEspecialidad = @idEspecialidad AND IdMedico = @idMedico";
                MySqlCommand comando = new MySqlCommand(consulta);
                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@idPaciente", idPaciente);
                comando.Parameters.AddWithValue("@idEspecialidad", idEspecialidad);
                comando.Parameters.AddWithValue("@idMedico", idMedico);

                comando.Connection = conexionBD;
                conexionBD.Open();
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    string hora = reader.GetString(0);
                    datos.Add(hora);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                conexionBD.Close();
            }

            return datos;
        }

        public static bool ExisteCitaParaFechaHoraMedicoEspecialidad(DateTime fecha, string hora, int idMedico, int idEspecialidad)
        {
            MySqlConnection conexionBD = GetConexion();

            try
            {
                string consulta = "SELECT COUNT(*) FROM CITA WHERE FECHA = @fecha AND HORA = @hora AND IdMedico = @idMedico AND IdEspecialidad = @idEspecialidad AND Anulada = 0";
                MySqlCommand comando = new MySqlCommand(consulta);
                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@hora", hora);
                comando.Parameters.AddWithValue("@idMedico", idMedico);
                comando.Parameters.AddWithValue("@idEspecialidad", idEspecialidad);

                comando.Connection = conexionBD;
                conexionBD.Open();

                int count = Convert.ToInt32(comando.ExecuteScalar());

                return count > 0;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                conexionBD.Close();
            }
        }
        
        public static List<Cita> LeerDatosCitasDNI(String texto)
        {

            MySqlDataReader? reader = null; //Variable para leer el resultado de la consulta
            List<Cita> datos = new List<Cita>(); //Variable para almacenar el resultado
            MySqlConnection conexionBD = GetConexion();
            //Agregamos try-catch para capturar posibles errores de conexión o sintaxis.
            try
            {
                string consulta = "SELECT cita.* FROM cita,paciente WHERE cita.idPaciente=paciente.id AND (paciente.dni=@texto OR paciente.Apellidos=@texto) AND CITA.ANULADA=0"; //Consulta a MySQL (Muestra las bases de datos que tiene el servidor)
                MySqlCommand comando = new MySqlCommand(consulta); //Declaración SQL para ejecutar contra una base de datos MySQL
                comando.Parameters.AddWithValue("@texto", texto);
                comando.Connection = conexionBD; //Establece la MySqlConnection utilizada por esta instancia de MySqlCommand
                conexionBD.Open(); //Abre la conexión
                reader = comando.ExecuteReader(); //Ejecuta la consulta y crea un MySqlDataReader
                while (reader.Read()) //Avanza MySqlDataReader al siguiente registro
                {
                    int id = 0;
                    if (!reader.IsDBNull(0))
                        id = reader.GetInt32(0);
                    int idEspecialidad = 0;
                    if (!reader.IsDBNull(1))
                        idEspecialidad = reader.GetInt32(1);
                    int idPaciente = 0;
                    if (!reader.IsDBNull(2))
                        idPaciente = reader.GetInt32(2);
                    int idMedico = 0;
                    if (!reader.IsDBNull(3))
                        idMedico = reader.GetInt32(3);
                    DateTime fecha = DateTime.Now;
                    if (!reader.IsDBNull(4))
                        fecha = reader.GetDateTime(4);
                    string hora = "";
                    if (!reader.IsDBNull(5))
                        hora = reader.GetString(5);
                    int anulada = 0;
                    if (!reader.IsDBNull(6))
                        anulada = reader.GetInt32(6);
                    //Almacena cada registro en la clase 
                    Cita cita = new Cita(id, idEspecialidad, idPaciente, idMedico, fecha, hora, anulada);
                    datos.Add(cita);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message); //Si existe un error aquí muestra el mensaje
            }
            finally
            {
                conexionBD.Close(); //Cierra la conexión a MySQL
            }
            return datos;
        }

        public static bool GuardarCita(int id, int idEspecialidad, int idPaciente, int idMedico, DateTime fecha, string hora, int anulada)
        {
            MySqlConnection conn = ConexionBD.GetConexion();
            bool result = true;
            try
            {

                conn.Open();

                string sql = "INSERT INTO CITA (id,idEspecialidad,idPaciente,idMedico,fecha,hora,anulada) VALUES (?id,?idEspecialidad,?idPaciente,?idMedico,?fecha,?hora,?anulada)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("?id", MySqlDbType.Int32).Value = id;
                cmd.Parameters.Add("?idEspecialidad", MySqlDbType.Int32).Value = idEspecialidad;
                cmd.Parameters.Add("?idPaciente", MySqlDbType.Int32).Value = idPaciente;
                cmd.Parameters.Add("?idMedico", MySqlDbType.Int32).Value = idMedico;
                cmd.Parameters.Add("?fecha", MySqlDbType.Date).Value = fecha;
                cmd.Parameters.Add("?hora", MySqlDbType.VarChar).Value = hora;
                cmd.Parameters.Add("?anulada", MySqlDbType.Int32).Value = anulada;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = false;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static bool AnularCita(int idCita)
        {
            MySqlConnection conn = ConexionBD.GetConexion();
            bool result = true;
            try
            {
                conn.Open();

                string sql = "UPDATE Cita SET anulada=1 WHERE id=@idCita";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("@idCita", MySqlDbType.Int32).Value = idCita;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = false;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static bool ModificarCita(int idCita, DateTime nuevaFecha, string nuevaHora)
        {
            MySqlConnection conn = ConexionBD.GetConexion();
            bool result = true;
            try
            {
                conn.Open();

                string sql = "UPDATE Cita SET anulada = 0, fecha = @nuevaFecha, hora = @nuevaHora WHERE id = @idCita";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("@idCita", MySqlDbType.Int32).Value = idCita;
                cmd.Parameters.Add("@nuevaFecha", MySqlDbType.Date).Value = nuevaFecha.ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@nuevaHora", MySqlDbType.VarChar).Value = nuevaHora;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = false;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static List<string> ObtenerHorasDisponibles(int idMedico, DateTime fecha)
        {
            List<string> horasDisponibles = new List<string>();

            MySqlConnection conexionBD = GetConexion();

            try
            {
                conexionBD.Open();

                //lógica específica para obtener las horas disponibles del médico en la fecha seleccionada.

                string consulta = "SELECT Hora FROM DisponibilidadMedico " +
                                  "WHERE IdMedico = @idMedico AND DiaDisponible = @diaDisponible " +
                                  "AND Hora NOT IN (SELECT Hora FROM Cita WHERE IdMedico = @idMedico AND Fecha = @fecha)";

                MySqlCommand comando = new MySqlCommand(consulta, conexionBD);
                comando.Parameters.AddWithValue("@idMedico", idMedico);
                comando.Parameters.AddWithValue("@diaDisponible", fecha.DayOfWeek.ToString());
                comando.Parameters.AddWithValue("@fecha", fecha);

                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string horaDisponible = reader.GetString(0);
                        horasDisponibles.Add(horaDisponible);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return horasDisponibles;
        }

        public static List<Compania> LeerDatosCompania()
        {

            MySqlDataReader? reader = null; 
            List<Compania> datos = new List<Compania>();
            MySqlConnection conexionBD = GetConexion();
            try
            {
                string consulta = "SELECT * FROM COMPANIA"; 
                MySqlCommand comando = new MySqlCommand(consulta); 
                comando.Connection = conexionBD; 
                conexionBD.Open(); 
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Compania compania = new Compania(reader.GetString(0), reader.GetString(1), reader.GetInt32(2));
                    datos.Add(compania);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message); 
            }
            finally
            {
                conexionBD.Close();
            }
            return datos;
        }

        public static List<Medico> LeerDatosMedico()
        {

            MySqlDataReader? reader = null;
            List<Medico> datos = new List<Medico>();
            MySqlConnection conexionBD = GetConexion();
            try
            {
                string consulta = "SELECT * FROM MEDICO";
                MySqlCommand comando = new MySqlCommand(consulta);
                comando.Connection = conexionBD;
                conexionBD.Open();
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Medico medico = new Medico(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetString(7), reader.GetInt32(8));
                    datos.Add(medico);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
            return datos;
        }

        public static List<Especialidad> LeerDatosEspecialidad()
        {

            MySqlDataReader? reader = null;
            List<Especialidad> datos = new List<Especialidad>();
            MySqlConnection conexionBD = GetConexion();
            try
            {
                string consulta = "SELECT * FROM ESPECIALIDAD";
                MySqlCommand comando = new MySqlCommand(consulta);
                comando.Connection = conexionBD;
                conexionBD.Open();
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Especialidad especialidad = new Especialidad(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                    datos.Add(especialidad);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
            return datos;
        }

        public static bool GuardarEspecialidad(String nombre, String descripcion)
        {
            MySqlConnection conn = ConexionBD.GetConexion();
            bool result = true;
            try
            {
                conn.Open();
                string sql = "INSERT INTO especialidad (Nombre, Descripcion) VALUES (?nombre,?descripcion)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("?nombre", MySqlDbType.VarChar).Value = nombre;
                cmd.Parameters.Add("?descripcion", MySqlDbType.VarChar).Value = descripcion;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = false;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static bool ModificarDescripcionEspecialidad(string descripcion, int id)
        {
            MySqlConnection conn = ConexionBD.GetConexion();
            bool result = true;
            try
            {
                conn.Open();
                string sql = "UPDATE especialidad SET Descripcion=?descripcion WHERE Id=?id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("?descripcion", MySqlDbType.VarChar).Value = descripcion;
                cmd.Parameters.Add("?id", MySqlDbType.Int32).Value = id;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = false;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static bool DarDeBajaEspecialidad(int idEspecialidad)
        {
            MySqlConnection conn = ConexionBD.GetConexion();
            bool result = true;
            try
            {
                conn.Open();
                string sql = "UPDATE especialidad SET baja=1 WHERE id=@idEspecialidad";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("@idEspecialidad", MySqlDbType.Int32).Value = idEspecialidad;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = false;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public static List<Medico> LeerDatosMedicoEspecialidadBaja(int nEspecialidad)
        {
            MySqlDataReader? reader = null;
            List<Medico> datos = new List<Medico>();
            MySqlConnection conexionBD = GetConexion();

            try
            {
                string consulta = "SELECT * FROM MEDICO WHERE IDESPECIALIDAD = @nEspecialidad AND baja = 0";
                MySqlCommand comando = new MySqlCommand(consulta);
                comando.Parameters.AddWithValue("@nEspecialidad", nEspecialidad);
                comando.Connection = conexionBD;
                conexionBD.Open();
                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    int id = 0;
                    if (!reader.IsDBNull(0))
                        id = reader.GetInt32(0);

                    int idEspecialidad = 0;
                    if (!reader.IsDBNull(1))
                        idEspecialidad = reader.GetInt32(1);

                    string? nombre = "";
                    if (!reader.IsDBNull(2))
                        nombre = reader.GetString(2);

                    string? apellidos = "";
                    if (!reader.IsDBNull(3))
                        apellidos = reader.GetString(3);

                    string numColegiado = "";
                    if (!reader.IsDBNull(4))
                        numColegiado = reader.GetString(4);

                    string telefono = "";
                    if (!reader.IsDBNull(5))
                        telefono = reader.GetString(5);

                    string dni = "";
                    if (!reader.IsDBNull(6))
                        dni = reader.GetString(6);

                    int baja = 0;
                    if (!reader.IsDBNull(7))
                        baja = reader.GetInt32(7);

                    string email = "";
                    if (!reader.IsDBNull(8))
                        email = reader.GetString(8);

                    // Almacena cada registro en la clase medico
                    Medico medico = new Medico(id, idEspecialidad, nombre, apellidos, numColegiado, telefono, dni, baja, email);
                    datos.Add(medico);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return datos;
        }

        public static bool DarDeBajaMedico(int idMedico)
        {
            MySqlConnection conn = GetConexion();
            bool result = true;

            try
            {
                conn.Open();
                string sql = "UPDATE MEDICO SET baja = 1 WHERE id = @idMedico";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idMedico", idMedico);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = false;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
