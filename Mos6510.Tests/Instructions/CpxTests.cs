using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class CpxTests : CompareTestUtils<Cpx>
  {
    public CpxTests() : base(RegisterName.X) {}
  }
}
