using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginApp.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaContactosPage : ContentPage
    {
        public ListaContactosPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                contactosListView.ItemsSource = await App.Database.GetAllPersonas();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        private async void btnNuevoregistro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}