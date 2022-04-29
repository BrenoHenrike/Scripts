//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
using RBot;

public class SevenCirclesWarXP
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public SevenCircles SC = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        SC.Circles();

        Adv.BestGear(GearBoost.exp);
        Adv.BestGear(GearBoost.gold);
        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Experience, true);
        // Change the level to 101 so it will run until you stop it. - (101, 100000000)
        
        Farm.SevenCirclesWar(100, 100000000);

        Core.SetOptions(false);
    }
}