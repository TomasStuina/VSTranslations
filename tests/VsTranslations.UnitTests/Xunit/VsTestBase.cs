using Microsoft.VisualStudio.Sdk.TestFramework;
using Xunit;

namespace VSTranslations.UnitTests.Xunit
{
    [Collection(MockedVS.Collection)]
    public class VsTestBase
    {
        protected VsTestBase(GlobalServiceProvider serviceProvider)
        {
            serviceProvider.Reset();
        }
    }
}
