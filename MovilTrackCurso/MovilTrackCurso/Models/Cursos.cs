using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Curso_Tracker.Models
{
    public class Cursos
    {
        [PrimaryKey, AutoIncrement]
    
        public int Id { get; set; }
        public string NomCurso { get; set; }
        
        public string TipoCurso { get; set; }
        
        public string DescripcionCurso { get; set; }
       
        public string HorasCurso { get; set; }
        
    }
}
