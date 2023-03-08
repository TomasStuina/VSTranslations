namespace VSTranslations.Plugin.Abstractions.Translating;

public class Language
{
    public string Name { get; set; }

    public string Code { get; set; }

    public LanguageDirection Direction { get; set; }

    public static Language Invariant { get; } = new Language { Code = "en", Name = "English" };
}