﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Imagen.API
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