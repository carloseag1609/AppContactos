using AppContactos.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppContactos
{
    public class ContactoFavoritoDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ContactoFavoritoTemplate { get; set; }
        public DataTemplate ContactoNormalTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object contacto, BindableObject container)
        {
            if(((AppContactos.Models.Contacto)contacto).Favorito)
            {
                return ContactoFavoritoTemplate;
            } else
            {
                return ContactoNormalTemplate;
            }
        }
    }
}
