/*
name: Verus DoomKnight Class
description: This script will farm Verus DoomKnight Class.
tags: versus,verus,doomKnight,vdk, class
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

//cs_include Scripts/Seasonal/TalkLikeaPirateDay/DoomPirateStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/MergeShops/DoomPirateHaulMerge.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Skills;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Shops;

public class VerusDoomKnightClass
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv = new();
    private SepulchuresOriginalHelm SOH = new();
    private ArchDoomKnight ADK = new();
    private SRoD SRoD = new();
    private TerminaTempleMerge TTMerge = new();
    private DoomPirateHaulMerge DPHM = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        GetClass();
        Core.SetOptions(false);
    }

    public void GetClass(bool rankup = true)
    {
        if (Core.CheckInventory("Verus DoomKnight"))
            return;

        Farm.Experience(40);
        Farm.EvilREP();
        Core.EquipClass(ClassType.Solo);

        // Body, Soul and, Domination (9411)
        if (!Core.isCompletedBefore(9411))
        {
            Core.EnsureAccept(9411);
            Core.HuntMonster(("underrealm", "Fear", "Fear's Bones", 13, false);
            Core.HuntMonster(("brainmeat", "Brain Matter", "Gray Matter", 13, false);
            Bot.Quests.UpdateQuest(8777);
            Core.HuntMonster(("titanattack", "Titanic DoomKnight", "Titanic Spine", 13, false);
            Core.HuntMonsterMapID("valleyofdoom", 25, "Doom Knight Plating", 13, false);
            Core.EnsureComplete(9411);
        }

        // Of the Same Cloak (9412)
        if (!Core.isCompletedBefore(9412))
        {
            Core.EnsureAccept(9412);
            Core.HuntMonsterMapID("necrodungeon", 47, "The Mask of the Skulls", isTemp: false);
            Core.HuntMonster(("lumafortress", "Corrupted Luma", "Doom Worshipper's Blade Of Doom", isTemp: false);
            Core.HuntMonster(("innershadows", "Krahen", "Empress' ShadowCloak", isTemp: false);
            Bot.Quests.UpdateQuest(7646);
            Core.HuntMonster(("techfortress", "MechaVortrix", "Cybernetic Doom Blade", isTemp: false);
            Core.GhostItem(55823, "Kyger", 1, false, ItemCategory.Pet, "Time for training!", 1);
            Bot.Quests.UpdateQuest(7650);
            Core.HuntMonsterMapID("stonewooddeep", 16, "Asherion Armor", isTemp: false);
            Core.EnsureComplete(9412);
        }

        // Refracted Light (9413)
        if (!Core.isCompletedBefore(9413))
        {
            Core.EnsureAccept(9413);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster(("brightshadow", "Shadowflame Paladin", "Shadowflame Spike", 150, false);
            Core.HuntMonster(("fiendshard", "Paladin Fiend", "Light Fiend Horn", 150, false);
            Core.HuntMonster(("legionarena", "Dark Legion Paladin", "Underworld Soul Glow", 50, false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster(("noxustower", "General Goldhammer", "Gold Hammer Chip", isTemp: false);
            Core.HuntMonster(("chaoslab", "Chaos Artix", "Shimmering Tentacle", 20, false);
            Core.EnsureComplete(9413);
        }

        // Life Carve (9417)
        if (!Core.isCompletedBefore(9417))
        {
            Core.EnsureAccept(9417);
            Core.Logger("The map \"Wanders\", is a bit broke,\n" +
            "it will take a minute to hunt the mosnter");
            Bot.Quests.UpdateQuest(3773);
            Core.HuntMonsterMapID("wanders", 46, "Trace of Light", 8, false); //i hate this map
            Core.HuntMonster(("lightguardwar", "Extreme Noxus", "Trace of Dark", 8, false);
            Core.HuntMonster(("eternalchaos", "Bandit Drakath", "Trace of Wind", 8, false);
            Core.HuntMonster(("quibblehunt", "Entropy Dragon", "Trace of Earth", 8, false);
            Core.HuntMonster(("crashsite", "ProtoSartorium", "Trace of Energy", 8, false);
            Core.HuntMonster(("northlands", "Aisha's Drake", "Trace of Ice", 8, false);
            Core.HuntMonster(("deepchaos", "Kathool", "Trace of Water", 8, false);
            Core.HuntMonster(("drakonnan", "Ultra Drakonnan", "Trace of Fire", 8, false);
            Core.HuntMonster(("battlefowl", "Zeuster Projection", "Trace of Bacon", isTemp: false);
            Core.EnsureComplete(9417);
        }

        // Soul Fracture (9416)
        if (!Core.isCompletedBefore(9416))
        {
            Core.EnsureAccept(9416);
            Core.HuntMonster(("ultraalteon", "Ultra Alteon", "Soul of Alteon", 40, false); //goodluck
            Core.HuntMonster(("ebondungeon", "Dethrix", "Soul of Dethrix", 40, false);
            Core.HuntMonster(("shadowstrike", "Sepulchuroth", "Soul of Sepulchuroth", 40, false);
            Core.HuntMonster(("ultradrakath", "Champion of Chaos", "Soul of Drakath", 40, false);
            Core.HuntMonster(("ebilcorphq", "Gravelyn", "Soul of Gravelyn", 40, false);
            Core.HuntMonster(("shadowvoid", "Fragment of Doom", "Soul of Doom", 40, false);
            Core.EnsureComplete(9416);
        }

        // Doom Spikes (9418)
        if (!Core.isCompletedBefore(9418))
        {
            Core.EnsureAccept(9418);
            Adv.GearStore();
            Core.KillDoomKitten("Doomkitten's Molar", 20, false);
            Adv.GearStore(true);
            if (!Core.CheckInventory("Deadly Duo's Decayed Denture"))
                Core.Logger("InfernalArena is a **SOLO ONLY** map!");
            Adv.GearStore();
            Core.BossClass(Core.CheckInventory(new[] { "Void Highlord", "Void Highlord (IoDA)" }, any: true)
            ? (Core.CheckInventory("Void Highlord (IoDA)")
            ? "Void Highlord (IoDA)" : "Void Highlord")
            : "ArchPaladin");
            Core.JumpWait();
            Core.Sleep();
            Core.HuntMonster("infernalarena", "Deadly Duo", "Deadly Duo's Decayed Denture", 10, false);
            Core.JumpWait();
            Adv.GearStore(true);

            if (!Core.CheckInventory("Xyfrag's Slimy Tooth", 5) || !Core.CheckInventory("Nerfkitten's Fang", 3) || !Core.CheckInventory("Maw of the Sea", 10))
            {
                Core.Logger("You will need to manually kill the following to proceed with the quest:\n" +
                            "1. Xyfrag - in /join voidxyfrag\n" +
                            "2. Sarah the Nerfkitten - in /join voidnerfkitten\n" +
                            "3. Voice of the Sea - in /join seavoice\n" +
                            "Once done, you can continue with the quest by running the bot again.", stopBot: true);
            }
            else Core.EnsureComplete(9418);
        }

        // Necrotic Blade (9414)
        if (!Core.isCompletedBefore(9414))
        {
            Core.EnsureAccept(9414);
            SRoD.ShadowReaperOfDoom();
            SOH.DoAll();
            ADK.AMeansToAnEnd(HelmOnly: true);
            TTMerge.BuyAllMerge("Dragonlord of Evil");
            DPHM.BuyAllMerge("DoomTech DoomKnight");

            //Ensure Everything is unbanked.
            Core.Unbank("Dragonlord of Evil", "DoomTech DoomKnight", "Arch DoomKnight Helm", "Sepulchure's Original Helm", "ShadowReaper Of Doom");
            Core.EnsureComplete(9414);
        }

        // Unleash Doom (9419)
        if (!Core.isCompletedBefore(9419))
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(9419);
            Core.HuntMonster(("citadelruins", "Inquisitor Hobo", "Inquisitor Bones", 1000000, false);
            Core.HuntMonster(("deltavlab", "Pistol Guard", "Refined Metal", 1000000, false);
            Core.HuntMonster(("etherwardes", "Earth Dragon Warrior", "Dragon Skin", 1000000, false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster(("necrocavern", "Chaos Vordred", "PaladinSlayer's Skull", 100000, false);
            Core.EnsureComplete(9419);
        }

        Core.BuyItem("terminatemple", 2343, "Verus DoomKnight");

        if (rankup)
            Adv.RankUpClass("Verus DoomKnight");
    }
}
