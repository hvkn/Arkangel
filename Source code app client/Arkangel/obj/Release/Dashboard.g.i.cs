﻿#pragma checksum "..\..\Dashboard.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "46E7F86052509EA3CCFD27F2B9B1C71EECF05E47"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Arkangel;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Arkangel {
    
    
    /// <summary>
    /// Dashboard
    /// </summary>
    public partial class Dashboard : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid _dasboard;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tb_hello;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_General;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Clipboard;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_Webcam;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_Target;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_User;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_websiteusage;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_screenshot;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_Email;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Arkangel;component/dashboard.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Dashboard.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this._dasboard = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.tb_hello = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.btn_General = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\Dashboard.xaml"
            this.btn_General.Click += new System.Windows.RoutedEventHandler(this.btn_General_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_Clipboard = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\Dashboard.xaml"
            this.btn_Clipboard.Click += new System.Windows.RoutedEventHandler(this.btn_Clipboard_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 50 "..\..\Dashboard.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 6:
            this.bt_Webcam = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\Dashboard.xaml"
            this.bt_Webcam.Click += new System.Windows.RoutedEventHandler(this.bt_Webcam_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.bt_Target = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\Dashboard.xaml"
            this.bt_Target.Click += new System.Windows.RoutedEventHandler(this.bt_Target_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.bt_User = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\Dashboard.xaml"
            this.bt_User.Click += new System.Windows.RoutedEventHandler(this.bt_User_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.bt_websiteusage = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\Dashboard.xaml"
            this.bt_websiteusage.Click += new System.Windows.RoutedEventHandler(this.bt_websiteusage_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.bt_screenshot = ((System.Windows.Controls.Button)(target));
            
            #line 88 "..\..\Dashboard.xaml"
            this.bt_screenshot.Click += new System.Windows.RoutedEventHandler(this.bt_screenshot_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.bt_Email = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\Dashboard.xaml"
            this.bt_Email.Click += new System.Windows.RoutedEventHandler(this.bt_Email_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 101 "..\..\Dashboard.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

