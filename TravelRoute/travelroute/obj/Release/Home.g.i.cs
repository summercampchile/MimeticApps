﻿#pragma checksum "C:\Users\Ignacio\Desktop\MSSummerCamp\TravelRoute\travelroute\Home.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6F8A7D17BEEC3269EAEDDE916A261962"
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
    
    
    public partial class Home : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock appNameText;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.Panorama HomePanorama;
        
        internal Microsoft.Phone.Controls.PanoramaItem popularesPanorama;
        
        internal Microsoft.Phone.Controls.PanoramaItem misRutasPanorama;
        
        internal Microsoft.Phone.Controls.Pivot rutasPivot;
        
        internal Microsoft.Phone.Controls.PivotItem activasPivotItem;
        
        internal System.Windows.Controls.TextBlock rutasActivas;
        
        internal Microsoft.Phone.Controls.PivotItem planificadasPivotItem;
        
        internal System.Windows.Controls.TextBlock rutasPlanificadas;
        
        internal System.Windows.Controls.Grid planificadasMainGrid;
        
        internal System.Windows.Controls.Grid plan1Grid;
        
        internal System.Windows.Controls.Image plan1Image;
        
        internal System.Windows.Controls.Canvas plan1Canvas;
        
        internal System.Windows.Controls.TextBlock plan1Name;
        
        internal Microsoft.Phone.Controls.PivotItem finalizadasPivotItem;
        
        internal System.Windows.Controls.TextBlock rutasFinalizadas;
        
        internal System.Windows.Controls.Grid finalizadasMainGrid;
        
        internal System.Windows.Controls.Grid fin1Grid;
        
        internal System.Windows.Controls.Image fin1Image;
        
        internal System.Windows.Controls.Canvas fin1Canvas;
        
        internal System.Windows.Controls.TextBlock fin1Name;
        
        internal System.Windows.Controls.TextBlock fin1Place;
        
        internal System.Windows.Controls.TextBlock fin1Days;
        
        internal System.Windows.Controls.TextBlock fin1Price;
        
        internal System.Windows.Controls.Image fin1Privacy;
        
        internal System.Windows.Controls.Grid fin1Stars;
        
        internal System.Windows.Controls.Image fin1Star1;
        
        internal System.Windows.Controls.Image fin1Star2;
        
        internal System.Windows.Controls.Image fin1Star3;
        
        internal System.Windows.Controls.Image fin1Star4;
        
        internal System.Windows.Controls.Image fin1Star5;
        
        internal Microsoft.Phone.Controls.PanoramaItem buscarPanorama;
        
        internal System.Windows.Controls.Grid buscarMainGrid;
        
        internal Microsoft.Phone.Controls.PanoramaItem perfilPanorama;
        
        internal System.Windows.Controls.Grid perfilMainGrid;
        
        internal System.Windows.Controls.Button signOutButton;
        
        internal Microsoft.Phone.Controls.PanoramaItem tiendaPanorama;
        
        internal System.Windows.Controls.Grid tiendaMainGrid;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/travelroute;component/Home.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.appNameText = ((System.Windows.Controls.TextBlock)(this.FindName("appNameText")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.HomePanorama = ((Microsoft.Phone.Controls.Panorama)(this.FindName("HomePanorama")));
            this.popularesPanorama = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("popularesPanorama")));
            this.misRutasPanorama = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("misRutasPanorama")));
            this.rutasPivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("rutasPivot")));
            this.activasPivotItem = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("activasPivotItem")));
            this.rutasActivas = ((System.Windows.Controls.TextBlock)(this.FindName("rutasActivas")));
            this.planificadasPivotItem = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("planificadasPivotItem")));
            this.rutasPlanificadas = ((System.Windows.Controls.TextBlock)(this.FindName("rutasPlanificadas")));
            this.planificadasMainGrid = ((System.Windows.Controls.Grid)(this.FindName("planificadasMainGrid")));
            this.plan1Grid = ((System.Windows.Controls.Grid)(this.FindName("plan1Grid")));
            this.plan1Image = ((System.Windows.Controls.Image)(this.FindName("plan1Image")));
            this.plan1Canvas = ((System.Windows.Controls.Canvas)(this.FindName("plan1Canvas")));
            this.plan1Name = ((System.Windows.Controls.TextBlock)(this.FindName("plan1Name")));
            this.finalizadasPivotItem = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("finalizadasPivotItem")));
            this.rutasFinalizadas = ((System.Windows.Controls.TextBlock)(this.FindName("rutasFinalizadas")));
            this.finalizadasMainGrid = ((System.Windows.Controls.Grid)(this.FindName("finalizadasMainGrid")));
            this.fin1Grid = ((System.Windows.Controls.Grid)(this.FindName("fin1Grid")));
            this.fin1Image = ((System.Windows.Controls.Image)(this.FindName("fin1Image")));
            this.fin1Canvas = ((System.Windows.Controls.Canvas)(this.FindName("fin1Canvas")));
            this.fin1Name = ((System.Windows.Controls.TextBlock)(this.FindName("fin1Name")));
            this.fin1Place = ((System.Windows.Controls.TextBlock)(this.FindName("fin1Place")));
            this.fin1Days = ((System.Windows.Controls.TextBlock)(this.FindName("fin1Days")));
            this.fin1Price = ((System.Windows.Controls.TextBlock)(this.FindName("fin1Price")));
            this.fin1Privacy = ((System.Windows.Controls.Image)(this.FindName("fin1Privacy")));
            this.fin1Stars = ((System.Windows.Controls.Grid)(this.FindName("fin1Stars")));
            this.fin1Star1 = ((System.Windows.Controls.Image)(this.FindName("fin1Star1")));
            this.fin1Star2 = ((System.Windows.Controls.Image)(this.FindName("fin1Star2")));
            this.fin1Star3 = ((System.Windows.Controls.Image)(this.FindName("fin1Star3")));
            this.fin1Star4 = ((System.Windows.Controls.Image)(this.FindName("fin1Star4")));
            this.fin1Star5 = ((System.Windows.Controls.Image)(this.FindName("fin1Star5")));
            this.buscarPanorama = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("buscarPanorama")));
            this.buscarMainGrid = ((System.Windows.Controls.Grid)(this.FindName("buscarMainGrid")));
            this.perfilPanorama = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("perfilPanorama")));
            this.perfilMainGrid = ((System.Windows.Controls.Grid)(this.FindName("perfilMainGrid")));
            this.signOutButton = ((System.Windows.Controls.Button)(this.FindName("signOutButton")));
            this.tiendaPanorama = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("tiendaPanorama")));
            this.tiendaMainGrid = ((System.Windows.Controls.Grid)(this.FindName("tiendaMainGrid")));
        }
    }
}

