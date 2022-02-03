using RBot;

public class CoreDailys
{
    // [Can Change] Default metals to be acquired by MineCrafting quest
    public string[] MineCraftingMetals = { "Barium", "Copper", "Silver" };
    // [Can Change] Default metals to be acquired by Hard Core Metals quest
    public string[] HardCoreMetalsMetals = { "Arsenic", "Chromium", "Rhodium" };

    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void DoAllDailys()
    {
        Core.Logger("Doing all dailys");
        Core.Logger("Mad Weaponsmith");
        MadWeaponSmith();
        Core.Logger("Cysero's Super Hammer");
        CyserosSuperHammer();
        Core.Logger("Bright Knight Armor");
        BrightKnightArmor();
        Core.Logger("Collector Class");
        CollectorClass();
        Core.Logger("Cryomancer Class");
        Cryomancer();
        Core.Logger("Pyromancer Class");
        Pyromancer();
        Core.Logger("Death KnightLord Class");
        DeathKnightLord();
        Core.Logger("ShadowScythe General Class");
        ShadowScytheClass();
        Core.Logger("Grumble Grumble (Blood Gem of the Archfiend");
        GrumbleGrumble();
        Core.Logger("Elders' Blood");
        EldersBlood();
        Core.Logger("Sparrow's Blood");
        SparrowsBlood();
        Core.Logger("Shadow Shroud");
        ShadowShroud();
        Core.Logger("Dage's Scroll Fragment");
        DagesScrollFragment();
        Core.Logger("Crypto Token (/Join Curio)");
        CryptoToken();
        Core.Logger("Beast Master Class");
        BeastMasterChallenge();
        Core.Logger("Fungi for a Fun Guy (BrightOak Reputation)");
        FungiforaFunGuy();
        Core.Logger("Mine Crafting (Barium/Copper/Silver)");
        MineCrafting();
        Core.Logger("Hard Core Metals (Arsenic/Chromium/Rhodium)");
        HardCoreMetals();
        Core.Logger("Dailys completed");
    }

    /// <summary>
    /// Accepts the quest and kills the monster to complete, if no cell/pad is given will hunt for the monster.
    /// </summary>
    /// <param name="quest">ID of the quest</param>
    /// <param name="map">Map where the monster is</param>
    /// <param name="monster">Name of the monster</param>
    /// <param name="item">Item to get</param>
    /// <param name="quant">Quantity of the item</param>
    /// <param name="isTemp">Whether it is temporary</param>
    /// <param name="cell">Cell where the monster is (optional)</param>
    /// <param name="pad">Pad where the monster is</param>
    public void DailyRoutine(int quest, string map, string monster, string item, int quant = 1, bool isTemp = true, string cell = null, string pad = null, bool publicRoom = false)
    {
        if (Bot.Quests.IsDailyComplete(quest))
            return;
        Core.Join(map);
        Core.EnsureAccept(quest);
        if (cell != null || pad != null)
            Core.KillMonster(map, cell, pad, monster, item, quant, isTemp, true, publicRoom);
        else
            Core.HuntMonster(map, monster, item, quant, isTemp, true, publicRoom);
        Core.EnsureComplete(quest);
        Bot.Player.Pickup(Bot.Drops.Pickup.ToArray());
    }

    /// <summary>
    /// Checks if the daily is complete, if not will add the specified drops and unbank if necessary
    /// </summary>
    /// <param name="quest">ID of the quest</param>
    /// <param name="items">Items to add to drop grabber and unbank</param>
    /// <returns></returns>
    public bool CheckDaily(int quest, params string[] items)
    {
        if (Bot.Quests.IsDailyComplete(quest))
            return false;
        Core.AddDrop(items);
        return true;
    }

    /// <summary>
    /// Does the Mine Crafting quest for 2 Barium, Copper and Silver by default.
    /// </summary>
    /// <param name="metals">Metals you want to be collected</param>
    /// <param name="quant">Quantity you want of the metals</param>
    public void MineCrafting(string[] metals = null, int quant = 2)
    {
        if (metals == null)
            metals = MineCraftingMetals;
        if (Core.CheckInventory(metals, quant))
            return;
        if (!CheckDaily(2091, metals))
            return;

        Core.EnsureAccept(2091);
        Core.HuntMonster("stalagbite", "Balboa", "Axe of the Prospector", 1, false);
        Core.HuntMonster("stalagbite", "Balboa", "Raw Ore", 30);
        foreach (string metal in metals)
        {
            if (!Core.CheckInventory(metal, quant, false))
            {
                int metalID = MetalID(metal);
                Core.EnsureComplete(2091, metalID);
                Bot.Player.Pickup(metal);
                break;
            }
        }
        if (Bot.Quests.IsInProgress(2091))
            Core.Logger($"All desired metals were found with the needed quantity ({quant}), quest not completed");
    }

    /// <summary>
    /// Does the Hard Core Metals quest for 1 Arsenic, Chromium and Rhodium by default
    /// </summary>
    /// <param name="metals">Metals you want to be collected</param>
    /// <param name="quant">Quantity you want of the metals</param>
    public void HardCoreMetals(string[] metals = null, int quant = 1)
    {
        if(!Core.IsMember)
            return;
        if(metals == null)
            metals = HardCoreMetalsMetals;
        if (Core.CheckInventory(metals))
            return;
        if(!CheckDaily(2098, metals))
            return;

        Core.EnsureAccept(2098);
        Core.HuntMonster("stalagbite", "Balboa", "Axe of the Prospector", 1, false);
        Core.HuntMonster("stalagbite", "Balboa", "Raw Ore", 30);
        foreach (string metal in metals)
        {
            if(!Core.CheckInventory(metal, quant, false))
            {
                int metalID = MetalID(metal);
                Core.EnsureComplete(2098, metalID);
                Bot.Player.Pickup(metal);
                break;
            }
        }
        if(Bot.Quests.IsInProgress(2098))
            Core.Logger($"All desired metals were found with the needed quantity ({quant}), quest not completed");
    }

    private int MetalID(string metal)
    {
        switch (metal.ToLower())
        {
            case "arsenic":
                return 11287;
            case "beryllium":
                return 11534;
            case "chromium":
                return 11591;
            case "palladium":
                return 11864;
            case "rhodium":
                return 12032;
            case "thorium":
                return 12075;
            case "mercury":
                return 12122;
            case "aluminum":
                return 11608;
            case "barium":
                return 11932;
            case "gold":
                return 12157;
            case "iron":
                return 12263;
            case "copper":
                return 12297;
            case "silver":
                return 12308;
            case "platinum":
                return 12315;
        }
        Core.Logger($"Could not find {metal}, is it written right?", messageBox: true, stopBot: true);
        return 0;
    }

    public void FungiforaFunGuy()
    {
        if (!CheckDaily(4465) || Bot.Player.GetFactionRank("Brightoak") == 10 || !Core.IsMember)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.EnsureAccept(4465);
        Core.HuntMonster("brightoak", "Grove Spore", "Colony Spore");
        Core.HuntMonster("brightoak", "Grove Spore", "Intact Spore");
        Core.EnsureComplete(4465);
    }

    public void BeastMasterChallenge()
    {
        if (!CheckDaily(3759) || Bot.Player.GetFactionRank("BeastMaster") == 10 || !Core.IsMember)
            return;

        DailyRoutine(3759, "swordhavenbridge", "Purple Slime", "Purple Slime", 10);
    }

    public void CyserosSuperHammer()
    {
        if (Core.CheckInventory("Cysero's SUPER Hammer", toInv: false))
            return;
        if(!Core.CheckInventory("Mad Weaponsmith"))
            return;
        if (!CheckDaily(4310, "C-Hammer Token") && !Core.IsMember)
            return;
        if (!CheckDaily(4311, "C-Hammer Token") && Core.IsMember)
            return;
        Core.EquipClass(ClassType.Solo);
        DailyRoutine(4310, "deadmoor", "Geist", "Geist's Chain Link");
        if (Core.IsMember)
            DailyRoutine(4311, "deadmoor", "Geist", "Geist's Pocket Lint");
        Core.ToBank("C-Hammer Token");
    }

    public void MadWeaponSmith()
    {
        if (Core.CheckInventory("Mad Weaponsmith", toInv: false))
            return;
        if (!CheckDaily(4308, "C-Armor Token") && !Core.IsMember)
            return;
        if (!CheckDaily(4309, "C-Armor Token") && Core.IsMember)
            return;
        Core.EquipClass(ClassType.Solo);
        DailyRoutine(4308, "deadmoor", "Nightmare", "Nightmare Fire");
        if (Core.IsMember)
            DailyRoutine(4309, "deadmoor", "Nightmare", "Unlucky Horseshoe");
        Core.ToBank("C-Armor Token");
    }

    public void BrightKnightArmor(bool checkArmor = true)
    {
        if (checkArmor)
            if (Core.CheckInventory("Bright Knight", toInv: false))
                return;
        if (!CheckDaily(3826, "Seal of Light") & !CheckDaily(3825, "Seal of Darkness"))
            return;
        Core.EquipClass(ClassType.Solo);
        DailyRoutine(3826, "alteonbattle", "Ultra Alteon", "Alteon Defeated");
        DailyRoutine(3825, "sepulchurebattle", "Ultra Sepulchure", "Sepulchure Defeated");
    }
    
    public void CollectorClass()
    {
        if (Core.CheckInventory("The Collector", toInv: false))
            return;
        if (!CheckDaily(1316, "This Might Be A Token", "Tokens of Collection") && !Core.IsMember)
            return;
        if (!CheckDaily(1331, "This Is Definitely A Token", "Tokens of Collection") && !CheckDaily(1332, "This Could Be A Token", "Tokens of Collection") && Core.IsMember)
            return;
        Core.EquipClass(ClassType.Farm);
        DailyRoutine(1316, "terrarium", "*", "This Might Be A Token", 2, false, "r2", "Right");
        if (Core.IsMember)
        {
            DailyRoutine(1331, "terrarium", "*", "This Is Definitely A Token", 2, false, "r2", "Right");
            DailyRoutine(1332, "terrarium", "*", "This Could Be A Token", 2, false, "r2", "Right"); 
        }
    }
    
    public void Cryomancer()
    {
        if (Core.CheckInventory("Cryomancer", toInv: false))
            return;
        if (!CheckDaily(3966, "Glacera Ice Token") && !Core.IsMember)
            return;
        if (!CheckDaily(3965, "Glacera Ice Token") && Core.IsMember)
            return;
        if (Core.IsMember)
            DailyRoutine(3965, "frozentower", "Frost Invader", "Dark Ice");
        else
            DailyRoutine(3966, "frozentower", "Frost Invader", "Dark Ice");
        Core.ToBank("Glacera Ice Token");
    }
    
    public void Pyromancer()
    {
        if (Core.CheckInventory("Pyromancer", toInv: false))
            return;
        if (!CheckDaily(2209, "Shurpu Blaze Token") && !Core.IsMember)
            return;
        if (!CheckDaily(2210, "Shurpu Blaze Token") && Core.IsMember)
            return;
        Core.EquipClass(ClassType.Solo);
        if (Core.IsMember)
            DailyRoutine(2210, "xancave", "Shurpu Ring Guardian", "Guardian Shale");
        else
            DailyRoutine(2209, "xancave", "Shurpu Ring Guardian", "Guardian Shale");
        Core.ToBank("Shurpu Blaze Token");
    }
    
    public void DeathKnightLord()
    {
        if (Core.CheckInventory("DeathKnight Lord", toInv: false))
            return;
        if (!CheckDaily(492, "Shadow Skull") || !Core.IsMember)
            return;
        DailyRoutine(492, "bludrut4", "Shadow Serpent", "Shadow Scales", 5);
        Core.ToBank("Shadow Skull");
    }
    
    public void ShadowScytheClass()
    {
        if (Core.CheckInventory("ShadowScythe General"))
            return;
        if (!CheckDaily(3828, "Shadow Shield") && (!CheckDaily(3827, "Shadow Shield") && Core.IsMember))
            return;
        DailyRoutine(3828, "lightguardwar", "Citadel Crusader", "Broken Blade");
        if (Core.IsMember)
            DailyRoutine(3827, "lightguardwar", "Citadel Crusader", "Broken Blade");
    }

    public void GrumbleGrumble()
    {
        if (!Core.CheckInventory("Crag & Bamboozle") || !CheckDaily(592, "Diamond of Nulgath", "Blood Gem of the Archfiend"))
            return;
        Core.ChainComplete(592);
    }

    public void EldersBlood()
    {
        if (Core.CheckInventory("Elders' Blood", 5))
            return;
        if (!CheckDaily(802, "Elders' Blood"))
            return;
        Core.EquipClass(ClassType.Farm);
        DailyRoutine(802, "arcangrove", "Gorillaphant", "Slain Gorillaphant", 50, cell: "Right", pad: "Left");
    }

    public void SparrowsBlood()
    {
        if (Core.CheckInventory("Sparrow's Blood", 5))
            return;
        if (!CheckDaily(803, "Sparrow's Blood"))
            return;
        Core.AddDrop("Sparrow's Blood");
        Core.EquipClass(ClassType.Farm);
        Core.EnsureAccept(803);
        Core.HuntMonster("arcangrove", "Gorillaphant", "Blood Lily", 30);
        Core.HuntMonster("arcangrove", "Seed Spitter", "Snapdrake ", 17);
        Core.HuntMonster("arcangrove", "Seed Spitter|Gorillaphant", "DOOM Dirt", 12);
        Core.EnsureComplete(803);
    }

    public void ShadowShroud()
    {
        if (!CheckDaily(486, "Shadow Shroud"))
            return;
        DailyRoutine(486, "bludrut2", "Shadow Creeper", "Shadow Canvas", 5, cell: "Enter", pad: "Down");
        Core.ToBank("Shadow Shroud");
    }

    public void DagesScrollFragment()
    {
        if (!CheckDaily(3596, "Dage Scroll Fragment"))
            return;
        DailyRoutine(3596, "mountdoomskull", "*", "Chaos Power Increased", 6, cell: "b1", pad: "Left");
        Core.ToBank("Dage's Scroll Fragment");
    }

    public void CryptoToken()
    {
        if (!CheckDaily(6187, "Crypto Token"))
            return;
        DailyRoutine(6187, "boxes", "Sneevil", "Metal Ore", cell: "Enter", pad: "Spawn");
        Core.ToBank("Crypto Token");
    }
}
