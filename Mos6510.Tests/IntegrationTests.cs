using NUnit.Framework;
using Mos6510;
using System.Collections;

namespace Mos6510.Tests
{
  [TestFixture]
  public class IntegrationTests
  {
    public delegate void SetUp(ProgrammingModel model, Memory memory);

    public static object[] TestCases =
    {
        new TestCaseData(new SetUp(Empty), "Inx", "X:  0x01"),
        new TestCaseData(new SetUp(Empty), "Iny", "Y:  0x01"),
        new TestCaseData(new SetUp(InitializeForAnd), "And #$80", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForAnd), "And $1000", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForAnd), "And $FF0,X", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForAnd), "And $FF0,Y", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForAnd), "And $10", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForAnd), "And $00,X", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForAnd), "And ($10,X)", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForAnd), "And ($10),Y", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForOra), "Ora #$80", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForOra), "Ora $1000", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForOra), "Ora $FF0,X", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForOra), "Ora $FF0,Y", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForOra), "Ora $10", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForOra), "Ora $00,X", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForOra), "Ora ($10,X)", "A:  0x80"),
        new TestCaseData(new SetUp(InitializeForOra), "Ora ($10),Y", "A:  0x80"),
    };

    [TestCaseSource("TestCases")]
    public void InstructionWorksEndToEnd(SetUp setup, string input,
                                         string expectedOutput)
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      setup(model, memory);
      var repl = new Repl(model, memory);

      if (!repl.TryRead(input))
        Assert.Fail(string.Format("Unable to read assembly input: '{0}'", input));
      if (!repl.Execute())
        Assert.Fail(string.Format("Unable to execute input: '{0}'", input));
      Assert.That(repl.PrintRegisters(), Is.StringContaining(expectedOutput));
    }

    private static void Empty(ProgrammingModel model, Memory memory)
    {
    }

    private static void InitializeForAnd(ProgrammingModel model, Memory memory)
    {
      model.GetRegister(RegisterName.A).SetValue(0xFF);
      model.GetRegister(RegisterName.X).SetValue(0x10);
      model.GetRegister(RegisterName.Y).SetValue(0x10);
      memory.SetValue(0x1000, 0x80);
      memory.SetValue(0x10, 0x80);
      memory.SetValue(0x21, 0x10);
    }

    private static void InitializeForOra(ProgrammingModel model, Memory memory)
    {
      model.GetRegister(RegisterName.A).SetValue(0x00);
      model.GetRegister(RegisterName.X).SetValue(0x10);
      model.GetRegister(RegisterName.Y).SetValue(0x10);
      memory.SetValue(0x1000, 0x80);
      memory.SetValue(0x10, 0x80);
      memory.SetValue(0x21, 0x10);
    }
  }
}
