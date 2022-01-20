//cs_include Scripts/CoreBots.cs

using RBot;

public class CoreAwe
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

public bool GuardianCheck()
    {
        Core.Logger("Checking AQ Guardian");
        if (Core.CheckInventory("Guardian Awe Pass", 1, true))
        {
            Core.Logger("You're AQ Guardian!");
            return true;
        }
        Core.BuyItem("museum", 53, "Guardian Awe Pass");
        if (Core.CheckInventory("Guardian Awe Pass"))
        {
            Core.Logger("Guardian Awe Pass bought successfully! You're AQ Guardian!");
            return true;
        }
        else
        {
            Core.Logger("You're not AQ Guardian.");
            return false;
        }
    }

}