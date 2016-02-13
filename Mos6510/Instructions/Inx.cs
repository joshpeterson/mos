using Mos6510;

namespace Mos6510.Instructions
{
  public class Inx : Instruction
  {
    public virtual int Execute(ProgrammingModel model, Memory memory,
                                AddressingMode mode, ushort operand)
    {
      RegisterUtils.IncrementRegister(model, RegisterName.X);
      return 2;
    }
  }
}
