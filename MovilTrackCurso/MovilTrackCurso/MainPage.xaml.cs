using Curso_Tracker.Models;
using Curso_Tracker.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Curso_Tracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

              private async void BtnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            if (txtContrasena.Text.Trim().Equals("") || txtEmail.Text.Trim().Equals(""))
            {
                await DisplayAlert("Oops", "Ingrese todos los datos", "Aceptar");
                return;
            }

            UserAuthentication oUsuario = new UserAuthentication()
            {
                email = txtEmail.Text,
                password = txtContrasena.Text,
                returnSecureToken = true
            };

            bool respuesta = await ApiServiceAuthentication.Login(oUsuario);
            if (respuesta)
            {
                //Application.Current.MainPage = new NavigationPage(new MasterPage());
                //Application.Current.MainPage = new MasterPage();
                Navigation.PushModalAsync(new Index());
            }
            else
            {
                await DisplayAlert("Oops", "Usuario no encontrado", "OK");
            }
        }

        private void BtnRegistrarse_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Registrar());
        }
    }
}
