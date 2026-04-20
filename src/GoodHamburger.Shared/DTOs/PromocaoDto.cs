using GoodHamburger.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Shared.DTOs
{
    public class PromocaoDto
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Percentual { get; set; }
        public List<TipoItem> Requisitos { get; set; } = new();
    }
}
