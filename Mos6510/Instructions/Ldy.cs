using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class Ldy : Instruction
  {
    private static Dictionary<AddressingMode, int> numberOfCycles =
    new Dictionary<AddressingMode, int> {
      { AddressingMode.Immediate, 2 },
      { AddressingMode.Absolute, 4 },
      { AddressingMode.AbsoluteX, 4 },
      { AddressingMode.Zeropage, 3 },
      { AddressingMode.ZeropageX, 4 },
    };

    public void Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.LoadRegister(model, RegisterName.Y, argument.value);
    }

    public int CyclesFor(AddressingMode mode)
    {
      return numberOfCycles[mode];
    }
  }
}
