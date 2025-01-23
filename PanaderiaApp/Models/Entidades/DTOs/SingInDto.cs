using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades.DTOs
{
    public class SingInDto
    {
        [Required (ErrorMessage = "Username requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password requerido")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
