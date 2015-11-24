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
      var addressHigh = _model.GetRegister(RegisterName.PCH).GetValue();
      var addressLow = _model.GetRegister(RegisterName.PCL).GetValue();
      return _memory.GetValue((ushort)(addressLow + (addressHigh << 8)));
    }
  }
}
