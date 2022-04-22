using System;
using System.Collections.Generic;

namespace Imagen.DAL.Models
{
    public partial class Imagetagconfig
    {
        public string ImageId { get; set; }
        public string TagId { get; set; }

        public virtual Image Image { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
