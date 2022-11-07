//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/OblivionBlade(RareandNot)/CoreOblivionBladeofNulgath.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class OblivionNulgath
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreOblivionBladeofNulgath COBoN = new();

    public string OptionsStorage = "TheDarkDeal";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<OblivionNulgathRewards>("Reward", "Item Selec", "Select the Item then Quantity", OblivionNulgathRewards.Unidentified_13),
        new Option<int>("Quanity", "Item Quanity", "How many of the Selected reward", 00),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        COBoN.OblivionNulgath(Bot.Config.Get<string >("Reward"), Bot.Config.Get<int>("Quanity"));

        Core.SetOptions(false);
    }  

    private enum OblivionNulgathRewards
    {
        Unidentified_13,
        Tainted_Gem,
        Dark_Crystal_Shard,
        Diamond_of_Nulgath,
        Totem_of_Nulgath,
        Gem_of_Nulgath,
        Blood_Gem_of_the_Archfiend
    };
}

