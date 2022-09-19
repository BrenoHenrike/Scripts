//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/LivingDungeon.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
using Skua.Core.Interfaces;

public class LordOfOrder
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CitadelRuins CR = new();
    public LivingDungeon LD = new();
    public DragonFableOrigins DFO = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetLoO();

        Core.SetOptions(false);
    }

    public void GetLoO(bool rankUpClass = true)
    {
        if (Core.CheckInventory(50741, toInv: false))
            return;

        Core.Logger("Daily: Lord Of Order Class");
        if (Bot.Quests.IsDailyComplete(7156))
        {
            Core.Logger("Daily Quest unavailable right now");
            return;
        }

        Story.PreLoad(this);

        // Heart of Servitude
        if (!Story.QuestProgression(7156))
        {
            Core.EnsureAccept(7156);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("watchtower", "Chaorrupted Knight", "Pristine Blades of Order", isTemp: false);
            Core.BuyItem("dreadrock", 1221, "Dreadrock Donation Receipt");
            Core.HuntMonster("deadmoor", "Banshee Mallora", "Deadmoor Spirits Helped", isTemp: false);
            CR.MurrysQuests();
            CR.PolishsQuestsCitadelRuins();
            if (!Core.CheckInventory("Mage's Gratitude"))
            {
                Core.AddDrop("Mage's Gratitude");
                Core.EnsureAccept(6182);
                Core.HuntMonster("citadelruins", "Enn'tröpy", "Enn'tröpy Defeated", isTemp: true);
                Core.EnsureComplete(6182);
            }
            Core.BuyItem("ravenscar", 614, "Ravenscar's Truth");

            Core.EnsureComplete(7156);
            return;
        }

        // Spirit of Justice
        if (!Story.QuestProgression(7157))
        {
            Core.EnsureAccept(7157);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("dwarfprison", "Warden Elfis", "Warden Elfis Detained", isTemp: false);
            Core.HuntMonster("prison", "Piggy Drake", "Piggy Drake Punished", isTemp: false);
            Core.HuntMonster("mysteriousdungeon", "Mysterious Stranger", "Mysterious Stranger Foiled", isTemp: false);
            Core.HuntMonster("dreammaster", "Calico Cobby", "Calico Cobby Crushed", isTemp: false);

            Core.EnsureComplete(7157);
            return;
        }

        // Purification of Chaos
        if (!Story.QuestProgression(7158))
        {
            Core.EnsureAccept(7158);

            Core.EquipClass(ClassType.Solo);

            if (!Core.CheckInventory("Chaoroot", 15))
            {
                Farm.Gold(600000);
                Core.BuyItem("tercessuinotlim", 1951, "Receipt of Swindle", 2);
                Core.BuyItem("tercessuinotlim", 1951, "Chaoroot", 15, 10);
            }
            Core.HuntMonster("chaosboss", "Ultra Chaos Warlord", "Chaotic War Essence", 15, false);
            Core.HuntMonster("shadowgates", "Chaorruption", "Chaorrupting Particles", 15, false);
            Core.HuntMonster("stormtemple", "Chaos Lord Lionfang", "Purified Raindrop", 45, false, publicRoom: true);

            Core.EnsureComplete(7158);
            return;
        }

        // Steadfast Will
        if (!Story.QuestProgression(7159))
        {
            Core.EnsureAccept(7159);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("gaiazor", "Gaiazor", "Gaiazor's Cornerstone", isTemp: false, publicRoom: true);
            LD.LivingDungeonStory();
            Core.HuntMonster("treetitanbattle", "Dakka the Dire Dragon", "Dakka's Crystal", isTemp: false);
            Core.HuntMonster("andre", "Giant Necklace", "Andre's Necklace Fragment", isTemp: false);
            Core.HuntMonster("desolich", "Desolich", "Desolich's Skull", isTemp: false, publicRoom: true);

            Core.EnsureComplete(7159);
            return;
        }

        // Strike of Order
        if (!Story.QuestProgression(7160))
        {
            Core.EnsureAccept(7160);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("kitsune", "Kitsune", "Hanzamune Dragon Koi Blade", isTemp: false);
            Core.HuntMonster("ledgermayne", "Ledgermayne", "The Supreme Arcane Staff", isTemp: false);
            Core.HuntMonster("mqlesson", "Dragonoid", "Dragonoid of Hours", isTemp: false);
            if (!Core.CheckInventory("Safiria's Spirit Orb"))
            {
                Core.AddDrop("Safiria's Spirit Orb");
                Core.GetMapItem(5470, 1, "maxius");
                Bot.Wait.ForPickup("Safiria's Spirit Orb");
            }
            DFO.DragonFableOriginsAll();
            if (!Core.CheckInventory("Ice Katana"))
            {
                Core.AddDrop("Ice Katana");
                Core.EnsureAccept(6319);
                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("drakonnan", "Living Fire", "Inferno Heart");
                Core.EnsureComplete(6319);
            }

            Core.EnsureComplete(7160);
            return;
        }

        // Harmony
        if (!Story.QuestProgression(7161))
        {
            Core.EnsureAccept(7161);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("elemental", "Tree of Destiny", "Unity of Life", isTemp: false);
            Core.HuntMonster("orchestra", "Faust", "Harmony of Solace", isTemp: false);
            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("cathedral", "Pactagonal Knight", "Teamwork Observed", 100, false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("goose", "Queen's ArchSage", "Scroll of Enchantment", isTemp: false);

            Core.EnsureComplete(7161);
            return;
        }

        // Ordinance
        if (!Story.QuestProgression(7162))
        {
            Core.EnsureAccept(7162);

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("newfinale", "Chaos Healer", "Acolyte's Braille", isTemp: false);
            Core.HuntMonster("wardwarf", "Drow Assassin", "Suppressed Drows", 50, false);
            Core.HuntMonster("warundead", "Skeletal Fire Mage", "Suppressed Undead", 50, false);
            Core.HuntMonster("warhorc", "Horc Warrior", "Suppressed Horcs", 50, false);
            Core.HuntMonster("weaverwar", "Weaver Queen's Hound", "Suppressed Weavers", 50, false);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("thevoid", "Xyfrag", "Strength of Resilience", isTemp: false);

            Core.EnsureComplete(7162);
            return;
        }

        // Axiom
        if (!Story.QuestProgression(7163))
        {
            Core.EnsureAccept(7163);

            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("elfhame", "Guardian Spirit", "Law of Nature", isTemp: false);
            Core.HuntMonster("deepchaos", "Kathool", "Law of Time", isTemp: false);
            Core.HuntMonster("necrocavern", "Shadowstone Support", "Law of Gravity", isTemp: false);
            Core.HuntMonster("blackholesun", "Reflecteract", "Law of Relativity", isTemp: false);
            Core.HuntMonster("thunderfang", "Tonitru", "Law of Conservation of Energy", isTemp: false);
            Core.HuntMonster("lair", "Red Dragon", "Law of Low Drop Rates", 100, false);

            Core.EnsureComplete(7163);
            return;
        }

        // Blessing of Order
        if (!Story.QuestProgression(7164))
        {
            Core.EnsureAccept(7164);

            Bot.Quests.UpdateQuest(3008);
            Core.SetAchievement(18);
            Bot.Quests.UpdateQuest(3004);

            Core.EquipClass(ClassType.Solo);
            Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Weapon Imprint", 15, false);
            Farm.FishingREP(7);
            Core.BuyItem("greenguardwest", 363, "Lure of Order");
            Adv.GearStore();
            Core.KillXiang("Quixotic Mana Essence", 10, true, false, true, true);
            Adv.GearStore(true);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("yasaris", "Serepthys", "Inversion Infusion", 5, false);

            Core.EnsureComplete(7164);
            return;
        }

        // The Final Challenge
        Core.AddDrop("Lord Of Order");
        Core.EnsureAccept(7165);
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("ultradrakath", "Champion of Chaos", "Champion of Chaos Confronted", isTemp: false, publicRoom: true);
        Core.EnsureComplete(7165);

        Bot.Wait.ForPickup("Lord Of Order");

        if (rankUpClass)
            Adv.rankUpClass("Lord Of Order");
    }
}
