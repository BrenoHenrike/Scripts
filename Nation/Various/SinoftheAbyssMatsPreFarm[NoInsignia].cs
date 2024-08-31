/*
name: SinoftheAbyssMatsPreFarm[NoInsignia]
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class SofAPreFarm
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public CoreAdvanced Adv => new();

    public string OptionsStorage = "SinoftheAbyss";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>("MergeSword", "Merge \"Sin of the Abyss\"?", "if you have all the Requirements, should the bot buy the sword?", false),
        CoreBots.Instance.SkipOptions,
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        SofAMats();

        Core.SetOptions(false);
    }

    public void SofAMats()
    {
        if (Core.CheckInventory("Sin of the Abyss"))
            return;

        // Log the MergeSword config value
        Core.Logger($"MergeSword config: {Bot.Config!.Get<bool>("MergeSword")}");

        // Perform material farming
        Nation.SwindleBulk(300);
        Nation.FarmDarkCrystalShard(200);
        Nation.FarmDiamondofNulgath(500);
        Nation.FarmTotemofNulgath(50);
        Nation.FarmGemofNulgath(150);
        Nation.FarmBloodGem(50);

        // Notify user about materials
        Core.Logger("Materials Farm finished. Get the Insignias yourself if you own them, and if the option is enabled it'll buy them.");

        bool mergeSwordEnabled = Bot.Config.Get<bool>("MergeSword");

        if (mergeSwordEnabled)
        {
            int insigniasRequired = 25;
            int insigniasOwned = Bot.Inventory.GetQuantity("Nulgath Insignia");
            if (Core.CheckInventory("Nulgath Insignia", insigniasRequired))
            {
                Adv.BuyItem("ultranulgath", 2137, "Sin of the Abyss");
                Core.Logger("Merging Sin of the Abyss...");
            }
            else
            {
                int missingInsignias = insigniasRequired - insigniasOwned;
                Core.Logger($"Missing {missingInsignias}/25 Nulgath Insignias. Get off your lazy ass and farm more!");
            }
        }
        else
        {
            Core.Logger("MergeSword option is disabled. Fucking farm those Insignias yourself!");
        }
    }

}
