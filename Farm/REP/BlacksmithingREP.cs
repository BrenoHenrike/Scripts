//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;
using RBot.Options;

public class BlacksmithingREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv => new();



    public bool DontPreconfigure = false;

    public string OptionsStorage = "BlackSmithRepGold";

    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("UseGold", "Use Gold To Get Rep?", "Will Farm the Quest \"Intrepid Investing\" which costs 500k/ turnin, if you dont have the gold the bot will farm it.", false)
    };


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, false);

        Adv.BestGear(GearBoost.rep);
        Farm.BlacksmithingREP(10, bot.Config.Get<bool>("UseGold"));

        Core.SetOptions(false);
    }
}