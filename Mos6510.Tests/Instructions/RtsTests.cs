using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class RtsTests
  {
    [Test]
    public void IncrementsTheTopOfTheStackByOneAndStoresItInTheProgramCounter()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();

      const ushort returnAddress = 0x0460;
      var highByte = (byte)(returnAddress >> 8);
      var lowByte = (byte)(returnAddress & 0xFF);

      Stack.Push(model, memory, highByte);
      Stack.Push(model, memory, lowByte);

      new Rts().Execute(model, memory, new Argument(0, 0));

      Assert.That(model.GetRegister(RegisterName.PC).GetValue(),
                  Is.EqualTo(returnAddress + 1));
    }

    [Test]
    public void PopsTwoBytesFromTheStack()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();

      const byte originalStackValue = 0xFD;
      Stack.Push(model, memory, originalStackValue);
      Stack.Push(model, memory, 0x04);
      Stack.Push(model, memory, 0x60);

      new Rts().Execute(model, memory, new Argument(0, 0));

      Assert.That(Stack.Top(model, memory), Is.EqualTo(originalStackValue));
    }
  }
}
