using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FourLeggedHead.IO
{
    public static class FileReader
    {
        public static IEnumerable<string> ReadAllLines(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException(null, path);

            var lines = File.ReadAllLines(path);

            if (!lines.Any()) throw new FileIsEmptyException(null, path);

            return lines;
        }
    }
}
