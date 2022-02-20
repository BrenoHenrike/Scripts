//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class MysteriousEgg
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetMysteriousEgg();

        Core.SetOptions(false);
    }

    public void GetMysteriousEgg()
    {
        Core.AddDrop("Mysterious Egg", "Key of Pride", "Key of Gluttony", "Key of Greed", "Key of Sloth",
        "Key of Lust", "Key of Envy", "Key of Wrath");
        if (Core.CheckInventory("Mysterious Egg"))
            return;
        Core.EnsureAccept(6171);
        Core.HuntMonster("pride", "Valsarian", "Key of Pride", isTemp: false);
        Core.KillMonster("gluttony", "Enter2", "Left", "*", "Key of Gluttony", isTemp: false);
        Core.HuntMonster("greed", "Goregold", "Key of Greed", isTemp: false);
        if (!Core.CheckInventory("Key of Sloth"))
        {
            Core.EnsureAccept(5944);
            Core.GetMapItem(5380, map: "sloth");
            Core.GetMapItem(5381, map: "sloth");
            Core.EnsureComplete(5944);
        }
        Core.HuntMonster("sloth", "Phlegnn", "Key of Sloth", isTemp: false);
        Core.HuntMonster("lust", "Lascivia", "Key of Lust", isTemp: false);
        Maloth();
        Core.HuntMonster("wrath", "Gorgorath", "Key of Wrath", isTemp: false);
        Core.EnsureCompleteChoose(6171, new[] { "Mysterious Egg" });
    }

    public void Maloth()
    {
        if (Core.CheckInventory("Key of Envy"))
            return;

        if (!Bot.Quests.IsUnlocked(6000))
        {
            if (!Bot.Quests.IsUnlocked(5990))
            {
                if (!Bot.Quests.IsUnlocked(5989))
                {
                    if (!Bot.Quests.IsUnlocked(5984))
                        Story.MapItemQuest(5983, "dragoncrown", 5420, GetReward: false);
                    if (!Bot.Quests.IsUnlocked(5985))
                        Story.MapItemQuest(5984, "dragoncrown", 5421, GetReward: false);
                    if (!Bot.Quests.IsUnlocked(5986))
                    {
                        Core.EnsureAccept(5985);
                        Core.KillMonster("dragoncrown", "r6", "Left", "Fire Sprite", "Fire Sprites Slain", 6);
                        Core.GetMapItem(5422, 6, "dragoncrown");
                        Core.EnsureComplete(5985);
                    }
                    if (!Bot.Quests.IsUnlocked(5987))
                    {
                        Core.EnsureAccept(5986);
                        Core.KillMonster("dragoncrown", "r6", "Left", "Llama", "Llamas Rustled", 5);
                        Core.KillMonster("dragoncrown", "r6", "Left", "Rampaging Boar", "Boars Rustled", 5);
                        Core.EnsureComplete(5986);
                    }
                    if (!Bot.Quests.IsUnlocked(5988))
                    {
                        Core.EnsureAccept(5987);
                        Core.KillMonster("dragoncrown", "r3", "Left", "Rock Elemental", "Strong Rocks", 10);
                        Core.KillMonster("dragoncrown", "r3", "Left", "Earth Elemental", "Sticky Mud", 5);
                        Core.EnsureComplete(5987);
                    }
                    Story.MapItemQuest(5988, "dragoncrown", 5423, GetReward: false);
                }
                Story.MapItemQuest(5989, "dragoncrown", 5424, GetReward: false);
            }
            if (!Bot.Quests.IsUnlocked(5991))
            {
                Core.EnsureAccept(5990);
                Core.KillMonster("dragoncrown", "r11", "Top", "*", "Torgat Defeated");
                Core.EnsureComplete(5990);
            }
            if (!Bot.Quests.IsUnlocked(5992))
            {
                Core.EnsureAccept(5991);
                Core.KillMonster("dragoncrown", "r11a", "Top", "*", "Fressa Defeated");
                Core.EnsureComplete(5991);
            }
            if (!Bot.Quests.IsUnlocked(5993))
            {
                Core.EnsureAccept(5992);
                Core.KillMonster("dragoncrown", "r11b", "Top", "*", "Radroth Defeated");
                Core.EnsureComplete(5992);
            }
            if (!Bot.Quests.IsUnlocked(5994))
            {
                Core.EnsureAccept(5993);
                Core.KillMonster("dragoncrown", "r11c", "Top", "*", "Nizex Defeated");
                Core.EnsureComplete(5993);
            }
            if (!Bot.Quests.IsUnlocked(5995))
            {
                Core.EnsureAccept(5994);
                Core.KillMonster("dragoncrown", "r11d", "Top", "*", "Tathu Defeated");
                Core.EnsureComplete(5994);
            }
            if (!Bot.Quests.IsUnlocked(5996))
            {
                Core.EnsureAccept(5995);
                Core.KillMonster("dragoncrown", "r11e", "Top", "*", "Lanshen Defeated");
                Core.EnsureComplete(5995);
            }
            if (!Bot.Quests.IsUnlocked(5997))
            {
                Core.EnsureAccept(5996);
                Core.KillMonster("dragoncrown", "r11f", "Top", "*", "Ashax Defeated");
                Core.EnsureComplete(5996);
            }
            if (!Bot.Quests.IsUnlocked(5998))
            {
                Core.EnsureAccept(5997);
                Core.KillMonster("dragoncrown", "r11g", "Top", "*", "Letori Defeated");
                Core.EnsureComplete(5997);
            }
            if (!Bot.Quests.IsUnlocked(5999))
            {
                Core.EnsureAccept(5998);
                Core.KillMonster("dragoncrown", "r11h", "Top", "*", "Nayzol Defeated");
                Core.EnsureComplete(5998);
            }
            Core.EnsureAccept(5999);
            Core.KillMonster("dragoncrown", "r11i", "Top", "*", "Zathas Defeated");
            Core.EnsureComplete(5999);
        }
        if (!Bot.Quests.IsUnlocked(6001))
        {
            Core.EnsureAccept(6000);
            Core.HuntMonster("dragoncrown", "Argo", "Argo Defeated");
            Core.EnsureComplete(6000);
        }
        Core.HuntMonster("maloth", "Maloth", "Key of Envy", isTemp: false);
    }
}