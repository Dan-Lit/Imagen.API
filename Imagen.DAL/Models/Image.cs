using System;
using System.Collections.Generic;

namespace Imagen.DAL.Models
{
    public partial class Image
    {
        public Image()
        {
            Imagetagconfig = new HashSet<Imagetagconfig>();
        }

        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
        public bool? Tagged { get; set; }
        public bool? Processed { get; set; }
        public int? UserId { get; set; }

        public virtual Usuario? User { get; set; }
        public virtual ICollection<Imagetagconfig>? Imagetagconfig { get; set; }
    }
}
