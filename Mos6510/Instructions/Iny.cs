using Mos6510;

namespace Mos6510.Instructions
{
  public class Iny : Instruction
  {
    public virtual void Execute(ProgrammingModel model, Memory memory, byte argument)
    {
      RegisterUtils.IncrementRegister(model, RegisterName.Y);
    }

    public virtual int CyclesFor(ProgrammingModel model, AddressingMode mode,
                                  ushort operand)
    {
      return 2;
    }
  }
}
