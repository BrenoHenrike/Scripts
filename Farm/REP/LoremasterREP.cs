//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using RBot;
public class LoremasterREP
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreStory Story = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        //Farm.UseBoost(ChangeToBoostID, RBot.Items.BoostType.Reputation, false);

        Farm.LoremasterREP();

        Core.SetOptions(false);
    }
}