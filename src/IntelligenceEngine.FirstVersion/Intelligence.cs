using DataClasses;
using Microsoft.Extensions.Configuration;

namespace IntelligenceEngine.FirstVersion;

public class Intelligence : IIntelligence
{
    private const string LANG = "cat";
    private readonly IConfiguration Configuration;

    public Intelligence(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public enum CaseEnum
    {
        // Without Description
        FirstNonDescriptionToot,
        SecondNonDescriptionToot,
        ThirdNonDescriptionToot,
        ConsecutiveNonDescriptionTootMultiple50,

        // With Description
        ConsecutiveDescription5Toot,
        ConsecutiveDescription15Toot,
        ConsecutiveDescriptionMultiple50Toot,

        // With and Without
        PctGte80Multiple50Toot,
        PctLt80Gte20Multiple50Toot,
        PctLt20Gte5Multiple50Toot,

        //
        NoCase
    }
    public string GetTootFromData(IEnumerable<MediaToot> toots)
    {
        var situation = GetCase(toots);
        return GetTootFromCase(situation);
    }

    public CaseEnum GetCase(IEnumerable<MediaToot> toots)
    {
        // Without description

        if (toots.Count() == 1 && !toots.First().HasAltText)
        {
            return CaseEnum.FirstNonDescriptionToot;
        }

        if (toots.Count() == 2 && toots.All(x => !x.HasAltText))
        {
            return CaseEnum.SecondNonDescriptionToot;
        }

        if (toots.Count() == 3 && toots.All(x => !x.HasAltText))
        {
            return CaseEnum.ThirdNonDescriptionToot;
        }

        var lastNonDescription = GetLatestConsecutive(toots, withDescription: false);

        if (lastNonDescription.Any() && lastNonDescription.Count() % 50 == 0 && lastNonDescription.All(x => !x.HasAltText))
        {
            return CaseEnum.ConsecutiveNonDescriptionTootMultiple50;
        }

        // with Description
        var startingAtWithDesc = SkipFirstsNonDescriptionToots(toots);

        if (startingAtWithDesc.Count() == 5 && startingAtWithDesc.All(x => x.HasAltText))
        {
            return CaseEnum.ConsecutiveDescription5Toot;
        }

        if (startingAtWithDesc.Count() == 15 && startingAtWithDesc.All(x => x.HasAltText))
        {
            return CaseEnum.ConsecutiveDescription15Toot;
        }

        var latestsWithDesc = GetLatestConsecutive(toots, withDescription: true);

        if (latestsWithDesc.Any() && latestsWithDesc.Count() % 50 == 0 && startingAtWithDesc.All(x => x.HasAltText))
        {
            return CaseEnum.ConsecutiveDescriptionMultiple50Toot;
        }

        // pct
        var nToots = toots.Count();
        var isMultiple50 = nToots % 50 == 0 && toots.Any();
        var last50Toots = toots.Skip(nToots-50).ToList();
        var pct = last50Toots.Where(t => t.HasAltText).Count() / nToots;

        if (isMultiple50 && pct >= 80)
        {
            return CaseEnum.PctGte80Multiple50Toot;
        }

        if (isMultiple50 && pct >= 20)
        {
            return CaseEnum.PctLt80Gte20Multiple50Toot;
        }

        if (isMultiple50 && pct >= 5)
        {
            return CaseEnum.PctLt20Gte5Multiple50Toot;
        }

        // No case
        return CaseEnum.NoCase;
    }


    private IEnumerable<MediaToot> GetLatestConsecutive(IEnumerable<MediaToot> toots, bool withDescription)
    {
        var tootsaux = toots;
        var totsauxreverse = tootsaux.Reverse();
        var goodToots = totsauxreverse.TakeWhile(t => t.HasAltText == withDescription);
        return goodToots.Reverse().ToList();
    }

    private IEnumerable<MediaToot> SkipFirstsNonDescriptionToots(IEnumerable<MediaToot> toots)
    {
        var n = toots.TakeWhile(t => !t.HasAltText).Count();
        return toots.Skip(n).ToList();
    }

    public string GetTootFromCase(CaseEnum situation)
        =>
        TryException( ()=> 
            Configuration
            .GetValue<string>($"Toots:{LANG}:{situation}")!
            .ToString()
            ,
            $"Missatge per a [{situation}] no trobat a la configuració."
        );

    private static T TryException<T>(Func<T> a, string message)
    {
        try
        {
            return a();
        }
        catch (Exception e)
        {
            throw new Exception(message, e);
        };
    }

}
