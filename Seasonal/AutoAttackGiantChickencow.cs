//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using RBot;

public class AAGiantChickenCow
{
    public CoreBots Core => CoreBots.Instance;
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreAdvanced Adv = new CoreAdvanced();

    public readonly int[] SkillOrder = { 2, 4, 3, 1 };

    public void ScriptMain(ScriptInterface Bot)
    {
        Core.SetOptions();

        KFC();

        Core.SetOptions(false);
    }

    private void KFC()
    {
        Core.Join("battleontown", "r9", "Right");
        Bot.Player.SetSpawnPoint();
        while (!Bot.ShouldExit())
        {
            if (Bot.Map.Name == "battleontown" && Bot.Player.Cell == "r9" && Bot.Player.Alive)
                foreach (var Skill in SkillOrder)
                    if (Bot.Player.CanUseSkill(Skill))
                        Bot.Player.UseSkill(Skill);
        }
    }
}