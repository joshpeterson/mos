using System;
using Mos6510.Instructions;

namespace Mos6510
{
  public class Repl
  {
    private readonly ProgrammingModel model;
    private readonly Memory memory;

    private readonly Assembler assembler;

    private readonly Fetcher fetcher;
    private readonly Decoder decoder;
    private readonly Executor executor;

    public Repl(ProgrammingModel model, Memory memory)
    {
      this.model = model;
      this.memory = memory;
      this.assembler = new Assembler(InstructionRegistry.All);
      this.fetcher = new Fetcher(model, memory);
      this.decoder = new Decoder(InstructionRegistry.All);
      this.executor = new Executor(InstructionRegistry.All, model, memory);
    }

    public bool TryRead(string line)
    {
      var code = assembler.GetByteCode(line);
      if (code == 0x00)
        return false;

      memory.SetValue((ushort)model.GetRegister(RegisterName.PC).GetValue(), code);
      return true;
    }

    public void Execute()
    {
      var code = fetcher.Fetch();
      OpcodeAddressModePair pair;
      if (decoder.TryDecode(code, out pair))
        executor.Execute(pair.Opcode, pair.Mode, 0);
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
