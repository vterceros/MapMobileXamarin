using App172S.Models.Negocio;
using App172S.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace App172S.ViewModels
{
    public class ImagePreviewerViewModel : INotifyPropertyChanged
    {
        //Variable para el campo de busqueda
        string buscador;
        //Booleano para indicar si esta cargando
        bool isLoading;
        //Booleano para mostrar el mensaje
        bool esVisibleTexto;
        //Booleano para mostrar la imagen
        bool esVisibleImagen;
        //Texto de carga
        string textoCarga;
        //Imagen a mostrar
        ImageSource imagen;

        //Se implementa la interfaz
        public event PropertyChangedEventHandler PropertyChanged;

        public ImagePreviewerViewModel()
        {
            CargarImagen();
        }

        private void CargarImagen()
        {
            //Se inicia el texto de carga
            TextoCarga = "Descargando la imagen, por favor espere...";
            //Se muestra el icono de carga
            IsLoading = true;
            //Se muestra el texto de carga
            EsVisibleTexto = true;
            //Se oculta la imagen
            EsVisibleImagen = false;
            //Se inicia la suscripcion
            MessagingCenter.Subscribe<string>(this, "ImagenUrl", async (url) => {                
                //Se descarga la imagen
                var response = await ApiService.Get<del_UsuarioViajeDocumentos>(url);
                if (!response.IsSuccess)
                {
                    TextoCarga = "Ha ocurrido un error, no se ha podido descargar la imagen.";
                    //Se muestra el texto de carga
                    EsVisibleTexto = true;
                    await App.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");
                }
                del_UsuarioViajeDocumentos img = (del_UsuarioViajeDocumentos)response.Result;
                if (img != null && img.iDocumento != null)
                {                    
                    Imagen = ImageSource.FromStream(() => new MemoryStream(img.iDocumento));
                    //Se muestra el texto de carga
                    EsVisibleImagen = true;
                    //Se muestra el texto de carga
                    EsVisibleTexto = false;
                }
                //Se muestra el icono de carga
                IsLoading = false;                
                //Se termina la suscripcion
                MessagingCenter.Unsubscribe<string>(this, "ImagenUrl");
            });
        }

        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged("IsLoading");
                }
            }
        }
        public bool EsVisibleImagen
        {
            get
            {
                return esVisibleImagen;
            }
            set
            {
                if (esVisibleImagen != value)
                {
                    esVisibleImagen = value;
                    OnPropertyChanged("EsVisibleImagen");
                }
            }
        }
        public bool EsVisibleTexto
        {
            get
            {
                return esVisibleTexto;
            }
            set
            {
                if (esVisibleTexto != value)
                {
                    esVisibleTexto = value;
                    OnPropertyChanged("EsVisibleTexto");
                }
            }
        }
        public string TextoCarga
        {
            get
            {
                return textoCarga;
            }
            set
            {
                if (textoCarga != value)
                {
                    textoCarga = value;
                    OnPropertyChanged("TextoCarga");
                }
            }
        }
        public ImageSource Imagen
        {
            get
            {
                return imagen;
            }
            set
            {
                if (imagen != value)
                {
                    imagen = value;
                    OnPropertyChanged("Imagen");
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
