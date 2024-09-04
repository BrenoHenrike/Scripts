/*
name: IconicArtifactQuest
description: Does the IconicArtifactQuest Quest, for the Krenos Spirit Katana
tags: iconicartifactquest, treasure, hunt, treasure hunt, krenos spirit katana
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class IconicArtifact
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.Add("Krenos Spirit Katana");
        Core.SetOptions();

        IconicArtifactQuest();

        Core.SetOptions(false);
    }

    public void IconicArtifactQuest()
    {
        if (Core.CheckInventory("Krenos Spirit Katana"))
        {
            Core.Logger("\"Krenos Spirit Katana\" Owned.");
            return;
        }
        Core.AddDrop("Krenos Spirit Katana");
        // Iconic ArtifactQuest
        Core.EnsureAccept(9804);

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("shimazu", "Shimazu", "First Rune");
        Core.GetMapItem(13328, map: "evilmarsh");
        Core.HuntMonster("seraphicwarlaken", "Rayce", "Third Rune");
        Bot.Quests.UpdateQuest(4077);
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("pyrewatch", "Firestorm Major", "Fourth Rune");
        Core.GetMapItem(13329, map: "icewindpass");

        Core.EnsureComplete(9804);
    }
}



