﻿#pragma checksum "C:\Users\Ignacio\Desktop\MSSummerCamp\TravelRoute\travelroute\Login.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "897A1EE2872D8D76E116658877E7BEF2"
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
    
    
    public partial class Login : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock appNameText;
        
        internal System.Windows.Controls.TextBlock pageNameText;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Button facebookLoginButton;
        
        internal System.Windows.Controls.Button twitterLoginButton;
        
        internal System.Windows.Controls.TextBlock skipLoginButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/travelroute;component/Login.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.appNameText = ((System.Windows.Controls.TextBlock)(this.FindName("appNameText")));
            this.pageNameText = ((System.Windows.Controls.TextBlock)(this.FindName("pageNameText")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.facebookLoginButton = ((System.Windows.Controls.Button)(this.FindName("facebookLoginButton")));
            this.twitterLoginButton = ((System.Windows.Controls.Button)(this.FindName("twitterLoginButton")));
            this.skipLoginButton = ((System.Windows.Controls.TextBlock)(this.FindName("skipLoginButton")));
        }
    }
}

