using System;
using System.Collections.Generic;
using System.IO;

namespace CalcConversion
{
    public class ArchiveReader
    {
        public List<string> Paths { get; }

        public ArchiveReader(List<string> paths)
        {
            Paths = paths;
        }

        public List<string> ReadTxtAndGetOpcodes()
        {
            List<string> opcodeList = new List<string>();

            foreach (string path in Paths)
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"O arquivo {path} não existe.");
                    continue;
                }

                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Length >= 7)
                            {
                                string lastSevenDigits = line.Substring(line.Length - 7);
                                opcodeList.Add(lastSevenDigits);
                            }
                            else
                            {
                                Console.WriteLine(line);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro ao ler o arquivo {path}: {ex.Message}");
                }
            }

            return opcodeList;
        }
    }
}
