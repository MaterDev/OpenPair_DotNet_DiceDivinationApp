namespace Astrology;

using RC.Moon;
using Translator;

public class Lunar
{
    
    public Dictionary<string, object> GetCurrentMoonPhase()
    {
        Translator translator = new();
        
        LunaZodiaco.MoonDay currentMoonPhase = LunaZodiaco.getMoonDay(DateTime.UtcNow);

        int zodiacIndex = Array.IndexOf(Zodiac.zodiacsStrings, Array.Find(Zodiac.zodiacsStrings, zodiac => zodiac == currentMoonPhase.Zodiac.ToString()));
        int moonPhaseIndex = Array.IndexOf(MoonPhase.moonPhaseStrings, Array.Find(MoonPhase.moonPhaseStrings, phase => phase == currentMoonPhase.Phase.ToString()));

        Console.WriteLine($"All Moon Phases: {MoonPhase.moonPhaseStrings}");
        Console.WriteLine($"All Zodiacs: {Zodiac.zodiacsStrings}");

        var moonPhaseContent = new Dictionary<string, object>
    {
        {"phase", translator.TranslatePhase(currentMoonPhase.Phase.ToString())},
        {"zodiac", translator.TranslateZodiac(currentMoonPhase.Zodiac.ToString())},
        {"zodiac_emoji", Zodiac.zodiacsUnicode[zodiacIndex]},
        {"moon_phase_emoji", MoonPhase.moonPhaseUnicode[moonPhaseIndex]},
    };

        return moonPhaseContent;
    }
}
