using Mos6510.Instructions;

namespace Mos6510
{
  public class Executor
  {
    private readonly Registry registry;
    private readonly ProgrammingModel model;

    public Executor(Registry registry, ProgrammingModel model, Memory memory)
    {
      this.registry = registry;
      this.model = model;
    }

    public void Execute(Opcode opcode, AddressingMode mode)
    {
      var instruction = registry.Get(opcode);
      instruction.Execute(model);
    }
  }
}
