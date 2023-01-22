/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class OrbHunt
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        OrbHuntSaga();

        Core.SetOptions(false);
    }

    public void OrbHuntSaga()
    {
        if (Core.isCompletedBefore(8349))
            return;

        Story.PreLoad(this);

        // 8302|Fallen Nopperabo
        Story.KillQuest(8302, "queenreign", "Samurai Nopperabo");

        // 8303|Samurai of Jaaku
        Story.KillQuest(8303, "queenreign", "Shadow Samurai");

        // 8304|Constructing the Portal
        Story.MapItemQuest(8304, "queenreign", 9120);
        Story.KillQuest(8304, "queenreign", new[] { "Samurai Nopperabo", "Shadow Samurai" });

        // 8305|Into the Yokai Realm
        Story.KillQuest(8305, "queenreign", "Tsukumo-Gami");

        // 8306|Jaaku's Shadow
        Story.KillQuest(8306, "queenreign", "Jaaku's Shadow");

        // 8307|The Forces of Jaaku
        Story.KillQuest(8307, "queenreign", "Tsukumo-Gami");

        // 8308|The Wind Orb
        Story.KillQuest(8308, "queenreign", "Jaaku");

        // 8309|Super Spreaders
        Story.KillQuest(8309, "queenreign", "Plague Spreader");

        // 8310|Sample Size
        Story.MapItemQuest(8310, "queenreign", 9121, 4);

        // 8311|Moss You Be This Nasty
        Story.KillQuest(8311, "queenreign", "Plaguemoss");

        // 8312|Infected With The Cure
        Story.MapItemQuest(8312, "queenreign", 9122, 4);
        Story.KillQuest(8312, "queenreign", "Plaguemoss");

        // 8313|Hurtful Healing
        Story.KillQuest(8313, "queenreign", "Plaguemoss");

        // 8314|The Energy Orb
        Story.KillQuest(8314, "queenreign", "Extriki");

        // 8315|Amethite, Am I Right?
        Story.KillQuest(8315, "queenreign", "Calcified Amethite");

        // 8316|Reinforcements Deployed
        Story.MapItemQuest(8316, "queenreign", 9123, 4);

        // 8317|Wyrms Below
        Story.KillQuest(8317, "queenreign", "Calcified Wyrm");

        // 8318|Lair Located
        Story.MapItemQuest(8318, "queenreign", 9124);
        Story.KillQuest(8318, "queenreign", "Calcified Remains");

        // 8319|The Earth Orb
        Story.KillQuest(8319, "queenreign", "Grou'luu");

        // 8320|Goblin Down Water
        Story.KillQuest(8320, "queenreign", "Water Goblin");

        // 8321|Spawn of the Salt Sower
        Story.KillQuest(8321, "queenreign", "Sa-Laatan Spawn");

        // 8322|Faerie in Danger
        Story.KillQuest(8322, "queenreign", "Water Goblin");

        // 8323|Fertility Ward
        Story.MapItemQuest(8323, "queenreign", 9125, 5);

        // 8324|Path to Sa-Laatan
        Story.KillQuest(0008324, "queenreign", "Water Goblin");

        // 8325|The Water Orb
        Story.KillQuest(8325, "queenreign", "Sa-Laatan");

        // 8326|Water. Earth. Energy? Air.
        Story.KillQuest(8326, "queenreign", new[] { "Sa-Laatan", "Grou'luu", "Extriki", "Jaaku" });

        // 8328|Plant Stalk-ers
        Story.KillQuest(8328, "orbhunt", "Seed Stalker");

        // 8329|Rage Against the Mach-Wing
        Story.KillQuest(8329, "orbhunt", "Ragewing");

        // 8330|Tower Wards
        Story.MapItemQuest(8330, "orbhunt", 9156, 4);
        Story.MapItemQuest(8330, "orbhunt", 9157, 4);

        // 8331|The Final Barrier
        Story.KillQuest(8331, "orbhunt", "Seed Stalker");

        // 8332|The Fire Orb
        Story.KillQuest(8332, "orbhunt", "Chamat");

        // 8333|Lotus Be Done With This
        Story.KillQuest(8333, "orbhunt", "Lotus Spider");

        // 8334|Lightless Light
        Story.KillQuest(8334, "orbhunt", "Suffocated Light");

        // 8335|Suffocation Investigation
        Story.MapItemQuest(8335, "orbhunt", 9158, 4);

        // 8336|Gone With the Djinn
        Story.MapItemQuest(8336, "orbhunt", 9159);
        Story.KillQuest(8336, "orbhunt", "Lotus Spider");

        // 8337|The Light Orb
        Story.KillQuest(8337, "orbhunt", "Sek Duat I");

        // 8338|The Light Orb, Part II
        Story.KillQuest(8338, "orbhunt", "Horothotep");

        // 8339|Nax-ty Beasts
        Story.KillQuest(8339, "orbhunt", "Nax Beast");

        // 8340|Children of the Night
        Story.MapItemQuest(8340, "orbhunt", 9160, 4);
        Story.KillQuest(8340, "orbhunt", "Hive");

        // 8341|We've Woken The Hive
        Story.KillQuest(8341, "orbhunt", "Hive");

        // 8342|Ritual Disruption
        Story.MapItemQuest(8342, "orbhunt", 9161, 4);
        Story.MapItemQuest(8342, "orbhunt", 9162);
        Story.KillQuest(8342, "orbhunt", "Hive");

        // 8343|The Darkness Orb
        Story.KillQuest(8343, "orbhunt", "Kolyaban");

        // 8344|The Infernal Cold
        Story.KillQuest(8344, "orbhunt", "Ice Infernal");

        // 8345|Light a Fire
        Story.MapItemQuest(8345, "orbhunt", 9163, 4);

        // 8346|We Ani-must Win
        Story.KillQuest(8346, "orbhunt", "Animus of Ice");

        // 8347|Bones For Stones
        Story.KillQuest(8347, "orbhunt", new[] { "Seed Stalker", "Lotus Spider", "Hive", "Ice Infernal" });

        // 8348|The Ice Orb
        Story.KillQuest(8348, "orbhunt", "Quetzal");

        // 8349|A Hymn of Ice and Fire, and Light and Dark  
        Story.KillQuest(8349, "orbhunt", new[] { "Chamat", "Horothotep", "Kolyaban", "Quetzal" });
    }
}
