//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class BlacksmithingREP
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();

    public bool DontPreconfigure = false;

    public string OptionsStorage = "BlackSmithRepGold";

    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("UseGold", "Use Gold To Get Rep?", "Will Farm the Quest \"Intrepid Investing\" which costs 500k/ turnin, if you dont have the gold the bot will farm it.", false),
        new Option<bool>("CanSolo", "SoloSlugButter", "If not, it will do the mobs in the begining of the map.", false)
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        //Farm.UseBoost(ChangeToBoostID, Skua.Core.Models.Items.BoostType.Reputation, false);

        Adv.BestGear(GearBoost.rep);
        Farm.BlacksmithingREP(10, Bot.Config.Get<bool>("UseGold"), Bot.Config.Get<bool>("CanSolo"));

        Core.SetOptions(false);
    }
}