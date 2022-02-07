//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/LordofChaos/Core13LoC.cs
using RBot;
public class LycanREP
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public Core13LoC LOC => new Core13LoC();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        LOC.Wolfwing();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, false);

        Farm.LycanREP();

        Core.SetOptions(false);
    }
}