using System;
using System.Collections.Generic;

namespace Imagen.DAL.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Image = new HashSet<Image>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }

        public virtual ICollection<Image> Image { get; set; }
    }
}
