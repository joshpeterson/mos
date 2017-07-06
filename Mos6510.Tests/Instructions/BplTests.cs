using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class BplTests
  {
    [Test]
    public void SetsPCWhenNegativeFlagIsClear()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.NegativeFlag = false;

      const ushort branchTarget = 0xD000;
      new Bpl().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(programCounter.GetValue(), Is.EqualTo(branchTarget));
    }

    [Test]
    public void DoesNotSetPCWhenNegativeFlagIsSet()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      const ushort previousPCValue = 0xC000;
      programCounter.SetValue(previousPCValue);
      model.NegativeFlag = true;

      new Bpl().Execute(model, null, new Argument(0, 0xD000));
      Assert.That(programCounter.GetValue(), Is.EqualTo(previousPCValue));
    }

    [Test]
    public void ReturnsSuccessWhenTheNegativeFlagIsClear()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.NegativeFlag = false;

      const ushort branchTarget = 0xD000;
      var result = new Bpl().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.Success));
    }

    [Test]
    public void ReturnsSuccessWhenTheNegativeFlagIsSet()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.NegativeFlag = true;

      const ushort branchTarget = 0xD000;
      var result = new Bpl().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.Success));
    }

    [Test]
    public void ReturnsBranchTakenWhenTheNegativeFlagIsClear()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.NegativeFlag = false;

      const ushort branchTarget = 0xD000;
      var result = new Bpl().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.BranchTaken));
    }

    [Test]
    public void DoesNotReturnsBranchTakenWhenTheNegativeFlagIsSet()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.NegativeFlag = true;

      const ushort branchTarget = 0xD000;
      var result = new Bpl().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(!result.HasFlag(Result.BranchTaken));
    }

    [Test]
    public void ReturnsBranchAcrossPageBoundaryWhenBranchIsFarEnough()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.NegativeFlag = false;

      const ushort branchTarget = 0xD000;
      var result = new Bpl().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.BranchAcrossPageBoundary));
    }

    [Test]
    public void DoesNotReturnBranchAcrossPageBoundaryWhenBranchIsCloseEnough()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.NegativeFlag = false;

      const ushort branchTarget = 0xC100;
      var result = new Bpl().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(!result.HasFlag(Result.BranchAcrossPageBoundary));
    }
  }
}
