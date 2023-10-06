using System;
using System.Collections.Generic;

namespace DataConflict
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> paths = new List<string>
            {
                @"C:\Users\AndréMeyer\OneDrive - SMART CONSULTING\Área de Trabalho\C#\ConflitoDeDadosPipeline\CalcConversion\BinaryFiles\BinaryCode.txt"
            };

            ArchiveReader archiveReader = new ArchiveReader(paths);
            PerformanceGUID performanceGUID = new PerformanceGUID(paths);

            performanceGUID.Menu();
        }
    }
}
