﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENERGY_NOW_BE.Core.Entity.Auth
{
    public class ConfirmEmailModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
