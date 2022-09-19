//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ThirdSpell
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    string[] RequiredItems = { "Brainz n' Eggs", "Mana Token I", "Mana Token II", "Mana Token III", "Mana Token IV", "Mana Token V", "Mana Token VI", "Mana Token VI", "Mana Token VII", "Mana Token VIII", "Mana Token IX", "Mana Token X", "Mana Token XI", "Sun Token I", "Sun Token II", "Sun Token III", "Sun Token IV", "Sun Token V", "Sun Token VI", "Sun Token VII", "Sun Token VIII", "Heart of the Sun" };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Core.BankingBlackList.AddRange(RequiredItems);
        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine(bool HoTS = false)
    {
        if (!HoTS)
        {
            if (Core.CheckInventory("Sun Token VIII", toInv: false))
                return;
        }
        else if (Core.CheckInventory("Heart of the Sun"))
            return;


        Story.PreLoad(this);

        Core.AddDrop(RequiredItems);

        // 4493|See the Hero Run        
        if (!Core.CheckInventory("Sun Token VIII") || HoTS)
        {
            // 4492|Selfishness
            if (!Core.CheckInventory("Sun Token VII") || HoTS)
            {
                // 4491|Mother Knows The Sun
                if (!Core.CheckInventory("Sun Token VI") || HoTS)
                {
                    if (HoTS && Core.CheckInventory("Heart of the Sun"))
                        return;
                    // 4490|Assault With a Deadly Shadow
                    if (!Core.CheckInventory("Sun Token V"))
                    {
                        // 4489|Burning Like Me!
                        if (!Core.CheckInventory("Sun Token IV"))
                        {
                            // 4488|I Enjoy Being a Soul
                            if (!Core.CheckInventory("Sun Token III"))
                            {
                                // 4487|The Sunspots, They Are Changing!
                                if (!Core.CheckInventory("Sun Token II"))
                                {
                                    // 4486|Angry Elements
                                    if (!Core.CheckInventory("Sun Token I"))
                                    {
                                        // 4485|Frozen Mana
                                        if (!Core.CheckInventory("Mana Token XI"))
                                        {
                                            // 4484|The Art of Persuasion
                                            if (!Core.CheckInventory("Mana Token X"))
                                            {
                                                // 4483|Truth or De-Feathering
                                                if (!Core.CheckInventory("Mana Token IX"))
                                                {
                                                    // 4482|Abduction
                                                    if (!Core.CheckInventory("Mana Token VIII"))
                                                    {
                                                        // 4481|Body & Soul & The Hero
                                                        if (!Core.CheckInventory("Mana Token VII"))
                                                        {
                                                            // 4480|A Lonely Cysero
                                                            if (!Core.CheckInventory("Mana Token VI"))
                                                            {
                                                                // 4479|Green-Eyed Incantations
                                                                if (!Core.CheckInventory("Mana Token V"))
                                                                {
                                                                    // 4478|Sssssmoking...
                                                                    if (!Core.CheckInventory("Mana Token IV"))
                                                                    {
                                                                        // 4477|Elementals Are From the Sun, We Are From Lore
                                                                        if (!Core.CheckInventory("Mana Token III"))
                                                                        {
                                                                            // 4476|Phoenix’s First Birthday
                                                                            if (!Core.CheckInventory("Mana Token II"))
                                                                            {
                                                                                // 4475|Post-Elemental Apocalypse
                                                                                if (!Core.CheckInventory("Mana Token I"))
                                                                                {
                                                                                    // 4474|Breakfast With... Brains?
                                                                                    if (!Core.CheckInventory("Brainz n' Eggs"))
                                                                                    {
                                                                                        Core.EnsureAccept(4474);
                                                                                        Core.HuntMonster("doomwood", "Doomwood Treeant", "Braaaainz", 10);
                                                                                        Core.EnsureComplete(4474);
                                                                                    }
                                                                                    Core.EnsureAccept(4475);
                                                                                    Core.HuntMonster("thirdspell", "Mana Phoenix", "Mana Plane Monster Defeated", 12);
                                                                                    Core.EnsureComplete(4475);
                                                                                }
                                                                                Core.EnsureAccept(4476);
                                                                                Core.HuntMonster("thirdspell", "Mana Phoenix", "Proxy Eggs", 8);
                                                                                Core.EnsureComplete(4476);
                                                                            }
                                                                            Core.EnsureAccept(4477);
                                                                            Core.GetMapItem(3668, 10, "thirdspell");
                                                                            Core.EnsureComplete(4477);
                                                                        }
                                                                        Core.EnsureAccept(4478);
                                                                        Core.HuntMonster("thirdspell", "Mana Phoenix", "Elemental Pathway Cleared", 20);
                                                                        Core.EnsureComplete(4478);
                                                                    }
                                                                    Core.EnsureAccept(4479);
                                                                    Core.HuntMonster("thirdspell", "Mana Phoenix", "Green Eye", 5);
                                                                    Core.EnsureComplete(4479);
                                                                }
                                                                Core.EnsureAccept(4480);
                                                                Core.GetMapItem(3671, map: "thirdspell");
                                                                Core.EnsureComplete(4480);
                                                            }
                                                            Core.EnsureAccept(4481);
                                                            Core.GetMapItem(3675, map: "thirdspell");
                                                            Core.EnsureComplete(4481);
                                                        }
                                                        Core.EnsureAccept(4482);
                                                        Core.HuntMonster("thirdspell", "Mana Phoenix", "Abducted Mana Phoenix");
                                                        Core.EnsureComplete(4482);
                                                    }
                                                    Core.EnsureAccept(4483);
                                                    Core.GetMapItem(3672, map: "thirdspell");
                                                    Core.EnsureComplete(4483);
                                                }
                                                Core.EnsureAccept(4484);
                                                Core.HuntMonster("thirdspell", "Great Solar Elemental", "Great Solar Elemental Defeated");
                                                Core.EnsureComplete(4484);
                                            }
                                            Core.EnsureAccept(4485);
                                            Core.HuntMonster("thirdspell", "Mana Phoenix", "Mana Phoenix Egg", 7);
                                            Core.EnsureComplete(4485);
                                        }
                                        Core.EnsureAccept(4486);
                                        Core.HuntMonster("thirdspell", "Great Solar Elemental", "Great Solar Elemental Defeated Again");
                                        Core.EnsureComplete(4486);
                                    }
                                    Core.EnsureAccept(4487);
                                    Core.GetMapItem(3673, 3, "thirdspell");
                                    Core.EnsureComplete(4487);
                                }
                                Core.EnsureAccept(4488);
                                Core.HuntMonster("thirdspell", "Living Fire", "Sun Monster Ember", 15);
                                Core.EnsureComplete(4488);
                            }
                            Core.EnsureAccept(4489);
                            Core.HuntMonster("thirdspell", "Sun Flare", "Sun Flare Defeated", 10);
                            Core.HuntMonster("thirdspell", "Living Fire", "Living Fire Defeated", 5);
                            Core.EnsureComplete(4489);
                        }
                        Core.EnsureAccept(4490);
                        Core.GetMapItem(3674, map: "thirdspell");
                        Core.EnsureComplete(4490);
                    }
                    Core.EnsureAccept(4491);
                    Core.HuntMonster("thirdspell", "Solar Incarnation", "Heart of the Sun Received");
                    Core.EnsureComplete(4491);
                    if (HoTS)
                    {
                        Core.ToBank("Sun Token VI");
                        return;
                    }
                    Core.EnsureAccept(4492);
                    Core.GetMapItem(3676, map: "thirdspell");
                    Core.EnsureComplete(4492);
                }
                Core.EnsureAccept(4493);
                Core.GetMapItem(3677, map: "thirdspell");
                if (!Core.CheckInventory("Heart of the Sun"))
                {
                    Core.EnsureAccept(4491);
                    Core.HuntMonster("thirdspell", "Solar Incarnation", "Heart of the Sun Received");
                    Core.EnsureComplete(4491);
                }
                Core.EnsureComplete(4493);
            }
            Core.ToBank("Sun Token VIII", "Heart of the Sun");
            Core.Logger("All Quests Complete");
        }
    }
}