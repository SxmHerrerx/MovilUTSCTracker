using System;
using System.Collections.Generic;
using System.Text;

namespace Curso_Tracker.Models
{
    internal class UserAuthentication
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool returnSecureToken { get; set; }
    }
}
