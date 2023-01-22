/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class UnlockMineCrafting_ArtixQuests
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreStory Story = new CoreStory();

    string[] MineCaftingItems = {"BLinding Light of Destiny Handle", "Bonegrinder Medal",
                     "Bone Dust", "Undead Essence", "Undead Energy",
                     "Spirit Orb", "Loyal Spirit Orb"};

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(MineCaftingItems);
        Core.SetOptions();

        DoUnlockMineCrafting_ArtixQuests();

        Core.SetOptions(false);
    }

    public void DoUnlockMineCrafting_ArtixQuests()
    {
        Core.AddDrop(MineCaftingItems);

        BLOD.UnlockMineCrafting();

        Core.ToBank(MineCaftingItems);
    }
}
