using Curso_Tracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Curso_Tracker.Service
{
    internal class ApiServiceFirebase
    {
        public static async Task<bool> RegistrarUsuario(Usuario oUsuario, ResponseAuthentication oResponse)
        {
            try
            {

                HttpClient client = new HttpClient();
                var body = JsonConvert.SerializeObject(oUsuario);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var formatoapi = string.Concat(AppSettings.ApiFirebase, "{0}/{1}.json?auth={2}");

                var response = await client.PutAsync(
                    string.Format(formatoapi, "usuarios", oResponse.LocalId, oResponse.IdToken),
                    content);
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
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

        public static async Task<Usuario> ObtenerUsuario()
        {
            Usuario oObject = new Usuario();
            try
            {
                HttpClient client = new HttpClient();
                string apiformat = string.Concat(AppSettings.ApiFirebase, "usuarios/{0}.json?auth={1}");
                var response = await client.GetAsync(string.Format(apiformat, AppSettings.oAuthentication.LocalId, AppSettings.oAuthentication.IdToken));
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var jsonstring = await response.Content.ReadAsStringAsync();

                    if (jsonstring != "null")
                    {
                        oObject = JsonConvert.DeserializeObject<Usuario>(jsonstring);
                    }
                    return oObject;
                }
                else
                {
                    return oObject;
                }
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                return oObject;
            }

        }
    }
}
