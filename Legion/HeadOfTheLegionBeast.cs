//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
using Skua.Core.Interfaces;

public class HeadoftheLegionBeast
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();
    public CoreLegion Legion = new CoreLegion();
    public CoreAdvanced Adv = new CoreAdvanced();
    public SevenCircles Circles = new SevenCircles();

    public void ScriptMain(IScriptInterface bot)
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
        Adv.BuyItem("sevencircleswar", 1984, "Head of the Legion Beast");
        Bot.Wait.ForItemBuy();
        Adv.EnhanceItem("Head of the Legion Beast", EnhancementType.Lucky);
    }

    public void HelmSevenCircles()
    {
        if (Core.CheckInventory(60137))
            return;

        Core.AddDrop(HeadLegionBeast);
        (string, int)[] HelmsCircle = { ("Aspect of Luxuria", 8233), ("Gluttony's Maw", 8234), ("Stare of Greed", 8234) };
        foreach ((string Helm, int shopItemID) in HelmsCircle)
        {
            if (!Core.CheckInventory(Helm))
            {
                Core.Logger($"Started Farm for: {Helm}");
                Core.AddDrop(Helm);
                Legion.FarmLegionToken(1500);
                Indulgence(10);
                Core.BuyItem("sevencircles", 1980, Helm, shopItemID);
                Bot.Wait.ForItemBuy();
            }
        }
        (string, int)[] HelmsCircleWar = { ("Crown of Wrath", 8250), ("Face of Treachery", 8254), ("Faces of Violence", 8249) };
        foreach ((string Helm, int shopItemID) in HelmsCircleWar)
        {
            if (!Core.CheckInventory(Helm))
            {
                Core.Logger($"Started Farm for: {Helm}");
                Core.AddDrop(Helm);
                Legion.FarmLegionToken(1500);
                Penance(10);
                Adv.BuyItem("sevencircleswar", 1984, Helm, shopItemID);
                Bot.Wait.ForItemBuy();
            }
        }
        Adv.BuyItem("sevencircleswar", 1984, "Helms of the Seven Circles");
        Bot.Wait.ForItemBuy();
    }

    public void EssenceWrath(int quant = 300)
    {
        if (Core.CheckInventory("Essence of Wrath", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant}x Essence of Wrath");

        Core.RegisterQuests(7979);
        while (!Bot.ShouldExit && !Core.CheckInventory("Essence of Wrath", quant))
            Core.KillMonster("sevencircleswar", "Enter", "Spawn", "Wrath Guard", "Wrath Guards Defeated", 12);

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

        while (!Bot.ShouldExit && !Core.CheckInventory("Essence of Violence", quant))
        {
            Core.EnsureAccept(7985);
            Core.KillMonster("sevencircleswar", "r9", "Left", "Violence Guard", "Violence Guards Defeated", 12);
            Core.EnsureComplete(7985);
        }
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

        while (!Bot.ShouldExit && !Core.CheckInventory("Essence of Treachery", quant))
        {
            Core.EnsureAccept(7988);
            Core.KillMonster("sevencircleswar", "r13", "Left", "Treachery Guard", "Treachery Guards Defeated", 12);
            Core.EnsureComplete(7988);
        }
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

        while (!Bot.ShouldExit && !Core.CheckInventory("Souls of Heresy", quant))
        {
            Core.EnsureAccept(7983);
            Core.KillMonster("sevencircleswar", "r7", "Left", "Heresy Guard", "Heresy Guards Defeated", 12);
            Core.EnsureComplete(7983);
        }
        Bot.Wait.ForPickup("Souls of Heresy");
        Core.Logger($"Finished farming {quant} Souls of Heresy");
        Core.CancelRegisteredQuests();
    }

    public void Penance(int quant = 30)
    {
        if (Core.CheckInventory(60137, quant))
            return;
        int quant2 = 5;
        Core.AddDrop(HeadLegionBeast);
        Core.Logger($"Farming {quant} Penance");
        Core.EquipClass(ClassType.Farm);
        while (!Bot.ShouldExit && !Core.CheckInventory("Penance", quant))
        {
            EssenceWrath(quant2);
            EssenceViolence(quant2);
            EssenceTreachery(quant2);
            SoulsHeresy(15 * quant2);
            Adv.BuyItem("sevencircleswar", 1984, "Penance", Bot.Inventory.GetQuantity("Penance") + 5);
        }
        Bot.Wait.ForItemBuy();
        Core.Logger($"Finished farming {quant} Penance");
    }

    public void Indulgence(int quant = 100)
    {
        if (Core.CheckInventory("Indulgence", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Indulgence");

        Core.RegisterQuests(7978);
        while (!Bot.ShouldExit && !Core.CheckInventory("Indulgence", quant))
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