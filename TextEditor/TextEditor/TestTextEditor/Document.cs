using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;


namespace TestTextEditor
{
    class Document
    {
        private string filePath;

        public string FilePath { get { return filePath; } set { filePath = value; } }

        private bool hasPath;

        public bool HasPath 
        {
            get
            {
                if (string.IsNullOrEmpty(filePath))
                    return false;

                else return true;
            }

            set { hasPath = value; }
        }


        public Document()
        {
        }
    }
}
