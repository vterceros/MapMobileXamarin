using App172S.Models.Negocio;
using App172S.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace App172S.ViewModels
{
    public class SecondViewModel : INotifyPropertyChanged
    {
        ObservableCollection<seg_Empresa> listaEmpresas = null;
        public ObservableCollection<seg_Empresa> ListaEmpresas
        {
            get { return listaEmpresas; }
            set { SetProperty(ref listaEmpresas, value); }
        }
        string test = string.Empty;
        public string Test
        {
            get { return test; }
            set { SetProperty(ref test, value); }
        }
        public SecondViewModel()
        {
            this.Test = "Victor ctor";
            this.LoadData();
        }

        private async void LoadData()
        {
            var response = await ApiService.Get<List<seg_Empresa>>(string.Format("{0}/api/empresa/getall", App.ServiceIP));
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message,"Ok");
                return;
            }

            var list = (List<seg_Empresa>)response.Result;
            this.Test = "Hello world";
            this.ListaEmpresas = new ObservableCollection<seg_Empresa>(list);
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
