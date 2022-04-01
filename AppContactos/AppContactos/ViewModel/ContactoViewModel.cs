using AppContactos.Models;
using AppContactos.VieModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppContactos.ViewModel
{
    public class ContactoViewModel : BaseViewModel
    {
        #region Propiedades
        public ObservableCollection<Contacto> Contactos { get; set; }
        public Contacto contacto;
        public Contacto Contacto
        {
            get { return contacto; }
            set { contacto = value; OnPropertyChanged(); }
        }
        public string icono {
            get { return icono;  }
            set { icono = value; OnPropertyChanged(); }
        }
        #endregion

        #region Comandos
        public ICommand cmdContactoAgregar { get; set; }
        public ICommand cmdContactoDetalle { get; set; }
        public ICommand cmdContactoEliminar { get; set; }
        public ICommand cmdContactoEditar { get; set; }
        public ICommand cmdContactoCancelar { get; set; }
        public ICommand cmdContactoGrabar { get; set;}  
        public ICommand cmdContactoAgregaTelefono { get; set; }
        public ICommand cmdContactoEliminaTelefono { get; set; }
        public ICommand cmdContactoMarcarFavorito { get; set; }
        public ICommand cmdContactoVerFavoritos { get; set; }
        #endregion

        public ContactoViewModel()
        {
            Contactos = new ObservableCollection<Contacto>();
            Contactos.Add(new Contacto()
            {
                Id = Guid.NewGuid().ToString(),
                Nombre = "Carlos",
                ApellidoPaterno = "Aguirre",
                ApellidoMaterno = "Garcia",
                Organizacion = "Facultad de Telematica",
                Favorito = true,
                Telefonos = new ObservableCollection<Telefono>() {
                    new Telefono() { Id = Guid.NewGuid().ToString(), Numero = "3121348172"},
                    new Telefono() { Id = Guid.NewGuid().ToString(), Numero = "3121564441"},
                }
            });
            Contactos.Add(new Contacto()
            {
                Id = Guid.NewGuid().ToString(),
                Nombre = "Karla",
                ApellidoPaterno = "Arellano",
                ApellidoMaterno = "Robles",
                Organizacion = "Facultad de Psicologia",
                Favorito = false,
                Telefonos = new ObservableCollection<Telefono>() {
                    new Telefono() { Id = Guid.NewGuid().ToString(), Numero = "3129827645"},
                    new Telefono() { Id = Guid.NewGuid().ToString(), Numero = "3137268921"},
                }
            });

            cmdContactoDetalle = new Command<Contacto>(async (x) => await PCmdContactoDetalle(x));
            cmdContactoEliminar = new Command<Contacto>(async (x) => await PCmdContactoEliminar(x));
            cmdContactoEditar = new Command<Contacto>(async (x) => await PCmdContactoEditar(x));
            cmdContactoCancelar = new Command(async () => await PCmdContactoCancelar());
            cmdContactoAgregar = new Command(async () => await PCmdContactoAgregar());
            cmdContactoGrabar = new Command<Contacto>(async (x) => await PCmdContactoGrabar(x));
            cmdContactoAgregaTelefono = new Command(async () => await PCmdContactoAgregaTelefono());
            cmdContactoEliminaTelefono = new Command<Telefono>(async (x) => await PCmdContactoEliminaTelefono(x));
            cmdContactoMarcarFavorito = new Command<Contacto>(async (x) => await PCmdContactoMarcarFavorito(x));
            cmdContactoVerFavoritos = new Command(async () => await PCmdContactoVerFavoritos());
        }

        public async Task PCmdContactoAgregar()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AppContactos.Views.ContactoMtto(this));
        }

        public async Task PCmdContactoVerFavoritos()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AppContactos.Views.Favoritos(this));
        }

        public async Task PCmdContactoAgregaTelefono()
        {
            if(Contacto.Telefonos == null)
            {
                Contacto.Telefonos = new ObservableCollection<Telefono>();
            }
            Contacto.Telefonos.Add(new Telefono() { Id = Guid.NewGuid().ToString() });
            await Task.Delay(500);
        }

        public async Task PCmdContactoMarcarFavorito(Contacto _contacto)
        {
            int indice = -1;
            Contacto tmpContacto = Contactos.FirstOrDefault((item) => item.Id == _contacto.Id);
            if (tmpContacto != null)
            {
                indice = Contactos.IndexOf(tmpContacto);
                Contactos[indice].Favorito = !Contactos[indice].Favorito;
                if (Contactos[indice].Favorito) icono = "FavoriteFilled.png";
                else icono = "FavoriteOutlined.png";
            }
            OnPropertyChanged();
            await Task.Delay(100);
        }

        public async Task PCmdContactoEliminaTelefono(Telefono _telefono)
        {
            Contacto.Telefonos.Remove(_telefono);
            await Task.Delay(500);
        }

        public async Task PCmdContactoDetalle(Contacto _contacto)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AppContactos.Views.ContactoDetalle(_contacto, this));
        }

        public async Task PCmdContactoGrabar(Contacto _contacto)
        {
            int indice = -1;
            Contacto tmpContacto = Contactos.FirstOrDefault((item) => item.Id == _contacto.Id);
            // Si el contacto ya existe en la coleccion (se esta editando)
            if(tmpContacto != null)
            {
                indice = Contactos.IndexOf(tmpContacto);
                Contactos[indice] = _contacto;
            } else
            {
                Contactos.Add(_contacto);
            }
            OnPropertyChanged();
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public async Task PCmdContactoEliminar(Contacto _contacto)
        {
            int indice = Contactos.IndexOf(_contacto);
            if(indice >= 0)
            {
                Contactos.Remove(_contacto);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        public async Task PCmdContactoEditar(Contacto _contacto)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AppContactos.Views.ContactoMtto(_contacto, this));
        }

        public async Task PCmdContactoCancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
