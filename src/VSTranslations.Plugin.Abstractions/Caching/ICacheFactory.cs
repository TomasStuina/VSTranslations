namespace VSTranslations.Plugin.Abstractions.Caching;

/// <summary>
/// An interface describing <see cref="ICache"/> factory.
/// </summary>
public interface ICacheFactory
{
    /// <summary>
    /// Creates <see cref="ICache"/> instance.
    /// </summary>
    /// <returns><see cref="ICache"/> instance.</returns>
    ICache Create();
}