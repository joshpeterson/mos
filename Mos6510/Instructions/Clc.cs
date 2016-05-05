namespace Mos6510.Instructions
{
  public class Clc : Instruction
  {
    public void Execute(ProgrammingModel model, Memory memory, byte argument)
    {
      model.CarryFlag = false;
    }

    public int CyclesFor(AddressingMode mode)
    {
      return 2;
    }
  }
}
