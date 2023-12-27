using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGestionDeCitas
{
    public class Especialidad
    {

        private int id;
        private string nombre;
        private string descripcion;
        private int baja;

        public Especialidad()
        {
            this.id = -1;
            this.nombre = "";
            this.descripcion = "";
        }

        public Especialidad(int id, string nombre, string descripcion, int baja)
        {
            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.baja = baja;
        }


        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Baja { get => baja; set => baja = value; }
    }
}
