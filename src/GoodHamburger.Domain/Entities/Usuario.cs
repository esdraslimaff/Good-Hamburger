using GoodHamburger.Domain.Enums;
using GoodHamburger.Domain.Exceptions;

namespace GoodHamburger.Domain.Entities
{
    public class Usuario : BaseEntity 
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; } 
        public TipoPerfil Perfil { get; private set; }
        public bool Ativo { get; private set; }

        protected Usuario() { }

        public Usuario(string nome, string email, string senhaHash, TipoPerfil perfil)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new DomainException("Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(email)) throw new DomainException("E-mail é obrigatório.");

            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Perfil = perfil;
            Ativo = true;
        }

        public void Inativar() => Ativo = false;
        public void Ativar() => Ativo = true;
    }
}
