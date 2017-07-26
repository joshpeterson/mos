using NUnit.Framework;

namespace Mos6510.Tests
{
  [TestFixture]
  public class StackTests
  {
    [Test]
    public void PushingToTheStackDecrementsTheStackPointer()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      Stack.Push(model, memory, 0x50);

      Assert.That(model.GetRegister(RegisterName.S).GetValue(), Is.EqualTo(0xFE));
    }

    [Test]
    public void PushingToTheStackPutsTheValueInTheProperMemoryLocation()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      const byte expectedValue = 0x50;
      Stack.Push(model, memory, expectedValue);

      Assert.That(memory.GetValue(0x1FF), Is.EqualTo(expectedValue));
    }

    [Test]
    public void PoppingFromTheStackIncrementsTheStackPointer()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      Stack.Push(model, memory, 0x50);
      Stack.Pop(model, memory);

      Assert.That(model.GetRegister(RegisterName.S).GetValue(), Is.EqualTo(0xFF));
    }

    [Test]
    public void ReadingFromTheTopOfTheStackReturnsTheProperValueFromMemory()
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      const byte expectedValue = 0x50;
      Stack.Push(model, memory, expectedValue);

      Assert.That(Stack.Top(model, memory), Is.EqualTo(expectedValue));
    }
  }
}
