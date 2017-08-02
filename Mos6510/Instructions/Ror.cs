namespace Mos6510.Instructions
{
  public class Ror : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      ShiftUtils.Rotate(model, memory, argument, (int v) => v >> 1, 0x80, 0x01);
      return Result.Success;
    }
  }
}
