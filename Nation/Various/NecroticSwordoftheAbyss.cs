/*
name: Necrotic Sword of the Abyss
description: This will farm the required materials and buy the item.
tags: necrotic-sword-of-the-abyss, nation, boosted-item, nsoa
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
//cs_include Scripts/Nation/VHL/CoreVHL.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
using Skua.Core.Interfaces;

public class NSoA
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv => new();
    private CoreVHL VHL => new();
    private CoreNSOD NSOD => new();
    private CoreNation Nation => new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.SetOptions();

        GetNSoA();

        Core.SetOptions(false);
    }

    public void GetNSoA()
    {
        if (Core.CheckInventory("Necrotic Sword of the Abyss"))
            return;

        NSOD.GetNSOD();

        VHL.VHLChallenge(2);

        VHL.VHLCrystals();

        Adv.BuyItem("tercessuinotlim", 1355, "Necrotic Sword of the Abyss");

    }
}
