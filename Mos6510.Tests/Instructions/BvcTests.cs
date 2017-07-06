using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class BvcTests
  {
    [Test]
    public void SetsPCWhenOverflowFlagIsClear()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.OverflowFlag = false;

      const ushort branchTarget = 0xD000;
      new Bvc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(programCounter.GetValue(), Is.EqualTo(branchTarget));
    }

    [Test]
    public void DoesNotSetPCWhenOverflowFlagIsSet()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      const ushort previousPCValue = 0xC000;
      programCounter.SetValue(previousPCValue);
      model.OverflowFlag = true;

      new Bvc().Execute(model, null, new Argument(0, 0xD000));
      Assert.That(programCounter.GetValue(), Is.EqualTo(previousPCValue));
    }

    [Test]
    public void ReturnsSuccessWhenTheOverflowFlagIsClear()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.OverflowFlag = false;

      const ushort branchTarget = 0xD000;
      var result = new Bvc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.Success));
    }

    [Test]
    public void ReturnsSuccessWhenTheOverflowFlagIsSet()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.OverflowFlag = true;

      const ushort branchTarget = 0xD000;
      var result = new Bvc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.Success));
    }

    [Test]
    public void ReturnsBranchTakenWhenTheOverflowFlagIsClear()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.OverflowFlag = false;

      const ushort branchTarget = 0xD000;
      var result = new Bvc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.BranchTaken));
    }

    [Test]
    public void DoesNotReturnsBranchTakenWhenTheOverflowFlagIsSet()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.OverflowFlag = true;

      const ushort branchTarget = 0xD000;
      var result = new Bvc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(!result.HasFlag(Result.BranchTaken));
    }

    [Test]
    public void ReturnsBranchAcrossPageBoundaryWhenBranchIsFarEnough()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.OverflowFlag = false;

      const ushort branchTarget = 0xD000;
      var result = new Bvc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.BranchAcrossPageBoundary));
    }

    [Test]
    public void DoesNotReturnBranchAcrossPageBoundaryWhenBranchIsCloseEnough()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.OverflowFlag = false;

      const ushort branchTarget = 0xC100;
      var result = new Bvc().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(!result.HasFlag(Result.BranchAcrossPageBoundary));
    }
  }
}
