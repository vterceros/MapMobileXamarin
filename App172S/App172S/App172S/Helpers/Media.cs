using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App172S.Helpers
{
    public static class Media
    {
        public static async Task<MediaFile> GetFoto()
        {
            try
            {
                //Se obtienen si tienen permisos 
                var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                //Se verifica si no tiene permisos
                if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
                {
                    //Se piden los permisos
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Camera))
                        cameraStatus = results[Permission.Camera];
                    if (results.ContainsKey(Permission.Storage))
                        storageStatus = results[Permission.Storage];
                }

                //Se verifica si tiene los permisos
                if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
                {
                    var camera = CrossMedia.Current;

                    if (!camera.IsCameraAvailable)
                    {
                        App.ShowAlert("Cámara no disponible", "Su dispositivo no cuenta o no está disponible la cámara.");
                        return null;
                    }


                    var accion = await App.Current.MainPage.DisplayActionSheet("¿Qué acción desea realizar?", "Cancelar", null, new string[] { "Tomar foto desde Cámara", "Seleccionar foto desde Galería" });

                    MediaFile file = null;

                    if (accion == "Tomar foto desde Cámara")
                    {
                        //Se verifica si se puede tomar foto desde camara
                        if (camera.IsTakePhotoSupported)
                        {
                            //Se obtiene la fecha del dia
                            var name = DateTime.Now.ToString("DIS_yyyyMMdd_HHmmss");
                            //Se toma la foto
                            file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                            {
                                Directory = "Distribucion",
                                Name = name + ".jpg",
                                PhotoSize = PhotoSize.Small,
                                MaxWidthHeight = 1920,
                                SaveToAlbum = true
                            });
                        }
                        else
                        {
                            App.ShowAlert("Cámara no disponible", "Su dispositivo no cuenta o no está disponible la cámara.");
                            return null;
                        }
                    }
                    if (accion == "Seleccionar foto desde Galería")
                    {
                        //Se verifica si se puede seleccionar foto desde galeria
                        if (camera.IsTakePhotoSupported)
                        {
                            //Se selecciona la foto
                            file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                            {
                                PhotoSize = PhotoSize.Small,
                                MaxWidthHeight = 1920
                            });
                        }
                        else
                        {
                            App.ShowAlert("Galería no disponible", "Su dispositivo no cuenta o no está disponible la Galería.");
                            return null;
                        }
                    }


                    /*if (file == null)
                    {
                        App.ShowAlert("Posición no encontrada", "Por favor, intente nuevamente.");
                        return null;
                    }*/
                    return file;
                }
                else
                {
                    App.ShowAlert("Permiso denegado", "No se ha podido tomar o seleccionar fotos. Por favor, brinde los permisos de Cámara y Almacenamiento.");
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
