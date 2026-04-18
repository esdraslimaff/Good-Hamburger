using FluentValidation;
using GoodHamburger.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Shared.Validators
{
    public class PedidoRequestValidator : AbstractValidator<PedidoRequest>
    {
        public PedidoRequestValidator()
        {
            RuleFor(x => x.ItensIds)
            .NotEmpty().WithMessage("O pedido deve conter ao menos um item.")
            .Must(x => x.Count <= 3).WithMessage("Um pedido só pode ter no máximo 3 itens (1 sanduíche, 1 batata, 1 bebida).")
            .Must(x => x.Distinct().Count() == x.Count).WithMessage("Você não pode adicionar o mesmo item mais de uma vez.");
        }
    }
}
