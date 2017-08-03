namespace Mos6510.Instructions
{
  public class Asl : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var values = ShiftUtils.Shift(model, memory, argument,
                                    (int v) => v << 1, 0x80);

      RegisterUtils.SetZeroFlag(model, (byte)values.NewValue);
      RegisterUtils.SetNegativeFlag(model, (byte)values.NewValue);

      return Result.Success;
    }
  }
}
