/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;

public class EliteVoid
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CitadelRuins CitadelRuins = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        string[] EliteVoidSet = { "Elite Void", "Elite Void Horns", "Elite Void Plume", "Elite Void Cape", "Elite Void Spiked Cape", "Elite Void Sword", "Elite Void Broadsword", "Elite Void Sword Pet" };
        string[] ChooseRewardQuest = { "Elite Void", "Elite Void Horns", "Elite Void Plume", "Elite Void Cape", "Elite Void Spiked Cape" };

        if (Core.CheckInventory(EliteVoidSet, toInv: false))
            return;

        int count = 0;
        Core.CheckSpaces(ref count, EliteVoidSet);

        Core.AddDrop(EliteVoidSet);

        CitadelRuins.CasparillasQuests();

        for (int i = 0; i < ChooseRewardQuest.Count(); i++)
        {
            if (!Core.CheckInventory(ChooseRewardQuest[i]))
            {
                Core.Logger($"Getting \"{ChooseRewardQuest[i]}\"");
                //Spoiled Dragon 6679
                Core.EnsureAccept(6679);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("lair", "Red Dragon", "Limited Time Drop");
                Core.EnsureCompleteChoose(6679, new[] { ChooseRewardQuest[i] });
            }
        }

        if (!Core.CheckInventory("Elite Void Sword"))
        {
            Core.Logger($"Getting \"Elite Void Sword\"");
            while (Bot.ShouldExit && !Core.CheckInventory("Elite Void Sword"))
            {
                //The Unpredictable Element 6680
                Core.EnsureAccept(6680);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("ledgermayne", "ledgermayne", "Concentrated Mana Extracted", 2);
                Core.EnsureComplete(6680);
            }
        }

        if (!Core.CheckInventory("Elite Void Broadsword"))
        {
            Core.Logger($"Getting \"Elite Void Broadsword\"");
            while (Bot.ShouldExit && !Core.CheckInventory("Elite Void Broadsword"))
            {
                //There, But Not There 6681
                Core.EnsureAccept(6681);
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("palace", "Invisible", "…Something? Slain", 5);
                Core.EnsureComplete(6681);
            }
        }

        if (!Core.CheckInventory("Elite Void Sword Pet"))
        {
            Core.Logger($"Getting \"Elite Void Sword Pet\"");
            while (Bot.ShouldExit && !Core.CheckInventory("Elite Void Sword Pet"))
            {
                //Staying Humble 6682
                Core.EnsureAccept(6682);
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("underlair", "ArchFiend DragonLord", "Twisted Armor Piece", 3);
                Core.EnsureComplete(6682);
            }
        }
        Core.ToBank(EliteVoidSet);
    }
}
