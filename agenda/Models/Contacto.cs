using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace agenda.Models
{
    public class Contacto
    {
        public int ContactoId { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        [MaxLength(10)]
        public string Empresa { get; set; }
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual IEnumerable<Telefono> Telefonos { get; set; }
    }
}
