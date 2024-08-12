/*
name: crulonwedding
description: does the 3 story quest for /crulonwed
tags: crulonwed, weekly, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;

public class crulonwedding
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    private CoreAdvanced Adv = new();
    private CoreFarms Farm = new();
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "item1", "Item2", "Etc" });
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example(bool TestMode = false)
    {

        if (Core.isCompletedBefore(9850))
            return;

        Story.PreLoad(this);

        #region Useable Monsters
        string[] UseableMonsters = new[]
        {
            "King Almoravid", // UseableMonsters[0]
            "Jaan al Nair",
            "Silver Elemental",
        };
        #endregion Useable Monsters

        // 9848 | The Red Rival
        if (!Story.QuestProgression(9848))
        {
            Core.HuntMonsterQuest(9848, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("djinnguard", UseableMonsters[1], ClassType.Solo) });
        }


        // 9849 | Pale Invocation
        if (!Story.QuestProgression(9849))
        {
            Core.HuntMonsterQuest(9849, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("towerofmirrors", UseableMonsters[2], ClassType.Farm) });
        }


        // 9850 | Moon's Self-Reflection
        if (!Story.QuestProgression(9850))
        {
            Core.HuntMonsterQuest(9850, new (string? mapName, string? monsterName, ClassType classType)[] {
        ("crulonwed", UseableMonsters[0], ClassType.Solo) });
        }


    }
}



