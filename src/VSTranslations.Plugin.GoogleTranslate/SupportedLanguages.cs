using System.Collections.Generic;
using System.Linq;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.Plugin.GoogleTranslate;

internal static class SupportedLanguages
{
    public static IReadOnlyList<Language> Languages { get; } = new Language[]
    {
        new Language
        {
          Code =  "auto",
          Name =  "Detect language",
          Direction = LanguageDirection.Source
        },
        new Language
        {
          Code =  "af",
          Name =  "Afrikaans",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "sq",
          Name =  "Albanian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "am",
          Name =  "Amharic",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ar",
          Name =  "Arabic",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "hy",
          Name =  "Armenian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "as",
          Name =  "Assamese",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ay",
          Name =  "Aymara",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "az",
          Name =  "Azerbaijani",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "bm",
          Name =  "Bambara",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "eu",
          Name =  "Basque",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "be",
          Name =  "Belarusian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "bn",
          Name =  "Bengali",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "bho",
          Name =  "Bhojpuri",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "bs",
          Name =  "Bosnian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "bg",
          Name =  "Bulgarian",
          Direction = LanguageDirection.TwoWay
        },
        new Language 
        {
          Code =  "ca",
          Name =  "Catalan",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ceb",
          Name =  "Cebuano",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ny",
          Name =  "Chichewa",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "zh-CN",
          Name =  "Chinese",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "co",
          Name =  "Corsican",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "hr",
          Name =  "Croatian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "cs",
          Name =  "Czech",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "da",
          Name =  "Danish",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "dv",
          Name =  "Dhivehi",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "doi",
          Name =  "Dogri",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "nl",
          Name =  "Dutch",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "en",
          Name =  "English",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "eo",
          Name =  "Esperanto",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "et",
          Name =  "Estonian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ee",
          Name =  "Ewe",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "tl",
          Name =  "Filipino",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "fi",
          Name =  "Finnish",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "fr",
          Name =  "French",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "fy",
          Name =  "Frisian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "gl",
          Name =  "Galician",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ka",
          Name =  "Georgian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "de",
          Name =  "German",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "el",
          Name =  "Greek",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "gn",
          Name =  "Guarani",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "gu",
          Name =  "Gujarati",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ht",
          Name =  "Haitian Creole",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ha",
          Name =  "Hausa",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "haw",
          Name =  "Hawaiian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "iw",
          Name =  "Hebrew",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "hi",
          Name =  "Hindi",
          Direction = LanguageDirection.TwoWay
        },
        new Language {
          Code =  "hmn",
          Name =  "Hmong",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "hu",
          Name =  "Hungarian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "is",
          Name =  "Icelandic",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ig",
          Name =  "Igbo",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ilo",
          Name =  "Ilocano",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "id",
          Name =  "Indonesian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ga",
          Name =  "Irish",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "it",
          Name =  "Italian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ja",
          Name =  "Japanese",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "jw",
          Name =  "Javanese",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "kn",
          Name =  "Kannada",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "kk",
          Name =  "Kazakh",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "km",
          Name =  "Khmer",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "rw",
          Name =  "Kinyarwanda",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "gom",
          Name =  "Konkani",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ko",
          Name =  "Korean",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "kri",
          Name =  "Krio",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ku",
          Name =  "Kurdish (Kurmanji)",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ckb",
          Name =  "Kurdish (Sorani)",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ky",
          Name =  "Kyrgyz",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "lo",
          Name =  "Lao",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "la",
          Name =  "Latin",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "lv",
          Name =  "Latvian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ln",
          Name =  "Lingala",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "lt",
          Name =  "Lithuanian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "lg",
          Name =  "Luganda",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "lb",
          Name =  "Luxembourgish",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "mk",
          Name =  "Macedonian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "mai",
          Name =  "Maithili",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "mg",
          Name =  "Malagasy",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ms",
          Name =  "Malay",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ml",
          Name =  "Malayalam",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "mt",
          Name =  "Maltese",
          Direction = LanguageDirection.TwoWay
        },
        new Language {
          Code =  "mi",
          Name =  "Maori",
          Direction = LanguageDirection.TwoWay
        },
        new Language 
        {
          Code =  "mr",
          Name =  "Marathi",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "mni-Mtei",
          Name =  "Meiteilon (Manipuri)",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "lus",
          Name =  "Mizo",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "mn",
          Name =  "Mongolian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "my",
          Name =  "Myanmar (Burmese)",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ne",
          Name =  "Nepali",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "no",
          Name =  "Norwegian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "or",
          Name =  "Odia (Oriya)",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "om",
          Name =  "Oromo",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ps",
          Name =  "Pashto",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "fa",
          Name =  "Persian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "pl",
          Name =  "Polish",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "pt",
          Name =  "Portuguese",
          Direction = LanguageDirection.TwoWay
        },
        new Language {
          Code =  "pa",
          Name =  "Punjabi",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "qu",
          Name =  "Quechua",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ro",
          Name =  "Romanian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ru",
          Name =  "Russian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "sm",
          Name =  "Samoan",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "sa",
          Name =  "Sanskrit",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "gd",
          Name =  "Scots Gaelic",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "nso",
          Name =  "Sepedi",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "sr",
          Name =  "Serbian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "st",
          Name =  "Sesotho",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "sn",
          Name =  "Shona",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "sd",
          Name =  "Sindhi",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "si",
          Name =  "Sinhala",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "sk",
          Name =  "Slovak",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "sl",
          Name =  "Slovenian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "so",
          Name =  "Somali",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "es",
          Name =  "Spanish",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "su",
          Name =  "Sundanese",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "sw",
          Name =  "Swahili",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "sv",
          Name =  "Swedish",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "tg",
          Name =  "Tajik",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ta",
          Name =  "Tamil",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "tt",
          Name =  "Tatar",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "te",
          Name =  "Telugu",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "th",
          Name =  "Thai",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ti",
          Name =  "Tigrinya",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ts",
          Name =  "Tsonga",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "tr",
          Name =  "Turkish",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "tk",
          Name =  "Turkmen",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ak",
          Name =  "Twi",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "uk",
          Name =  "Ukrainian",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ur",
          Name =  "Urdu",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "ug",
          Name =  "Uyghur",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "uz",
          Name =  "Uzbek",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "vi",
          Name =  "Vietnamese",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "cy",
          Name =  "Welsh",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "xh",
          Name =  "Xhosa",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "yi",
          Name =  "Yiddish",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "yo",
          Name =  "Yoruba",
          Direction = LanguageDirection.TwoWay
        },
        new Language
        {
          Code =  "zu",
          Name =  "Zulu",
          Direction = LanguageDirection.TwoWay
        }
    };
}
