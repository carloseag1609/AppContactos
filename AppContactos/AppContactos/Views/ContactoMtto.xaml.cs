using AppContactos.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppContactos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactoMtto : ContentPage
    {
        public ContactoMtto(ContactoViewModel vm)
        {
            // Nuevo contacto
            InitializeComponent();
            vm.Contacto = new Models.Contacto()
            {
                Id = Guid.NewGuid().ToString()
            };
            vm.Contacto.Telefonos = new ObservableCollection<Models.Telefono>();
            BindingContext = vm;
            Title = "Nuevo contacto";
        }

        public ContactoMtto(Models.Contacto contacto, ContactoViewModel vm)
        {
            // Editar contacto
            InitializeComponent();
            vm.Contacto = new Models.Contacto();
            vm.Contacto = contacto;
            BindingContext = vm;
            Title = "Editar contacto";
        }
    }
}