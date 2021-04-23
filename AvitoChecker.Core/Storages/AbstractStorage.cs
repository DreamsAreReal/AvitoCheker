using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AvitoChecker.Core
{
    abstract class AbstractStorage
    {
        protected const string FileDirectory = "data";

        public AbstractStorage()
        {
            if (!Directory.Exists(FileDirectory))
                Directory.CreateDirectory(FileDirectory);
        }

        
    }
}
