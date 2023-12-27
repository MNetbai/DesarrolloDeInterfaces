using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace WpfGestionDeCitas
{
    public class Cita
    {
        private int id;
        private int idEspecialidad { get; set; }
        private int idMedico { get; set; }
        private int idPaciente { get; set; }
        private DateTime fecha;
        private string? hora;
        private int anulada;

        public Cita()
        {
            Id = 0;
            IdEspecialidad = 0;
            IdMedico = 0;
            Fecha = DateTime.MinValue;
            Hora = "";
            Anulada = 0;
        }

        public Cita(int id, int idEspecialidad, int idPaciente, int idMedico, DateTime fecha, string hora, int anulada)
        {
            this.id = id;
            this.idEspecialidad = idEspecialidad;
            this.idPaciente = idPaciente;
            this.idMedico = idMedico;
            this.fecha = fecha;
            this.hora = hora;
            this.anulada = anulada;
        }

        public int Id { get => id; set => id = value; }
        public int IdEspecialidad { get => idEspecialidad; set => idEspecialidad = value; }
        public int IdMedico { get => idMedico; set => idMedico = value; }
        public DateTime Fecha { get => fecha.Date; set => fecha = value; }
        public string? Hora { get => hora; set => hora = value; }
        public int Anulada { get => anulada; set => anulada = value; }
        public int IdPaciente { get => idPaciente; set => idPaciente = value; }
    }
}