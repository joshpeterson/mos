using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class JsrTests
  {
    ProgrammingModel model;
    Memory memory;
    Argument argument;
    Register pc;
    const ushort expectedAddress = 0x4804;

    [SetUp]
    public void SetUp()
    {
      model = new ProgrammingModel();
      memory = new Memory();
      argument = new Argument(0, expectedAddress);
      pc = model.GetRegister(RegisterName.PC);
      pc.SetValue(0x2680);
    }

    [Test]
    public void SetsTheProgramCounterToTheAddressArgument()
    {
      new Jsr().Execute(model, memory, argument);

      Assert.That(pc.GetValue(), Is.EqualTo(expectedAddress));
    }

    [Test]
    public void PushesTheProgramCounterIncrementedByTwoOnToTheStack()
    {
      var originalValue = pc.GetValue();

      new Jsr().Execute(model, memory, argument);

      var lowByte = Stack.Top(model, memory);
      Stack.Pop(model, memory);
      var highByte = Stack.Top(model, memory);
      var valuePushed = highByte << 8 | lowByte;

      Assert.That(valuePushed, Is.EqualTo(originalValue + 2));
    }
  }
}
