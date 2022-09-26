//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class EnchantedVictoryBladeWeapons
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetWeapon(VictoryBladeStyles.ArcaneBladeOfGlory);
        GetWeapon(VictoryBladeStyles.ShadowBladeOfDispair);

        Core.SetOptions(false);
    }

    public void GetWeapon(VictoryBladeStyles Method = VictoryBladeStyles.Smart)
    {

        if ((int)Method == 2)
        {
            if (Core.CheckInventory(new[] { "Enchanted Mana Blade", "Bright Aura Gem", "Amulet of Glory", "Arcane Blade of Glory" }, any: true))
                Method = VictoryBladeStyles.ArcaneBladeOfGlory;
            else if (Core.CheckInventory(new[] { "Enchanted Shadow Blade", "Dark Aura Gem", "Amulet of Dispair", "Shadow Blade of Dispair" }, any: true))
                Method = VictoryBladeStyles.ShadowBladeOfDispair;
            else
            {
                Alignment alignment = (Alignment)Bot.Flash.CallGameFunction<int>("world.getQuestValue", 41);
                if (alignment == Alignment.Good)
                    Method = VictoryBladeStyles.ArcaneBladeOfGlory;
                else if (alignment == Alignment.Evil)
                    Method = VictoryBladeStyles.ShadowBladeOfDispair;
                else Method = Bot.Random.Next(0, 100) > 50 ? VictoryBladeStyles.ArcaneBladeOfGlory : VictoryBladeStyles.ShadowBladeOfDispair;
            }
        }
        bool doGlory = (int)Method == 0;

        if (doGlory && Core.CheckInventory("Arcane Blade of Glory"))
            return;
        else if ((int)Method == 1 && Core.CheckInventory("Shadow Blade of Dispair"))
            return;

        TypeAuraGem(doGlory ? "bright" : "dark");
        Adv.BuyItem("river", 1213, doGlory ? "Enchanted Mana Blade" : "Enchanted Shadow Blade");
        AmuletOfType(doGlory ? "glory" : "dispair");
        Adv.BuyItem("river", 1213, doGlory ? "Arcane Blade of Glory" : "Shadow Blade of Despair");
    }

    public void EnchantedVictoryBlade()
    {
        if (Core.CheckInventory("Enchanted Victory Blade"))
            return;

        Adv.BuyItem("river", 1213, "Silver Victory Blade");
        if (!Core.CheckInventory("Enchantment Rune"))
        {
            Core.AddDrop("Enchantment Rune");
            Core.FarmingLogger("Enchantment Rune", 1);
            Core.EnsureAccept(4811);

            Core.HuntMonster("graveyard", "Skeletal Viking", "Ravenwing Scroll", 1, false);
            Core.HuntMonster("graveyard", "Skeletal Warrior", "Unseeing Eye", 3, false);
            Core.HuntMonster("graveyard", "Big Jack Sprat", "Shard of Diamond Blade", 5, false); 

            Core.EnsureAccept(4811);
            Bot.Wait.ForPickup("Enchantment Rune");
        }
        Adv.BuyItem("river", 1213, "Enchanted Victory Blade");
    }

    public void TypeAuraGem(string type)
    {
        type = Captialize(type.Trim());
        if (type != "Bright" && type != "Dark")
        {
            Core.Logger("Invalid paramater. Expected \"Bright\" or \"Dark\"");
            return;
        }
        type = $"{type} Aura Gem";
        if (Core.CheckInventory(type))
            return;

        EnchantedVictoryBlade();

        Core.AddDrop(type);
        Core.FarmingLogger(type, 1);
        Core.EnsureAccept(4812);

        Core.HuntMonster("graveyard", "Skeletal Warrior", "Broken Dream Catcher", 10, false);

        Core.EnsureAccept(4812, type == "Bright Aura Gem" ? 33500 : 33499);
        Bot.Wait.ForPickup(type);
    }

    public void AmuletOfType(string type)
    {
        type = Captialize(type.Trim());
        if (type != "Glory" && type != "Dispair")
        {
            Core.Logger("Invalid paramater. Expected \"Glory\" or \"Dispair\"");
            return;
        }
        type = $"Amulet of {type}";
        if (Core.CheckInventory(type))
            return;

        Core.AddDrop(type);
        Core.FarmingLogger(type, 1);
        Core.EnsureAccept(4813);

        Core.HuntMonster("graveyard", "Skeletal Viking", "Nornir Triad Shard", 12, false);

        Core.EnsureAccept(4813, type == "Amulet of Glory" ? 33502 : 33501);
        Bot.Wait.ForPickup(type);
    }

    private string Captialize(string input)
    {
        if (String.IsNullOrEmpty(input))
            return input;
        if (input.Count() == 1)
            return Char.ToUpper(input[0]).ToString();
        return Char.ToUpper(input[0]) + input.Substring(1).ToLower();
    }
}

public enum VictoryBladeStyles
{
    ArcaneBladeOfGlory = 0,
    ShadowBladeOfDispair = 1,
    Smart = 2,
}
