using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    public class MovieActionFactory
    {
        public static IActionMovie<T> CreateAction<T>(ActionType actionType) 
            where T : MovieModelBase
        {
            StrategyBase actionInstance = actionType switch
            {
                ActionType.Create => new Create(),
                ActionType.Update => new Update(),
                ActionType.Delete => new Delete(),
                _ => throw new ArgumentException("Invalid strategy type")
            };

            return actionInstance as IActionMovie<T>;
        }
    }
}
