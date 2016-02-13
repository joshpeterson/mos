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

    public void Execute(Opcode opcode, AddressingMode mode)
    {
      var instruction = registry.Get(opcode);
      instruction.Execute(model, memory, mode, 0);
    }
  }
}
