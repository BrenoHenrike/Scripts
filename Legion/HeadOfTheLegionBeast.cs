//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class HeadoftheLegionBeast
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreLegion Legion = new CoreLegion();
    public CoreAdvanced Adv = new CoreAdvanced();
    public SevenCircles Circles = new SevenCircles();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        LegionBeastHead();

        Core.SetOptions(false);
    }

    public string[] HeadLegionBeast =
    {
        "Penance",
        "Essence of Wrath",
        "Essence of Violence",
        "Essence of Treachery",
        "Souls of Heresy",
        "Indulgence",
        "Beast Soul",
        "Helms of the Seven Circles",
        "Faces of Violence",
        "Crown of Wrath",
        "Stare of Greed",
        "Gluttony's Maw",
        "Aspect of Luxuria",
        "Face of Treachery"
    };

    public void LegionBeastHead()
    {
        if (Core.CheckInventory("Head of the Legion Beast"))
            return;

        Story.PreLoad();

        Circles.CirclesWar();

        Core.AddDrop(HeadLegionBeast);
        HelmSevenCircles();
        Penance(30);
        Indulgence(30);
        Legion.FarmLegionToken(15000);
        Core.EquipClass(ClassType.Solo);
        Core.KillMonster("sevencircleswar", "r17", "Left", "The Beast", "Beast Soul", 15, false, publicRoom: true);
        Core.BuyItem("sevencircleswar", 1984, "Head of the Legion Beast");
    }

    public void HelmSevenCircles()
    {
        if (Core.CheckInventory("Helms of the Seven Circles"))
            return;

        Core.AddDrop(HeadLegionBeast);
        string[] HelmsCircle = { "Aspect of Luxuria", "Gluttony's Maw", "Stare of Greed" };
        foreach (string Helm in HelmsCircle)
        {
            if (!Core.CheckInventory(Helm))
            {
                Legion.FarmLegionToken(1500);
                Indulgence(10);
                Core.BuyItem("sevencircles", 1980, Helm);
            }
        }
        string[] HelmsCircleWar = { "Crown of Wrath", "Face of Treachery", "Faces of Violence" };
        foreach (string Helm in HelmsCircleWar)
        {
            if (!Core.CheckInventory(Helm))
            {
                Legion.FarmLegionToken(1500);
                Penance(10);
                Core.BuyItem("sevencircleswar", 1984, Helm);
            }
        }
        Core.BuyItem("sevencircleswar", 1984, "Helms of the Seven Circles");
    }

    public void EssenceWrath(int quant = 300)
    {
        if (Core.CheckInventory("Essence of Wrath", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.Logger($"Farming {quant}x Essence of Wrath");
        int i = 1;
        Core.Join("sevencircleswar");
        Core.EquipClass(ClassType.Farm);
        while (!Bot.Inventory.Contains("Essence of Wrath", quant))
        {
            Core.EnsureAccept(7979);
            Core.HuntMonster("sevencircleswar", "Wrath Guard", "Wrath Guards Defeated", 12);
            Core.JumpWait();
            Core.EnsureComplete(7979);
            Bot.Wait.ForPickup("Essence of Wrath");
            Core.Logger($"Completed x{i}");
            i++;
        }
    }

    public void EssenceViolence(int quant = 300)
    {
        if (Core.CheckInventory("Essence of Violence", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Essence of Violence");
        int i = 0;
        Core.Join("sevencircleswar");
        while (!Bot.Inventory.Contains("Essence of Violence", quant))
        {
            Core.EnsureAccept(7985);
            Core.HuntMonster("sevencircleswar", "Violence Guard", "Violence Guards Defeated", 12);
            Core.EnsureComplete(7985);
            Bot.Wait.ForPickup("Essence of Violence");
            i++;
            Core.Logger($"Completed x{i}");
        }
        Core.Logger($"Finished farming {quant} Essence of Violence");
    }

    public void EssenceTreachery(int quant = 300)
    {
        if (Core.CheckInventory("Essence of Treachery", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Essence of Treachery");
        int i = 0;
        while (!Bot.Inventory.Contains("Essence of Treachery", quant))
        {
            Core.EnsureAccept(7988);
            Core.HuntMonster("sevencircleswar", "Treachery Guard", "Treachery Guards Defeated", 12);
            Core.EnsureComplete(7988);
            Bot.Wait.ForPickup("Essence of Treachery");
            i++;
            Core.Logger($"Completed x{i}");
        }
        Core.Logger($"Finished farming {quant} Essence of Treachery");
    }

    public void SoulsHeresy(int quant = 300)
    {

        if (Core.CheckInventory("Souls of Heresy", quant))
            return;

        int i = 0;
        Core.AddDrop(HeadLegionBeast);
        Core.Logger($"Farming {quant} Souls of Heresy");
        while (!Core.CheckInventory("Souls of Heresy", quant))
        {
            Core.EnsureAccept(7979, 7980, 7981);
            Core.KillMonster("sevencircleswar", "Enter", "Spawn", "Wrath Guard", "Wrath Guards Defeated", 12);
            Core.EnsureComplete(7979);
            while (Bot.Inventory.ContainsTempItem("War Medal", 5))
                Core.EnsureComplete(7980);
            while (Bot.Inventory.ContainsTempItem("Mega War Medal", 3))
                Core.EnsureComplete(7981);
            i++;
            Core.Logger($"Completed x{i}");
        }
        Core.Logger($"Finished farming {quant} Souls of Heresy");
    }

    public void Penance(int quant)
    {
        if (Core.CheckInventory("Penance", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.Logger($"Farming {quant} Penance");
        Core.EquipClass(ClassType.Farm);
        int i = 0;
        while (!Core.CheckInventory("Penance", quant))
        {
            EssenceWrath(1);
            EssenceViolence(1);
            EssenceTreachery(1);
            SoulsHeresy(15);
            Core.Join("sevencircleswar");
            Core.JumpWait();
            Bot.Shops.BuyItem(1984, "Penance");
            i++;
            Core.Logger($"Completed x{i}");
        }
        Core.Logger($"Finished farming {quant} Penance");
    }

    public void Indulgence(int quant = 100)
    {
        if (Core.CheckInventory("Indulgence", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Indulgence");
        int i = 0;
        while (!Core.CheckInventory("Indulgence", quant))
        {
            Core.EnsureAccept(7978);
            Core.KillMonster("sevencircles", "r2", "Left", "Limbo Guard", "Souls of Limbo", 25);
            Core.KillMonster("sevencircles", "r3", "Left", "Luxuria Guard", "Essence of Luxuria", 1);
            Core.KillMonster("sevencircles", "r5", "Left", "Gluttony Guard", "Essence of Gluttony", 1);
            Core.KillMonster("sevencircles", "r7", "Left", "Avarice Guard", "Essence of Avarice", 1);
            Core.EnsureComplete(7978);
            Bot.Wait.ForPickup("Indulgence");
            i++;
            Core.Logger($"Completed x{i}");
        }
        Core.Logger($"Finished farming {quant} Indulgence");
    }
}