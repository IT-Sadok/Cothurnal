﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    public record UpdateMovieModel(int id, string description) : MovieModelBase;
}
