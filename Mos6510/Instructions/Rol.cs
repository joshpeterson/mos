namespace Mos6510.Instructions
{
  public class Rol : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      ShiftUtils.Rotate(model, memory, argument, (int v) => v << 1, 0x01, 0x80);
      return Result.Success;
    }
  }
}
