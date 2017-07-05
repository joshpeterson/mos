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

    [Test]
    public void ReturnsSuccessWhenTheCarryFlagIsClear()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.CarryFlag = false;

      const ushort branchTarget = 0xD000;
      var result = new Bcc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.Success));
    }

    [Test]
    public void ReturnsSuccessWhenTheCarryFlagIsSet()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.CarryFlag = true;

      const ushort branchTarget = 0xD000;
      var result = new Bcc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.Success));
    }

    [Test]
    public void ReturnsBranchTakenWhenTheCarryFlagIsClear()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.CarryFlag = false;

      const ushort branchTarget = 0xD000;
      var result = new Bcc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.BranchTaken));
    }

    [Test]
    public void DoesNotReturnsBranchTakenWhenTheCarryFlagIsSet()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.CarryFlag = true;

      const ushort branchTarget = 0xD000;
      var result = new Bcc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(!result.HasFlag(Result.BranchTaken));
    }

    [Test]
    public void ReturnsBranchAcrossPageBoundaryWhenBranchIsFarEnough()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.CarryFlag = false;

      const ushort branchTarget = 0xD000;
      var result = new Bcc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.BranchAcrossPageBoundary));
    }

    [Test]
    public void DoesNotReturnBranchAcrossPageBoundaryWhenBranchIsCloseEnough()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.CarryFlag = false;

      const ushort branchTarget = 0xC100;
      var result = new Bcc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(!result.HasFlag(Result.BranchAcrossPageBoundary));
    }
  }
}
