using App172S.Helpers;
using App172S.Interfaces;
using App172S.Models.Negocio;
using App172S.Services;
using App172S.ViewModels;
using Plugin.Geolocator;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace App172S.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Maps : ContentPage
	{
        private Timer timer;

        public Maps(double lat, double lon)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            MapView.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(lat, lon), Distance.FromKilometers(50)));
            this.txtOrigenDestino.Text = "No hay viaje en progreso";
            this.txtFechaSalida.Text = "";
            this.btnRefrescar.Text = "Actualizar";

            //var job = DependencyService.Get<IJobScheduler>();
            //job.CancelAllJobs();
            //job.ScheduleJob();
        }
        public Maps(del_UsuarioViaje viaje, del_UsuarioViajeDetalle ultimoPunto, List<del_UsuarioViajeDetalle> detalle)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            this.LoadData(viaje, ultimoPunto, detalle);
            this.btnRefrescar.Text = "Refrescar Posición";
            //this.GetCurrent();
        }

        private void Street_OnClicked(object sender, EventArgs e)
        {
            MapView.MapType = MapType.Street;
        }


        private void Hybrid_OnClicked(object sender, EventArgs e)
        {
            MapView.MapType = MapType.Hybrid;
        }

        private void Satellite_OnClicked(object sender, EventArgs e)
        {
            MapView.MapType = MapType.Satellite;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
        public static double CalculateDistance(Position location1, Position location2)
        {
            double circumference = 40000.0; // Earth's circumference at the equator in km
            double distance = 0.0;

            //Calculate radians
            double latitude1Rad = DegreesToRadians(location1.Latitude);
            double longitude1Rad = DegreesToRadians(location1.Longitude);
            double latititude2Rad = DegreesToRadians(location2.Latitude);
            double longitude2Rad = DegreesToRadians(location2.Longitude);

            double logitudeDiff = Math.Abs(longitude1Rad - longitude2Rad);

            if (logitudeDiff > Math.PI)
            {
                logitudeDiff = 2.0 * Math.PI - logitudeDiff;
            }

            double angleCalculation =
                Math.Acos(
                    Math.Sin(latititude2Rad) * Math.Sin(latitude1Rad) +
                    Math.Cos(latititude2Rad) * Math.Cos(latitude1Rad) * Math.Cos(logitudeDiff));

            distance = circumference * angleCalculation / (2.0 * Math.PI);

            return distance;
        }

        private async void MapView_Tapped(object sender, Extends.MapTapEventArgs e)
        {
            //var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
            //if (answer)
            //{
            //    var pinDestino = new Pin()
            //    {
            //        Position = new Position(e.Position.Latitude, e.Position.Longitude),
            //        Label = "Nuevo",
            //        Type = PinType.SearchResult,
            //    };
            //    MapView.Pins.Add(pinDestino);
            //    MapView.RouteCoordinates.Add(pinDestino.Position);
            //    MapView.MoveToRegion(MapSpan.FromCenterAndRadius(pinDestino.Position, Distance.FromKilometers(2500)));
            //}
        }

        protected void GetCurrent(int interval = 2)
        {
            timer = new Timer();
            timer.Interval = interval * 60 * 1000; // 1 milliseconds  
            //timer.Interval = 20 * 1000; // 1 milliseconds  
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            
            DateTime ultimaU = CrossSettings.Current.GetValueOrDefault("DeltaUltimaUbicacionHora", new DateTime(2000,1,1));
            if (ultimaU == new DateTime(2000,1,1))
            {
                CrossSettings.Current.AddOrUpdateValue("DeltaUltimaUbicacionHora", DateTime.Now);
            }
            else if ((DateTime.Now - ultimaU).TotalMinutes >= 30)
            {
                CrossSettings.Current.AddOrUpdateValue("DeltaUltimaUbicacionHora", DateTime.Now);
            }
            else
            {
                return;
            }
            CrossGeolocator.Current.DesiredAccuracy = 50;
            Plugin.Geolocator.Abstractions.Position position = null;
            Task getPosition = Task.Run(async () =>
            {
                try
                {
                    await Utils.PostLocation();
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error al obtener su ubicación por favor verifique que su ubicación esté habilitada", "Ok");
                    var deviceInfo = Xamarin.Forms.DependencyService.Get<Interfaces.IDeviceInfo>();
                    deviceInfo.ValidateLocatorPermission();
                    deviceInfo.ValidateMapsPermission();
                }
                
            });
        }

        #region API

        private void LoadData(del_UsuarioViaje viaje, del_UsuarioViajeDetalle ultimoPunto, List<del_UsuarioViajeDetalle> detalle)
        {
            if (viaje == null || viaje.fLatOrigen == null)
            {
                MapView.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-17.7862900, -63.1811700), Distance.FromKilometers(50)));
                this.txtOrigenDestino.Text = "No hay viaje en progreso";
                this.txtFechaSalida.Text = "";
                return;
            }
            this.txtOrigenDestino.Text = string.Format("{0} {1} - {2} {3}",viaje.CiudadOrigen,viaje.PaisOrigen,viaje.CiudadDestino,viaje.PaisDestino);
            this.txtFechaSalida.Text = string.Format("Salida: {0}",viaje.dtFechaInicio.ToShortDateString());
            var pinOrigen = new Pin()
            {
                Position = new Position((double)viaje.fLatOrigen, (double)viaje.fLonOrigen),
                Label = "Origen",
                Type = PinType.SearchResult,
            };
            var pinDestino = new Pin()
            {
                Position = new Position((double)viaje.fLatDestino, (double)viaje.fLonDestino),
                Label = "Destino",
                Type = PinType.SearchResult,
            };
            MapView.Pins.Add(pinOrigen);
            MapView.Pins.Add(pinDestino);

           
            if (ultimoPunto == null || ultimoPunto.fLat == null)
            {
                return;
            }

            var pinActual = new Pin()
            {
                Position = new Position((double)ultimoPunto.fLat, (double)ultimoPunto.fLon),
                Label = "Carga",
                Type = PinType.SearchResult,
            };
            MapView.Pins.Add(pinActual);

          
            if (detalle == null)
            {
                return;
            }
            MapView.RouteCoordinates.Add(pinOrigen.Position);
            foreach (del_UsuarioViajeDetalle item in detalle)
            {
                MapView.RouteCoordinates.Add(new Position((double)item.fLat, (double)item.fLon));
            }
            
            MapView.MoveToRegion(MapSpan.FromCenterAndRadius(pinActual.Position, Distance.FromKilometers(CalculateDistance(pinOrigen.Position, pinDestino.Position))));
        }

        #endregion

        private void btnRefrescar_Clicked(object sender, EventArgs e)
        {
            MainViewModel.Instance().MapsPrevious = new MapsPreviousViewModel();
            Application.Current.MainPage = new MapsPrevious();
        }
    }
}