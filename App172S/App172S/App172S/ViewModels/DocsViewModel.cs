using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using App172S.Models;
using App172S.Views;
using App172S.Models.Negocio;
using App172S.Services;
using System.Collections.Generic;
using Plugin.Settings;

namespace App172S.ViewModels
{
    public class DocsViewModel : BaseViewModel
    {
        public ObservableCollection<del_UsuarioViajeDocumentos> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public DocsViewModel()
        {
            Title = "Documentos";
            Items = new ObservableCollection<del_UsuarioViajeDocumentos>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();                
                var viajeId = CrossSettings.Current.GetValueOrDefault("DeltaViajeId", 0);
                var response = await ApiService.Get<List<del_UsuarioViajeDocumentos>>(string.Format("{0}/Api/UsuarioViajeDocumentos/GetByViajeId?viajeId={1}", App.ServiceIP, viajeId));
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
                    return;
                }
                List<del_UsuarioViajeDocumentos> items = (List<del_UsuarioViajeDocumentos>)response.Result;
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}