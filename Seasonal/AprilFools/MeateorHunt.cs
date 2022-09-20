//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
public class MeateorHunt
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (Core.isCompletedBefore(8628))
        {
            Core.Logger("You have already completed this storyline");
            return;
        }

        Story.PreLoad(this);

        //Defeat the Giant ChickenCow
        Story.KillQuest(8612, "MeateorTown", "Giant ChickenCow");


        //Chick Your Surroundings
        if (!Story.QuestProgression(8614))
        {
            Core.EnsureAccept(8614);
            Core.HuntMonster("EarthStorm", "Blue Chick", "Tiny Blue Freggment");
            Core.HuntMonster("Mythsong", "Pink Chick", "Tiny Pink Freggment");
            Core.HuntMonster("WaterStorm", "Purple Chick", "Tiny Purple Freggment");
            Core.HuntMonster("GreenguardWest", "Green Chick", "Tiny Green Freggment");
            Core.HuntMonster("DarkoviaGrave", "Rainbow Chick", "Tiny Rainbow Freggment");
            Core.EnsureComplete(8614);
        }

        //PainFowl Recollection
        Story.KillQuest(8615, "Battlefowl", "Chicken");

        //Empty Nest Syndrome
        if (!Story.QuestProgression(8616))
        {
            Core.EnsureAccept(8616);
            Core.KillMonster("Uppercity", "r2", "Left", 249, "Nested Freggment", 8);
            Core.EnsureComplete(8616);
        }

        //Comet-ted to Memory     
        Story.KillQuest(8617, "Comet", new[] { "Vaderix", "Vaderix" });

        //Right Under Your Nose
        Story.KillQuest(8618, "BattleFowl", "ChickenCow");

        //Don't Chicken Out!
        if (!Story.QuestProgression(8619))
        {
            Core.EquipClass(ClassType.Farm);
            Core.EnsureAccept(8619);
            Core.HuntMonster("BattleFowl", "Sabertooth Chicken", "Pointy Freggment", 8);
            Core.HuntMonster("MeateorTown", "Red Chicken", "Red Freggment", 10);
            Core.HuntMonster("DfLesson", "Chaotic Chicken", "Chaotic Freggment", 6);
            Core.EnsureComplete(8619);
        }
        //Ashville Hot
        Story.KillQuest(8620, "AshfallCamp", new[] { "Infernus", "Blackrawk", "Smoldur" });

        //Holy (Chicken)Cow!
        Story.KillQuest(8621, "CrashRuins", new[] { "Unlucky Explorer", "Cluckmoo Idol" });

        //Chilling Tales of Chicken Cows
        Story.KillQuest(8622, "northlands", new[] { "Ice Symbiote", "Ice Symbiote" });

        //Cowissa
        Bot.Quests.UpdateQuest(8000);
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(8623, "astravia", "The Moon");

        //Collection Dejection
        Story.KillQuest(8624, "Future", new[] { "The Collector", "The Collector" });

        //Poached Eggs
        Story.KillQuest(8625, "byrodax", new[] { "Byro-Dax Monstrosity", "Space Goop", "Mutated Critter", });

        //Dumpster Diving
        Story.KillQuest(8626, "Junkhoard", new[] { "Magpie Junk Heap", "Junk Golem", "Magpie" });

        //A Dreadful Dinner
        Story.KillQuest(8627, "Dreadspace", "Troblor");

        //Succeed or Fry Trying
        Story.KillQuest(8628, "thirdspell", new[] { "Great Solar Elemental", "Sun Flare", "Solar Incarnation" });

    }
}