//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/OblivionBlade(RareandNot)/CoreOblivionBladeofNulgath.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class DiamondsofNulgathSale
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreOblivionBladeofNulgath COBoN = new();

    public string OptionsStorage = "DiamondsofNulgathSale";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<int>("DiamondQuant", "Diamond Quant", "Diamond of Nulgath quant", 00),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        COBoN.DiamondsofNulgathSale(Bot.Config.Get<int>("DiamondQuant"));

        Core.SetOptions(false);
    }
}
