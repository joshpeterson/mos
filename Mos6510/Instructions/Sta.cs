using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class Sta : Instruction
  {
    private static Dictionary<AddressingMode, int> numberOfCycles =
    new Dictionary<AddressingMode, int> {
      { AddressingMode.Absolute, 4 },
      { AddressingMode.AbsoluteX, 5 },
      { AddressingMode.AbsoluteY, 5 },
      { AddressingMode.Zeropage, 3 },
      { AddressingMode.ZeropageX, 4 },
      { AddressingMode.ZeropageY, 4 },
      { AddressingMode.IndexedIndirectX, 6 },
      { AddressingMode.IndexedIndirectY, 6 },
    };

    public void Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var value = (byte)model.GetRegister(RegisterName.A).GetValue();
      memory.SetValue(argument.address, value);
    }

    public int CyclesFor(AddressingMode mode)
    {
      return numberOfCycles[mode];
    }
  }
}
