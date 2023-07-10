using LoginApp.model;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginApp.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {

        decimal longitud;
        decimal latitu;

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
 
            string mensaje = "";

    
            if (string.IsNullOrEmpty(nombrecompleto.Text)) {mensaje += "- Nombre\n";}

            if (string.IsNullOrEmpty(edad.Text)){mensaje += "- Edad\n";}

            if (string.IsNullOrEmpty(telefono.Text)){mensaje += "- Teléfono\n";}

            if (string.IsNullOrEmpty(nota.Text)){ mensaje += "- Nota\n";}

            if (paisPicker.SelectedItem == null){mensaje += "- País\n";}

            if (!string.IsNullOrEmpty(mensaje))
            {
                // Mostrar mensaje de validación
                mensaje = " "+mensaje;
                DisplayAlert("Datos obligatorios:", mensaje, "Aceptar");
                return;
            }
            this.obtenerLongiLati();

            var contacto = new Contactos()
            {
                Nombres = nombrecompleto.Text,
                Edad = int.Parse(edad.Text),
                Pais = paisPicker.SelectedItem.ToString(),
                Nota = nota.Text,
                Latitud = this.latitu,// Asignar el valor de la latitud obtenido desde la geolocalización,
                Longitud = this.longitud,// Asignar el valor de la longitud obtenido desde la geolocalización
            };


            // Insertar el contacto en la base de datos
            int resultado = await App.Database.AddContact(contacto);

            if (resultado != -1)
            {
                // Mostrar mensaje de éxito
                await DisplayAlert("Éxito", "Contacto guardado correctamente", "Aceptar");
                // Redirigir a la página de lista de contactos
                await Navigation.PushAsync(new ListaContactosPage());
            }
            else
            {
                // Mostrar mensaje de error
                await DisplayAlert("Error", "No se pudo guardar el contacto", "Aceptar");
            }
        }


        public async void obtenerLongiLati() {

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50; // Precisión deseada en metros

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

                this.latitu = (decimal)position.Latitude;
                this.longitud = (decimal)position.Longitude;
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error
                await DisplayAlert("Error", "No se pudo obtener la ubicación", "Aceptar");
                return;
            }


        }
    }
}