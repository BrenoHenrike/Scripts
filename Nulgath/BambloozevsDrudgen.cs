//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class BambloozevsDrudgen
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nulgath.bagDrops);
        Core.SetOptions();

        MaxBags();

        Core.SetOptions(false);
    }

    public void MaxBags()
    {
        Core.AddDrop(Nulgath.bagDrops);

        Nulgath.BambloozevsDrudgen("Diamond of Nulgath", 1000);
        if (Core.CheckInventory("Drudgen the Assistant"))
        {
            while (!Bot.ShouldExit() && !Core.CheckInventory("Tainted Gem", 1000))
                Nulgath.ContractExchange(ChooseReward.TaintedGem);
            while (!Bot.ShouldExit() && !Core.CheckInventory("Dark Crystal Shard", 1000))
                Nulgath.ContractExchange(ChooseReward.DarkCrystalShard);
            while (!Bot.ShouldExit() && !Core.CheckInventory("Gem of Nulgath", 300))
                Nulgath.ContractExchange(ChooseReward.GemofNulgath);
            while (!Bot.ShouldExit() && !Core.CheckInventory("Blood Gem of the Archfiend", 100))
                Nulgath.ContractExchange(ChooseReward.BloodGemoftheArchfiend);
        }
        Nulgath.BambloozevsDrudgen("Diamond of Nulgath", 1000);
    }
}