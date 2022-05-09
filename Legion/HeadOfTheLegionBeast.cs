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
        int[] HelmShopIDs = new[] { 8233, 8234, 8234 };
        foreach (string Helm in HelmsCircle) foreach (int HelmShopID in HelmShopIDs)
            {
                if (!Core.CheckInventory(Helm))
                {
                    Legion.FarmLegionToken(1500);
                    Indulgence(10);
                    Core.BuyItem("sevencircles", 1980, Helm, shopItemID: HelmShopID);
                }
            }
        string[] HelmsCircleWar = { "Crown of Wrath", "Face of Treachery", "Faces of Violence" };
        int[] HelmShop2IDs = new[] { 8250, 8254, 8249 };
        foreach (string Helm in HelmsCircleWar) foreach (int HelmShop2ID in HelmShop2IDs)
            {
                if (!Core.CheckInventory(Helm))
                {
                    Legion.FarmLegionToken(1500);
                    Penance(10);
                    Core.BuyItem("sevencircleswar", 1984, Helm, shopItemID: HelmShop2ID);
                }
            }
        Core.BuyItem("sevencircleswar", 1984, "Helms of the Seven Circles", shopItemID: 8255);
    }

    public void EssenceWrath(int quant = 300)
    {
        if (Core.CheckInventory("Essence of Wrath", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.Logger($"Farming {quant}x Essence of Wrath");
        Core.Join("sevencircleswar");
        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(7979);

        while (!Bot.Inventory.Contains("Essence of Wrath", quant))
            Core.HuntMonster("sevencircleswar", "Wrath Guard", "Wrath Guards Defeated", 12);

        Bot.Wait.ForPickup("Essence of Wrath");
        Core.Logger($"Finished farming {quant} Essence of Wrath");
        Core.CancelRegisteredQuests();
    }

    public void EssenceViolence(int quant = 300)
    {
        if (Core.CheckInventory("Essence of Violence", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Essence of Violence");
        Core.RegisterQuests(7979);

        while (!Bot.Inventory.Contains("Essence of Violence", quant))
            Core.HuntMonster("sevencircleswar", "Violence Guard", "Violence Guards Defeated", 12);

        Bot.Wait.ForPickup("Essence of Violence");
        Core.Logger($"Finished farming {quant} Essence of Violence");
        Core.CancelRegisteredQuests();
    }

    public void EssenceTreachery(int quant = 300)
    {
        if (Core.CheckInventory("Essence of Treachery", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Essence of Treachery");
        Core.RegisterQuests(7988);

        while (!Bot.Inventory.Contains("Essence of Treachery", quant))
            Core.HuntMonster("sevencircleswar", "Treachery Guard", "Treachery Guards Defeated", 12);

        Bot.Wait.ForPickup("Essence of Treachery");
        Core.Logger($"Finished farming {quant} Essence of Treachery");
        Core.CancelRegisteredQuests();
    }

    public void SoulsHeresy(int quant = 300)
    {

        if (Core.CheckInventory("Souls of Heresy", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.Logger($"Farming {quant} Souls of Heresy");
        Core.RegisterQuests(7979, 7980, 7981);

        while (!Core.CheckInventory("Souls of Heresy", quant))
            Core.KillMonster("sevencircleswar", "Enter", "Spawn", "Wrath Guard", "Wrath Guards Defeated", 12);

        Bot.Wait.ForPickup("Souls of Heresy");
        Core.Logger($"Finished farming {quant} Souls of Heresy");
        Core.CancelRegisteredQuests();
    }

    public void Penance(int quant)
    {
        if (Core.CheckInventory("Penance", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.Logger($"Farming {quant} Penance");
        Core.EquipClass(ClassType.Farm);

        while (!Core.CheckInventory("Penance", quant))
        {
            EssenceWrath(quant);
            EssenceViolence(quant);
            EssenceTreachery(quant);
            SoulsHeresy(15 * quant);
            Core.BuyItem("sevencircleswar", 1984, "Penance", quant);
        }
        Core.Logger($"Finished farming {quant} Penance");
        Core.CancelRegisteredQuests();
    }

    public void Indulgence(int quant = 100)
    {
        if (Core.CheckInventory("Indulgence", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Indulgence");
        Core.RegisterQuests(7978);

        while (!Core.CheckInventory("Indulgence", quant))
        {
            Core.KillMonster("sevencircles", "r2", "Left", "Limbo Guard", "Souls of Limbo", 25);
            Core.KillMonster("sevencircles", "r3", "Left", "Luxuria Guard", "Essence of Luxuria", 1);
            Core.KillMonster("sevencircles", "r5", "Left", "Gluttony Guard", "Essence of Gluttony", 1);
            Core.KillMonster("sevencircles", "r7", "Left", "Avarice Guard", "Essence of Avarice", 1);
        }
        Bot.Wait.ForPickup("Indulgence");
        Core.Logger($"Finished farming {quant} Indulgence");
        Core.CancelRegisteredQuests();
    }
}