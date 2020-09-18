using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR141_2017_WebProjekat.Models
{
    public class UploadedFile
    {
        public string FileName { get; set; }
        public string DirectoryPath { get; set; }

        public UploadedFile(string fileName, string directoryPath)
        {
            FileName = fileName;
            DirectoryPath = directoryPath;
        }
    }
}