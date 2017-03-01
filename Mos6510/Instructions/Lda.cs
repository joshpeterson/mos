using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class Lda : Instruction
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

    public void Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.LoadRegister(model, RegisterName.A, argument.value);
    }

    public int CyclesFor(AddressingMode mode)
    {
      return numberOfCycles[mode];
    }
  }
}
