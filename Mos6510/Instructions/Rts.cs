using System;
namespace Mos6510.Instructions
{
  public class Rts : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var lowByte = Stack.Top(model, memory);
      Stack.Pop(model, memory);

      var highByte = Stack.Top(model, memory);
      Stack.Pop(model, memory);

      var returnAddress = ((highByte << 8) | lowByte) + 1;

      model.GetRegister(RegisterName.PC).SetValue(returnAddress);

      return Result.Success;
    }
  }
}
