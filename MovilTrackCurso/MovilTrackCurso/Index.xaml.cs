using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Curso_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Index : ContentPage
    {
        public Index()
        {
            InitializeComponent();
        }
        private async void RegistroEmpleado_Clicked(object sender, EventArgs e)
        {
         //   await Navigation.PushAsync(new Registro_de_Empleado());
            Navigation.PushModalAsync(new Registro_de_Empleado());
        }
        private async void Seguimiento_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Seguimiento());
            Navigation.PushModalAsync(new Seguimiento());
        }
        private async void ReguistroCurso_Clicked(object sender, EventArgs e)
        {
           // await Navigation.PushAsync(new Registro_de_Cursos());
            Navigation.PushModalAsync(new Registro_de_Cursos());
        }

    }
}