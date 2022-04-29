//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story\QueenofMonsters\Extra\BrightOak.cs

using RBot;
public class BrightoakREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public BrightOak BrightOak = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, false);
        
        BrightOak.doall();
        Farm.BrightoakREP();

        Core.SetOptions(false);
    }
}