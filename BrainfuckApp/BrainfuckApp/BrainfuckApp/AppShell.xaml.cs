using BrainfuckApp.ViewModels;
using BrainfuckApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BrainfuckApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
        }

    }
}
