namespace Mos6510
{
  public class Fetcher
  {
    private readonly ProgrammingModel model;
    private readonly Memory memory;

    public Fetcher(ProgrammingModel model, Memory memory)
    {
      this.model = model;
      this.memory = memory;
    }

    public byte Fetch()
    {
      var pc = model.GetRegister(RegisterName.PC);
      var address = pc.GetValue();
      pc.SetValue(address + 1);

      return memory.GetValue((ushort)address);
    }
  }
}
