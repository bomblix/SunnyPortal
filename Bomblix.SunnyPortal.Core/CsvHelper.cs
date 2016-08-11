using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Bomblix.SunnyPortal.Core
{
    public static class CsvHelper
    {
        /// <summary>
        /// Method extracts data from csv to Dicionary
        /// </summary>
        public static Dictionary<string, float> ExtractToDictionary( string csvContent )
        {
            var result = new Dictionary<string, float>();

            StringReader reader = new StringReader( csvContent );
            var line = reader.ReadLine(); // skipped the first line
            line = reader.ReadLine();
            while ( !string.IsNullOrEmpty( line ) )
            {
                float value;
                var splitedValues = line.Split( ';' );
                if ( splitedValues.Length >= 2 && !string.IsNullOrEmpty( splitedValues[ 1 ] ) && float.TryParse( splitedValues[ 1 ], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value ) )
                {
                    result.Add( splitedValues[ 0 ], value );
                }
                line = reader.ReadLine();
            }
            return result;
        }
    }
}
