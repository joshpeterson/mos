using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class CmpTests : CompareTestUtils<Cmp>
  {
    public CmpTests() : base(RegisterName.A) {}
  }
}
