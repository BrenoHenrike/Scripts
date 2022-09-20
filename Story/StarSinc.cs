//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs

using Skua.Core.Interfaces;
public class StarSinc
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFarms Farm = new();


    public string[] RequiredItems = { "Nova Badge 1.0", "Nova Badge 2.0", "Nova Badge 3.0", "Nova Badge 3.0", "Nova Badge 4.0", "Nova Badge 5.0", "Nova Badge 6.0", "Nova Badge 7.0", "Nova Badge 8.0", "Nova Badge 9.0", "Nova Badge 10.0", "Nova Badge 11.0", "SuperNova Badge" };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StarSincQuests();

        Core.SetOptions(false);
    }

    public void StarSincQuests()
    {
        if (Core.CheckInventory("SuperNova Badge"))
            return;

        Story.PreLoad(this);

        Core.AddDrop(RequiredItems);

        Core.EquipClass(ClassType.Farm);

        if (!Core.CheckInventory("SuperNova Badge"))
        {

            if (!Core.CheckInventory("Nova Badge 11.0"))
            {
                if (!Core.CheckInventory("Nova Badge 10.0"))
                {
                    if (!Core.CheckInventory("Nova Badge 9.0"))
                    {
                        if (!Core.CheckInventory("Nova Badge 8.0"))
                        {
                            if (!Core.CheckInventory("Nova Badge 7.0"))
                            {
                                if (!Core.CheckInventory("Nova Badge 6.0"))
                                {
                                    if (!Core.CheckInventory("Nova Badge 5.0"))
                                    {
                                        if (!Core.CheckInventory("Nova Badge 4.0"))
                                        {
                                            if (!Core.CheckInventory("Nova Badge 3.0"))
                                            {
                                                if (!Core.CheckInventory("Nova Badge 2.0"))
                                                {
                                                    //4400 | Weaken his Powers
                                                    if (!Core.CheckInventory("Nova Badge 1.0"))
                                                    {
                                                        Core.EnsureAccept(4400);
                                                        Core.HuntMonster("starsinc", "Star Sprites", "Stardust", 15);
                                                        Core.EnsureComplete(4400);
                                                        Bot.Wait.ForPickup("Nova Badge 1.0");
                                                    }
                                                    //4401 | Light and Dark
                                                    Core.EnsureAccept(4401);
                                                    Core.HuntMonster("starsinc", "Star Sprites", "Sprite Magic Essence", 7);
                                                    Core.EnsureComplete(4401);
                                                    Bot.Wait.ForPickup("Nova Badge 2.0");
                                                }
                                                //4402 | Paintings Give Him Strength
                                                Core.EnsureAccept(4402);
                                                Core.GetMapItem(3607, 4, "starsinc");
                                                Core.EnsureComplete(4402);
                                                Bot.Wait.ForPickup("Nova Badge 3.0");
                                            }
                                            //4403 | Learning the Land
                                            Core.EnsureAccept(4403);
                                            Core.GetMapItem(3608, 1, "starsinc");
                                            Core.EnsureComplete(4403);
                                            Bot.Wait.ForPickup("Nova Badge 4.0");
                                        }
                                        //4404 | Slay the Light and Dark
                                        Core.EnsureAccept(4404);
                                        Core.HuntMonster("starsinc", "Infernal Imp", "Darkness Fragment", 5);
                                        Core.HuntMonster("starsinc", "Living Star", "Light Fragment", 5);
                                        Core.EnsureComplete(4404);
                                        Bot.Wait.ForPickup("Nova Badge 5.0");
                                    }
                                    //4405 | Chaos Fragments
                                    Core.EnsureAccept(4405);
                                    Core.KillMonster("watchtower", "Frame2", "Left", "Chaos Spider", "Chaos Fragment", 10);
                                    Core.EnsureComplete(4405);
                                    Bot.Wait.ForPickup("Nova Badge 6.0");
                                }
                                //4406 | Kill Them All
                                Core.EnsureAccept(4406);
                                Core.HuntMonster("starsinc", "Star Sprites", "Monster Killed", 15);
                                Core.EnsureComplete(4406);
                                Bot.Wait.ForPickup("Nova Badge 7.0");
                            }
                            //4407 | Get Rid of Those Guards
                            Core.EnsureAccept(4407);
                            Core.HuntMonster("starsinc", "Fortress Guard", "Guard Slain", 5);
                            Core.EnsureComplete(4407);
                            Bot.Wait.ForPickup("Nova Badge 8.0");
                        }
                        //4408 | Breach the Gate
                        Core.EnsureAccept(4408);
                        Core.HuntMonster("starsinc", "Fortress Guard", "Guard's Key");
                        Core.EnsureComplete(4408);
                        Bot.Wait.ForPickup("Nova Badge 9.0");
                    }
                    //4409 | Defeat the Prime Dominus
                    Core.EquipClass(ClassType.Solo);
                    Core.EnsureAccept(4409);
                    Core.HuntMonster("starsinc", "Prime Dominus", "Prime Defeated");
                    Core.EnsureComplete(4409);
                    Bot.Wait.ForPickup("Nova Badge 10.0");
                }
                //4410 | Place the Beacons
                Core.EnsureAccept(4410);
                Core.GetMapItem(3609, 6, "starsinc");
                Core.EnsureComplete(4410);
                Bot.Wait.ForPickup("Nova Badge 11.0");
            }
            //4412 | Retrieve the Core
            Core.EnsureAccept(4412);
            Core.HuntMonster("starsinc", "Final", "Final Defeated");
            Core.EnsureComplete(4412);
            Bot.Wait.ForPickup("SuperNova Badge");
        }
        //4413 | Become one with the Universe
        Core.EquipClass(ClassType.Farm);
        Core.EnsureAccept(4413);
        Core.HuntMonster("starsinc", "Living Star", "Living Star Defeated", 30);
        Core.EnsureComplete(4413);

        //4414 | Becoming the Star Dominus
        Core.EnsureAccept(4414);
        Core.KillMonster("battleunderb", "Enter", "Spawn", "*", "Bone dust", 15);
        Core.HuntMonster("starsinc", "Living Star", "Living Star Essence", 30, false);
        Farm.BludrutBrawlBoss(quant: 15);
        Core.EnsureComplete(4414);

        //4415 | Your Hardest Task
        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(4415);
        Core.HuntMonster("starsinc", "Empowered Prime", "Empowered Primed Defeated", 10);
        Core.EnsureComplete(4415);
        Core.ToBank(RequiredItems);
    }
}