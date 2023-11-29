using Curso_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Curso_Tracker
{
    internal class AppSettings
    {
        public static string ApiFirebase = "https://ventastest1-b5384-default-rtdb.firebaseio.com/";
        private static string KeyAplication = "AIzaSyCjoY4uVfAGIoiWdbKR7COyIO1NKR5d4SI";


        public static ResponseAuthentication oAuthentication { get; set; }
        private static string ApiUrlGoogleApis = "https://identitytoolkit.googleapis.com/v1/";

        public static string ApiAuthentication(string tipo)
        {
            if (tipo == "LOGIN")
                return ApiUrlGoogleApis + "accounts:signInWithPassword?key=" + KeyAplication;
            else if (tipo == "SIGNIN")
                return ApiUrlGoogleApis + "accounts:signUp?key=" + KeyAplication;
            else
                return "";
        }

    }
}