using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
