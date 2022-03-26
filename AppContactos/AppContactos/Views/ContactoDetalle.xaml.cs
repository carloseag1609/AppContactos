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
    public partial class ContactoDetalle : ContentPage
    {
        public ContactoDetalle(Models.Contacto contacto, ContactoViewModel vm)
        {
            InitializeComponent();
            vm.Contacto = contacto;
            this.BindingContext = vm;
        }
    }
}