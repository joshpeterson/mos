using NUnit.Framework;

namespace Mos6510.Tests
{
  [TestFixture]
  public class IntegrationTests
  {
    public delegate void SetUp(ProgrammingModel model, Memory memory);

    public static object[] RegisterTestCases =
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
      new TestCaseData(new SetUp(InitializeForEor), "Eor #$FF", "A:  0xF5"),
      new TestCaseData(new SetUp(InitializeForEor), "Eor $1000", "A:  0xF5"),
      new TestCaseData(new SetUp(InitializeForEor), "Eor $FF0,X", "A:  0xF5"),
      new TestCaseData(new SetUp(InitializeForEor), "Eor $FF0,Y", "A:  0xF5"),
      new TestCaseData(new SetUp(InitializeForEor), "Eor $10", "A:  0xF5"),
      new TestCaseData(new SetUp(InitializeForEor), "Eor $00,X", "A:  0xF5"),
      new TestCaseData(new SetUp(InitializeForEor), "Eor ($10,X)", "A:  0xF5"),
      new TestCaseData(new SetUp(InitializeForEor), "Eor ($10),Y", "A:  0xF5"),
      new TestCaseData(new SetUp(InitializeForLda), "Lda #$77", "A:  0x77"),
      new TestCaseData(new SetUp(InitializeForLda), "Lda $1000", "A:  0xDF"),
      new TestCaseData(new SetUp(InitializeForAsl), "Asl A", "A:  0xFE"),
    };

    public static object[] MemoryTestCases =
    {
      new TestCaseData(new SetUp(InitializeForSta), "Sta $2400", 0x2400, 0xFF),
    };

    [TestCaseSource("RegisterTestCases")]
    public void RegisterInstructionWorksEndToEnd(SetUp setup, string input,
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

    [TestCaseSource("MemoryTestCases")]
    public void MemoryInstructionWorksEndToEnd(SetUp setup, string input,
        int addressToCheck,
        int expectedValue)
    {
      var model = new ProgrammingModel();
      var memory = new Memory();
      setup(model, memory);
      var repl = new Repl(model, memory);

      if (!repl.TryRead(input))
        Assert.Fail(string.Format("Unable to read assembly input: '{0}'", input));
      if (!repl.Execute())
        Assert.Fail(string.Format("Unable to execute input: '{0}'", input));
      Assert.That(memory.GetValue((ushort)addressToCheck),
                  Is.EqualTo(expectedValue));
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

    private static void InitializeForEor(ProgrammingModel model, Memory memory)
    {
      model.GetRegister(RegisterName.A).SetValue(0x0A);
      model.GetRegister(RegisterName.X).SetValue(0x10);
      model.GetRegister(RegisterName.Y).SetValue(0x10);
      memory.SetValue(0x1000, 0xFF);
      memory.SetValue(0x10, 0xFF);
      memory.SetValue(0x21, 0x10);
    }

    private static void InitializeForLda(ProgrammingModel model, Memory memory)
    {
      model.GetRegister(RegisterName.A).SetValue(0x0A);
      memory.SetValue(0x1000, 0xDF);
    }

    private static void InitializeForSta(ProgrammingModel model, Memory memory)
    {
      model.GetRegister(RegisterName.A).SetValue(0xFF);
      memory.SetValue(0x2400, 0x87);
    }

    private static void InitializeForAsl(ProgrammingModel model, Memory memory)
    {
      model.GetRegister(RegisterName.A).SetValue(0x7F);
    }
  }
}
