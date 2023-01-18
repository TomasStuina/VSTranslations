using Moq.Language;
using System.Collections.Generic;

namespace VsTranslations.UnitTests.Common.Moq.Extensions
{
    public static class SetupSequentialResultExtensions
    {
        public static ISetupSequentialResult<TResult> ReturnsMany<TResult>(this ISetupSequentialResult<TResult> setupSequentialResult, params TResult[] results)
        {
            return setupSequentialResult.ReturnsMany((IEnumerable<TResult>)results);
        }

        public static ISetupSequentialResult<TResult> ReturnsMany<TResult>(this ISetupSequentialResult<TResult> setupSequentialResult, IEnumerable<TResult> results)
        {
            foreach (var result in results)
            {
                setupSequentialResult = setupSequentialResult.Returns(result);
            }

            return setupSequentialResult;
        }
    }
}
