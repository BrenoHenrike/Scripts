/*
name: UnlockMineCrafting
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class UnlockMineCrafting
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreBLOD BLOD = new CoreBLOD();
    public CoreStory Story = new CoreStory();

    string[] MineCaftingItems = 
    {
        "Blinding Light of Destiny Handle",
        "Bonegrinder Medal",
        "Bone Dust",
        "Undead Essence",
        "Undead Energy",
        "Spirit Orb",
        "Loyal Spirit Orb"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(MineCaftingItems);
        Core.SetOptions();

        DoUnlockMineCrafting();

        Core.SetOptions(false);
    }

    public void DoUnlockMineCrafting()
    {
        Core.AddDrop(MineCaftingItems);

        BLOD.UnlockMineCrafting();

        Core.ToBank(MineCaftingItems);
    }
}
