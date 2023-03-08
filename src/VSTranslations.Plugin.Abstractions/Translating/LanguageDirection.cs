using System;

namespace VSTranslations.Plugin.Abstractions.Translating;

/// <summary>
/// Defines the direction of a language,
/// whether a language can be used as a source or/and a target.
/// </summary>
[Flags]
public enum LanguageDirection
{
    None = 0,
    Source,
    Target,
    TwoWay = Source | Target,
}