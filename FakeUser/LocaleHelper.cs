using System.Collections.Generic;

namespace FakeUser
{
    public class LocaleHelper
    {
        public static Dictionary<string, string> Map = new Dictionary<string, string>
        {
            { "United States", "en" },
            { "United Kingdom", "en_GB" },
            { "Australia", "en_AU" },
            { "Canada", "en_CA" },
            { "Ireland", "en_IE" },
            { "India", "en_IN" },
            { "New Zealand", "en_NZ" },
            { "South Africa", "en_ZA" },

            { "Germany", "de" },
            { "Austria", "de_AT" },
            { "Switzerland", "de_CH" },
            { "France", "fr" },
            { "Belgium", "fr_BE" },
            { "Spain", "es" },
            { "Mexico", "es_MX" },
            { "Italy", "it" },
            { "Brazil", "pt_BR" },
            { "Portugal", "pt_PT" },
            { "Netherlands", "nl" },
            { "Poland", "pl" },
            { "Russia", "ru" },
            { "Sweden", "sv" },
            { "Norway", "nb_NO" },
            { "Denmark", "da" },
            { "Finland", "fi" },
            { "Romania", "ro" },
            { "Slovakia", "sk" },
            { "Greece", "el" },
            { "Czech Republic", "cs" },
            { "Ukraine", "uk" },
            { "Turkey", "tr" },

            { "China", "zh_CN" },
            { "Taiwan", "zh_TW" },
            { "Japan", "ja" },
            { "South Korea", "ko" },
            { "Indonesia", "id_ID" },
            { "Thailand", "th" },
            { "Vietnam", "vi" },

            { "Saudi Arabia", "ar" },
            { "Iran", "fa" },
            { "Israel", "he" },

            { "Azerbaijan", "az" },
            { "Georgia", "ge" },
            { "Croatia", "hr" },
            { "Hungary", "hu" },
            { "Armenia", "hy" },
            { "Latvia", "lv" },
            { "North Macedonia", "mk" },
            { "Nepal", "ne" },
            { "Serbia", "sr_RS_latin" }
        };

        /// <summary>
        /// Gets the locale code for a given country name
        /// </summary>
        /// <param name="countryName">Country name in English</param>
        /// <returns>Locale code or null if not found</returns>
        public static string GetLocale(string countryName)
        {
            return Map.TryGetValue(countryName, out var locale) ? locale : null;
        }

        /// <summary>
        /// Checks if a country is in the map
        /// </summary>
        /// <param name="countryName">Country name in English</param>
        /// <returns>True if country exists in map</returns>
        public static bool ContainsCountry(string countryName)
        {
            return Map.ContainsKey(countryName);
        }
    }
}