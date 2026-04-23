using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataUltimaAlteracao { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;
        }

        public void RegistrarAlteracao()
        {
            DataUltimaAlteracao = DateTime.UtcNow;
        }
    }
}
