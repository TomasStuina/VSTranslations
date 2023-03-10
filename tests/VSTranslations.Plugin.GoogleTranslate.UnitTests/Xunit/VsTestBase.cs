using Microsoft.VisualStudio.Sdk.TestFramework;
using Xunit;

namespace VSTranslations.Plugin.GoogleTranslate.UnitTests.Xunit
{
    [Collection(MockedVS.Collection)]
    public class VsTestBase
    {
        protected VsTestBase(GlobalServiceProvider serviceProvider)
        {
            serviceProvider.Reset();
            ServiceProvider = serviceProvider;
        }

        protected GlobalServiceProvider ServiceProvider { get; }
    }
}
