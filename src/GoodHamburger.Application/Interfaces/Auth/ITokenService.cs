using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        string GenerateToken(string email, string role);
    }
}
