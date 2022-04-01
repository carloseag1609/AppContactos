using AppContactos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppContactos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Favoritos : ContentPage
    {
        public Favoritos(ContactoViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}