﻿#pragma checksum "C:\Users\Ignacio\Desktop\MSSummerCamp\TravelRoute\travelroute\NewRegister.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F3120F40860A3F8084C097146B507050"
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
using Microsoft.Phone.Shell;
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
    
    
    public partial class NewRegister : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock appName;
        
        internal System.Windows.Controls.TextBlock newRouteTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox registerName;
        
        internal System.Windows.Controls.Image registerImage;
        
        internal System.Windows.Controls.ScrollViewer registerDescriptionScrollViewer;
        
        internal System.Windows.Controls.TextBox registerDescription;
        
        internal System.Windows.Controls.TextBox registerExpenses;
        
        internal Microsoft.Phone.Controls.Rating registerAppreciation;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton saveButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/travelroute;component/NewRegister.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.appName = ((System.Windows.Controls.TextBlock)(this.FindName("appName")));
            this.newRouteTitle = ((System.Windows.Controls.TextBlock)(this.FindName("newRouteTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.registerName = ((System.Windows.Controls.TextBox)(this.FindName("registerName")));
            this.registerImage = ((System.Windows.Controls.Image)(this.FindName("registerImage")));
            this.registerDescriptionScrollViewer = ((System.Windows.Controls.ScrollViewer)(this.FindName("registerDescriptionScrollViewer")));
            this.registerDescription = ((System.Windows.Controls.TextBox)(this.FindName("registerDescription")));
            this.registerExpenses = ((System.Windows.Controls.TextBox)(this.FindName("registerExpenses")));
            this.registerAppreciation = ((Microsoft.Phone.Controls.Rating)(this.FindName("registerAppreciation")));
            this.saveButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("saveButton")));
        }
    }
}

