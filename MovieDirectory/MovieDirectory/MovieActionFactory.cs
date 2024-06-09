using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    public class MovieActionFactory
    {
        public static IActionMovie GetAction(ActionType type)
        {
            return type switch
            {
                ActionType.Create => new Create(),
                ActionType.Update => new Update(),
                ActionType.Delete => new Delete(),
                _ => throw new ArgumentException("Invalid strategy type")
            };
        }
    }
}
