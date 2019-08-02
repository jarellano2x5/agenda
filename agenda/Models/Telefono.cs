using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace agenda.Models
{
    public class Telefono
    {
        public int TelefonoId { get; set; }
        public int ContactoId { get; set; }
        [MaxLength(15)]
        public string Tipo { get; set; }
        [MaxLength(15)]
        public string Numero { get; set; }

        public virtual Contacto Contacto { get; set; }
    }
}
