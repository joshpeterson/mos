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
      var result = instruction.Execute(model, memory, argument);
      return NumberOfCycles(opcode, mode, operand, result);
    }

    private int NumberOfCycles(Opcode opcode, AddressingMode mode, ushort operand,
                               Result result)
    {
      var numberOfCycles = registry.CyclesFor(opcode, mode);
      if (ArgumentUtils.CrossesPageBoundary(model, mode, operand))
        numberOfCycles++;

      if (result.HasFlag(Result.BranchTaken))
        numberOfCycles++;

      if (result.HasFlag(Result.BranchAcrossPageBoundary))
        numberOfCycles++;

      return numberOfCycles;
    }
  }
}
