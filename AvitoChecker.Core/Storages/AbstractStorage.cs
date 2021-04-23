using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AvitoChecker.Core
{
    abstract class AbstractStorage
    {
        private const string fileDirectory = "data";

        public AbstractStorage()
        {
            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);
        }
    }
}
