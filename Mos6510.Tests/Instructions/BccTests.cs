using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class BccTests
  {
    [Test]
    public void SetsPCWhenCarryFlagIsClear()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.CarryFlag = false;

      const ushort branchTarget = 0xD000;
      new Bcc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(programCounter.GetValue(), Is.EqualTo(branchTarget));
    }

    [Test]
    public void DoesNotSetPCWhenCarryFlagIsSet()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      const ushort previousPCValue = 0xC000;
      programCounter.SetValue(previousPCValue);
      model.CarryFlag = true;

      new Bcc().Execute(model, null, new Argument(0, 0xD000));
      Assert.That(programCounter.GetValue(), Is.EqualTo(previousPCValue));
    }
  }
}
