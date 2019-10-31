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
    public class MapsPreviousViewModel : INotifyPropertyChanged
    {
        bool showPleaseWait = false;
        public bool ShowPleaseWait
        {
            get { return showPleaseWait; }
            set { SetProperty(ref showPleaseWait, value); }
        }
        public ICommand PassComand { get; }
        public MapsPreviousViewModel()
        {
                     
            this.ShowMapEvent();            
        }

        private async Task ShowMapEvent()
        {

            var response = await ApiService.Get<del_UsuarioViaje>(string.Format("{0}/Api/UsuarioViaje/GetByTelfId?telfId={1}", App.ServiceIP, App.ImeiTelefono));
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
                return;
            }
            
            del_UsuarioViaje viaje = (del_UsuarioViaje)response.Result;
            if (viaje == null)
            {
                try
                {
                    CrossGeolocator.Current.DesiredAccuracy = 50;
                    var position = await CrossGeolocator.Current.GetPositionAsync(timeout: TimeSpan.FromSeconds(5));
                    if (position != null)
                    {
                        Application.Current.MainPage = new NavigationPage(new Maps(position.Latitude, position.Longitude));
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error al obtener su ubicación por favor verifique que su ubicación esté habilitada", "Ok");
                    var deviceInfo = Xamarin.Forms.DependencyService.Get<Interfaces.IDeviceInfo>();
                    deviceInfo.ValidateLocatorPermission();
                    deviceInfo.ValidateMapsPermission();
                    Application.Current.MainPage = new NavigationPage(new Maps(-17.7862900, -63.1811700));
                }                
            }
            else
            {
                var responseUltimoPunto = await ApiService.Get<del_UsuarioViajeDetalle>(string.Format("{0}/Api/UsuarioViajeDetalle/GetUltimoPuntoByTelfId?telfId={1}", App.ServiceIP, App.ImeiTelefono));
                if (!responseUltimoPunto.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", responseUltimoPunto.Message, "Ok");
                    return;
                }

                var responseDetalle = await ApiService.Get<List<del_UsuarioViajeDetalle>>(string.Format("{0}/Api/UsuarioViajeDetalle/GetByTelfId?telfId={1}", App.ServiceIP, App.ImeiTelefono));
                if (!responseDetalle.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", responseDetalle.Message, "Ok");
                    return;
                }

                del_UsuarioViajeDetalle ultimoPunto = (del_UsuarioViajeDetalle)responseUltimoPunto.Result;
                List<del_UsuarioViajeDetalle> detalle = (List<del_UsuarioViajeDetalle>)responseDetalle.Result;
                CrossSettings.Current.AddOrUpdateValue("DeltaViajeId", viaje.iUsuarioViaje_id);
                Application.Current.MainPage = new NavigationPage(new MainPage(viaje, ultimoPunto, detalle));
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
