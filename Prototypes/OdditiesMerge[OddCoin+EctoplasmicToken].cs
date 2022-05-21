//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Oddities[Mem].cs
using RBot;

public class OdditiesMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Oddities Odd => new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        OddCoin_EctoplasmicToken();

        Core.SetOptions(false);
    }

    public void OddCoin_EctoplasmicToken(int quant = 300)
    {
        if (Core.CheckInventory("Odd Coin", quant) && Core.CheckInventory("Ectoplasmic Token", quant))
            return;

        Odd.StoryLine();

        int coinQuant = Bot.Inventory.GetQuantity("Odd Coin");
        int tokenQuant = Bot.Inventory.GetQuantity("Ectoplasmic Token");
        Core.Logger($"Farming Odd Coins ({coinQuant}/{quant}) and Ectoplasmic Tokens ({tokenQuant}/{quant})");
        Core.EquipClass(ClassType.Solo);

        Core.RegisterQuests(8674, 8675);
        while (!Core.CheckInventory("Odd Coin", quant) && !Core.CheckInventory("Ectoplasmic Token", quant))
        {
            Core.KillMonster("oddities", "r6", "Left", "*", "Frankensteined Teddy");
            Bot.Wait.ForPickup("Odd Coin");
            Core.KillMonster("oddities", "r10", "Left", "Cursed Spirit", "Doll Eye", 5);
            Bot.Wait.ForPickup("Ectoplasmic Token");
        }
        Core.CancelRegisteredQuests();
    }
}
