//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ThirdSpell
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] {
                                "Brainz n' Eggs",
                                "Mana Token I", "Mana Token II", "Mana Token III", "Mana Token IV", "Mana Token V", "Mana Token VI", "Mana Token VI",
                                "Mana Token VII", "Mana Token VIII", "Mana Token IX", "Mana Token X", "Mana Token XI",
                                "Sun Token I", "Sun Token II", "Sun Token III", "Sun Token IV", "Sun Token V", "Sun Token VI", "Sun Token VII", "Sun Token VIII",
                                "Heart of the Sun"});
        Core.SetOptions();
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

        Story.LegacyQuestManager(QuestLogic, 4474, 4475, 4476, 4477, 4478, 4479, 4480, 4481, 4482, 4483, 4484, 4485, 4486, 4487, 4488, 4489, 4490, 4491, 4492, 4493);

        void QuestLogic()
        {
            switch (Story.LegacyQuestID)
            {
                case 4474: //Breakfast With... Brains?
                    Core.HuntMonster("doomwood", "Doomwood Treeant", "Braaaainz", 10);
                    break;

                case 4475: //Post-Elemental Apocalypse
                    Core.HuntMonster("thirdspell", "Mana Phoenix", "Mana Plane Monster Defeated", 12);
                    break;

                case 4476: //Phoenix’s First Birthday
                    Core.HuntMonster("thirdspell", "Mana Phoenix", "Proxy Eggs", 8);
                    break;

                case 4477: //Elementals Are From the Sun, We Are From Lore
                    Core.GetMapItem(3668, 10, "thirdspell");
                    break;

                case 4478: //Sssssmoking...
                    Core.HuntMonster("thirdspell", "Mana Phoenix", "Elemental Pathway Cleared", 20);
                    break;

                case 4479: //Green-Eyed Incantations
                    Core.HuntMonster("thirdspell", "Mana Phoenix", "Green Eye", 5);
                    break;

                case 4480: //A Lonely Cysero
                    Core.GetMapItem(3671, map: "thirdspell");
                    break;

                case 4481: //Body & Soul & The Hero
                    Core.GetMapItem(3675, map: "thirdspell");
                    break;

                case 4482: //Abduction
                    Core.HuntMonster("thirdspell", "Mana Phoenix", "Abducted Mana Phoenix");
                    break;

                case 4483: //Truth or De-Feathering
                    Core.GetMapItem(3672, map: "thirdspell");
                    break;

                case 4484: //The Art of Persuasion
                    Core.HuntMonster("thirdspell", "Great Solar Elemental", "Great Solar Elemental Defeated");
                    break;

                case 4485: //Frozen Mana
                    Core.HuntMonster("thirdspell", "Mana Phoenix", "Mana Phoenix Egg", 7);
                    break;

                case 4486: //Angry Elements
                    Core.HuntMonster("thirdspell", "Great Solar Elemental", "Great Solar Elemental Defeated Again");
                    break;

                case 4487: //The Sunspots, They Are Changing!
                    Core.GetMapItem(3673, 3, "thirdspell");
                    break;

                case 4488: //I Enjoy Being a Soul
                    Core.HuntMonster("thirdspell", "Living Fire", "Sun Monster Ember", 15);
                    break;

                case 4489: //Burning Like Me!
                    Core.HuntMonster("thirdspell", "Sun Flare", "Sun Flare Defeated", 10);
                    Core.HuntMonster("thirdspell", "Living Fire", "Living Fire Defeated", 5);
                    break;

                case 4490: //Assault With a Deadly Shadow
                    Core.GetMapItem(3674, map: "thirdspell");
                    break;

                case 4491: // 4491|Mother Knows The Sun
                    Core.HuntMonster("thirdspell", "Solar Incarnation", "Heart of the Sun Received");
                    if (HoTS)
                    {
                        Core.ToBank("Sun Token VI");
                        Story.LegacyQuestStop();
                    }
                    break;

                case 4492: // 4492|Selfishness
                    Core.GetMapItem(3676, map: "thirdspell");
                    break;

                case 4493: // 4493|See the Hero Run        
                    Core.GetMapItem(3677, map: "thirdspell");
                    if (!Core.CheckInventory("Heart of the Sun"))
                    {
                        Core.EnsureAccept(4491);
                        Core.HuntMonster("thirdspell", "Solar Incarnation", "Heart of the Sun Received");
                        Core.EnsureComplete(4491);
                    }
                    break;
            }
        }
    }
}