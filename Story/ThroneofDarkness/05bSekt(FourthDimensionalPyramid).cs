//cs_include Scripts/CoreBots.cs
using RBot;
public class FourthDimensionalPyramid
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        FourthDimensionalPyramidSaga();

        Core.SetOptions(false);
    }

    public void FourthDimensionalPyramidSaga()
    {
        if (Core.isCompletedBefore(5212))
            return;

        // Eye for an Eye of the Old Gods
        Core.KillQuest(5189, "fourdpyramid", "Sekt");
        // Hounded by History
        Core.KillQuest(5190, "fourdpyramid", "Negastri Hound");
        // Stand and De-Lever
        Core.MapItemQuest(5191, "fourdpyramid", 4556, 1);
        // Yo Mummy
        Core.KillQuest(5192, "fourdpyramid", "Sekt's Mummy");
        Core.MapItemQuest(5192, "fourdpyramid", 4557, 1);
        // Find the Secret Brick
        Core.MapItemQuest(5193, "fourdpyramid", 4558, 1);
        // Gauze in 60 Seconds
        Core.KillQuest(5194, "fourdpyramid", "Nega Mummy");
        // De-Lever-ence
        Core.MapItemQuest(5195, "fourdpyramid", 4559, 1);
        // A Jarring Solution
        Core.KillQuest(5196, "fourdpyramid", new[] { "Nega Mummy", "Guardian of Anubyx" });
        Core.MapItemQuest(5196, "fourdpyramid", 4560, 1);
        // Ra of Light
        Core.MapItemQuest(5197, "fourdpyramid", 4561, 1);
        // Sphynxes are Riddled With Gems
        Core.KillQuest(5198, "fourdpyramid", new[] { "Stone Sphynx", "Stone Sphynx" });
        // 4th Dimensional Teleport
        Core.MapItemQuest(5199, "fourdpyramid", 4562, 4);
        Core.MapItemQuest(5199, "fourdpyramid", 4564, 1);
        // Mummies vs Daddies
        Core.KillQuest(5200, "fourdpyramid", "Nega Mummy");
        // The Tesseract
        Core.KillQuest(5201, "fourdpyramid", "Guardian of Anubyx|Negastri Hound");
        // Another Mystery To Solve
        Core.MapItemQuest(5202, "fourdpyramid", 4565, 1);
        // Whose Skeleton Is This?
        Core.MapItemQuest(5203, "fourdpyramid", 4566, 1);
        // Fourth Scrap of the Propechy
        Core.MapItemQuest(5204, "fourdpyramid", 4567, 1);
        // Fighting in 4D
        Core.KillQuest(5205, "fourdpyramid", "Tesseract Sprite");
        Core.MapItemQuest(5205, "fourdpyramid", 4568, 1);
        // Lever-age    
        Core.MapItemQuest(5206, "fourdpyramid", 4569, 1);
        // 4D Goblins?
        Core.KillQuest(5207, "fourdpyramid", "Tesseract Goblin");
        Core.MapItemQuest(5207, "fourdpyramid", 4570, 1);
        // Stone Sphynx Gems
        Core.KillQuest(5208, "fourdpyramid", new[] { "Stone Sphynx", "Stone Sphynx" });
        // Beam Me Up Scotty
        Core.MapItemQuest(5209, "fourdpyramid", 4571, 4);
        Core.MapItemQuest(5209, "fourdpyramid", 4572, 1);
        // Sekt... Again
        Core.MapItemQuest(5210, "fourdpyramid", 4573, 1);
        // The Black Plague
        Core.KillQuest(5211, "fourdpyramid", "Black Plague");
        // The Hero's Doom
        Core.MapItemQuest(5212, "fourdpyramid", 4574, 1);
    }
}
