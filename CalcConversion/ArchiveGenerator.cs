using System;
using System.Collections.Generic;
using System.IO;

namespace DataConflict
{
    public class ArchiveGenerator
    {
        public void GenerateTxt(List<string> binaryCode, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string code in binaryCode)
                    {
                        writer.WriteLine(code);
                    }
                }

                Console.WriteLine("Arquivo gerado com sucesso em: " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao gerar o arquivo: " + ex.Message);
            }
        }
    }
}
