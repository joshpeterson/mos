using System;
using System.Linq;
using Mos6510.Instructions;

namespace Mos6510
{
  public class Repl
  {
    private readonly InstructionRegistry registry;

    private readonly ProgrammingModel model;
    private readonly Memory memory;

    private readonly Assembler assembler;

    private readonly Fetcher fetcher;
    private readonly Decoder decoder;
    private readonly Executor executor;

    private readonly CoreLoop coreLoop;

    public Repl(ProgrammingModel model, Memory memory)
    {
      registry = new InstructionRegistry();
      this.model = model;
      this.memory = memory;
      assembler = new Assembler(registry.All);
      fetcher = new Fetcher(model, memory);
      decoder = new Decoder(registry.All);
      executor = new Executor(registry.All, model, memory);
      coreLoop = new CoreLoop(fetcher, decoder, executor);
    }

    public bool TryRead(string line)
    {
      var disassembly = assembler.GetDisassembly(line);
      if (!disassembly.Any())
        return false;

      var address = (ushort)model.GetRegister(RegisterName.PC).GetValue();
      foreach (var datum in disassembly)
        memory.SetValue(address++, datum);

      return true;
    }

    public bool Execute()
    {
      return coreLoop.SingleStep();
    }

    public string PrintRegisters()
    {
      const string registerFormat = @"
        Registers:
        A:  0x{0:X2}
        Y:  0x{1:X2}
        X:  0x{2:X2}
        PC: 0x{3:X4}
        S:  0x{4:X2}
        P:  {5}b
            NVXBDIZC";

        return string.Format(registerFormat,
          model.GetRegister(RegisterName.A).GetValue(),
          model.GetRegister(RegisterName.Y).GetValue(),
          model.GetRegister(RegisterName.X).GetValue(),
          model.GetRegister(RegisterName.PC).GetValue(),
          model.GetRegister(RegisterName.S).GetValue(),
          Convert.ToString(model.GetRegister(RegisterName.P).GetValue(), 2).
            PadLeft(8, '0'));
    }
  }
}
