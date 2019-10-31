using App172S.Helpers.SQLite;
using App172S.Models.Negocio;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Settings;
using System;
using System.Threading.Tasks;

namespace App172S.Helpers
{
    public class Utils
    {
        public Utils()
        {

        }
        static Utils()
        {

        }

        public static bool HasConnectivity()
        {
            bool isConnected = CrossConnectivity.Current.IsConnected;
            return isConnected;
        }

        public static async Task<bool> IsReachable(string url = "google.com")
        {
            bool isReachable = await CrossConnectivity.Current.IsRemoteReachable(url);
            return isReachable;
        }

        public static async Task<bool> HasConnectivityAndIsReachable(string url = "google.com")
        {
            bool isConnected = HasConnectivity();
            bool isReachable = await IsReachable(url);
            return isConnected && isReachable;
        }

        public static async Task PostLocation()
        {            
            //Se obtiene la posicion actual
            var position = await CrossGeolocator.Current.GetPositionAsync(timeout: TimeSpan.FromSeconds(5));
            //Se obtiene el ID del viaje
            var viajeId = CrossSettings.Current.GetValueOrDefault("DeltaViajeId", 0);
            var response = await Services.ApiService.Get<del_UsuarioViaje>(string.Format("{0}/Api/UsuarioViaje/GetByTelfId?telfId={1}", App.ServiceIP, App.ImeiTelefono));
            if (response.IsSuccess)
            {
                del_UsuarioViaje viaje = (del_UsuarioViaje)response.Result;
                CrossSettings.Current.AddOrUpdateValue("DeltaViajeId", viaje.iUsuarioViaje_id);
                viajeId = viaje.iUsuarioViaje_id;
            }

            //Se crea el URL a donde se va a realizar el POST
            var URLPost = string.Format("{0}/Api/UsuarioViajeDetalle/PostDetalle?viajeId={1}&lat={2}&lon={3}", 
                App.ServiceIP, viajeId, position.Latitude, position.Longitude);

            //Se crea la conexion con la BD local SQLite
            var db = new ViajeDetalleDB();
            //Se verifica si no hay conexion
            if (!HasConnectivity())
            {
                //Se crea un nuevo detalle a almacenar
                var detalle = new del_UsuarioViajeDetalle()
                {
                    iUsuarioViaje_id = viajeId,
                    fLat = (decimal)position.Latitude,
                    fLon = (decimal)position.Longitude,
                    dtFecha = DateTime.Now
                };
                //Se almacena el detalle en la BD local
                db.SaveItem(detalle);

                return;
            }

            //Se envia el resultado
            var result = await Services.ApiService.Post<int>(URLPost.Replace(",","."));
            //Si el post fue exitoso se envia los detalles que no pudieron ser enviados
            if (result.IsSuccess)
            {
                //Se obtiene los detalles no enviados obtenidas de la BD
                var detallesNoEnviados = db.GetAll();
                //Se recorre la lista de no enviados
                for (var i = 0; i < detallesNoEnviados.Count; i++)
                {
                    //Se obtiene el detalle de la lista
                    var detalle = detallesNoEnviados[i];
                    //Se envia los no enviados
                    var res = await Services.ApiService.Post<int>(URLPost);
                    //Si fue exitoso el envio
                    if (res.IsSuccess)
                    {
                        //Se elimina el detalle de la BD local
                        db.DeleteItem(detalle.iUsuarioViajeDetalle_id);
                    }
                    //Si no fue exitoso el envio
                    else
                    {
                        //Se termina el bucle
                        i = detallesNoEnviados.Count;
                    }
                }
            }
            //Si no fue exitoso el envio del detalle
            else
            {
                //Se crea un nuevo detalle a almacenar
                var detalle = new del_UsuarioViajeDetalle()
                {
                    iUsuarioViaje_id = viajeId,
                    fLat = (decimal)position.Latitude,
                    fLon = (decimal)position.Longitude,
                    dtFecha = DateTime.Now
                };
                //Se almacena el detalle en la BD local
                db.SaveItem(detalle);
            }

            return;
        }
    }


}
