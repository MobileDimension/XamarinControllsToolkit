using testcontrolls.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testcontrols.Core.DI;
using Xamarin.Forms;

namespace testcontrols
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Bootstrapper.RegisterIoC();
            MainPage = new NavigationPage(new testcontrols.LoginPage());
        }



        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
