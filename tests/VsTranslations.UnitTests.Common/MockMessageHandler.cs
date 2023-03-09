using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace VSTranslations.UnitTests.Common;

public class MockMessageHandler : HttpMessageHandler
{
    private readonly HttpMessageDelegate _messageDelegate;
    public MockMessageHandler(HttpMessageDelegate messageDelegate)
    {
        _messageDelegate = messageDelegate;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => _messageDelegate(request);
}