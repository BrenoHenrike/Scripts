//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs

using Skua.Core.Interfaces;

public class VordredArmor
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreDoomwood DW = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreSDKA SDKA = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetVordredsArmor();

        Core.SetOptions(false);
    }

    public void GetVordredsArmor()
    {
        if (Core.CheckInventory("Empowered Vordred's Armor"))
            return;

        Story.PreLoad(this);

        DW.DoomwoodPart3();
        Farm.Experience(60);
        Adv.BestGear(GearBoost.Undead);

        if (!Story.QuestProgression(8376))
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(8376);
            Core.HuntMonster("warundead", "Skeletal Fire Mage", "Undead Army Skull", 1000, isTemp: false);
            Core.EnsureComplete(8376);
        }

        // MORE SKULLS
        if (!Story.QuestProgression(8377))
        {
            Core.EnsureAccept(8377);
            if (!Core.CheckInventory("Screaming Might"))
            {
                Core.HuntMonster("dagefortress", "Scorned Knight", "Dark Palace Token", 10, isTemp: false);
                Core.BuyItem("dagefortress", 1144, "Screaming Might");
            }
            Core.HuntMonster("graveyard", "Big Jack Sprat", "Bone Axe", isTemp: false);
            Core.HuntMonster("battleundera", "Angry Undead Giant", "Spine Gripper", isTemp: false);
            Core.HuntMonster("marsh", "Dreadspider", "Dread Staff", isTemp: false);
            Core.KillMonster("Odokuro", "Boss", "Left", "O-Dokuro", "O-dokuro Blade", isTemp: false);
            Farm.ArcangroveREP();
            if (!Core.CheckInventory("Ancient Skull Blade"))
                Core.BuyItem("arcangrove", 214, "Ancient Skull Blade");
            Farm.DoomWoodREP();
            Core.HuntMonster("dragonheart", "Avatar of Desolich", "Dracolich Destroyer Scythe", isTemp: false);
            Core.HuntMonster("dragonbone", "Gorgorath", "Boneblade of Gorgorath", isTemp: false);
            Core.EnsureComplete(8377);
        }

        // MORE SKULLS ON THE SKULLS
        if (!Story.QuestProgression(8378))
        {
            Core.EnsureAccept(8378);
            Farm.EvilREP();
            Core.BuyItem("Shadowfall", 89, "Shadow Lich");
            Core.KillEscherion("Escherion's Robe");
            Core.HuntMonster("battleundera", "Bone Terror", "Undead Terror Armor", isTemp: false);
            Core.HuntMonster("necrocavern", "Shadow Dragon", "Skulls of the Necromancer", isTemp: false);
            if (!Core.CheckInventory("Undead Warrior Executioner"))
            {
                if (!Core.CheckInventory("Dage's Approval", 100))
                    Core.HuntMonster("evilwardage", "Bloodfiend", "Dage's Approval", 100, isTemp: false);
                if (!Core.CheckInventory("Dage's Favor", 75))
                    Core.HuntMonster("evilwardage", "Bloodfiend", "Dage's Favor", 75, isTemp: false);
                if (!Core.CheckInventory("Undead Warrior Bruiser"))
                    Core.HuntMonster("underworld", "Revontheus ", "Undead Warrior Bruiser", isTemp: false);
                Core.BuyItem("evilwardage", 454, "Undead Warrior Executioner");
            }
            Core.EnsureComplete(8378);
        }

        // MORE SKULLS ON THOSE SKULLS - 8379
        if (!Story.QuestProgression(8379))
        {
            Core.EnsureAccept(8379);
            Core.HuntMonster("desolich", "Desolich", "Desolich's Skull", 5, isTemp: false);
            Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Undead Raxgore's Skull", 10, isTemp: false);
            Core.HuntMonster("frozenlair", "Legion Lich Lord", "Legion Lich Lord's Skull", 15, isTemp: false);
            Core.HuntMonster("thevoid", "Reaper", "Reaper's Skull", 20, isTemp: false);
            Core.EnsureComplete(8379);
        }

        // VOIDUMINANCE - 8380
        if (!Story.QuestProgression(8380))
        {
            Core.EnsureAccept(8380);
            Core.HuntMonster("epicvordred", "Ultra Vordred", "(Necro) Scroll of Dark Arts", 1, isTemp: false);
            Core.HuntMonster("lightguardwar", "Extreme Noxus", "Noxus Runes", isTemp: false);
            Farm.Gold(20000000);
            Core.BuyItem("necrotower", 282, "Sally's Necronomicon");
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("necrodungeon", "r22", "Down", "*", "Ancient Evil of the Necropolis", isTemp: false);
            Core.EnsureComplete(8380);
        }

        // Empower Vordred's Armor - 8381
        if (!Story.QuestProgression(8381))
        {
            Adv.BestGear(GearBoost.Undead);
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(8381);
            if (!Core.CheckInventory("Sepulchure's DoomKnight Armor"))
            {
                SDKA.DoAll();
            }
            if (!Core.CheckInventory("Vordred's Armor") && Bot.Quests.IsUnlocked(8381))
            {
                Core.BuyItem("stonewood", 2063, "Vordred's Armor");
            }
            Core.HuntMonster("epicvordred", "Ultra Vordred", "Ultra Vordred Beat Up");
            Core.EnsureComplete(8381);
        }
    }

    public void EspeciallyUnbrokenSkull(int quant = 500) //idk if thisll ever be used but its here 🥔
    {
        if (Core.CheckInventory("Especially Unbroken Skull", quant))
            return;

        Core.AddDrop("Especially Unbroken Skull");
        Adv.BestGear(GearBoost.Undead);
        Core.EquipClass(ClassType.Farm);

        // UNBROKEN SKULLS (Mem) - 8342
        if (Core.IsMember)
        {
            Core.RegisterQuests(8411);
            while (!Bot.ShouldExit && !Core.CheckInventory("Especially Unbroken Skull", quant))
            {
                Core.EnsureAccept(8411);
                Core.HuntMonster("warundead", "Undead Mage", "Unbroken Skulls", 100, isTemp: false);
                Core.HuntMonster("warundead", "Summon Lich", "Summon Lich's Orb", 1, isTemp: false);
                Core.EnsureComplete(8411);
                Bot.Wait.ForPickup("Especially Unbroken Skull");
            }
            Core.CancelRegisteredQuests();
        }
        else
        {
            // UNBROKEN SKULLS - 8341
            Core.RegisterQuests(8341);
            while (!Bot.ShouldExit && !Core.CheckInventory("Especially Unbroken Skull", quant))
            {
                Core.HuntMonster("warundead", "Undead Mage", "Unbroken Skulls", 100, isTemp: false);
                Bot.Wait.ForPickup("Especially Unbroken Skull");
            }
        }
        Core.CancelRegisteredQuests();
    }
}