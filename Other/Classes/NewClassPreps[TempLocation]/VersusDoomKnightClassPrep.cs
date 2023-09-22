/*
name: VersusDoomKnightClassPrep
description: Quest & item Prep for the new class "Verus DoomKnight".
tags: versus, doomKnight, prep, prerequisite, class
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs

//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs

//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Evil/ADK.cs
//cs_include Scripts/Other/Weapons/ShadowReaperOfDoom.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Other/Classes/Necromancer.cs

//cs_include Scripts/Evil/NSoD/CoreNSOD.cs

//cs_include Scripts/Other/MergeShops/TerminaTempleMerge.cs

using Skua.Core.Interfaces;

public class VersusDoomKnightClassPrep
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private SepulchuresOriginalHelm SOH = new();
    private ArchDoomKnight ADK = new();
    private SRoD SRoD = new();
    private TerminaTempleMerge TTMerge = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetClassPrep();
        Core.SetOptions(false);
    }

    public void GetClassPrep(bool rankup = true)
    {
        if (Core.CheckInventory("Verus DoomKnight"))
        {
            if (rankup)
                Adv.RankUpClass("Verus DoomKnight");
            return;
        }

        Farm.Experience(80);

        // Sepulchure's Original Helm
        SOH.DoAll();

        // Arch DoomKnight Helm
        ADK.AMeansToAnEnd();

        // ShadowReaper of Doom
        SRoD.ShadowReaperOfDoom();

        // Dragonlord of Evil
        TTMerge.BuyAllMerge("Dragonlord of Evil");

        // incase items get banked.
        Core.Unbank(
            "Arch DoomKnight Helm",
            "Sepulchure's Original Helm",
            "ShadowReaper of Doom",
            "Dragonlord of Evil");

        Core.Logger("This is the end for now --\n" +
        "the class isnt released yet.\n" +
        "Please ping tato when its out.");

        // insert line to get class once all items are obtained.

        // then uncomment this vv
        // if (rankup)
        //     Adv.RankUpClass("Verus DoomKnight");

    }
}
