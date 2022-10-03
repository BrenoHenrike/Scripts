//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class QueenReign
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        FourthHeroOfBalance();
        EleventhHeroOfBalance();
        ThirdAndTwelfthHeroOfBalance();
        TenthHeroOfBalance();
    }

    public void FourthHeroOfBalance()
    {
        //Fourth Hero of Balance
        if (Core.isCompletedBefore(8308))
            return;

        //Fallen Nopperabo
        Story.KillQuest(8302, "queenreign", "Samurai Nopperabo");

        //Samurai of Jaaku
        Story.KillQuest(8303, "queenreign", "Shadow Samurai");

        //Constructing the Portal
        Story.MapItemQuest(8304, "queenreign", 9120);
        Story.KillQuest(8304, "queenreign", new[] { "Samurai Nopperabo", "Shadow Samurai" });

        //Into the Yokai Realm
        Story.KillQuest(8305, "queenreign", "Tsukumo-Gami");

        //Jaaku's Shadow
        Story.KillQuest(8306, "queenreign", "Jaaku's Shadow");

        //The Forces of Jaaku
        Story.KillQuest(8307, "queenreign", "Jaaku's Shadow");

        //The Wind Orb
        Story.KillQuest(8308, "queenreign", "Jaaku");
    }

    public void EleventhHeroOfBalance()
    {
        //Eleventh Hero of Balance
        if (Core.isCompletedBefore(8314))
            return;

        //Super Spreaders
        Story.KillQuest(8309, "queenreign", "Plague Spreader");

        //Sample Size
        Story.MapItemQuest(8310, "queenreign", 9121, 4);

        //Moss You Be This Nasty
        Story.KillQuest(8311, "queenreign", "Plaguemoss");

        //Infected With The Cure
        Story.MapItemQuest(8312, "queenreign", 9122, 4);
        Story.KillQuest(8312, "queenreign", "Plague Spreader");

        //Hurtful Healing
        Story.KillQuest(8313, "queenreign", "Plague Spreader");

        //The Energy Orb
        Story.KillQuest(8314, "queenreign", "Extriki");
    }

    public void ThirdAndTwelfthHeroOfBalance()
    {
        //Third And Twelfth Hero of Balance
        if (Core.isCompletedBefore(8319))
            return;

        //Amethite, Am I Right?
        Story.KillQuest(8315, "queenreign", "Calcified Amethite");

        //Reinforcements Deployed
        Story.MapItemQuest(8316, "queenreign", 9123, 4);

        //Wyrms Below
        Story.KillQuest(8317, "queenreign", "Calcified Wyrm");

        //Lair Located
        Story.MapItemQuest(8318, "queenreign", 9124);
        Story.KillQuest(8318, "queenreign", "Calcified Remains");

        //The Earth Orb
        Story.KillQuest(8319, "queenreign", "Grou'luu");
    }

    public void TenthHeroOfBalance()
    {
        //Tenth Hero of Balance
        if (Core.isCompletedBefore(8325))
            return;

        //Goblin Down Water
        Story.KillQuest(8320, "queenreign", "Water Goblin");

        //Spawn of the Salt Sower
        Story.KillQuest(8321, "queenreign", "Sa-Laatan Spawn");

        //Faerie in Danger
        Story.KillQuest(8322, "queenreign", "Sa-Laatan Spawn");

        //Fertility Ward
        Story.MapItemQuest(8323, "queenreign", 9125, 5);

        //Path to Sa-Laatan
        Story.KillQuest(8324, "queenreign", "Water Goblin");

        //The Water Orb
        Story.KillQuest(8325, "queenreign", "Sa-Laatan");
    }
}
