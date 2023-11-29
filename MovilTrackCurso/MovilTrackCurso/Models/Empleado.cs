using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Curso_Tracker.Models
{
    public class Empleado
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int  NumEmpleado { get; set; }
       
        public string Name { get; set; }
      
        public string Direccion { get; set; }
     
        public long Telefono { get; set; }
      
        public int Edad { get; set; }
        public string Curp { get; set; } 
       
        public string TipoDeEmpledo { get; set; }
    }
}
