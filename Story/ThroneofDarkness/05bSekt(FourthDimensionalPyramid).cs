//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class FourthDimensionalPyramid
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        FourthDimensionalPyramidSaga();

        Core.SetOptions(false);
    }

    public void FourthDimensionalPyramidSaga()
    {
        if (Story.isCompletedBefore(5212))
            return;

        // Eye for an Eye of the Old Gods
        Story.KillQuest(5189, "fourdpyramid", "Sekt");
        // Hounded by History
        Story.KillQuest(5190, "fourdpyramid", "Negastri Hound");
        // Stand and De-Lever
        Story.MapItemQuest(5191, "fourdpyramid", 4556, 1);
        // Yo Mummy
        Story.KillQuest(5192, "fourdpyramid", "Sekt's Mummy");
        Story.MapItemQuest(5192, "fourdpyramid", 4557, 1);
        // Find the Secret Brick
        Story.MapItemQuest(5193, "fourdpyramid", 4558, 1);
        // Gauze in 60 Seconds
        Story.KillQuest(5194, "fourdpyramid", "Nega Mummy");
        // De-Lever-ence
        Story.MapItemQuest(5195, "fourdpyramid", 4559, 1);
        // A Jarring Solution
        Story.KillQuest(5196, "fourdpyramid", new[] { "Nega Mummy", "Guardian of Anubyx" });
        Story.MapItemQuest(5196, "fourdpyramid", 4560, 1);
        // Ra of Light
        Story.MapItemQuest(5197, "fourdpyramid", 4561, 1);
        // Sphynxes are Riddled With Gems
        Story.KillQuest(5198, "fourdpyramid", new[] { "Stone Sphynx", "Stone Sphynx" });
        // 4th Dimensional Teleport
        Story.MapItemQuest(5199, "fourdpyramid", 4562, 4);
        Story.MapItemQuest(5199, "fourdpyramid", 4564, 1);
        // Mummies vs Daddies
        Story.KillQuest(5200, "fourdpyramid", "Nega Mummy");
        // The Tesseract
        Story.KillQuest(5201, "fourdpyramid", "Guardian of Anubyx|Negastri Hound");
        // Another Mystery To Solve
        Story.MapItemQuest(5202, "fourdpyramid", 4565, 1);
        // Whose Skeleton Is This?
        Story.MapItemQuest(5203, "fourdpyramid", 4566, 1);
        // Fourth Scrap of the Propechy
        Story.MapItemQuest(5204, "fourdpyramid", 4567, 1);
        // Fighting in 4D
        Story.KillQuest(5205, "fourdpyramid", "Tesseract Sprite");
        Story.MapItemQuest(5205, "fourdpyramid", 4568, 1);
        // Lever-age    
        Story.MapItemQuest(5206, "fourdpyramid", 4569, 1);
        // 4D Goblins?
        Story.KillQuest(5207, "fourdpyramid", "Tesseract Goblin");
        Story.MapItemQuest(5207, "fourdpyramid", 4570, 1);
        // Stone Sphynx Gems
        Story.KillQuest(5208, "fourdpyramid", new[] { "Stone Sphynx", "Stone Sphynx" });
        // Beam Me Up Scotty
        Story.MapItemQuest(5209, "fourdpyramid", 4571, 4);
        Story.MapItemQuest(5209, "fourdpyramid", 4572, 1);
        // Sekt... Again
        Story.MapItemQuest(5210, "fourdpyramid", 4573, 1);
        // The Black Plague
        Story.KillQuest(5211, "fourdpyramid", "Black Plague");
        // The Hero's Doom
        Story.MapItemQuest(5212, "fourdpyramid", 4574, 1);
    }
}
