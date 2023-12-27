using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGestionDeCitas
{
    public class Paciente
    {
        private int id;
        private string nombre;
        private string apellidos;
        private string direccion;
        private string dni;
        private string telefono;
        private int idCompania;
        private string email;

        public Paciente(int id, string nombre, string apellidos, string direccion,
            string dni, string telefono, int idCompania, string email)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.direccion = direccion;
            this.dni = dni;
            this.telefono = telefono;
            this.idCompania = idCompania;
            this.email = email;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public int IdCompania { get => idCompania; set => idCompania = value; }
        public string Email { get => email; set => email = value; }

    }
}
