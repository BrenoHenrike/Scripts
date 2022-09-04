//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class Artixpointe
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        OmniArtifact();

        Core.SetOptions(false);
    }

    public void OmniArtifact()
    {
        if (Core.CheckInventory("Omni Artifact"))
            return;

        Core.EquipClass(ClassType.Solo);

        Core.HuntMonster("artixpointe", "Corrupted Sushi Chef", "Unholy Wasabi");
        Core.HuntMonster("battlefowl", "Chickencow", "Chickencow Claw");
        Core.HuntMonster("graveyard", "Big Jack Sprat", "Zorbak's Staff Skull");
        Core.HuntMonster("vendorbooths", "Dragon Khan", "Dragon Khan's Corrupt Scepter");
        Core.HuntMonster("undergroundlabb", "Rabid Server Hamster", "Sir Ver's Broken Power Button");

        if (!Core.CheckInventory("Cysero's Doom Sock"))
        {
            Core.EnsureAccept(3800);
            Core.GetMapItem(2911, 1, "artixpointe");
            Core.EnsureComplete(3801);
            Bot.Wait.ForPickup("Cysero's Doom Sock");
        }

        if (!Core.CheckInventory("Death's Cursed Hourglass"))
        {
            if (!Core.isCompletedBefore(3807))
            {
                Core.EnsureAccept(3806);
                Core.HuntMonster("gravefang", "Gravefang", "Gravefang Defeated");
                Core.EnsureComplete(3806);
            }
            Core.EnsureAccept(3807);
            Core.GetMapItem(2912, 1, "artixpointe");
            Core.EnsureComplete(3807);
            Bot.Wait.ForPickup("Death's Cursed Hourglass");
        }
        Core.BuyItem("artixpointe", 1002, "Omni Artifact");
    }
}