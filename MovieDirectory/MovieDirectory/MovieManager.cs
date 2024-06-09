using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    public class MovieManager
    {
        private IActionMovie? _action;

        public void SetAction(IActionMovie action)
        {
            _action = action;
        }

        public void ExecuteAction(Dictionary<int, Movie> vault, params object[] data)
        {
            if (_action == null)
                throw new InvalidOperationException("Action is not set. Please set an action before executing.");

            _action.Action(vault, data);
        }
    }
}
