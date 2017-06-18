using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testcontrols.ViewModels;
using Xamarin.Forms;

namespace testcontrols
{
    public partial class AuthorizationView : ContentView
    {
        public AuthorizationView()
        {
            InitializeComponent();
            this.BindingContext = new AuthorizationViewModel();
        }
    }
}
