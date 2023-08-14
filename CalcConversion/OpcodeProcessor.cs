using System;
using System.Collections.Generic;

namespace CalcConversion
{
    public class OpcodeProcessor
    {
        private Dictionary<string, int> opcodeValues;

        public OpcodeProcessor()
        {
            // Inicializa os valores dos opcodes
            opcodeValues = new Dictionary<string, int>
            {
                { "0110111", 2 },   // U
                { "1101111", 3 },   // J
                { "1100111", 4 },   // I
                { "1100011", 5 },   // B
                { "0000011", 2 },   // L
                { "0100011", 1 },   // S
                { "0010011", 6 },   // II
                { "0110011", 3 },   // R
                { "0001111", 5 }    // III
            };
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

        public int GetOpcodeValue(string opcode)
        {
            if (opcodeValues.ContainsKey(opcode))
            {
                return opcodeValues[opcode];
            }
            return 0; // Caso não exista valor
        }

        public string GetOpcodeName(string opcode)
        {
            if (opcodeValues.ContainsKey(opcode))
            {
                switch (opcode)
                {
                    case "0110111": return "U";
                    case "1101111": return "J";
                    case "1100111": return "I";
                    case "1100011": return "B";
                    case "0000011": return "L";
                    case "0100011": return "S";
                    case "0010011": return "II";
                    case "0110011": return "R";
                    case "0001111": return "III";
                    default: return "Valor Desconhecido";
                }
            }
            return "Não encontrado";
        }

        public int CalculateTotalCycles(List<string> opcodes)
        {
            int totalCycles = 0;

            foreach (string opcode in opcodes)
            {
                totalCycles += GetOpcodeValue(opcode);
            }

            return totalCycles;
        }

        public double CalculateCyclesByInstructions(List<string> opcodes)
        {
            int totalCycles = CalculateTotalCycles(opcodes);
            int totalInstructions = opcodes.Count;

            double cpi = (double)totalCycles / totalInstructions;
            return cpi;
        }

        /*public string CalculateProgramsPerformance(List<string> opcodesOrgA, List<string> opcodesOrgB, double clockOrgA, double clockOrgB)
        {
            OpcodeProcessor opcodeProcessor = new OpcodeProcessor();

            double ciclosTotaisOrgA = opcodeProcessor.CalculateTotalCycles(opcodesOrgA);
            double ciclosTotaisOrgB = opcodeProcessor.CalculateTotalCycles(opcodesOrgB);

            double tExecucaoCPUA = ciclosTotaisOrgA * clockOrgA;
            double tExecucaoCPUB = ciclosTotaisOrgB * clockOrgB;

            double desempenhoCPUA = tExecucaoCPUB / tExecucaoCPUA;
            double desempenhoCPUB = tExecucaoCPUA / tExecucaoCPUB;

            string resultado = $"Desempenho CPU A: {desempenhoCPUA:F2}\nDesempenho CPU B: {desempenhoCPUB:F2}";

            return resultado;
        }*/

        public double CalculateProgramsPerformance(List<string> opcodes, List<string> filePaths, double clockOrgA, double clockOrgB)
        {
            int totalCyclesOrgA = CalculateTotalCycles(opcodes);
            double execTimeOrgA = totalCyclesOrgA * clockOrgA;

            List<string> opcodesOrgB = new List<string>();
            foreach (string filePath in filePaths)
            {
                ArchiveReader archiveReader = new ArchiveReader(filePath);
                List<string> opcodeList = archiveReader.ReadTxtAndGetOpcodes();
                opcodesOrgB.AddRange(opcodeList);
            }

            int totalCyclesOrgB = CalculateTotalCycles(opcodesOrgB);
            double execTimeOrgB = totalCyclesOrgB * clockOrgB;

            double performanceComparation = execTimeOrgB / execTimeOrgA;
            return performanceComparation;
        }

    }
}
