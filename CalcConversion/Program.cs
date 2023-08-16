using System;
using System.Collections.Generic;

namespace CalcConversion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> paths = new List<string>
            {
                @"C:\Users\AndréMeyer\Downloads\BinaryCode.txt",
                @"C:\Users\AndréMeyer\Downloads\BinaryCode2.txt",
            };

            ArchiveReader archiveReader = new ArchiveReader(paths);
            PerformanceGUID performanceGUID = new PerformanceGUID(paths);

            performanceGUID.Menu();
        }
    }
}
