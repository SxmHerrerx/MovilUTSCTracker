using Curso_Tracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using static Xamarin.Essentials.Permissions;
using System.Threading.Tasks;

namespace Curso_Tracker.Service
{
    internal class ApiServiceAuthentication
    {

        public static async Task<bool> Login(UserAuthentication oUsuario)
        {
            try
            {

                HttpClient client = new HttpClient();
                var body = JsonConvert.SerializeObject(oUsuario);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(AppSettings.ApiAuthentication("LOGIN"), content);
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    ResponseAuthentication oResponse = JsonConvert.DeserializeObject<ResponseAuthentication>(jsonResult);
                    AppSettings.oAuthentication = oResponse;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                return false;
            }

        }

        public static async Task<bool> RegistrarUsuario(Usuario oUsuario)
        {
            bool respuesta = true;
            try
            {
                UserAuthentication oUser = new UserAuthentication()
                {
                    email = oUsuario.Email,
                    password = oUsuario.Clave,
                    returnSecureToken = true
                };

                HttpClient client = new HttpClient();
                var body = JsonConvert.SerializeObject(oUser);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(AppSettings.ApiAuthentication("SIGNIN"), content);
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    ResponseAuthentication oResponse = JsonConvert.DeserializeObject<ResponseAuthentication>(jsonResult);
                    if (oResponse != null)
                    {
                        respuesta = await ApiServiceFirebase.RegistrarUsuario(oUsuario, oResponse);
                    }
                    else
                    {
                        respuesta = false;
                    }
                }
                else
                {
                    respuesta = false;
                }
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                respuesta = false;
            }

            return respuesta;

        }
    }
}

