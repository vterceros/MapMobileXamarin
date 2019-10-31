using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App172S.Droid.Extends;
using App172S.Extends;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(ExtMap), typeof(ExtMapRenderer))]
namespace App172S.Droid.Extends
{
    public class ExtMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        // We use a native google map for Android
        private GoogleMap _map;
        List<Position> routeCoordinates;

        public ExtMapRenderer() :base(Android.App.Application.Context)
        {
        }

        protected override void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;

            if (_map != null)
            _map.MapClick += googleMap_MapClick;
            base.OnMapReady(googleMap);

            //---------Route---------------------------------------------------
            var polylineOptions = new PolylineOptions();
            polylineOptions.InvokeColor(0x66FF0000);

            foreach (var position in routeCoordinates)
            {
                polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
            }

            NativeMap.AddPolyline(polylineOptions);
            //---------Route---------------------------------------------------
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            if (_map != null)
                _map.MapClick -= googleMap_MapClick;

            base.OnElementChanged(e);

            if (Control != null)
            ((MapView)Control).GetMapAsync(this);

            //---------Route---------------------------------------------------
            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (ExtMap)e.NewElement;
                routeCoordinates = formsMap.RouteCoordinates;
                Control.GetMapAsync(this);
            }
            //---------Route---------------------------------------------------
        }

        private void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ((ExtMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
        }
    }
}
