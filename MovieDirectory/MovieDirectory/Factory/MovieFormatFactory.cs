using DataMovie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDirectory
{
    public class DataMovieSaveFactory
    {
        public static IDataRepository GetInstance(FormatType formatType)
        {
            return formatType switch
            {
                FormatType.Json => new MovieVaultJson(),
                FormatType.CSV => new MovieVaultCSV(),
                _ => throw new ArgumentException("Invalid strategy type")
            };
        }
    }
}
