namespace Mos6510.Instructions
{
  public class Lsr : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var values = ShiftUtils.Shift(model, memory, argument,
                                    (int v) => v >> 1, 0x01);

      model.NegativeFlag = false;
      RegisterUtils.SetZeroFlag(model, (byte)values.PreviousValue);

      return Result.Success;
    }
  }
}
