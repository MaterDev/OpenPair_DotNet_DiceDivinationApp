namespace Astrology;

using RC.Moon;

public class Lunar
{
    public Dictionary<string, object> PrintCurrentMoonPhase()
    {
        LunaZodiaco.MoonDay currentMoonPhase = LunaZodiaco.getMoonDay(DateTime.UtcNow);

        int zodiacIndex = Array.IndexOf(Zodiac.zodiacsStrings, Array.Find(Zodiac.zodiacsStrings, zodiac => zodiac == currentMoonPhase.Zodiac.ToString()));
        int moonPhaseIndex = Array.IndexOf(MoonPhase.moonPhaseStrings, Array.Find(MoonPhase.moonPhaseStrings, phase => phase == currentMoonPhase.Phase.ToString()));

        var moonPhaseContent = new Dictionary<string, object>
    {
        {"phase", currentMoonPhase.Phase.ToString()},
        {"zodiac", currentMoonPhase.Zodiac.ToString()},
        {"zodiac_sign", Zodiac.zodiacsUnicode[zodiacIndex]},
        {"moon_phase_unicode", MoonPhase.moonPhaseUnicode[moonPhaseIndex]}
        // Add more properties as needed
    };

        return moonPhaseContent;
    }
}
