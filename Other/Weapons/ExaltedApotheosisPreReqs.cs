/*
name: ExaltedApotheosisPreReqs
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Shops;

public class ExaltedApotheosisPreReqs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    private string[] Weps =
    {
        "Exalted Apotheosis",
        "Exalted Penultima", "Exalted Unity",
        "Apostate Ultima", "Thaumaturgus Ultima",
        "Apostate Omega", "Thaumaturgus Omega",
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Ezrajal Insignia", "Warden Insignia", "Engineer Insignia" });
        Core.SetOptions();

        PreReqs();

        Core.SetOptions(false);
    }

    public void PreReqs()
    {
        // if (Core.CheckInventory("Exalted Node", 300, toInv: false) && Core.CheckInventory("Thaumaturgus Alpha", toInv: false) && Core.CheckInventory("Apostate Alpha", toInv: false))
        // {
        //     Core.Logger("Got all prerequisites! Kill the ultra bosses manually for insignias next to complete Exalted Apotheosis.");
        //     return;
        // }

        /// No ultras required
        // if (!Core.CheckInventory(new[] { "Thaumaturgus Alpha", "Apostate Alpha" }))
        // {
        //     Core.EquipClass(ClassType.Farm);
        //     Core.KillMonster("timeinn", "r3", "Top", "Energy Elemental", "Exalted Node", 300, isTemp: false);
        //     Core.EquipClass(ClassType.Solo);
        //     Core.KillMonster("timeinn", "r7", "Bottom", "The Warden", "Exalted Relic Piece", 10, isTemp: false);
        //     Core.KillMonster("timeinn", "r8", "Left", "The Engineer", "Exalted Artillery Shard", 10, isTemp: false);
        //     Core.KillMonster("timeinn", "r6", "Left", "Ezrajal", "Exalted Forgemetal", 10, isTemp: false);

        //     Adv.BuyItem("timeinn", 2010, "Apostate Alpha");
        //     Adv.BuyItem("timeinn", 2010, "Thaumaturgus Alpha");

        //     Core.EquipClass(ClassType.Farm);
        //     Core.KillMonster("timeinn", "r3", "Top", "Energy Elemental", "Exalted Node", 300, isTemp: false);

        //     Core.Logger("Got all prerequisites! Kill the ultra bosses manually for insignias next to complete Exalted Apotheosis.");
        // }

        if (!Core.CheckInventory("Ezrajal Insignia", 24) || !Core.CheckInventory("Warden Insignia", 24) || !Core.CheckInventory("Engineer Insignia", 16))
        {
            Core.Logger("First if");
            Core.Logger($" Ezrajal Insignia: {Core.dynamicQuant("Ezrajal Insignia", false)} / 24");
            Core.Logger($" Warden Insignia: {Core.dynamicQuant("Warden Insignia", false)} / 24");
            Core.Logger($" Engineer Insignia: {Core.dynamicQuant("Engineer Insignia", false)} / 16");
            Core.Logger("Please obtain the rest of the insignias with your army to complete the merge. Skua will *not* be able to do ULTRAs for you. (not sorry)", stopBot: true);
        }
        else
        {
            Core.Logger("2nd if");
            //Ensure shop is loaded:
            Core.Join("timeinn");
            Bot.Shops.Load(2010);

            ShopItem? ExaltedApo = Bot.Shops.Items.Find(x => x.Name == "Exalted Apotheosis");

            while (!Bot.ShouldExit && !Core.CheckInventory(ExaltedApo!.ID))
            {
                // Define the weapon pairs in each tier
                string[][] weaponPairs = new[]
                {
                    new[] { "Apostate Omega", "Thaumaturgus Omega" },
                    new[] { "Apostate Ultima", "Thaumaturgus Ultima" },
                    new[] { "Exalted Penultima", "Exalted Unity" }
                };

                foreach (string[] pair in weaponPairs)
                {
                    bool hasPairInInventory = pair.Any(wep => Bot.Inventory.Contains(wep));

                    // Check if the pair is already in the inventory
                    if (hasPairInInventory)
                        continue;

                    foreach (string wep in pair)
                    {
                        ShopItem? WepData = Bot.Shops.Items.Find(x => x.Name == wep);

                        // Check if the weapon has any requirements before buying
                        if (WepData?.Requirements == null || WepData.Requirements.All(req => Core.CheckInventory(req.ID)))
                        {
                            Core.KillMonster("timeinn", "r3", "Top", "Energy Elemental", "Exalted Node", 300, isTemp: false);
                            Adv.BuyItem("timeinn", 2010, wep);

                            if (Core.CheckInventory(ExaltedApo!.ID))
                                break;
                        }
                    }
                }
                if (Core.CheckInventory(ExaltedApo!.ID))
                    Core.Logger("Congratulations on completing the Exalted Apotheosis weapon!");
                else
                    foreach (var item in ExaltedApo.Requirements)
                    {
                        if (Core.CheckInventory(item.ID, toInv: false))
                            continue;
                        else Core.Logger($"Mising {item.Name}, {item.Quantity}");
                    }
            }
            Bot.Wait.ForPickup(ExaltedApo!.ID);
        }
    }
}
