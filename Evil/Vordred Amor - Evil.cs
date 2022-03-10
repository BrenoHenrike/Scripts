//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Doomwood/DoomwoodPart3.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs

using RBot;

public class VordredAmor //you can rename this anything you want it will be the "Class" you refference elsewhere
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public DoomwoodPart3 DW3 = new DoomwoodPart3();
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreSDKA SDKA = new CoreSDKA();

    string[] SKULLSItems = {
        // SKULLS
        "Undead Army Skull",
    };

    string[] MORESKULLSItems = {
        // MORE SKULLS
        "Bone Axe",
        "Spine Gripper",
        "Dread Staff",
        "O-dokuro Blade",
        "Ancient Skull Blade",
        "Dracolich Destroyer Scythe",
        "Screaming Might",
        "Boneblade of Gorgorath",
    };

    string[] MORESKULLSONTHESKULLSItems = {
        // MORE SKULLS ON THE SKULLS
        "Shadow Lich",
        "Escherion's Robe ",
        "Undead Terror Armor",
        "Skulls of the Necromancer",
        "Undead Warrior Executioner",
    };

    string[] MORESKULLSONTHOSESKULLSItems = {
        // MORE SKULLS ON THOSE SKULLS
        "Desolich's Skull",
        "Undead Raxgore's Skull",
        "Legion Lich Lord's Skull",
        "Reaper's Skull",
    };

    string[] VOIDUMINANCEItems = {
        // VOIDUMINANCE
        "(Necro) Scroll of Dark Arts",
        "Noxus Runes",
        "Sally's Necronomicon",
        "Ancient Evil of the Necropolis",
    };

    string[] EmpowerVordredsArmor = {
        // Empower Vordred's Armor
        "Sepulchure's DoomKnight Armor",
        "Vordred's Armor",
        "Empowered Vordred's Armor",
        "Altar Of Vordred",
        "Chibi Vordred",
        "Ultra Vordred Beat Up" //potentialy unneeded wiki doesnt say if its temp or not
    };

    //"Classsname" Is the Class name from "Included script you entered above"
    //"Field" is what you are going to use below
    //"New = Classname();" is .. idk its just needed ._. it should be the same as the classname but with " (); " at the end

    public void ScriptMain(ScriptInterface bot)
    {
        //Quests Wiki: http://aqwwiki.wikidot.com/vordred-s-quests#SWood
        Core.SetOptions();

        DW3.StoryLine();
        Farm.Experience(60);
        Amor();

        Core.SetOptions(false);
    }

    public void Amor()
    {
        Core.AddDrop(SKULLSItems);

        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.Undead);
        if (!Story.QuestProgression(8376))
        {
            //Map: Stonewood 
            //VORDRED(NPC) - The Lil' Head
            // SKULLS - 8376
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(8376);
            Core.HuntMonster("warundead", "Skeletal Fire Mage", "Undead Army Skull", 1000, isTemp: false);
            Core.EnsureComplete(8376);
        }
        Core.ToBank(SKULLSItems);

        // MORE SKULLS - 8377
        Core.AddDrop(MORESKULLSItems);
        Core.EquipClass(ClassType.Solo);
        Adv.BestGear(GearBoost.Undead);

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
            Farm.DoomwoodREP();
            Core.HuntMonster("dragonheart", "Avatar of Desolich", "Dracolich Destroyer Scythe", isTemp: false);
            Core.HuntMonster("dragonbone", "Gorgorath", "Boneblade of Gorgorath", isTemp: false);
            Core.EnsureComplete(8377);
        }
        Core.ToBank(MORESKULLSItems);

        // MORE SKULLS ON THE SKULLS - 8378
        Core.AddDrop(MORESKULLSONTHESKULLSItems);
        Core.EquipClass(ClassType.Solo);
        Adv.BestGear(GearBoost.Undead);

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
        Core.ToBank(MORESKULLSONTHESKULLSItems);

        // MORE SKULLS ON THOSE SKULLS - 8379
        Core.AddDrop(MORESKULLSONTHOSESKULLSItems);
        Core.EquipClass(ClassType.Solo);
        Adv.BestGear(GearBoost.Undead);

        if (!Story.QuestProgression(8379))
        {
            Core.EnsureAccept(8379);
            Core.HuntMonster("desolich", "Desolich", "Desolich's Skull", 5, publicRoom: false);
            Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Undead Raxgore's Skull", 10, isTemp: false, publicRoom: false);
            Core.HuntMonster("frozenlair", "Legion Lich Lord", "Legion Lich Lord's Skull", 15, publicRoom: false);
            Core.HuntMonster("thevoid", "Reaper", "Reaper's Skull", 20, publicRoom: false);
            Core.EnsureComplete(8379);
        }
        Core.ToBank(MORESKULLSONTHOSESKULLSItems);

        // VOIDUMINANCE - 8380
        Core.AddDrop(VOIDUMINANCEItems);
        Core.EquipClass(ClassType.Solo);
        Adv.BestGear(GearBoost.Undead);

        if (!Story.QuestProgression(8380))
        {
            Core.EnsureAccept(8380);
            Core.HuntMonster("epicvordred", "Ultra Vordred", "(Necro) Scroll of Dark Arts", 1, isTemp: false, publicRoom: false);
            Core.HuntMonster("lightguardwar", "Extreme Noxus", "Noxus Runes", isTemp: false, publicRoom: false);
            Farm.Gold(20000000);
            Core.BuyItem("necrotower", 282, "Sally's Necronomicon");
            Core.EquipClass(ClassType.Farm);
            Core.KillMonster("necrodungeon", "r22", "Down", "*", "Ancient Evil of the Necropolis", isTemp: false, publicRoom: false);
            Core.EnsureComplete(8380);
        }
        Core.ToBank(VOIDUMINANCEItems);

        // Empower Vordred's Armor - 8381
        Core.AddDrop(EmpowerVordredsArmor);
        Core.EquipClass(ClassType.Solo);
        Adv.BestGear(GearBoost.Undead);

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
        Core.ToBank(EmpowerVordredsArmor);
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
            if (!Core.CheckInventory("Especially Unbroken Skull", quant))
            {
                Core.EquipClass(ClassType.Farm);
                Core.EnsureAccept(8411);
                Core.HuntMonster("warundead", "Undead Mage", "Unbroken Skulls", 100, isTemp: false);
                Core.HuntMonster("warundead", "Summon Lich", "Summon Lich's Orb", 1, isTemp: false);
                Core.EnsureComplete(8411);
            }
        }

        // UNBROKEN SKULLS - 8341
        if (!Core.CheckInventory("Especially Unbroken Skull", quant))
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(8411);
            Core.HuntMonster("warundead", "Undead Mage", "Unbroken Skulls", 100, isTemp: false);
            Core.EnsureComplete(8411);
        }
    }
}