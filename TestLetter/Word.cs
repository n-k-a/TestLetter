using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLetter
{
    class Word
    {
        private string wordname;
        private string definition;
        public Word(string w, string d){
           wordname= w;
           definition= d;
        }

        public string Wordname { get => wordname; set => wordname = value; }
        public string Definition { get => definition; set => definition = value; }
    }
}
