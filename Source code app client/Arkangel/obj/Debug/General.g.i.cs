﻿#pragma checksum "..\..\General.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A9005C301B7D5FFF1A434ABB0CADEA9D7DB0023C"
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
using Dragablz;
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
    /// General
    /// </summary>
    public partial class General : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridCursor;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridMain;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cb_taskmanager;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cb_registry;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cb_runstart;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bt_OK;
        
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
            System.Uri resourceLocater = new System.Uri("/Arkangel;component/general.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\General.xaml"
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
            
            #line 24 "..\..\General.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.GridCursor = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.GridMain = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.cb_taskmanager = ((System.Windows.Controls.CheckBox)(target));
            
            #line 32 "..\..\General.xaml"
            this.cb_taskmanager.Click += new System.Windows.RoutedEventHandler(this.cb_taskmanager_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cb_registry = ((System.Windows.Controls.CheckBox)(target));
            
            #line 33 "..\..\General.xaml"
            this.cb_registry.Click += new System.Windows.RoutedEventHandler(this.cb_registry_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cb_runstart = ((System.Windows.Controls.CheckBox)(target));
            
            #line 39 "..\..\General.xaml"
            this.cb_runstart.Click += new System.Windows.RoutedEventHandler(this.cb_runstart_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.bt_OK = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\General.xaml"
            this.bt_OK.Click += new System.Windows.RoutedEventHandler(this.bt_OK_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

