using App172S.Helpers;
using App172S.Models.Negocio;
using App172S.Services;
using App172S.Views;
using Plugin.Geolocator;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App172S.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        string telefono = string.Empty;
        public string Telefono
        {
            get { return telefono; }
            set { SetProperty(ref telefono, value); }
        }
        bool btnEnabled = false;
        public bool BtnEnabled
        {
            get { return btnEnabled; }
            set { SetProperty(ref btnEnabled, value); }
        }
        bool showPleaseWait = false;
        public bool ShowPleaseWait
        {
            get { return showPleaseWait; }
            set { SetProperty(ref showPleaseWait, value); }
        }
        public ICommand LoginComand { get; }
        public LoginViewModel()
        {                    
            this.BtnEnabled = true;
            this.ShowPleaseWait = false;
            this.Telefono = "";
            var deviceInfo = Xamarin.Forms.DependencyService.Get<Interfaces.IDeviceInfo>();
            deviceInfo.ValidateLocatorPermission();
            deviceInfo.ValidateMapsPermission();

            //if (string.IsNullOrEmpty(App.ImeiTelefono))
            //{
            //    del_UsuarioTelefono telfData = null;
            //    string imei = deviceInfo.GetImei();
            //    Task getImei = Task.Run(async () =>
            //    {
            //
            //        var response = await ApiService.Get<del_UsuarioTelefono>(string.Format("{0}/Api/UsuarioTelefono/GetByTelfId?telfId={1}", App.ServiceIP, imei));
            //        if (!response.IsSuccess)
            //        {
            //            await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
            //            return;
            //        }
            //        telfData = (del_UsuarioTelefono)response.Result;
            //        if (telfData != null && !string.IsNullOrEmpty(telfData.sTelefono_Id))
            //        {
            //            App.ImeiTelefono = telfData.sTelefono_Id;
            //        }
            //    });
            //    getImei.Wait();
            //}
            this.LoginComand = new Command(async () => await this.LoginEvent());            
        }

        private async Task LoginEvent()
        {
            if (!Utils.HasConnectivity())
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor conectese a internet", "Ok");
            }
            this.BtnEnabled = false;
            this.ShowPleaseWait = true;
            if (string.IsNullOrEmpty(this.Telefono))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese su numero de telefono", "Ok");
                this.ShowPleaseWait = false;
                this.BtnEnabled = true;
                return;
            }
            else
            {
                App.ImeiTelefono = this.Telefono;
                var response = await ApiService.Get<object>(string.Format("{0}/Api/UsuarioTelefono/PostTelefono?telfId={1}&telfNro={2}", App.ServiceIP, App.ImeiTelefono, this.Telefono));
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
                    this.ShowPleaseWait = false;
                    this.BtnEnabled = true;
                    return;
                }
                this.ShowPleaseWait = false;
                this.BtnEnabled = true;
                MainViewModel.Instance().MapsPrevious = new MapsPreviousViewModel();
                Application.Current.MainPage = new MapsPrevious();

            }

        }
        #region INotifyPropertyChanged

        protected bool SetProperty<T>(ref T backingStore, T value,
           [CallerMemberName]string propertyName = "",
           Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
