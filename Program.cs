using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/*
The MIT License (MIT)

Copyright (c) 2014 Kousha Nakhaei

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */

namespace SVGSizeRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SVG Size Remover v0.1");
            if (args.Length == 0)
            {
                Console.WriteLine("File address not found. drag or submit the file address as argument.");
                Console.WriteLine("Press any key to close...");
                Console.ReadKey();
                return;
            }

            bool hasError = false;
            //loop all arguments
            for (int i = 0; i < args.Length; i++) {
                //store the arg
                string _fileAdr = args[i];
                Console.WriteLine("\nOpenning file: " + _fileAdr);
                //check if the file not exists, display msg and jumpout
                if (!File.Exists(_fileAdr)) 
                {
                    hasError = true; //to stop console from closing at end.
                    Console.WriteLine("not found.");
                    continue;
                }

                //get svg file contents
                string _svgContents = getFileContents(_fileAdr); 

                //strip tags
                _svgContents = stripTag(_svgContents, "width");
                _svgContents = stripTag(_svgContents, "height");

                setFileContents(_fileAdr, _svgContents);
                Console.WriteLine("size tags has been successfully removed.");
            }
            Console.Write("\nFinished.");
            if (hasError)
            {
                Console.WriteLine("Press any key to close...");
                Console.ReadKey();
            }
        }

        private static string stripTag(string _content, string _tag)
        {
            string _start = _tag + "=\"";
            string _end = "\"";
            if (_content.Contains(_start))
            {
                _content = _content.Remove(_content.IndexOf(_start), _content.IndexOf(_end, _content.IndexOf(_start) + _start.Length) - _content.IndexOf(_start) + _end.Length);
            }
            return _content;
        }

        private static string getFileContents(string _address) {
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(_address))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
            }
            return sb.ToString();
        }

        private static void setFileContents(string _address, string _contents)
        {
            StreamWriter writetext = new StreamWriter(_address);
            writetext.Write(_contents);
            writetext.Close();
        }

    }
}
