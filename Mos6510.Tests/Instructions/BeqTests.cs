using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class BeqTests
  {
    [Test]
    public void SetsPCWhenZeroFlagIsSet()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.ZeroFlag = true;

      const ushort branchTarget = 0xD000;
      new Beq().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(programCounter.GetValue(), Is.EqualTo(branchTarget));
    }

    [Test]
    public void DoesNotSetPCWhenZeroFlagIsClear()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      const ushort previousPCValue = 0xC000;
      programCounter.SetValue(previousPCValue);
      model.ZeroFlag = false;

      new Beq().Execute(model, null, new Argument(0, 0xD000));
      Assert.That(programCounter.GetValue(), Is.EqualTo(previousPCValue));
    }

    [Test]
    public void ReturnsSuccessWhenTheZeroFlagIsClear()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.ZeroFlag = false;

      const ushort branchTarget = 0xD000;
      var result = new Beq().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.Success));
    }

    [Test]
    public void ReturnsSuccessWhenTheZeroFlagIsSet()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.ZeroFlag = true;

      const ushort branchTarget = 0xD000;
      var result = new Beq().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.Success));
    }

    [Test]
    public void ReturnsBranchTakenWhenTheZeroFlagIsSet()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.ZeroFlag = true;

      const ushort branchTarget = 0xD000;
      var result = new Beq().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.BranchTaken));
    }

    [Test]
    public void DoesNotReturnsBranchTakenWhenTheZeroFlagIsClear()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.ZeroFlag = false;

      const ushort branchTarget = 0xD000;
      var result = new Beq().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(!result.HasFlag(Result.BranchTaken));
    }

    [Test]
    public void ReturnsBranchAcrossPageBoundaryWhenBranchIsFarEnough()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.ZeroFlag = true;

      const ushort branchTarget = 0xD000;
      var result = new Beq().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(result.HasFlag(Result.BranchAcrossPageBoundary));
    }

    [Test]
    public void DoesNotReturnBranchAcrossPageBoundaryWhenBranchIsCloseEnough()
    {
      var model = new ProgrammingModel();
      var programCounter = model.GetRegister(RegisterName.PC);
      programCounter.SetValue(0xC000);
      model.ZeroFlag = true;

      const ushort branchTarget = 0xC100;
      var result = new Beq().Execute(model, null, new Argument(0, branchTarget));
      Assert.That(!result.HasFlag(Result.BranchAcrossPageBoundary));
    }
  }
}
