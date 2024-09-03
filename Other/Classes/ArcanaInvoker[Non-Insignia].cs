/*
name: Arcana Invoker (Class)
description: This script will get Arcana Invoker class.
tags: arcana, invoker, class, arcana invoker, darkon, astravia, garden
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Other/MergeShops/TerminaTempleMerge.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Other/MergeShops/GooseMerge.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs
//cs_include Scripts/Other/MergeShops/BrightForestMerge.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/InfernalArena.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Other/MergeShops/DoomLegacyMerge.cs
//cs_include Scripts/Other/MergeShops/CelestialChallengerMerge.cs
//cs_include Scripts/Other/MergeShops/SpoilsofLightMerge.cs
//cs_include Scripts/Seasonal/NewYear/ArchiveofTimeMerge.cs
//cs_include Scripts/Other/MergeShops/CrocriverMerge.cs
//cs_include Scripts/Other/MergeShops/SuperSlayinMerge.cs
//cs_include Scripts/Story/DreamPalace.cs
//cs_include Scripts/Other/MergeShops/DreampalaceMerge.cs
//cs_include Scripts/Other/MergeShops/BonecastleMerge.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Other/MergeShops/CelestialRealmMerge.cs
//cs_include Scripts/Other/MergeShops/3LittleWolvesHousesMerge.cs
//cs_include Scripts/Other/Various/Potions.cs
//cs_include Scripts/Story/CruxShip.cs
//cs_include Scripts/Seasonal/Mogloween/MoonlightKhopeshMerge.cs
//cs_include Scripts/Other/MergeShops/ThirdspellMerge.cs
//cs_include Scripts/Seasonal/Friday13th/MergeShops/ShadowMerge.cs
//cs_include Scripts/Darkon/MergeShops/ArcanaInvokerResourceMerge.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Seasonal/BlackFriday/ShadowofDoom/CoreShadowofDoom.cs
//cs_include Scripts/Story/FableForest.cs
//cs_include Scripts/Story/Nation/VoidRefuge.cs
//cs_include Scripts/Story/AgeOfRuin/CoreAOR.cs
//cs_include Scripts/Story/SepulchureSaga/CoreSepulchure.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class ArcanaInvoker
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm => new();
    public CoreAdvanced Adv = new();
    public CoreAstravia Astravia = new();
    public ArcanaInvokerResourceMerge AIRM = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {

        Core.SetOptions();

        GetAI();

        Core.SetOptions(false);
    }

    public void GetAI(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Arcana Invoker"))
        {
            if (rankUpClass)
                Adv.RankUpClass("Arcana Invoker");
            return;
        }

        DoStory();

        Adv.BuyItem("arcana", 2436, "Arcana Invoker");

        if (rankUpClass)
            Adv.RankUpClass("Arcana Invoker");
    }

    public void DoStory()
    {
        if (Core.isCompletedBefore(9697))
            return;

        Farm.Experience(80);
        Astravia.TheWorld();

        Story.PreLoad(this);

        // Your Garden (9693)
        if (!Story.QuestProgression(9693))
        {
            Core.EnsureAccept(9693);
            // ItemBase[] reqs = Core.EnsureLoad(9693).Requirements.ToArray();
            Core.KillMonster("oaklore", "r1", "Spawn", "Undead Infantry", "0 - The Fool's Humble Beginnings", 1, false);
            foreach (ItemBase req in Core.EnsureLoad(9693).Requirements
                    .Where(x => x.Name != "0 - The Fool's Humble Beginnings")
                    .OrderBy(x => int.Parse(x.Name.Split(' ')[0])))
            {
                if (Core.CheckInventory(req.Name, req.Quantity))
                    continue;

                Core.Logger(req.Name);
                AIRM.BuyAllMerge(req.Name);
            }
            Core.EnsureComplete(9693);
        }

        // Love and Loss (9694)
        if (!Story.QuestProgression(9694))
        {
            Core.EnsureAccept(9694);

            foreach (ItemBase req in Core.EnsureLoad(9694).Requirements
                    .OrderBy(x => int.Parse(x.Name.Split(' ')[0]))
                    .ToArray())
            {
                if (Core.CheckInventory(req.Name, req.Quantity))
                    continue;

                Core.Logger(req.Name); // Logging the item name
                while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, req.Quantity))
                    AIRM.BuyAllMerge(req.Name);

                Bot.Wait.ForPickup(req.Name);
            }
            Core.EnsureComplete(9694);
        }


        // Dawn Before Dusk (9695)
        if (!Story.QuestProgression(9695))
        {
            Core.EnsureAccept(9695);
            foreach (ItemBase req in Core.EnsureLoad(9695).Requirements
           .OrderBy(x =>
           {
               string[] parts = x.Name.Split(' ');
               if (parts.Length > 0 && int.TryParse(parts[0], out int number))
                   return number;
               return int.MaxValue; // If parsing fails, place it at the end
           }))
            {
                if (Core.CheckInventory(req.Name, req.Quantity))
                    continue;

                Core.Logger(req.Name);
                while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, req.Quantity))
                    AIRM.BuyAllMerge(req.Name);
                Bot.Wait.ForPickup(req.Name);
            }
            Core.EnsureComplete(9695);
        }

        // Dusk Before Dawn (9696)
        if (!Story.QuestProgression(9696))
        {
            Core.EnsureAccept(9696);

            foreach (ItemBase req in Core.EnsureLoad(9696).Requirements
               .OrderBy(x =>
               {
                   string[] parts = x.Name.Split(' ');
                   if (parts.Length > 0 && int.TryParse(parts[0], out int number))
                       return number;
                   return int.MaxValue; // If parsing fails, place it at the end
               }))
            {
                if (Core.CheckInventory(req.Name, req.Quantity))
                    continue;

                Core.Logger(req.Name);
                while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, req.Quantity))
                    AIRM.BuyAllMerge(req.Name);
                Bot.Wait.ForPickup(req.Name);
            }
            Core.EnsureComplete(9696);
        }

        // Endless Beginning, Final Ends (9697)
        if (!Story.QuestProgression(9697))
        {
            Core.EnsureAccept(9697);
            foreach (ItemBase req in Core.EnsureLoad(9697).Requirements
                    .OrderBy(x =>
                    {
                        string[] parts = x.Name.Split(' ');
                        if (parts.Length > 0 && int.TryParse(parts[0], out int number))
                            return number;
                        return int.MaxValue; // If parsing fails, place it at the end
                    }))
            {
                if (Core.CheckInventory(req.Name, req.Quantity))
                    continue;

                Core.Logger(req.Name);
                while (!Bot.ShouldExit && !Core.CheckInventory(req.Name, req.Quantity))
                    AIRM.BuyAllMerge(req.Name);
                Bot.Wait.ForPickup(req.Name);
            }
            Core.EnsureComplete(9697);
        }

    }
}
