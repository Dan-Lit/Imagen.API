﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Imagen.API
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