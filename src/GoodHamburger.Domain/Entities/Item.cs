using GoodHamburger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public TipoItem Tipo { get; private set; }

        protected Item() { }

        public Item(string nome, decimal preco, TipoItem tipo)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Preco = preco;
            Tipo = tipo;
        }
    }
}
