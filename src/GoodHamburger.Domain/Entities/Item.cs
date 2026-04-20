using GoodHamburger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Entities
{
    public class Item : BaseEntity
    {
        public string Nome { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public TipoItem Tipo { get; private set; }

        protected Item() { }

        public Item(string nome, decimal precoUnitario, TipoItem tipo)
        {
            Nome = nome;
            PrecoUnitario = precoUnitario;
            Tipo = tipo;
        }
    }
}
