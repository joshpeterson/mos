using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class And : Instruction
  {
    private static Dictionary<AddressingMode, int> numberOfCycles =
      new Dictionary<AddressingMode, int> {
        { AddressingMode.Immediate, 2 },
        { AddressingMode.Absolute, 4 },
        { AddressingMode.AbsoluteX, 4 },
        { AddressingMode.AbsoluteY, 4 },
        { AddressingMode.Zeropage, 3 },
        { AddressingMode.ZeropageX, 4 },
        { AddressingMode.ZeropageY, 4 },
        { AddressingMode.IndexedIndirectX, 6 },
        { AddressingMode.IndexedIndirectY, 5 },
      };

    public int Execute(ProgrammingModel model, Memory memory, AddressingMode mode,
                        ushort operand)
    {
      var accumulator = model.GetRegister(RegisterName.A);
      var previousValue = (byte)accumulator.GetValue();
      var andValue = ArgumentUtils.ArgumentFor(model, memory, mode, operand);
      accumulator.SetValue(previousValue & andValue);

      var cycles = numberOfCycles[mode];
      if (ArgumentUtils.CrossesPageBoundary(model, mode, operand))
        cycles++;
      return cycles;
    }
  }
}
