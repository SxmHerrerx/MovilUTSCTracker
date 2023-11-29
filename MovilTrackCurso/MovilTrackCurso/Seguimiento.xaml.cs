using Curso_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Curso_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Seguimiento : ContentPage
    {
        public Seguimiento()
        {
            InitializeComponent();
            llenarDatos();
        }
        private async void RegistrarClicked(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Relacion relacion = new Relacion
                {

                    NumEmpleado = int.Parse(txtNumeroEmpleado.Text),
                    NombreEmpleado = txtNombreEmpleado.Text,
                    NombreCurso = txtNombreCurso.Text,
                    LugarDeCurso = txtLugarCurso.Text,
                    Fecha = FechaPicker.Date.ToString("yyyy-MM-dd"), // Ajusta el formato según tus necesidades
                    Hora = txtTiempoCurso.Text,
                    Estatus = txtEstatus.SelectedItem.ToString(),
                    Calificacion = txtCalificacion.Text,


                };

                // Validar el formato de tiempo en txtHorasCurso
                //if (!Regex.IsMatch(txtTiempoCurso.Text, @"^\d{2}:\d{2}(:\d{2})?$"))
                //{
                //    await DisplayAlert("Advertencia", "El formato de tiempo en Horas del Curso es inválido. Debe ser hh:mm o hh:mm:ss", "OK");
                //    return; // No continuar con el proceso de guardado
                //}

                await App.SQLiteDB.SaveRelacionAsync(relacion);

                await DisplayAlert("Registro", "Se guardó exitosamente", "OK");
                var RelacionList = await App.SQLiteDB.GetRelacionAsync();
                llenarDatos();
                if (RelacionList != null)
                {
                    lstRelacion.ItemsSource = RelacionList;
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

        public async void llenarDatos()
        {
            var RelacionList = await App.SQLiteDB.GetRelacionAsync();
            if (RelacionList !=  null)
            {
                lstRelacion.ItemsSource = RelacionList;
            }
        }


        public bool ValidarDatos()
        {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNumeroEmpleado.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtNombreEmpleado.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtNombreCurso.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtLugarCurso.Text))
            {
                respuesta = false;
            }
            else if (FechaPicker.Date == DateTime.MinValue)
            {
                respuesta = false;
            }


            else if (!Regex.IsMatch(txtTiempoCurso.Text, @"^\d{2}:\d{2}(:\d{2})?$"))
            {
                // ^\d{2}:\d{2}(:\d{2})?$ is a regex pattern for hh:mm or hh:mm:ss format
                respuesta = false;
                DisplayAlert("Advertencia", "El formato de tiempo en Horas del Curso es inválido. Debe ser hh:mm o hh:mm:ss", "OK");
            }
            else if (txtEstatus.SelectedItem == null)
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCalificacion.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }


        private async void lstRelacion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj =(Relacion)e.SelectedItem;
            BtnRegistrar.IsVisible= false;
            txtIdEmpleado.IsVisible=true;
            BtnActualizar.IsVisible = true;
            if (!string.IsNullOrEmpty(obj.Id.ToString()))
            {
                var relacion = await App.SQLiteDB.GetRelacionById(obj.Id);
                if (relacion!= null)
                {
                    txtIdEmpleado.Text=relacion.Id.ToString();
                    txtNumeroEmpleado.Text =relacion.NumEmpleado.ToString();       
                    txtNombreEmpleado.Text=relacion.NombreEmpleado.ToString() ;
                    txtNombreCurso.Text= relacion.NombreCurso.ToString();   
                    txtLugarCurso.Text=relacion.LugarDeCurso.ToString();
                    txtTiempoCurso.Text= relacion.Hora.ToString();
                    txtEstatus.SelectedItem= relacion.Estatus.ToString();
                    txtCalificacion.Text= relacion.Calificacion.ToString();

                }
            }
        }

        private async void ActualizarClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdEmpleado.Text))
            {
                if (int.TryParse(txtIdEmpleado.Text, out int id))
                {
                    Relacion relacion = await App.SQLiteDB.GetRelacionById(id);
                    if (relacion != null)
                    {
                        relacion.NumEmpleado = int.Parse(txtNumeroEmpleado.Text);
                        relacion.NombreEmpleado = txtNombreEmpleado.Text;
                        relacion.NombreCurso = txtNombreCurso.Text;
                        relacion.LugarDeCurso = txtLugarCurso.Text;
                        relacion.Hora = txtTiempoCurso.Text;
                        relacion.Estatus = txtEstatus.SelectedItem.ToString();
                        relacion.Calificacion = txtCalificacion.Text;

                        await App.SQLiteDB.SaveRelacionAsync(relacion);

                        BtnRegistrar.IsVisible = true;
                        txtIdEmpleado.IsVisible = false;
                        BtnActualizar.IsVisible = false;

                        // You might want to update the list view (lstRelacion) here.
                        var RelacionList = await App.SQLiteDB.GetRelacionAsync();
                        lstRelacion.ItemsSource = RelacionList;

                        await DisplayAlert("Actualización", "Se actualizó exitosamente", "OK");

                        // Clear the input fields
                        txtIdEmpleado.Text = "";
                        txtNumeroEmpleado.Text = "";
                        txtNombreEmpleado.Text = "";
                        txtNombreCurso.Text = "";
                        txtLugarCurso.Text = "";
                        txtTiempoCurso.Text = "";
                        txtEstatus.SelectedItem = null;
                        txtCalificacion.Text = "";
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se encontró la relación con el ID especificado", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "El ID debe ser un número entero válido", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Por favor, ingrese un ID válido", "OK");
            }
        }

        private async void BtnEliminiarClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdEmpleado.Text))
            {
                if (int.TryParse(txtIdEmpleado.Text, out int id))
                {
                    var result = await DisplayAlert("Confirmación", "¿Estás seguro de que deseas eliminar este registro?", "Sí", "No");

                    if (result)
                    {
                        Relacion relacion = await App.SQLiteDB.GetRelacionById(id);
                        if (relacion != null)
                        {
                            await App.SQLiteDB.DeleteRelacionAsync(id); // Método para eliminar el registro
                            var RelacionList = await App.SQLiteDB.GetRelacionAsync();
                            lstRelacion.ItemsSource = RelacionList;

                            await DisplayAlert("Eliminación", "Se eliminó el registro exitosamente", "OK");

                            // Limpia los campos de entrada
                            txtIdEmpleado.Text = "";
                            txtNumeroEmpleado.Text = "";
                            txtNombreEmpleado.Text = "";
                            txtNombreCurso.Text = "";
                            txtLugarCurso.Text = "";
                            txtTiempoCurso.Text = "";
                            txtEstatus.SelectedItem = null;
                            txtCalificacion.Text = "";
                        }
                        else
                        {
                            await DisplayAlert("Error", "No se encontró la relación con el ID especificado", "OK");
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Error", "El ID debe ser un número entero válido", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Por favor, ingrese un ID válido", "OK");
            }
        }



    }
}