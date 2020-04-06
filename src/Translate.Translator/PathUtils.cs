using System;
using System.Collections.Generic;
using System.Text;

namespace Translate.Translator
{
    static class PathUtils
    {
        public static string RemoveEmptyDirectory(string path)
        {
            return path.Replace(@"\\", @"\");
        }
    }
}
