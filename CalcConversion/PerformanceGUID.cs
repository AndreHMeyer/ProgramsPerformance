using System;
using System.Collections.Generic;

namespace CalcConversion
{
    public class PerformanceGUID
    {
        private List<string> filePaths;

        public PerformanceGUID(List<string> filePaths)
        {
            this.filePaths = filePaths;
        }

        public void Menu()
        {
            int choice;

            do
            {
                Console.WriteLine("--- COMPARATIVO DE DESEMPENHO ---\n");
                Console.WriteLine("1 - Binário e Instruções");
                Console.WriteLine("2 - Ciclos Totais");
                Console.WriteLine("3 - Ciclos por Instrução");
                Console.WriteLine("4 - Desempenho");
                Console.WriteLine("5 - Sair");
                Console.Write("\nEscolha uma opção: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ShowLinesAndInstruction();
                        break;
                    case 2:
                        TotalCycles();
                        break;
                    case 3:
                        InstructionsCycles();
                        break;
                    case 4:
                        Performance();
                        break;
                    case 5:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Saindo...");
                        break;
                }

            } while (choice != 5);
        }

        private void ShowLinesAndInstruction()
        {
            Console.Clear();
            Console.WriteLine("--- BINÁRIO E INSTRUÇÕES ---\n");

            OpcodeProcessor opcodeProcessor = new OpcodeProcessor();

            foreach (string filePath in filePaths)
            {
                List<string> singleFilePath = new List<string> { filePath }; // Crie uma lista com um único elemento
                ArchiveReader archiveReader = new ArchiveReader(singleFilePath);
                List<string> opcodeList = archiveReader.ReadTxtAndGetOpcodes();

                Console.WriteLine($"Arquivo: {filePath}\n");

                foreach (string opcode in opcodeList)
                {
                    string instruction = opcodeProcessor.GetOpcodeName(opcode);
                    Console.WriteLine($"Binário: {opcode} - Instrução: {instruction}");
                }

                Console.WriteLine(""); // Separa os binários
            }

            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();

            Console.Clear();
        }
        

        private void TotalCycles()
        {
            Console.Clear();
            Console.WriteLine("--- CICLOS TOTAIS ---\n");

            OpcodeProcessor opcodeProcessor = new OpcodeProcessor();

            foreach (string filePath in filePaths)
            {
                List<string> singleFilePath = new List<string> { filePath }; // Crie uma lista com um único elemento
                ArchiveReader archiveReader = new ArchiveReader(singleFilePath);
                List<string> opcodeList = archiveReader.ReadTxtAndGetOpcodes();

                int totalCycles = opcodeProcessor.CalculateTotalCycles(opcodeList);
                Console.WriteLine($"Total de ciclos no arquivo {filePath}: {totalCycles}");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();

            Console.Clear();
        }

        private void InstructionsCycles()
        {
            Console.Clear();
            Console.WriteLine("--- CICLOS POR INSTRUÇÃO ---\n");

            OpcodeProcessor opcodeProcessor = new OpcodeProcessor();

            foreach (string filePath in filePaths)
            {
                List<string> singleFilePath = new List<string> { filePath }; // Crie uma lista com um único elemento
                ArchiveReader archiveReader = new ArchiveReader(singleFilePath);
                List<string> opcodeList = archiveReader.ReadTxtAndGetOpcodes();

                double totalInstructions = opcodeProcessor.CalculateCyclesByInstructions(opcodeList);
                Console.WriteLine($"Total de instruções no arquivo {filePath}: {totalInstructions}");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();

            Console.Clear();
        }

        private void Performance()
        {
            Console.Clear();
            Console.WriteLine("--- DESEMPENHO ---\n");

            OpcodeProcessor opcodeProcessor = new OpcodeProcessor();

            foreach (string filePath in filePaths)
            {
                List<string> singleFilePath = new List<string> { filePath }; // Crie uma lista com um único elemento
                ArchiveReader archiveReader = new ArchiveReader(singleFilePath);
                List<string> opcodeList = archiveReader.ReadTxtAndGetOpcodes();

                double performanceComparation = opcodeProcessor.CalculateProgramsPerformance(opcodeList, singleFilePath);

                Console.WriteLine($"Desempenho para o arquivo {filePath}:\n{performanceComparation}");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();

            Console.Clear();
        }


    }
}
