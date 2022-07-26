//cs_include Scripts/CoreBots.cs
using RBot;

public class LunaCoveStory
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        //4399|Time to Rock this Finale!
        if (!Core.CheckInventory("Your Moonstone"))
        {
            //4398|Dis-lycan What Happened
            if (!Core.CheckInventory("Dis-Lycan this"))
            {
                //4397|Fur the Right Thing
                if (!Core.CheckInventory("Gravefang's Elixir"))
                {
                    //4396|Find the Lycanstone
                    if (!Core.CheckInventory("Stolen Lycanstone"))
                    {
                        //4395|Lycan this Next Task
                        if (!Core.CheckInventory("Werewolf Discovery"))
                        {
                            //4394|"Were" Did We Go Wrong?
                            if (!Core.CheckInventory("For the Horde!"))
                            {
                                //4393|Total Eclipse of the Heart
                                if (!Core.CheckInventory("Were Hair"))
                                {
                                    //4392|Hex on the Beach(balls)
                                    if (!Core.CheckInventory("Shiny Stone"))
                                    {
                                        //4391|Fish’n’Chips on my Shoulder
                                        if (!Core.CheckInventory("Air Pump"))
                                        {
                                            //4390|A Ritual by any Other Name
                                            if (!Core.CheckInventory("Stale Chips"))
                                            {
                                                //4389|Let's Get Started
                                                if (!Core.CheckInventory("Ritual Items"))
                                                {
                                                    Core.AddDrop("Ritual Items");
                                                    Core.EnsureAccept(4389);

                                                    Core.HuntMonster("lunacove", "Cove Warrior", "Ritual Items", 15);
                                                    Core.HuntMonster("lunacove", "Plessie", "Plessie", 1);
                                                    Core.HuntMonster("lunacove", "Island Girl", "Candle", 5);

                                                    Core.EnsureComplete(4389);
                                                    Bot.Wait.ForPickup("Ritual Items");
                                                }

                                                Core.AddDrop("Stale Chips");
                                                Core.EnsureAccept(4390);

                                                Core.GetMapItem(3533, 10, "lunacove");
                                                Core.HuntMonster("lunacove", "Cove Fisher", "Bag of Chips", 1);

                                                Core.EnsureComplete(4390);
                                                Bot.Wait.ForPickup("Stale Chips");
                                            }

                                            Core.AddDrop("Air Pump");
                                            Core.EnsureAccept(4391);

                                            Core.HuntMonster("lunacove", "Lunar Villager", "Chips", 3);

                                            Core.EnsureComplete(4391);
                                            Bot.Wait.ForPickup("Air Pump");
                                        }

                                        Core.AddDrop("Shiny Stone");
                                        Core.EnsureAccept(4392);

                                        Core.HuntMonster("lunacove", "Lunar Villager", "Villager Chastised", 10);
                                        Core.HuntMonster("lunacove", "Beach Ball", "Deflated Beach Balls", 5);

                                        Core.EnsureComplete(4392);
                                        Bot.Wait.ForPickup("Shiny Stone");
                                    }

                                    Core.AddDrop("Were Hair");
                                    Core.EnsureAccept(4393);

                                    Core.HuntMonster("lunacove", "Coral Merdraconian", "Coral Branch", 3);
                                    Core.HuntMonster("lunacove", "Plessie", "item", 2);

                                    Core.EnsureComplete(4393);
                                    Bot.Wait.ForPickup("Were Hair");
                                }

                                Core.AddDrop("For the Horde!");
                                Core.EnsureAccept(4394);

                                Core.GetMapItem(3534, 7, "lunacove");

                                Core.EnsureComplete(4394);
                                Bot.Wait.ForPickup("For the Horde!");
                            }

                            Core.AddDrop("Werewolf Discovery");
                            Core.EnsureAccept(4395);

                            Core.HuntMonster("lunacove", "Horde Knight", "Horde Knight Defeated", 7);
                            Core.HuntMonster("lunacove", "Horde Lycan", "Horde Lycan Defeated", 8);

                            Core.EnsureComplete(4395);
                            Bot.Wait.ForPickup("Werewolf Discovery");
                        }

                        Core.AddDrop("Stolen Lycanstone");
                        Core.EnsureAccept(4396);

                        Core.HuntMonster("lunacove", "Horde Knight", "Lycanstone", 1);

                        Core.EnsureComplete(4396);
                        Bot.Wait.ForPickup("Stolen Lycanstone");
                    }

                    Core.AddDrop("Gravefang's Elixir");
                    Core.EnsureAccept(4397);

                    Core.HuntMonster("lunacove", "Gravefang", "Gravefang Defeated", 1);

                    Core.EnsureComplete(4397);
                    Bot.Wait.ForPickup("Gravefang's Elixir");
                }

                Core.AddDrop("Dis-Lycan this");
                Core.EnsureAccept(4398);

                Core.HuntMonster("lunacove", "Beach Werewolf", "Beach Werewolf Defeated", 6);
                Core.HuntMonster("lunacove", "Lunar Lycan", "Lunar Lycan Subdued", 6);

                Core.EnsureComplete(4398);
                Bot.Wait.ForPickup("Dis-Lycan this");
            }

            Core.AddDrop("Your Moonstone");
            Core.EnsureAccept(4399);

            Core.HuntMonster("lunacove", "Moonrock", "Moon Rock Smashed", 1);

            Core.EnsureComplete(4399);
            Bot.Wait.ForPickup("Your Moonstone");
        }
    }
}
