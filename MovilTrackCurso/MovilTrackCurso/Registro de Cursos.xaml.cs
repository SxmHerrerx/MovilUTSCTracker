using Curso_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Curso_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro_de_Cursos : ContentPage
    {

        public Registro_de_Cursos()
        {
            InitializeComponent();
        
        }
        private async void BtnRegistrarCurso_Clicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Cursos emp = new Cursos
                {
                    NomCurso = txtNombreCurso.Text,
                    TipoCurso = PickerTipoCurso.SelectedItem.ToString(),
                    DescripcionCurso = txtDescripcion.Text,
                    HorasCurso = txtHorasCurso.Text,
                };

                // Validar el formato de tiempo en txtHorasCurso
                if (!Regex.IsMatch(txtHorasCurso.Text, @"^\d{2}:\d{2}(:\d{2})?$"))
                {
                    await DisplayAlert("Advertencia", "El formato de tiempo en Horas del Curso es inválido. Debe ser hh:mm o hh:mm:ss", "OK");
                    return; // No continuar con el proceso de guardado
                }

                await App.SQLiteDB.SaveEmpleadoAsync(emp);

                await DisplayAlert("Registro", "Se guardó exitosamente", "OK");
                var CursoList = await App.SQLiteDB.GetCursoAsync();
               
                if (CursoList != null)
                {
                    lstCurso.ItemsSource = CursoList;
                    //await DisplayAlert("Advertencia", "Ingresar todos los datos correctamente", "OK");
                }
            }
            else
            {
                // Esta parte del código solo se ejecutará si la validación general de datos no pasa
                // Muestra un mensaje de advertencia para ingresar todos los datos correctamente
             await DisplayAlert("Advertencia", "Ingresar todos los datos correctamente", "OK");
            }
        }




        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombreCurso.Text))
            {
                respuesta = false;
            }
            else if (PickerTipoCurso.SelectedItem == null)
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtHorasCurso.Text))
            {
                respuesta = false;
            }
            else if (!Regex.IsMatch(txtHorasCurso.Text, @"^\d{2}:\d{2}(:\d{2})?$"))
            {
                // ^\d{2}:\d{2}(:\d{2})?$ is a regex pattern for hh:mm or hh:mm:ss format
                respuesta = false;
                 DisplayAlert("Advertencia", "El formato de tiempo en Horas del Curso es inválido. Debe ser hh:mm o hh:mm:ss", "OK");
            }
            else if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }


    }

}