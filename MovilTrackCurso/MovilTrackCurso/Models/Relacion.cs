using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Curso_Tracker.Models
{
    public class Relacion

    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int NumEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string NombreCurso { get; set; }
        public string LugarDeCurso { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Estatus { get; set; }
        public string Calificacion { get; set; }
    }
}
