using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace agenda.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        [MaxLength(30)]
        public string Nombre { get; set; }
        [MaxLength(80)]
        public string Apellido { get; set; }
        [MaxLength(15)]
        public string Telefono { get; set; }

        public virtual IEnumerable<Contacto> Contactos { get; set; }
    }
}
