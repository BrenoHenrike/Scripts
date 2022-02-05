//cs_include Scripts/CoreBots.cs
using RBot;
public class CastleofBones
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CastleofBonesSaga();

        Core.SetOptions(false);
    }

    public void CastleofBonesSaga()
    {
        // Enter the Castle of Bone
        Core.KillQuest(4968, "bonecastle", "Undead Guard");
        // Slay Vaden's Undead Guards
        Core.KillQuest(4969, "bonecastle", "Undead Guard");
        // Destroyer in the Foyer
        Core.KillQuest(4970, "bonecastle", "Undead Knight");
        // Help! I'm a Fallen Lord and I Can't Get Up!
        Core.KillQuest(4971, "bonecastle", "Fallen Deathknight");
        // Gem-nastics
        Core.KillQuest(4972, "bonecastle", "Fallen Deathknight");
        Core.MapItemQuest(4972, "bonecastle", 4342, 3);
        // Bone Appetit
        Core.KillQuest(4973, "bonecastle", "Undead Waiter");
        // The Walking Bread
        Core.MapItemQuest(4974, "bonecastle", 4343, 2);
        Core.MapItemQuest(4974, "bonecastle", 4344, 2);
        Core.MapItemQuest(4974, "bonecastle", 4345, 3);
        Core.KillQuest(4974, "bonecastle", "Undead Knight");
        // Ghoul-ash
        Core.KillQuest(4975, "bonecastle", "Ghoul");
        // The Butcher
        Core.KillQuest(4976, "bonecastle", "The Butcher");
        // Lean Mean Undead Slaying Machine
        Core.KillQuest(4977, "bonecastle", "Skeletal Warrior");
        // Moar Loot
        Core.MapItemQuest(4978, "bonecastle", 4346, 1);
        Core.MapItemQuest(4978, "bonecastle", 4347, 1);
        Core.MapItemQuest(4978, "bonecastle", 4348, 1);
        Core.KillQuest(4978, "bonecastle", "Skeletal Warrior");
        // Putting Your Hands All over Everything
        Core.MapItemQuest(4979, "bonecastle", 4349, 1);
        Core.MapItemQuest(4979, "bonecastle", 4350, 1);
        Core.MapItemQuest(4979, "bonecastle", 4351, 1);        
        Core.KillQuest(4979, "bonecastle", "Skeletal Warrior");
        // Paladin Rock
        Core.KillQuest(4980, "bonecastle", new[] { "Grateful Undead", "That 70's Zombie" });
        Core.MapItemQuest(4980, "bonecastle", 4354, 1);
        Core.MapItemQuest(4980, "bonecastle", 4355, 1);
        // Do You Find This Humerus?
        Core.KillQuest(4981, "bonecastle", "Skeletal Warrior|Undead Knight|Undead Guard");
        // Vaden Says
        Core.KillQuest(4982, "bonecastle", new[] {"Skeletal Warrior", "Undead Guard", "Undead Knight"});
        // The Dead King's Bedroom
        Core.MapItemQuest(4983, "bonecastle", 4352, 1);
        // Game of Porcelain Thrones
        Core.MapItemQuest(4984, "bonecastle", 4353, 4);
        // Teenage Mutant Sewer Turtles
        Core.KillQuest(4985, "bonecastle", "Turtle");
        // Adolescent Inhuman Samurai Reptiles
        Core.KillQuest(4986, "bonecastle", new[] {"Turtle", "Turtle", "Turtle", "Turtle"});
        // Snuggles!
        Core.KillQuest(4987, "bonecastle", "Snuggles, Torturer");
        // Game of Bones
        Core.KillQuest(4988, "bonecastle", new[] {"Jon Bones", "Oberon Marrowtell", "Baskerville", "Knight of Lichens"});
        // Rot Tin Tin!
        Core.KillQuest(4989, "bonecastle", "Rot Tin Tin");
        // Gold Digger
        Core.KillQuest(4990, "bonecastle", new[] {"Undead Golden Knight", "Undead Golden Knight", "Undead Golden Knight"});
        // Gotta Hand It To Ya
        Core.KillQuest(4991, "bonecastle", new[] {"Undead Knight", "Skeletal Warrior"});
        // Vaden's Defeat
        Core.KillQuest(4992, "bonecastle", "Vaden", hasFollowup: false);
    }
}
