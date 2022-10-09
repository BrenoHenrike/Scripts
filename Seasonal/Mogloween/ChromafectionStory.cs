//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ChromafectionStory {
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot) {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline() {
        if (Core.isCompletedBefore(6537))
            return;

        Story.PreLoad(this);

        //Wacky Winka’s Candy Shop 6531
        Story.MapItemQuest(6531, "chromafection", 6004, 1);

        //Nougat-ta Save the Parents 6532
        Story.KillQuest(6532, "chromafection", "Colorless Drone");

        //Chroma Kids 6533
        Story.MapItemQuest(6533, "chromafection", 6006, 10);

        //Caramel Clues 6534
        Story.KillQuest(6534, "chromafection", "Free Samples");

        //Sweet, Sweet Proof 6535
        Story.MapItemQuest(6535, "chromafection", 6007, 15);

        //Whacky Snacky 6536
        Story.MapItemQuest(6536, "chromafection", 6537, 1);

        //Chroma CRASH 6537
        Story.KillQuest(6537, "chromafection", "Chromafection");
    }
}
