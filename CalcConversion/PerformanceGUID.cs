using System;
using System.Collections.Generic;

namespace DataConflict
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

            OpcodeProcessor opcodeProcessor = new OpcodeProcessor();

            Console.WriteLine("--- CONFLITOS DE DADOS NO PIPELINE ---\n");
            Console.WriteLine("Digite o tempo de clock do Pipeline");
            int valueClockTime = int.Parse(Console.ReadLine());
            opcodeProcessor.ClockTimePipeline = valueClockTime;

            if (valueClockTime > 0)
            {

                Console.Clear();

                do
                {
                    Console.WriteLine("--- CONFLITOS DE DADOS NO PIPELINE ---\n");
                    Console.WriteLine("1 - Binário e Instruções");
                    Console.WriteLine("2 - Inserção de NOPs");
                    Console.WriteLine("3 - Forwarding - Inserção de NOPs");
                    Console.WriteLine("4 - Reordenação de Instruções - Inserção de NOPs");
                    Console.WriteLine("5 - Forwarding - Reordenação de Instruções - Inserção de NOPs");
                    Console.WriteLine("6 - Sair");
                    Console.Write("\nEscolha uma opção: ");
                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            ShowLinesAndInstruction();
                            break;
                        case 2:
                            IncludeNops(opcodeProcessor);
                            break;
                        case 3:
                            ForwardingIncludeNops(opcodeProcessor);
                            break;
                        case 4:
                            ReordenateInsertNops(opcodeProcessor);
                            break;
                        case 5:
                            ForwardingReordenateInsertNops(opcodeProcessor);
                            break;
                        case 6:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Saindo...");
                            break;
                    }

                } while (choice != 6);
            }
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

        private void IncludeNops(OpcodeProcessor opcodeProcessor)
        {
            Console.Clear();
            Console.WriteLine("--- INSERÇÃO DE NOPS ---\n");

            foreach (string filePath in filePaths)
            {
                List<string> singleFilePath = new List<string> { filePath };
                ArchiveReader archiveReader = new ArchiveReader(singleFilePath);
                List<string> opcodeList = archiveReader.ReadTxt();

                Console.WriteLine($"Arquivo: {archiveReader.GetArchiveName(filePath)}\n");

                // Chame o método que faz a inserção de NOPs
                opcodeProcessor.IncludeNops(opcodeList);

                foreach (string opcode in opcodeList)
                {
                    string instruction = opcodeProcessor.GetOpcodeName(opcode);
                    Console.WriteLine($"Binário: {opcode} - Instrução: {instruction}");
                }

                Console.WriteLine("\n--- RESULTADOS DO DESEMPENHO ---\n");
                Console.WriteLine($"Tempo de Clock do Pipeline: {opcodeProcessor.ClockTimePipeline}");

                // Calcule o desempenho com os novos opcodes após a inclusão de NOPs
                double performance = opcodeProcessor.Performance(opcodeList);
                Console.WriteLine($"Desempenho: {performance} ns");

                Console.WriteLine("");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();

            Console.Clear();
        }

        private void ForwardingIncludeNops(OpcodeProcessor opcodeProcessor)
        {
            Console.Clear();
            Console.WriteLine("--- FORWARDING - INSERÇÃO DE NOPS ---\n");

            foreach (string filePath in filePaths)
            {
                List<string> singleFilePath = new List<string> { filePath };
                ArchiveReader archiveReader = new ArchiveReader(singleFilePath);
                List<string> opcodeList = archiveReader.ReadTxt();

                Console.WriteLine($"Arquivo: {archiveReader.GetArchiveName(filePath)}\n");

                // Chame o método do OpcodeProcessor que realiza a inserção de NOPs com encaminhamento de dados
                opcodeProcessor.ForwardingIncludeNops(opcodeList);

                foreach (string opcode in opcodeList)
                {
                    string instruction = opcodeProcessor.GetOpcodeName(opcode);
                    Console.WriteLine($"Binário: {opcode} - Instrução: {instruction}");
                }

                Console.WriteLine("\n--- RESULTADOS DO DESEMPENHO ---\n");
                Console.WriteLine($"Tempo de Clock do Pipeline: {opcodeProcessor.ClockTimePipeline}");

                // Calcule o desempenho com os novos opcodes após a inclusão de NOPs
                double performance = opcodeProcessor.Performance(opcodeList);
                Console.WriteLine($"Desempenho: {performance} ns");

                Console.WriteLine("");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();

            Console.Clear();
        }

        private void ReordenateInsertNops(OpcodeProcessor opcodeProcessor)
        {
            Console.Clear();
            Console.WriteLine("--- REORDENAÇÃO DE INSTRUÇÕES - INSERÇÃO DE NOPs ---\n");

            foreach (string filePath in filePaths)
            {
                List<string> singleFilePath = new List<string> { filePath };
                ArchiveReader archiveReader = new ArchiveReader(singleFilePath);
                List<string> opcodeList = archiveReader.ReadTxt();

                Console.WriteLine($"Arquivo: {archiveReader.GetArchiveName(filePath)}\n");

                // Chame o método do OpcodeProcessor que realiza a reordenação e inserção de NOPs
                opcodeProcessor.ReordenateInsertNops(opcodeList);

                foreach (string opcode in opcodeList)
                {
                    string instruction = opcodeProcessor.GetOpcodeName(opcode);
                    Console.WriteLine($"Binário: {opcode} - Instrução: {instruction}");
                }

                Console.WriteLine("\n--- RESULTADOS DO DESEMPENHO ---\n");
                Console.WriteLine($"Tempo de Clock do Pipeline: {opcodeProcessor.ClockTimePipeline}");

                // Calcule o desempenho com os novos opcodes após a inclusão de NOPs
                double performance = opcodeProcessor.Performance(opcodeList);
                Console.WriteLine($"Desempenho: {performance} ns");

                Console.WriteLine("");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();

            Console.Clear();
        }

        private void ForwardingReordenateInsertNops(OpcodeProcessor opcodeProcessor)
        {
            Console.Clear();
            Console.WriteLine("--- FORWARDING - REORDENAÇÃO DE INSTRUÇÕES - INSERÇÃO DE NOPs ---\n");

            foreach (string filePath in filePaths)
            {
                List<string> singleFilePath = new List<string> { filePath };
                ArchiveReader archiveReader = new ArchiveReader(singleFilePath);
                List<string> opcodeList = archiveReader.ReadTxt();

                Console.WriteLine($"Arquivo: {archiveReader.GetArchiveName(filePath)}\n");

                // Chame o método do OpcodeProcessor que realiza a reordenação, inserção de NOPs e encaminhamento de dados
                opcodeProcessor.ForwardingReordenateInsertNops(opcodeList);

                foreach (string opcode in opcodeList)
                {
                    string instruction = opcodeProcessor.GetOpcodeName(opcode);
                    Console.WriteLine($"Binário: {opcode} - Instrução: {instruction}");
                }

                Console.WriteLine("\n--- RESULTADOS DO DESEMPENHO ---");
                Console.WriteLine($"Tempo de Clock do Pipeline: {opcodeProcessor.ClockTimePipeline}");

                // Calcule o desempenho com os novos opcodes após a inclusão de NOPs
                double performance = opcodeProcessor.Performance(opcodeList);
                Console.WriteLine($"Desempenho: {performance} ns");

                Console.WriteLine("");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();

            Console.Clear();
        }
    }
}
