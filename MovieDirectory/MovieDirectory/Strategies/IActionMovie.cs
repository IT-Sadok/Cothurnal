﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMovie;

namespace MovieDirectory
{ 
    public interface IActionMovie<T>
    {
        public void Action(IDataRepository format, T data);
    }
}
