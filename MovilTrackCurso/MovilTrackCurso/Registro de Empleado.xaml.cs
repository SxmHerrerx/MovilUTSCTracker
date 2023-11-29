using Curso_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Curso_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro_de_Empleado : ContentPage
    {
        public Registro_de_Empleado()
        {
            InitializeComponent();
     
        }

        private async void OnRegistrarButtonClicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Empleado emp = new Empleado
                {
                    Name = txtNombre.Text,
                    NumEmpleado = int.Parse(txtNumEmpleado.Text),
                    Direccion = txtDireccion.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Telefono = long.Parse(txtTeléfono.Text),
                    Curp = txtCurp.Text,
                    TipoDeEmpledo= miPicker.SelectedItem.ToString(),
                };

                await App.SQLiteDB.SaveEmpleadoAsync(emp);

                txtNombre.Text = "";
                txtNumEmpleado.Text = "";
                txtDireccion.Text = "";
                txtEdad.Text = "";
                txtTeléfono.Text = "";
                txtCurp.Text = "";
                miPicker.SelectedIndex=-1;

                await DisplayAlert("Registro", "Se guardo exitosamente", "OK");
                var empleadoList = await App.SQLiteDB.GetEmpleadosAsync();
                if (empleadoList != null)
                {
                    lstEmpleado.ItemsSource = empleadoList;
                }
                else
                {

                }

            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "OK");
            }
        }

        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNumEmpleado.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if(string.IsNullOrEmpty(txtTeléfono.Text)) 
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCurp.Text))
            {
                respuesta = false;
            }
            else if (miPicker.SelectedItem == null)
            {
                respuesta = false;
            }
            else
            {
                 respuesta= true;
            }
            return respuesta;
        }
        
     }
}
