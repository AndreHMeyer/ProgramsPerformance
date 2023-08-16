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
                List<string> singleFilePath = new List<string> { filePath }; 
                ArchiveReader archiveReader = new ArchiveReader(singleFilePath);
                List<string> opcodeList = archiveReader.ReadTxt();

                Console.WriteLine($"Arquivo: {archiveReader.GetArchiveName(filePath)}\n");

                foreach (string opcode in opcodeList)
                {
                    string instruction = opcodeProcessor.GetOpcodeName(opcode);
                    Console.WriteLine($"Binário: {opcode} - Instrução: {instruction}");
                }

                Console.WriteLine("");
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
                List<string> singleFilePath = new List<string> { filePath };
                ArchiveReader archiveReader = new ArchiveReader(singleFilePath);
                List<string> opcodeList = archiveReader.ReadTxt();

                int totalCycles = opcodeProcessor.CalculateTotalCycles(opcodeList);
                Console.WriteLine($"Total de ciclos no arquivo {archiveReader.GetArchiveName(filePath)}: {totalCycles}");
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
                List<string> singleFilePath = new List<string> { filePath };
                ArchiveReader archiveReader = new ArchiveReader(singleFilePath);
                List<string> opcodeList = archiveReader.ReadTxt();

                double totalInstructions = opcodeProcessor.CalculateCyclesByInstructions(opcodeList);
                Console.WriteLine($"Total de instruções no arquivo {archiveReader.GetArchiveName(filePath)}: {totalInstructions}");
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

            for (int i = 0; i < filePaths.Count - 1; i++)
            {
                string filePath1 = filePaths[i];
                string filePath2 = filePaths[i + 1];

                List<string> singleFilePath1 = new List<string> { filePath1 };
                ArchiveReader archiveReader1 = new ArchiveReader(singleFilePath1);
                List<string> opcodeList1 = archiveReader1.ReadTxt();

                List<string> singleFilePath2 = new List<string> { filePath2 };
                ArchiveReader archiveReader2 = new ArchiveReader(singleFilePath2);
                List<string> opcodeList2 = archiveReader2.ReadTxt();

                double performanceA = opcodeProcessor.CalculateProgramsPerformance(opcodeList1);
                double performanceB = opcodeProcessor.CalculateProgramsPerformance(opcodeList2);

                Console.WriteLine($"Tempo de execução do programa {archiveReader1.GetArchiveName(filePath1)}: {performanceA}");
                Console.WriteLine($"Tempo de execução do programa {archiveReader2.GetArchiveName(filePath2)}: {performanceB}");

                double bestPerformance = 0;

                if(performanceA > performanceB)
                {
                    bestPerformance = performanceA / performanceB;

                    Console.WriteLine($"O programa {archiveReader1.GetArchiveName(filePath1)} é {bestPerformance} mais rápido que o programa {archiveReader2.GetArchiveName(filePath2)}");
                }
                else
                {
                    bestPerformance = performanceB / performanceA;

                    Console.WriteLine($"\nO programa {archiveReader1.GetArchiveName(filePath1)} é {bestPerformance.ToString("0.000")} mais rápido que o programa {archiveReader2.GetArchiveName(filePath2)}");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();

            Console.Clear();
        }


    }
}
