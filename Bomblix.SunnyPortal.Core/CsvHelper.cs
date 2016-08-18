using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Bomblix.SunnyPortal.Core
{
    public static class CsvHelper
    {
        private const char LineSpliter = ';';
        private const int CsvColumnCount = 2;
        private const int LabelColumnNumber = 0;
        private const int ValueColumnNumber = 1;

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
                string key;
                float value;
                if ( GetRowValues( line, out key, out value ) )
                {
                    result.Add( key, value );
                }
                line = reader.ReadLine();
            }
            return result;
        }

        private static bool GetRowValues( string line, out string key, out float value )
        {
            var splitedValues = line.Split( LineSpliter );

            if ( splitedValues.Length >= CsvColumnCount && !string.IsNullOrEmpty( splitedValues[ ValueColumnNumber ] ) )
            {
                key = splitedValues[ LabelColumnNumber ];
                return float.TryParse( splitedValues[ ValueColumnNumber ], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value );
            }

            key = null;
            value = 0.0f;
            return false;
        }
    }
}
