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

public class ExaltedApotheosisPreReqs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    private string[] Weps =
    {
        "Apostate Ultima", "Apostate Omega"
        "Thaumaturgus Ultima", "Thaumaturgus Omega",
        "Exalted Penultima", "Exalted Unity", "Exalted Apotheosis"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Core.BankingBlackList.AddRange(new[] { "Ezrajal Insignia", "Warden Insignia", "Engineer Insignia" });
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
        if (!Core.CheckInventory(new[] { "Thaumaturgus Alpha", "Apostate Alpha" }))
        {
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("timeinn", "r3", "Top", "Energy Elemental", "Exalted Node", 300, isTemp: false);
            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("timeinn", "r7", "Bottom", "The Warden", "Exalted Relic Piece", 10, isTemp: false);
            Core.KillMonster("timeinn", "r8", "Left", "The Engineer", "Exalted Artillery Shard", 10, isTemp: false);
            Core.KillMonster("timeinn", "r6", "Left", "Ezrajal", "Exalted Forgemetal", 10, isTemp: false);

            Adv.BuyItem("timeinn", 2010, "Apostate Alpha");
            Adv.BuyItem("timeinn", 2010, "Thaumaturgus Alpha");

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("timeinn", "r3", "Top", "Energy Elemental", "Exalted Node", 300, isTemp: false);

            Core.Logger("Got all prerequisites! Kill the ultra bosses manually\n" +
            "for insignias next to complete Exalted Apotheosis.");
        }

        if (Core.CheckInventory("Ezrajal Insignia", 24) && Core.CheckInventory("Warden Insignia", 24) && Core.CheckInventory("Engineer Insignia", 16))
        {
            Core.Logger($"{Bot.Inventory.GetQuantity("Ezrajal Insignia") / 24}");
            Core.Logger($"{Bot.Inventory.GetQuantity("Warden Insignia") / 24}");
            Core.Logger($"{Bot.Inventory.GetQuantity("Engineer Insignia") / 16}");
            Core.Logger(
            "Please obtain the rest of the insignias\n" +
            "with your army to complete the merge.\n" +
            "Currently our boats can't do the Ultra Bosses\n" +
            "for you until CoreArmy is finished.", stopBot: true);
        }
        else
        {
            /// Apotheosis merge once got insignias
            foreach (string wep in Weps)
            {
                if (Core.CheckInventory(wep))
                    continue;

                Core.KillMonster("timeinn", "r3", "Top", "Energy Elemental", "Exalted Node", 300, isTemp: false);
                Adv.BuyItem("timeinn", 2010, wep);
            }
            Bot.Wait.ForPickup("Exalted Apotheosis");
            Core.Logger("Congratulations on completing the Exalted Apotheosis weapon!");
        }
    }
}
