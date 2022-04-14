//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class CastleofBones
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CastleofBonesSaga();

        Core.SetOptions(false);
    }

    public void CastleofBonesSaga()
    {
        if (Core.isCompletedBefore(4992))
            return;

        Story.PreLoad();

        // Enter the Castle of Bone
        Story.KillQuest(4968, "bonecastle", "Undead Guard");

        // Slay Vaden's Undead Guards
        Story.KillQuest(4969, "bonecastle", "Undead Guard");

        // Destroyer in the Foyer
        Story.KillQuest(4970, "bonecastle", "Undead Knight");

        // Help! I'm a Fallen Lord and I Can't Get Up!
        Story.KillQuest(4971, "bonecastle", "Fallen Deathknight");

        // Gem-nastics
        Story.KillQuest(4972, "bonecastle", "Fallen Deathknight");
        Story.MapItemQuest(4972, "bonecastle", 4342, 3);

        // Bone Appetit
        Story.KillQuest(4973, "bonecastle", "Undead Waiter");

        // The Walking Bread
        Story.MapItemQuest(4974, "bonecastle", 4343, 2);
        Story.MapItemQuest(4974, "bonecastle", 4344, 2);
        Story.MapItemQuest(4974, "bonecastle", 4345, 3);
        Story.KillQuest(4974, "bonecastle", "Undead Knight");

        // Ghoul-ash
        Story.KillQuest(4975, "bonecastle", "Ghoul");

        // The Butcher
        Story.KillQuest(4976, "bonecastle", "The Butcher");

        // Lean Mean Undead Slaying Machine
        Story.KillQuest(4977, "bonecastle", "Skeletal Warrior");

        // Moar Loot
        Story.MapItemQuest(4978, "bonecastle", 4346, 1);
        Story.MapItemQuest(4978, "bonecastle", 4347, 1);
        Story.MapItemQuest(4978, "bonecastle", 4348, 1);
        Story.KillQuest(4978, "bonecastle", "Skeletal Warrior");

        // Putting Your Hands All over Everything
        Story.MapItemQuest(4979, "bonecastle", 4349, 1);
        Story.MapItemQuest(4979, "bonecastle", 4350, 1);
        Story.MapItemQuest(4979, "bonecastle", 4351, 1);
        Story.KillQuest(4979, "bonecastle", "Skeletal Warrior");

        // Paladin Rock
        Story.KillQuest(4980, "bonecastle", new[] { "Grateful Undead", "That 70's Zombie" });
        Story.MapItemQuest(4980, "bonecastle", 4354, 1);
        Story.MapItemQuest(4980, "bonecastle", 4355, 1);

        // Do You Find This Humerus?
        if (!Story.QuestProgression(4981))
        {
            Core.EnsureAccept(4981);
            Core.HuntMonster("bonecastle", "Skeletal Warrior", "Undead Humerus Bones", 5);
            Core.EnsureComplete(4981);
        }

        // Vaden Says
        Story.KillQuest(4982, "bonecastle", new[] { "Skeletal Warrior", "Undead Guard", "Undead Knight" });

        // The Dead King's Bedroom
        Story.MapItemQuest(4983, "bonecastle", 4352, 1);

        // Game of Porcelain Thrones
        Story.MapItemQuest(4984, "bonecastle", 4353, 4);

        // Teenage Mutant Sewer Turtles
        Story.KillQuest(4985, "bonecastle", "Turtle");

        // Adolescent Inhuman Samurai Reptiles
        Story.KillQuest(4986, "bonecastle", new[] { "Turtle", "Turtle", "Turtle", "Turtle" });

        // Snuggles!
        Story.KillQuest(4987, "bonecastle", "Snuggles, Torturer");

        // Game of Bones
        Story.KillQuest(4988, "bonecastle", new[] { "Jon Bones", "Oberon Marrowtell", "Baskerville", "Knight of Lichens" });

        // Rot Tin Tin!
        Story.KillQuest(4989, "bonecastle", "Rot Tin Tin");

        // Gold Digger
        Story.KillQuest(4990, "bonecastle", new[] { "Undead Golden Knight", "Undead Golden Knight", "Undead Golden Knight" });

        // Gotta Hand It To Ya
        Story.KillQuest(4991, "bonecastle", new[] { "Undead Knight", "Skeletal Warrior" });

        // Vaden's Defeat
        Story.KillQuest(4992, "bonecastle", "Vaden");
    }
}
