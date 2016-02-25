using Mos6510.Instructions;

namespace Mos6510
{
  public class Executor
  {
    private readonly Registry registry;
    private readonly ProgrammingModel model;
    private readonly Memory memory;

    public Executor(Registry registry, ProgrammingModel model, Memory memory)
    {
      this.registry = registry;
      this.model = model;
      this.memory = memory;
    }

    public void Execute(Opcode opcode, AddressingMode mode, ushort operand)
    {
      byte argument = 0;
      if (mode != AddressingMode.Implied)
        argument = ArgumentUtils.ArgumentFor(model, memory, mode, operand);
      var instruction = registry.Get(opcode);
      instruction.Execute(model, memory, argument);
    }
  }
}
