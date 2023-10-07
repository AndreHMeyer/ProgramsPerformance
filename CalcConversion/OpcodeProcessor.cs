using DataConflict;
using System;
using System.Collections.Generic;

namespace DataConflict
{
    public class OpcodeProcessor
    {
        private Dictionary<string, List<string>> opcodeInstructions = new Dictionary<string, List<string>>();
        private Dictionary<string, string> opcodeNames = new Dictionary<string, string>();
        private Dictionary<string, int> opcodeValues = new Dictionary<string, int>();

        public OpcodeProcessor()
        {
            opcodeInstructions["0110111"] = new List<string> { "rd" };
            opcodeNames["0110111"] = "U";
            opcodeValues["0110111"] = 2;

            opcodeInstructions["0010111"] = new List<string> { "rd" };
            opcodeNames["0010111"] = "U";
            opcodeValues["0010111"] = 3;

            opcodeInstructions["1101111"] = new List<string> { "rd" };
            opcodeNames["1101111"] = "J";
            opcodeValues["1101111"] = 1;

            opcodeInstructions["1100111"] = new List<string> {  "rs1", "rd" };
            opcodeNames["1100111"] = "I";
            opcodeValues["1100111"] = 2;

            opcodeInstructions["1100011"] = new List<string> { "rs2", "rs1" };
            opcodeNames["1100011"] = "B";
            opcodeValues["1100011"] = 4;

            opcodeInstructions["0000011"] = new List<string> { "rs1", "rd" };
            opcodeNames["0000011"] = "L";
            opcodeValues["0000011"] = 4;

            opcodeInstructions["0100011"] = new List<string> { "rs2", "rs1" };
            opcodeNames["0100011"] = "S";
            opcodeValues["0100011"] = 1;

            opcodeInstructions["0010011"] = new List<string> { "rs1", "rd" };
            opcodeNames["0010011"] = "II";
            opcodeValues["0010011"] = 5;

            opcodeInstructions["0110011"] = new List<string> { "rs2", "rs1", "rd" };
            opcodeNames["0110011"] = "R";
            opcodeValues["0110011"] = 2;

            opcodeInstructions["0001111"] = new List<string> { "" };
            opcodeNames["0001111"] = "III";
            opcodeValues["0001111"] = 3;

            opcodeInstructions["1110011"] = new List<string> { "rs1", "rd" };
            opcodeNames["1110011"] = "IIII";
            opcodeValues["1110011"] = 1;
        }

        public void ProcessOpcode(string opcode)
        {
            if (opcodeValues.ContainsKey(opcode))
            {
                Console.WriteLine(opcode + " - " + GetOpcodeName(opcode));
            }
            else
            {
                Console.WriteLine(opcode + " - Não encontrado");
            }
        }

        public List<string> GetOpcodeInstructions(string opcode)
        {
            if (opcodeValues.ContainsKey(opcode))
            {
                return opcodeInstructions[opcode];
            }
            else
            {
                // Retorna uma lista vazia se a chave não existir
                return new List<string>();
            }
        }

        public int GetOpcodeValues(string opcode)
        {
            if (opcodeValues.ContainsKey(opcode))
            {
                return opcodeValues[opcode];
            }
            else if (opcode == GetNOPInstruction())
            {
                return 2; // Valor do NOP
            }
            else
            {
                throw new ArgumentException("Opcode desconhecido");
            }
        }

        public string GetOpcodeName(string opcode)
        {
            if (opcodeValues.ContainsKey(opcode))
            {
                switch (opcode)
                {
                    case "0110111": return "U";
                    case "0010111": return "U";
                    case "1101111": return "J";
                    case "1100111": return "I";
                    case "1100011": return "B";
                    case "0000011": return "L";
                    case "0100011": return "S";
                    case "0010011": return "II";
                    case "0110011": return "R";
                    case "0001111": return "III";
                    case "1110011": return "IIII";
                    default: return "Valor Desconhecido";
                }
            }
            return "Não encontrado";
        }

        public int ClockTimePipeline { get; set; }
        public int NumInstructions { get; private set; }


        public int CalculateTotalCycles(List<string> opcodes)
        {
            int totalCycles = 0;

            foreach (string opcode in opcodes)
            {
                totalCycles += GetOpcodeValues(opcode);
            }

            return totalCycles;
        }

        public double CalculateCyclesByInstructions(List<string> opcodes)
        {
            int totalCycles = CalculateTotalCycles(opcodes);
            int totalInstructions = opcodes.Count;

            double cpi = (double)totalCycles / totalInstructions;
            cpi = Math.Round(cpi, 3);

            return cpi;
        }

        public double Performance(List<string> opcodes)
        {
            int totalInstructions = opcodes.Count;
            NumInstructions = totalInstructions;
            double cpi = CalculateCyclesByInstructions(opcodes);
            int clock = ClockTimePipeline;

            double performance = totalInstructions * cpi * clock;

            return performance;

        }

        public void IncludeNops(List<string> opcodes)
        {
            /* Considerar que não há nenhuma solução em hardware para conflitos e incluir 
            NOPs, quando necessário, para evitar o conflito de dados.*/

            List<string> modifiedOpcodes = new List<string>();

            for (int i = 0; i < opcodes.Count; i++)
            {
                // Verifica se é a primeira instrução no pipeline
                if (i == 0)
                {
                    // Se for a primeira instrução, adiciona à lista de instruções modificadas
                    modifiedOpcodes.Add(opcodes[i]);
                }
                else
                {
                    // Verifica se há dependência de dados entre instruções
                    List<string> currentInstructions = GetOpcodeInstructions(opcodes[i]);
                    List<string> previousInstructions = GetOpcodeInstructions(modifiedOpcodes[i - 1]);

                    bool hasDataDependency = false;

                    foreach (string currentInstruction in currentInstructions)
                    {
                        // Verifica se a instrução atual depende de alguma instrução anterior
                        if (previousInstructions.Contains(currentInstruction))
                        {
                            hasDataDependency = true;
                            break;
                        }
                    }

                    // Insere NOP, se houver dependência de dados
                    if (hasDataDependency)
                    {
                        // Adiciona uma instrução NOP à lista de instruções modificadas
                        modifiedOpcodes.Add(GetNOPInstruction());
                        // Adiciona a instrução original à lista
                        modifiedOpcodes.Add(opcodes[i]);
                    }
                    else
                    {
                        // Se não houver dependência de dados, adiciona a instrução original à lista de instruções modificadas
                        modifiedOpcodes.Add(opcodes[i]);
                    }
                }
            }

            opcodes.Clear();
            opcodes.AddRange(modifiedOpcodes);

            string filePath1 = "C:\\Users\\AndréMeyer\\OneDrive - SMART CONSULTING\\Área de Trabalho\\C#\\ConflitoDeDadosPipeline\\CalcConversion\\BinaryFiles\\IncludeNops.txt";

            ArchiveGenerator archiveGenerator = new ArchiveGenerator();
            archiveGenerator.GenerateTxt(modifiedOpcodes, filePath1);
        }

        private string GetNOPInstruction()
        {
            return "0010011";
        }

        public void ForwardingIncludeNops(List<string> opcodes)
        {
            /* Considerar que foi implementada a técnica de forwarding e inserir NOPs, quando 
            necessário, para evitar conflito de dados.*/

            // Crie uma lista para armazenar as instruções modificadas
            List<string> modifiedOpcodes = new List<string>();

            Dictionary<string, int> registerStatus = new Dictionary<string, int>();

            for (int i = 0; i < opcodes.Count; i++)
            {
                // Verifica se é a primeira instrução no pipeline
                if (i == 0)
                {
                    // Se for a primeira instrução, adiciona à lista de instruções modificadas
                    modifiedOpcodes.Add(opcodes[i]);

                    // Atualiza o status dos registradores com a instrução atual
                    UpdateRegisterStatus(opcodes[i], i, registerStatus);
                }
                else
                {
                    // Verifica se há encaminhamento de dados disponível
                    bool canForward = CanForwardData(opcodes, i, registerStatus);

                    // Insere NOP ou encaminhamento de dados, conforme necessário
                    if (!canForward)
                    {
                        // Adiciona uma instrução NOP à lista de instruções modificadas
                        modifiedOpcodes.Add(GetNOPInstruction());
                    }

                    // Adiciona a instrução original à lista de instruções modificadas
                    modifiedOpcodes.Add(opcodes[i]);

                    // Atualiza o status dos registradores com a instrução atual
                    UpdateRegisterStatus(opcodes[i], i, registerStatus);
                }
            }

            opcodes.Clear();
            opcodes.AddRange(modifiedOpcodes);

            string filePath2 = "C:\\Users\\AndréMeyer\\OneDrive - SMART CONSULTING\\Área de Trabalho\\C#\\ConflitoDeDadosPipeline\\CalcConversion\\BinaryFiles\\ForwardingIncludeNops.txt";

            ArchiveGenerator archiveGenerator = new ArchiveGenerator();
            archiveGenerator.GenerateTxt(modifiedOpcodes, filePath2);
        }

        private void UpdateRegisterStatus(string opcode, int currentIndex, Dictionary<string, int> registerStatus)
        {
            // Obtém as instruções associadas ao opcode atual
            List<string> currentInstructions = GetOpcodeInstructions(opcode);

            foreach (string reg in currentInstructions)
            {
                // Verifica se o registro não está vazio (Instrução vazia)
                if (!string.IsNullOrEmpty(reg))
                {
                    // Atualiza o status do registrador com o índice da instrução atual
                    registerStatus[reg] = currentIndex;
                }
            }
        }

        private bool CanForwardData(List<string> opcodes, int currentIndex, Dictionary<string, int> registerStatus)
        {
            // Obtém as instruções associadas ao opcode da instrução atual
            List<string> currentInstructions = GetOpcodeInstructions(opcodes[currentIndex]);

            // Itera sobre as instruções atuais
            foreach (string reg in currentInstructions)
            {
                // Verifica se a instrução atual lê ou escreve em um registrador
                if (!string.IsNullOrEmpty(reg))
                {
                    // Verifica se há uma entrada no dicionário 'registerStatus' para o registrador atual (reg)
                    // e se o valor associado a esse registrador (índice da última instrução que o modificou) é maior que o índice da instrução atual
                    if (registerStatus.ContainsKey(reg) && registerStatus[reg] > currentIndex)
                    {
                        // Há um conflito de escrita ou leitura em um registrador que foi modificado por uma instrução anterior
                        // Não é possível encaminhar dados para evitar o conflito
                        return false;
                    }
                }
            }

            // Não houve conflitos, encaminha os dados
            return true;
        }

        public void ReordenateInsertNops(List<string> opcodes)
        {
            /*Considerar que não há nenhuma solução em hardware para conflitos e quando 
            possível reordenar as instruções e quando não for possível inserir NOPs, para evitar conflito de dados.
            Por exemplo, é possível que o programa não tenha nenhuma instrução, a diante no código, para ser reordenada*/


            List<string> modifiedOpcodes = new List<string>();
            Dictionary<string, int> registerStatus = new Dictionary<string, int>();

            for (int i = 0; i < opcodes.Count; i++)
            {
                // Verifica se é a primeira instrução no pipeline
                if (i == 0)
                {
                    // Adiciona a primeira instrução à lista de instruções modificadas
                    modifiedOpcodes.Add(opcodes[i]);
                    // Atualiza o status dos registradores com o índice da instrução atual
                    UpdateRegisterStatus(opcodes[i], i, registerStatus);
                }
                else
                {
                    // Instruções atuais e anteriores
                    List<string> currentInstructions = GetOpcodeInstructions(opcodes[i]);
                    List<string> previousInstructions = GetOpcodeInstructions(modifiedOpcodes[i - 1]);

                    // Indicar se há dependência de dados entre as instruções
                    bool hasDataDependency = false;

                    foreach (string currentInstruction in currentInstructions)
                    {
                        // Verifica se alguma das instruções atuais está presente nas instruções anteriores
                        if (previousInstructions.Contains(currentInstruction))
                        {
                            hasDataDependency = true;
                            break;
                        }
                    }

                    // Verifica se é possível reordenar as instruções
                    bool canReorder = CanReorderInstructions(opcodes, i, registerStatus);

                    // Inserir NOP ou reordenar instruções, conforme necessário
                    if (hasDataDependency && !canReorder)
                    {
                        // Se houver dependência de dados e não for possível reordenar, insere um NOP
                        modifiedOpcodes.Add(GetNOPInstruction());
                    }
                    else
                    {
                        // Se não houver dependência de dados ou for possível reordenar, adiciona a instrução atual ou reordena instruções, se possível
                        if (canReorder)
                        {
                            ReorderInstructions(opcodes, i, modifiedOpcodes);
                        }
                        modifiedOpcodes.Add(opcodes[i]);
                    }

                    // Atualiza o status dos registradores com o índice da instrução atual
                    UpdateRegisterStatus(opcodes[i], i, registerStatus);
                }
            }

            opcodes.Clear();
            opcodes.AddRange(modifiedOpcodes);

            string filePath3 = "C:\\Users\\AndréMeyer\\OneDrive - SMART CONSULTING\\Área de Trabalho\\C#\\ConflitoDeDadosPipeline\\CalcConversion\\BinaryFiles\\ReordenateInsertNops.txt";

            ArchiveGenerator archiveGenerator = new ArchiveGenerator();
            archiveGenerator.GenerateTxt(modifiedOpcodes, filePath3);
        }

        private bool CanReorderInstructions(List<string> opcodes, int currentIndex, Dictionary<string, int> registerStatus)
        {
            // Verifica se é possível reordenar instruções com base nas dependências de dados

            List<string> currentInstructions = GetOpcodeInstructions(opcodes[currentIndex]);

            foreach (string reg in currentInstructions)
            {
                // Verifica se a instrução atual lê ou escreve em um registrador
                if (!string.IsNullOrEmpty(reg))
                {
                    // Verifica se há uma instrução anterior que escreveu no mesmo registrador
                    if (registerStatus.ContainsKey(reg))
                    {
                        // Obtém o índice da instrução anterior que escreveu no mesmo registrador
                        int previousIndex = registerStatus[reg];

                        // Verifica se a instrução anterior ainda não foi executada
                        if (previousIndex > currentIndex)
                        {
                            // Não é possível reordenar instruções se há dependência de dados
                            return false;
                        }
                    }
                }
            }

            // É possível reordenar as instruções
            return true;
        }

        private void ReorderInstructions(List<string> opcodes, int currentIndex, List<string> modifiedOpcodes)
        {
            // Se indíce atual for 0 ou maior que o tamanho da lista, não há instrução anterior para verificar
            if (currentIndex <= 0 || currentIndex >= modifiedOpcodes.Count)
            {
                return;
            }

            // Obtém as instruções atuais e anteriores
            List<string> currentInstructions = GetOpcodeInstructions(opcodes[currentIndex]);
            List<string> previousInstructions = GetOpcodeInstructions(modifiedOpcodes[currentIndex - 1]);

            // Verifica se a instrução atual depende de uma instrução anterior
            foreach (string reg in currentInstructions)
            {
                if (!string.IsNullOrEmpty(reg) && previousInstructions.Contains(reg))
                {
                    // Move a instrução atual para depois da instrução anterior
                    modifiedOpcodes.RemoveAt(currentIndex);
                    modifiedOpcodes.Insert(currentIndex - 1, opcodes[currentIndex]);
                    return; // Saia do loop após a instrução ser movida
                }
            }
        }

        public void ForwardingReordenateInsertNops(List<string> opcodes)
        {
            /*Considerar que foi implementada a técnica de forwarding e quando possível 
            reordenar as instruções e quando não for possível inserir NOPs, para evitar conflito de dados.*/

            // Armazena as instruções modificadas
            List<string> modifiedOpcodes = new List<string>();

            Dictionary<string, int> registerStatus = new Dictionary<string, int>();

            for (int i = 0; i < opcodes.Count; i++)
            {
                // Verificar se é a primeira instrução no pipeline
                if (i == 0)
                {
                    modifiedOpcodes.Add(opcodes[i]);
                    // Atualiza o status dos registradores para a primeira instrução
                    UpdateRegisterStatus(opcodes[i], i, registerStatus);
                }
                else
                {
                    // Verifica se há encaminhamento de dados disponível
                    bool canForward = CanForwardData(opcodes, i, registerStatus);

                    // Verifica se é possível reordenar instruções
                    bool canReorder = CanReorderInstructions(opcodes, i, registerStatus);

                    // Insere NOP ou reordena as instruções, conforme necessário
                    if (!canForward && !canReorder)
                    {
                        modifiedOpcodes.Add(GetNOPInstruction());
                    }
                    else
                    {
                        // Reordena as instruções se possível
                        if (canReorder)
                        {
                            ReorderInstructions(opcodes, i, modifiedOpcodes);
                        }
                        modifiedOpcodes.Add(opcodes[i]);
                    }

                    // Atualiza o status dos registradores para a instrução atual
                    UpdateRegisterStatus(opcodes[i], i, registerStatus);
                }
            }

            opcodes.Clear();
            opcodes.AddRange(modifiedOpcodes);

            string filePath4 = "C:\\Users\\AndréMeyer\\OneDrive - SMART CONSULTING\\Área de Trabalho\\C#\\ConflitoDeDadosPipeline\\CalcConversion\\BinaryFiles\\ForwardingReordenateInsertNops.txt";
            ArchiveGenerator archiveGenerator = new ArchiveGenerator();
            archiveGenerator.GenerateTxt(modifiedOpcodes, filePath4);
        }

    }
}
