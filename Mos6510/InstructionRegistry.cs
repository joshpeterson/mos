using Mos6510.Instructions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Mos6510
{
  public class InstructionRegistry
  {
    public Registry All = new Registry();

    private Dictionary<Opcode, Instruction> Ins;

    public InstructionRegistry()
    {
      Ins = new Dictionary<Opcode, Instruction>();

      foreach (var opcode in (Opcode[])Enum.GetValues(typeof(Opcode)))
      {
        var instruction = (Instruction)GetType().Assembly.CreateInstance(
                            string.Format("Mos6510.Instructions.{0}",
                                          Enum.GetName(typeof(Opcode), opcode)));
        if (instruction != null)
          Ins.Add(opcode, instruction);
      }

      LoadInstructionData();
    }

    private void LoadInstructionData()
    {
      var assembly = GetType().Assembly;
      using (var stream = assembly.GetManifestResourceStream("instructions.csv"))
      using (var reader = new StreamReader(stream))
      {
        string line = null;
        while ((line = reader.ReadLine()) != null)
          AddInstruction(line);
      }
    }

    private void AddInstruction(string line)
    {
      var parts = line.Split(',');
      var opcode = (Opcode)Enum.Parse(typeof(Opcode), parts[0]);
      var mode = (AddressingMode)Enum.Parse(typeof(AddressingMode), parts[1]);
      var code = byte.Parse(parts[2], NumberStyles.HexNumber);
      var cycles = int.Parse(parts[3]);
      var size = int.Parse(parts[4]);

      Instruction instruction;
      if (Ins.TryGetValue(opcode, out instruction))
        All.Add(code, opcode, instruction, mode, cycles, size);
    }
  }
}
