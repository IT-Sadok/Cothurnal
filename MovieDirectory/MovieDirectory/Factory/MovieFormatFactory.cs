using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    public static class DataMovieSaveFactory
    {
        private static Dictionary<FormatType, IDataMovieSave> _instances = new Dictionary<FormatType, IDataMovieSave>
        {
            { FormatType.Json, new MovieVaultJson() },
            { FormatType.CSV, new MovieVaultCSV() }
        };

        public static IDataMovieSave GetInstance(FormatType formatType)
        {
            return _instances.TryGetValue(formatType, out var instance) ? instance : null;
        }
    }
}
