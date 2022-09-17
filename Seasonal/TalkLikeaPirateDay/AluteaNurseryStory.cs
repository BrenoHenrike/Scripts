//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/LowTideStory.cs

using Skua.Core.Interfaces;

public class AluteaNurseryStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public LowTideStory LowTideStory = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        LowTideStory.Storyline();

        Story.PreLoad();

        //Carefree Drifters 8848
        Story.KillQuest(8848, "AluteaNursery", "Spectral Jellyfish");

        //Bone Bites 8849
        Story.KillQuest(8849, "AluteaNursery", "Bone Crustacean");

        //The Lost Generation 8850
        Story.MapItemQuest(8850, "AluteaNursery", 10557, 5);
        Story.KillQuest(8850, "AluteaNursery", "Bone Crustacean");

        //Tender Temple 8851
        Story.MapItemQuest(8851, "AluteaNursery", 10558);
        Story.KillQuest(8851, "AluteaNursery", "Spectral Jellyfish");

        //Cast Away Buds 8852
        Story.MapItemQuest(8852, "AluteaNursery", 10559, 2);
        Story.KillQuest(8852, "AluteaNursery", "Spectral Jellyfish");

        //Current Catching 8853
        Story.MapItemQuest(8853, "AluteaNursery", 10563, 5);

        //Silent Sea 8854
        Story.MapItemQuest(8854, "AluteaNursery", new[] { 10560, 10561 });
        Story.KillQuest(8854, "AluteaNursery", "Stagnant Water");

        //Hay! Bale Already! 8855
        Story.MapItemQuest(8855, "AluteaNursery", 10562, 2);
        Story.KillQuest(8855, "AluteaNursery", new[] { "Spectral Jellyfish", "Bone Crustacean" });

        //Abyss to Sun 8856
        Story.MapItemQuest(8856, "AluteaNursery", 10564);
        Story.KillQuest(8856, "AluteaNursery", "Stagnant Water");

        //Ebbing Grudges 8857
        Story.KillQuest(8857, "AluteaNursery", "Last Alutian");
    }
}