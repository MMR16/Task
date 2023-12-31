﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AuthModels
{
    public class RegisterModel
    {
        [Required,MaxLength(50)]
        public string UserName { get; set; }
        [Required, MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(100)]

        public string Password { get; set; }


    }
}
