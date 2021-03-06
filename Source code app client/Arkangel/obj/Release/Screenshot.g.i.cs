﻿#pragma checksum "..\..\Screenshot.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "225E7DA42BC6ACADC8B404179E3D8C7A4CD9EB05"
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
    /// Screenshot
    /// </summary>
    public partial class Screenshot : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\Screenshot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridCursor;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Screenshot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridMain;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\Screenshot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cb_enable;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Screenshot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_hours;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\Screenshot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_minutes;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\Screenshot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cb_timeNuser;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\Screenshot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cb_doubleScr;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\Screenshot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cb_enDel;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\Screenshot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cb_daysDel;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\Screenshot.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sld_quaity;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\Screenshot.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Arkangel;component/screenshot.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Screenshot.xaml"
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
            this.GridCursor = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.GridMain = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.cb_enable = ((System.Windows.Controls.CheckBox)(target));
            
            #line 30 "..\..\Screenshot.xaml"
            this.cb_enable.Click += new System.Windows.RoutedEventHandler(this.cb_enable_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tb_hours = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\Screenshot.xaml"
            this.tb_hours.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tb_hours_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tb_minutes = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\Screenshot.xaml"
            this.tb_minutes.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tb_minutes_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cb_timeNuser = ((System.Windows.Controls.CheckBox)(target));
            
            #line 43 "..\..\Screenshot.xaml"
            this.cb_timeNuser.Click += new System.Windows.RoutedEventHandler(this.cb_timeNuser_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.cb_doubleScr = ((System.Windows.Controls.CheckBox)(target));
            
            #line 44 "..\..\Screenshot.xaml"
            this.cb_doubleScr.Click += new System.Windows.RoutedEventHandler(this.cb_doubleScr_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cb_enDel = ((System.Windows.Controls.CheckBox)(target));
            
            #line 46 "..\..\Screenshot.xaml"
            this.cb_enDel.Click += new System.Windows.RoutedEventHandler(this.cb_enDel_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cb_daysDel = ((System.Windows.Controls.TextBox)(target));
            
            #line 48 "..\..\Screenshot.xaml"
            this.cb_daysDel.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.cb_daysDel_TextChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.sld_quaity = ((System.Windows.Controls.Slider)(target));
            
            #line 56 "..\..\Screenshot.xaml"
            this.sld_quaity.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.sld_quaity_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.bt_OK = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\Screenshot.xaml"
            this.bt_OK.Click += new System.Windows.RoutedEventHandler(this.bt_OK_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

