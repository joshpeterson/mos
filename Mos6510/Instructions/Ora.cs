using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class Ora : Instruction
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

    public virtual void Execute(ProgrammingModel model, Memory memory,
                                byte argument)
    {
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue((byte)accumulator.GetValue() | argument);
      RegisterUtils.SetZeroFlag(model, RegisterName.A);
      RegisterUtils.SetNegativeFlag(model, RegisterName.A);
    }

    public virtual int CyclesFor(AddressingMode mode)
    {
      return numberOfCycles[mode];
    }
  }
}
