﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMovie
{
    public class Movie
    {
        public Movie(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nDescription :{Description}";
        }
    }
}