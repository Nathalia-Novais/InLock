﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Domains
{
    public class UsuarioDomain
    {
        public int idUsuario { get; set; }
        public int idTipoUsuario { get; set; }
        public string email { get; set; }
        public string senha { get; set; }

        public TipoUsuarioDomain tipousuario { get; set; }
    }
}
