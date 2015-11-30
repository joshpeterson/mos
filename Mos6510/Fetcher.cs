namespace Mos6510
{
  public class Fetcher
  {
    private readonly ProgrammingModel _model;
    private readonly Memory _memory;

    public Fetcher(ProgrammingModel model, Memory memory)
    {
      _model = model;
      _memory = memory;
    }

    public byte Fetch()
    {
      var pc = _model.GetRegister(RegisterName.PC);
      var address = pc.GetValue();
      pc.SetValue(address + 1);

      return _memory.GetValue((ushort)address);
    }
  }
}
