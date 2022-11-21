//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
using Skua.Core.Interfaces;

public class HeadoftheLegionBeast
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public CoreLegion Legion = new();
    public SevenCircles Circles = new();

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

        Circles.CirclesWar();
        Core.AddDrop(HeadLegionBeast);

        HelmSevenCircles();
        Penance(30);
        Indulgence(30);
        Legion.FarmLegionToken(15000);

        Core.EquipClass(ClassType.Solo);
        Adv.BestGear(GearBoost.Undead);
        Core.KillMonster("sevencircleswar", "r17", "Left", "The Beast", "Beast Soul", 15, isTemp: false, publicRoom: true);

        Adv.BuyItem("sevencircleswar", 1984, "Head of the Legion Beast");
    }

    public void HelmSevenCircles()
    {
        if (Core.CheckInventory(60137))
            return;

        Core.AddDrop(HeadLegionBeast);

        CircleHelm("Aspect of Luxuria");
        CircleHelm("Gluttony's Maw");
        CircleHelm("Stare of Greed");

        CircleHelm("Crown of Wrath", true);
        CircleHelm("Face of Treachery", true);
        CircleHelm("Faces of Violence", true);

        Adv.BuyItem("sevencircleswar", 1984, "Helms of the Seven Circles");

        void CircleHelm(string helm, bool war = false)
        {
            if (Core.CheckInventory(helm))
                return;

            Core.FarmingLogger(helm, 1);
            Legion.FarmLegionToken(1500);

            if (war)
            {
                Penance(10);
                Adv.BuyItem("sevencircleswar", 1984, helm);
            }
            else
            {
                Indulgence(10);
                Adv.BuyItem("sevencircles", 1980, helm);
            }
        }
    }

    public void EssenceWrath(int quant = 300)
    {
        if (Core.CheckInventory("Essence of Wrath", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger("Essence of Wrath", quant);

        Core.RegisterQuests(7979);
        while (!Bot.ShouldExit && !Core.CheckInventory("Essence of Wrath", quant))
        {
            Core.KillMonster("sevencircleswar", "Enter", "Spawn", "Wrath Guard", "Wrath Guards Defeated", 12);
        }
        Core.CancelRegisteredQuests();
    }

    public void EssenceViolence(int quant = 300)
    {
        if (Core.CheckInventory("Essence of Violence", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger("Essence of Violence", quant);

        Core.RegisterQuests(7985);
        while (!Bot.ShouldExit && !Core.CheckInventory("Essence of Violence", quant))
        {
            Core.KillMonster("sevencircleswar", "r9", "Left", "Violence Guard", log: false);
        }
        Core.CancelRegisteredQuests();
    }

    public void EssenceTreachery(int quant = 300)
    {
        if (Core.CheckInventory("Essence of Treachery", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger("Essence of Treachery", quant);

        Core.RegisterQuests(7988);
        while (!Bot.ShouldExit && !Core.CheckInventory("Essence of Treachery", quant))
        {
            Core.KillMonster("sevencircleswar", "r13", "Left", "Treachery Guard", log: false);
        }
        Core.CancelRegisteredQuests();
    }

    public void SoulsHeresy(int quant = 300)
    {

        if (Core.CheckInventory("Souls of Heresy", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        if (!Bot.Quests.IsUnlocked(7983))
            Circles.CirclesWar(true);
        Core.Logger($"Farming {quant} Souls of Heresy");
        Core.RegisterQuests(7983, 7980, 7981); // Blasphemy? Blasphe-you! ID:7983 | War Medals ID:7980 | Mega War Medals ID:7981
        while (!Bot.ShouldExit && !Core.CheckInventory("Souls of Heresy", quant))
            Core.KillMonster("sevencircleswar", "r7", "Left", "Heresy Guard", log: false);
        Core.CancelRegisteredQuests();
    }

    public void Penance(int quant = 30)
    {
        if (Core.CheckInventory(60137, quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.FarmingLogger("Penance", quant);
        Core.EquipClass(ClassType.Farm);

        while (!Bot.ShouldExit && !Core.CheckInventory("Penance", quant))
        {
            EssenceWrath(5);
            EssenceViolence(5);
            EssenceTreachery(5);
            SoulsHeresy(75);
            Adv.BuyItem("sevencircleswar", 1984, "Penance", Bot.Inventory.GetQuantity("Penance") + 5);
        }
    }

    public void Indulgence(int quant = 100)
    {
        if (Core.CheckInventory("Indulgence", quant))
            return;

        Core.AddDrop(HeadLegionBeast);
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger("Indulgence", quant);

        Core.RegisterQuests(7978);
        while (!Bot.ShouldExit && !Core.CheckInventory("Indulgence", quant))
        {
            Core.KillMonster("sevencircles", "r2", "Left", "Limbo Guard", "Souls of Limbo", 25);
            Core.KillMonster("sevencircles", "r3", "Left", "Luxuria Guard", "Essence of Luxuria", 1);
            Core.KillMonster("sevencircles", "r5", "Left", "Gluttony Guard", "Essence of Gluttony", 1);
            Core.KillMonster("sevencircles", "r7", "Left", "Avarice Guard", "Essence of Avarice", 1);
        }
        Core.CancelRegisteredQuests();
    }
}
