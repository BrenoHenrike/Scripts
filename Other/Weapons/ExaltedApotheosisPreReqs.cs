//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class ExaltedApotheosisPreReqs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        PreReqs();

        Core.SetOptions(false);
    }

    public void PreReqs()
    {
        if (Core.CheckInventory("Exalted Node", 300, toInv: false) && Core.CheckInventory("Thaumaturgus Alpha", toInv: false) && Core.CheckInventory("Apostate Alpha", toInv: false))
        {
            Core.Logger("Got all prerequisites! Kill the ultra bosses manually for insignias next to complete Exalted Apotheosis.");
            return;
        }

        /// No ultras required
        if (!Core.CheckInventory("Apostate Alpha") && !Core.CheckInventory("Thaumaturgus Alpha"))
        {
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("timeinn", "r3", "Top", "Energy Elemental", "Exalted Node", 300, isTemp: false);
            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("timeinn", "r7", "Bottom", "The Warden", "Exalted Relic Piece", 10, isTemp: false);
            Core.KillMonster("timeinn", "r8", "Left", "The Engineer", "Exalted Artillery Shard", 10, isTemp: false);
            Core.KillMonster("timeinn", "r6", "Left", "Ezrajal", "Exalted Forgemetal", 10, isTemp: false);

            Core.BuyItem("timeinn", 2010, "Apostate Alpha");
            Core.BuyItem("timeinn", 2010, "Thaumaturgus Alpha");

            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("timeinn", "r3", "Top", "Energy Elemental", "Exalted Node", 300, isTemp: false);

            Core.Logger("Got all prerequisites! Kill the ultra bosses manually\n"+
            "for insignias next to complete Exalted Apotheosis.");
        }

        /// Apotheosis merge once got insignias
        if (Core.CheckInventory("Ezrajal Insignia", 24) && Core.CheckInventory("Warden Insignia", 24) && Core.CheckInventory("Engineer Insignia", 16))
        {
            /* 
            ///Keeping these buys in case Adv.BuyItem can't actually merge all the weapons by itself
            Core.BuyItem("timeinn", 2010, "Apostate Omega");
            Core.BuyItem("timeinn", 2010, "Apostate Ultima");
            Core.BuyItem("timeinn", 2010, "Thaumaturgus Omega");
            Core.BuyItem("timeinn", 2010, "Thaumaturgus Ultima");
            Core.BuyItem("timeinn", 2010, "Exalted Unity");
            Core.BuyItem("timeinn", 2010, "Exalted Penultima");
            Core.BuyItem("timeinn", 2010, "Exalted Apotheosis");
             */
            Adv.BuyItem("timeinn", 2010, "Exalted Apotheosis");
            Core.Logger("Congratulations on completing the Exalted Apotheosis weapon!");
        }

        else
        {
            Core.Logger("Please obtain the rest ofthe insinias with your army to complete the merge. \n"+
            "Currently our boats can't do the Ultra Bosses for you until CoreArmy is finished.");
            Core.Logger($"{Bot.Inventory.GetQuantity("Ezrajal Insignia")/24}");
            Core.Logger($"{Bot.Inventory.GetQuantity("Warden Insignia")/24}");
            Core.Logger($"{Bot.Inventory.GetQuantity("Engineer Insignia")/16}");
            return;
        }
    }
}
