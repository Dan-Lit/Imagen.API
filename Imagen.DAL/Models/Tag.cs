using System;
using System.Collections.Generic;

namespace Imagen.DAL.Models
{
    public partial class Tag
    {
        public Tag()
        {
            Imagetagconfig = new HashSet<Imagetagconfig>();
        }

        public string TagId { get; set; }

        public virtual ICollection<Imagetagconfig> Imagetagconfig { get; set; }
    }
}
