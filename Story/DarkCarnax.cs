//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Skills;

public class DarkCarnaxStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(8872))
            return;

        Story.PreLoad(this);

        //8870 The Beast Awakens
        Story.KillQuest(8870, "aqlesson", "Carnax");

        //8871 Nightmare Containment
        if (!Story.QuestProgression(8871))
        {
            Core.EnsureAccept(8871);
            Core.HuntMonster("doomvault", "Binky", "Binky's Uni-horn", isTemp: false);
            Core.HuntMonster("deadmoor", "Nightmare", "Nightmare Mane", isTemp: false);
            Core.HuntMonster("somnia", "NightWyrm", "NightWyrm Chitin", isTemp: false);
            Core.HuntMonster("fearhouse", "ALL FEARS", "Sheer Horror", isTemp: false);
            Core.EnsureComplete(8871);
        }

        //8872 The Last Stand
        if (!Story.QuestProgression(8872))
        {
            Core.Logger($"Doing Quest: [8872] - \"The Last Stand\"");
            SyntheticViscera(1);
            Core.Logger($"Completed Quest: [8872] - \"The Last Stand\"");
        }
        else Core.Logger($"Already Completed: [8872] - \"The Last Stand\"");
    }

    public void SyntheticViscera(int quant = 1000)
    {
        if (Core.CheckInventory("Synthetic Viscera", quant))
            return;

        Core.AddDrop("Synthetic Viscera");
        Core.FarmingLogger("Synthetic Viscera", quant);

        Bot.Options.AttackWithoutTarget = true;
        Bot.Events.RunToArea += DarkCarnaxMove;

        if (Core.CheckInventory("Dragon of Time"))
            Bot.Skills.StartAdvanced("Dragon of Time", true, ClassUseMode.Base);
        else if (Core.CheckInventory("Healer (Rare)"))
            Bot.Skills.StartAdvanced("Healer (Rare)", true, ClassUseMode.Base);
        else if (Core.CheckInventory("Healer"))
            Bot.Skills.StartAdvanced("Healer", true, ClassUseMode.Base);
        else Core.EquipClass(ClassType.Solo);

        Adv.GearStore();
        Adv.EnhanceEquipped(EnhancementType.Healer, wSpecial: WeaponSpecial.Health_Vamp);

        Core.Join("darkcarnax", "Boss", "Bottom", publicRoom: true);

        Core.RegisterQuests(8872);
        while (!Bot.ShouldExit && !Core.CheckInventory("Synthetic Viscera"))
            Bot.Combat.Attack("*");

        Core.CancelRegisteredQuests();
        Bot.Options.AttackWithoutTarget = false;
        Adv.GearStore(true);

        void DarkCarnaxMove(string zone)
        {
            switch (zone.ToLower())
            {
                case "a":
                    //Move to the left
                    Bot.Player.WalkTo(Bot.Random.Next(25, 325), Bot.Random.Next(380, 475));
                    break;
                case "b":
                    //Move to the right
                    Bot.Player.WalkTo(Bot.Random.Next(600, 930), Bot.Random.Next(380, 475));
                    break;
                default:
                    //Move to the center
                    Bot.Player.WalkTo(Bot.Random.Next(325, 600), Bot.Random.Next(380, 475));
                    break;
            }
        }
    }
}