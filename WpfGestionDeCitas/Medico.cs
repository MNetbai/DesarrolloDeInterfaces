using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGestionDeCitas
{
    public class Medico
    {
        private int id;
        private int idEspecialidad;
        private String nombre;
        private String apellidos;
        private String numColegiado;
        private String telefono;
        private String dni;
        private int baja;
        private String email;

        public Medico()
        {
            id = 0;
            idEspecialidad = 0;
            nombre = "";
            apellidos = "";
            numColegiado = "";
            telefono = "";
            dni = "";
            baja = 0;
            email = "";
        }

        public Medico(int a, string b, string c, string d, string e, string f, int g, string h, int i)
        {
            idEspecialidad = a;
            nombre = b;
            apellidos = c;
            numColegiado = d;
            telefono = e;
            dni = f;
            baja = g;
            email = h;
            id = i;
        }

        public Medico(int id, int idEspecialidad, string nombre, string apellidos, string numColegiado, string telefono, string dni, int baja, string email)
        {
            this.id = id;
            this.idEspecialidad = idEspecialidad;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.numColegiado = numColegiado;
            this.telefono = telefono;
            this.dni = dni;
            this.baja = baja;
            this.email = email;
        }

        public int Id { get => id; set => id = value; }
        public int IdEspecialidad { get => idEspecialidad; set => idEspecialidad = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string NumColegiados { get => numColegiado; set => numColegiado = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Dni { get => dni; set => dni = value; }
        public int Baja { get => baja; set => baja = value; }
        public string Email { get => email; set => email = value; }
    }
}
