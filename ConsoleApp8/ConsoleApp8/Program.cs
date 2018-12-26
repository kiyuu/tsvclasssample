using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConsoleApp8.Model;

namespace ConsoleApp8
{
    class Program
    {

        static void Main(string[] args)
        {
            new TsvConverter().Convert();
        }
    }
}
