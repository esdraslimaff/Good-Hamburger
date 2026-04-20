using GoodHamburger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Entities
{
    public class RegraDescontoItem:BaseEntity
    {
        public Guid RegraDescontoId { get; private set; }
        public TipoItem TipoItem { get; private set; }

        protected RegraDescontoItem() { }

        public RegraDescontoItem(TipoItem tipo)
        {
            TipoItem = tipo;
        }
    }
}
