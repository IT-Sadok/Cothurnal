﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public record RegisterUserRequest
    (
        [Required] string Username,
        [Required] string Email,
        [Required] string Password
    );
}
