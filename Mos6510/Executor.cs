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

    public int Execute(Opcode opcode, AddressingMode mode, ushort operand)
    {
      Argument argument = new Argument(0,0);
      if (mode != AddressingMode.Implied)
        argument = ArgumentUtils.ArgumentFor(model, memory, mode, operand);

      var instruction = registry.Get(opcode);
      instruction.Execute(model, memory, argument);

      var numberOfCycles = registry.CyclesFor(opcode, mode);
      if (ArgumentUtils.CrossesPageBoundary(model, mode, operand))
        numberOfCycles++;
      return numberOfCycles;
    }
  }
}
