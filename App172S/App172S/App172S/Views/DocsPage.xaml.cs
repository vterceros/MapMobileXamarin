using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App172S.Models;
using App172S.Views;
using App172S.ViewModels;
using System.IO;
using App172S.Interfaces;
using App172S.Models.Negocio;
using App172S.Services;
using Plugin.Settings;

namespace App172S.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DocsPage : ContentPage
	{
        DocsViewModel viewModel;
        Stream imgSelected;

        public DocsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new DocsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as del_UsuarioViajeDocumentos;
            if (item == null)
                return;

            #region Mostrar en otra pagina
            //Se crea la url para el mensaje
            string url = string.Format("{0}/Api/UsuarioViajeDocumentos/GetById?Id={1}", App.ServiceIP, item.iUsuarioViajeDocumentos_id);
            //Se crea la pagina a mostrar
            var page = new ImagePreviewerPage();
            //Se agrega de titulo el nombre de la imagen
            page.Title = item.sNombre;
            //Se dirige a la pantalla de mostrador de imagenes
            await App.Current.MainPage.Navigation.PushAsync(page);
            //Se envia el url de la imagen
            MessagingCenter.Send(url, "ImagenUrl");

            ItemsListView.SelectedItem = null;
            #endregion

            #region Mostrar en lista
            //this.indLoading.IsVisible = true;
            //this.indLoading.IsRunning = true;
            //this.ItemsListView.IsVisible = false;            
            //var response = await ApiService.Get<del_UsuarioViajeDocumentos>(string.Format("{0}/Api/UsuarioViajeDocumentos/GetById?Id={1}", App.ServiceIP, item.iUsuarioViajeDocumentos_id));
            //if (!response.IsSuccess)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
            //    return;
            //}
            //del_UsuarioViajeDocumentos img = (del_UsuarioViajeDocumentos)response.Result;
            //if (img != null && img.iDocumento != null)
            //{
            //    Image image = new Image
            //    {
            //        Source = ImageSource.FromStream(() => new MemoryStream(img.iDocumento)),
            //        BackgroundColor = Color.Gray
            //    };

            //    TapGestureRecognizer recognizer = new TapGestureRecognizer();
            //    recognizer.Tapped += (sender2, args2) =>
            //    {
            //        (this as ContentPage).Content = layMain;
            //    };
            //    image.GestureRecognizers.Add(recognizer);

            //    (this as ContentPage).Content = image;
            //}
            //// Manually deselect item.
            //ItemsListView.SelectedItem = null;
            //this.indLoading.IsVisible = false;
            //this.indLoading.IsRunning = false;
            //this.ItemsListView.IsVisible = true;
            #endregion
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            //imgSelected = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();
            var image = await Helpers.Media.GetFoto();
            if (image == null)
                return;
            imgSelected = image.GetStream();
            btnAgregar.IsEnabled = false;
            txtNombre.Text = string.Empty;
            modModalDocumentos.IsVisible = true;
            txtNombre.Focus();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        async void OnOKButtonClicked(object sender, EventArgs args)
        {
            if (this.txtNombre.Text == string.Empty)
            {
                await DisplayAlert("Documento", "Introduzca un Nombre", "OK");
                return;
            }
            modModalDocumentos.IsVisible = false;
            if (imgSelected != null)
            {
                this.indLoading.IsRunning = true;
                this.indLoading.IsVisible = true;
                var viajeId = CrossSettings.Current.GetValueOrDefault("DeltaViajeId", 0);
                var response = await ApiService.Post<int>(ReadFully(imgSelected), string.Format("{0}/Api/UsuarioViajeDocumentos/PostFile?viajeId={1}&nombre={2}", App.ServiceIP, viajeId, this.txtNombre.Text));//?viajeId={1}&nombre={2}
                this.indLoading.IsRunning = false;
                this.indLoading.IsVisible = false;
                btnAgregar.IsEnabled = true;
                this.imgSelected = null;
                ItemsListView.RefreshCommand.Execute(null);
            }
            else
            {
                btnAgregar.IsEnabled = true;
                this.imgSelected = null;
            }
        }

        void OnCancelButtonClicked(object sender, EventArgs args)
        {
            modModalDocumentos.IsVisible = false;
            btnAgregar.IsEnabled = true;
            this.imgSelected = null;
        }

    }
}