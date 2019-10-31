using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Telephony;
using Xamarin.Forms;
using App172S.Interfaces;
using Android.Support.V4.App;
using Android;
using Android.Content.PM;

[assembly: Xamarin.Forms.Dependency(typeof(App172S.Droid.Implementations.DeviceInfo))]
namespace App172S.Droid.Implementations
{
    public class DeviceInfo : IDeviceInfo
    {
        private static int REQUEST_PHONESTATE = 0;
        private static int REQUEST_LOCATOR = 1;
        private static int REQUEST_MAPS = 2;
        public string GetImei()
        {
            //var tMgr = (TelephonyManager)Forms.Context.ApplicationContext.GetSystemService(Android.Content.Context.TelephonyService);
            if (ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.ReadPhoneState)
                != Permission.Granted)
            {
                ActivityCompat.RequestPermissions((MainActivity)(Forms.Context), new String[] { Manifest.Permission.ReadPhoneState }, REQUEST_PHONESTATE);
            }
                
            if (ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.ReadPhoneState)
                == Permission.Granted)
            {
                var tMgr = (TelephonyManager)Android.App.Application.Context.ApplicationContext.GetSystemService(Android.Content.Context.TelephonyService);
                return tMgr.Imei;
            }
            return string.Empty;
        }

        public void ValidateLocatorPermission()
        {
            if (ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessCoarseLocation)
                != Permission.Granted
                || ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessFineLocation)
                != Permission.Granted)
            {
                ActivityCompat.RequestPermissions((MainActivity)(Forms.Context), new String[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation }, REQUEST_LOCATOR);
            }
        }

        public void ValidateMapsPermission()
        {
            if (ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessCoarseLocation) != Permission.Granted
                || ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessFineLocation) != Permission.Granted
                || ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessLocationExtraCommands) != Permission.Granted
                || ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessMockLocation) != Permission.Granted
                || ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessNetworkState) != Permission.Granted
                || ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.AccessWifiState) != Permission.Granted
                || ActivityCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.Internet) != Permission.Granted

                )
            {
                ActivityCompat.RequestPermissions((MainActivity)(Forms.Context), new String[] {
                     Manifest.Permission.AccessCoarseLocation
                    ,Manifest.Permission.AccessFineLocation
                    ,Manifest.Permission.AccessMockLocation
                    ,Manifest.Permission.AccessNetworkState
                    ,Manifest.Permission.AccessWifiState
                    ,Manifest.Permission.Internet
                }
                , REQUEST_MAPS);
            }           
        }
    }
}