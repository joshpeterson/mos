using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class TxsTests
  {
    [Test]
    public void CopiesTheXRegisterToTheStackPointer()
    {
      var model = new ProgrammingModel();
      byte expectedValue = 0x64;
      model.GetRegister(RegisterName.X).SetValue(expectedValue);

      new Txs().Execute(model, null, null);

      Assert.That(model.GetRegister(RegisterName.S).GetValue(),
                  Is.EqualTo(expectedValue));
    }
  }
}
