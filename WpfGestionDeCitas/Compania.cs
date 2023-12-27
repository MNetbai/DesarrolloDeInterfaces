using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGestionDeCitas
{
    public class Compania
    {
        private String nombre;
        private String descripcion;
        private int baja;


        public Compania()
        {
            nombre = "";
            descripcion = "";
            baja = 0;
        }

        public Compania(string a, string b, int c)
        {
            nombre = a;
            descripcion = b;
            baja = c;   
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Baja { get => baja; set => baja = value; }
    }
}
