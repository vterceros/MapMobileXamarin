using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace App172S.Extends
{
    /// <summary>
    /// Extended map:
    /// allow user to tap a point on the map to get the relative position.
    /// </summary>
    public class ExtMap : Map
    {
        /// <summary>
        /// Event thrown when the user taps on the map
        /// </summary>
        public event EventHandler<MapTapEventArgs> Tapped;
        public List<Position> RouteCoordinates { get; set; }        
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ExtMap() : base()
        {
            RouteCoordinates = new List<Position>();
        }

        /// <summary>
        /// Constructor that takes a region
        /// </summary>
        /// <param name="region"></param>
        public ExtMap(MapSpan region)
            : base(region)
        {
            RouteCoordinates = new List<Position>();
        }

        #endregion

        public void OnTap(Position coordinate)
        {
            OnTap(new MapTapEventArgs { Position = coordinate });
        }

        protected virtual void OnTap(MapTapEventArgs e)
        {
            var handler = Tapped;

            if (handler != null)
                handler(this, e);
        }
       
    }

    /// <summary>
    /// Event args used with maps, when the user tap on it
    /// </summary>
    public class MapTapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
}
