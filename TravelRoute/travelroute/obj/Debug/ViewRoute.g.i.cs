﻿#pragma checksum "C:\Users\Ignacio\Desktop\MSSummerCamp\TravelRoute\travelroute\ViewRoute.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "285910AC353D4CDA44D499B13CDB9D67"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace travelroute {
    
    
    public partial class ViewRoute : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock appNameText;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.Panorama RouteViewPanorama;
        
        internal Microsoft.Phone.Controls.PanoramaItem RouteMainPanorama;
        
        internal System.Windows.Controls.TextBlock routeName;
        
        internal Microsoft.Phone.Maps.Controls.Map routeMap;
        
        internal Microsoft.Phone.Controls.PanoramaItem RouteStatPanorama;
        
        internal System.Windows.Controls.Grid statMainGrid;
        
        internal System.Windows.Controls.TextBlock routeTime;
        
        internal System.Windows.Controls.TextBlock routePrice;
        
        internal System.Windows.Controls.TextBlock routeDistance;
        
        internal System.Windows.Controls.Canvas routeAppreciation;
        
        internal Microsoft.Phone.Controls.PanoramaItem RouteCommentsPanorama;
        
        internal System.Windows.Controls.Grid commentsMainGrid;
        
        internal System.Windows.Controls.TextBox userComment;
        
        internal Microsoft.Phone.Controls.Rating userRating;
        
        internal Microsoft.Phone.Controls.LongListSelector commentLLS;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/travelroute;component/ViewRoute.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.appNameText = ((System.Windows.Controls.TextBlock)(this.FindName("appNameText")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.RouteViewPanorama = ((Microsoft.Phone.Controls.Panorama)(this.FindName("RouteViewPanorama")));
            this.RouteMainPanorama = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("RouteMainPanorama")));
            this.routeName = ((System.Windows.Controls.TextBlock)(this.FindName("routeName")));
            this.routeMap = ((Microsoft.Phone.Maps.Controls.Map)(this.FindName("routeMap")));
            this.RouteStatPanorama = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("RouteStatPanorama")));
            this.statMainGrid = ((System.Windows.Controls.Grid)(this.FindName("statMainGrid")));
            this.routeTime = ((System.Windows.Controls.TextBlock)(this.FindName("routeTime")));
            this.routePrice = ((System.Windows.Controls.TextBlock)(this.FindName("routePrice")));
            this.routeDistance = ((System.Windows.Controls.TextBlock)(this.FindName("routeDistance")));
            this.routeAppreciation = ((System.Windows.Controls.Canvas)(this.FindName("routeAppreciation")));
            this.RouteCommentsPanorama = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("RouteCommentsPanorama")));
            this.commentsMainGrid = ((System.Windows.Controls.Grid)(this.FindName("commentsMainGrid")));
            this.userComment = ((System.Windows.Controls.TextBox)(this.FindName("userComment")));
            this.userRating = ((Microsoft.Phone.Controls.Rating)(this.FindName("userRating")));
            this.commentLLS = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("commentLLS")));
        }
    }
}

