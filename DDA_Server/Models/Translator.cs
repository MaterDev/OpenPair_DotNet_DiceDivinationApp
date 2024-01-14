namespace Translator;

public class Translator
{
    private Dictionary<string, string> moonPhaseTranslation = new Dictionary<string, string>
    {
        {"Luna nueva", "New Moon"},
        {"Luna creciente", "Waxing Crescent"},
        {"Cuarto creciente", "First Quarter"},
        {"Creciente gibosa", "Waxing Gibbous"},
        {"Luna Llena", "Full Moon"},
        {"Menguante gibosa", "Waning Gibbous"},
        {"Cuarto menguante", "Last Quarter"},
        {"Luna menguante", "Waning Crescent"}
    };

    private Dictionary<string, string> zodiacTranslation = new Dictionary<string, string>
    {
        {"Aries", "Aries"},
        {"Tauro", "Taurus"},
        {"Géminis", "Gemini"},
        {"Cáncer", "Cancer"},
        {"Leo", "Leo"},
        {"Virgo", "Virgo"},
        {"Libra", "Libra"},
        {"Escorpio", "Scorpio"},
        {"Sagitario", "Sagittarius"},
        {"Capricornio", "Capricorn"},
        {"Acuario", "Aquarius"},
        {"Piscis", "Pisces"}
    };

    public string TranslatePhase(string phaseInSpanish)
    {
        return moonPhaseTranslation.ContainsKey(phaseInSpanish) ? moonPhaseTranslation[phaseInSpanish] : "Unknown";
    }

    public string TranslateZodiac(string zodiacInSpanish)
    {
        return zodiacTranslation.ContainsKey(zodiacInSpanish) ? zodiacTranslation[zodiacInSpanish] : "Unknown";
    }
}
