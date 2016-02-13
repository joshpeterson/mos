using Mos6510;

namespace Mos6510.Instructions
{
  public class Iny : Instruction
  {
    public int Execute(ProgrammingModel model, Memory memory, AddressingMode mode,
                        ushort operand)
    {
      RegisterUtils.IncrementRegister(model, RegisterName.Y);
      return 2;
    }
  }
}
