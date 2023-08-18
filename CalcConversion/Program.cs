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
                @"",
                @"",
            };

            ArchiveReader archiveReader = new ArchiveReader(paths);
            PerformanceGUID performanceGUID = new PerformanceGUID(paths);

            performanceGUID.Menu();
        }
    }
}
