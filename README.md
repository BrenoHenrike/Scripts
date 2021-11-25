# Rbot-Scripts

If any of my scripts helped you and you want to donate:  
[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/donate?hosted_button_id=QVQ4Q7XSH9VBY)

- [Versão em Português Brasileiro](README.pt-br.md)

- [Rbot-Scripts](#rbot-scripts)
  - [To do](#to-do)
  - [Customizing CoreBots](#customizing-corebots)
  - [Core Skill Plugin](#core-skill-plugin)
  - [FAQ](#faq)

## To do

- Save option for the plugin;
- Complete SDKA quests.
- Make a script assistant UI like Grim ([$15/200](https://www.paypal.com/donate?hosted_button_id=QVQ4Q7XSH9VBY))

## Customizing CoreBots

Inside the **CoreBots.cs** you can find various properties you can change to your preferences, their default values are listed as bellow:

```csharp
// [Can Change] Delay between commom actions, 700 is the safe number
public int ActionDelay { get; set; } = 700;
// [Can Change] Delay used to get out of combat, 1600 is the safe number
public int ExitCombatDelay { get; set; } = 1600;
// [Can Change] Whether the bots will use private rooms
public bool PrivateRooms { get; set; } = true;
// [Can Change] Whether the player should rest after killing a monster
public bool ShouldRest { get; set; } = false;
// [Can Change] The interval, in milliseconds, at which to use skills, if they are available.
public int SkillTimer { get; set; } = 100;
// [Can Change] Name of your soloing class
public string SoloClass { get; set; } = "Generic";
// [Can Change] (Use the Core Skill plugin) Skill sequence string
public string SoloClassSkills { get; set; } = "1 | 2 | 3 | 4 | Mode Optimistic";
// [Can Change] (Use the Core Skill plugin if unsure) SkillTimeout of the soloing class
public int SoloClassSkillTimeout { get; set; } = 150;
// [Can Change] Name of your farming class
public string FarmClass { get; set; } = "Generic";
// [Can Change] (Use the Core Skill plugin) Skill sequence string
public string FarmClassSkills { get; set; } = "1 | 2 | 3 | 4 | Mode Optimistic";
// [Can Change] (Use the Core Skill plugin if unsure) SkillTimeout of the farming class
public int FarmClassSkillTimeout { get; set; } = 1;
// [Can Change] Some Sagas use the hero alignment to give extra reputation, change to your desired rep (Alignment.Evil or Alignment.Good).
public int HeroAlignment { get; set; } = (int)Alignment.Evil;
```

## Core Skill Plugin

If you want custom skills for your solo/farming class this plugin will help you with the sequence you want. The plugin by itself has all the necessary tooltips if you hover the mouse over the controls, but I can guide you through how to make a skill sequence for Void Highlord.

First to load the plugin you can either drop it in the **"_Rbot/plugins/_"** so it will be loaded when the application starts, or you can open the **"Plugins"** tab in RBot and then Load it by finding its directory:

<p align="center"><img src="https://imgur.com/IEVOrkl.png"></p>

After that the button will appear in the Main Menu of RBot, click on it and you will see the UI of the plugin:

<p align="center"><img src="https://imgur.com/AUIOhFe.png"></p>

1. This is the list where the skills you add will end up. After adding them you can use:
   - Arrow keys (Up/Down) to navigate;
   - Ctrl + Arrow keys to move them in the sequence;
   - Delete to remove from the sequence;
   - Ctrl + Delete to clear the sequence;
   - You can also drag them with your mouse or do any of the commands with a right click.
2. Here you can add how the bot will use the skills inside the game. If you set the values and want to reset them, right click and then click _"Reset"_. Each line does, respectively:
   - Checkbox that determines if you want to add the use rule to the skill;
   - Health percentage the skill need to be used, you can click in the '>' signal to invert it;
   - Mana quantity the skill need used, you can click in the '>' signal to invert it;
   - How many time, in milliseconds, the bot will wait to use the skill;
   - Checkbox that determine whether the skill should be skipped if it is not available.
3. Will add the skill with the index (1-4) of the numeric field with the use rules (if checked to do so) to the list.
4. The use mode of the skill:
   - Optimistic - If the bot can use the skill, it will use it. If not the skill will be skipped.
   - Wait (Default) - Will wait for the skill to cooldown (or the timeout time) before using the skill;
5. A simple calculator of the [SkillTimeout](#customizing-corebots) property. To use you enter the value of the SkillTimer (default is 100) and the longest skill cooldown of your class, after pressing enter you can use the value showed besides _"SkillTimeout"_ in **CoreBots.cs**;
6. This button will convert all data in the list sequence to a string you can use in the **CoreBots.cs** _"ClassSkill"_ property. After clicking that button it will automatically copy it to your clipboard.
7. Where the string you need will show up.

The Void Highlord damage skill sequence is `4-5-3-2-4-3-2`, to make it in this plugin we first need to consider that the index of the skill is its number -1 (minus one) as we do not count the auto attack, so the skill sequence will look like `3-4-2-1-3-2-1`. First we add the 3 which is _Unshackled_ and it uses 20% of your health, so to not embarrassingly die we set it up like this:

<p align="center"><img src="https://imgur.com/X4bDDxG.png"></p>

And then click the **Add** button. Note that I setted the health to be 25% so it give us a breathing range and checked _"Skip if not available"_ so we don't sit waiting this skill while bellow 25% HP. Following this step we can complete the sequence so it will look like that in the list:

<p align="center"><img src="https://imgur.com/QNOASl5.png"></p>

The **Use Mode** is optional but I recommend you always use the Wait mode (unless your class doesn't need a defined sequence). With everything right you can click the **"Convert"** button.

<p align="center"><img src="https://imgur.com/AKGlJY8.png"></p>

For the **SkillTimeout** (only calculate it if you're using the **Wait Use Mode**) of VHL we can use the longest CD which is of _Armageddon_ with 15 seconds, convert it to milliseconds (15000) and if our **SkillTimer** is 100ms it will give us **SkillTimeout = 150**, you can now change the **ClassSkillTimeout**, which in this case is our Solo Class. With all this info we can make the respective changes to **CoreBots.cs** file:

```csharp
// [Can Change] Name of your soloing class
public string SoloClass { get; set; } = "Void Highlord";
// [Can Change] Skill sequence string (use the plugin)
public string SoloClassSkills { get; set; } = "3 H>25S | 4 | 2 | 1 H>25S | 3 H>25S | 2 | 1 H>25S";
// [Can Change] (Use the Core Skill plugin if unsure) SkillTimeout of the soloing class
public int SoloClassSkillTimeout { get; set; } = 150;
```

Now your bot will use the defined class and skills when needed.

> **Notes:**
>
> - You can make use of all use rules for the same skill without any problem. 
> - Wait rules have priority over all rules, even if you can't use the skill and skip is checked, it will first wait the desired time first and then check the other rules.

## FAQ

To use just drop the Scripts folder in RBot.exe folder.  
<p align="center"><img src="https://imgur.com/SDU0oqd.gif" width=450></p>

**Q:** How do I download the scripts?  
**A:** Click in the green **"*Code*"** button then in **"*Download ZIP*"**, from WinRar/7z you can drag and drop the **"*Scripts*"** folder directly in the **"*RBot.exe*"** folder like in the gif above.

**Q:** I try to run CoreBots/Dailys/Farms and get an error, what do I do?  
**A:** All files starting with **"*Core*"** aren't bots, they are used by bots.

**Q:** I'm running a bot and I get an error like *"The type or namespace 'CoreBots' could not be found"*, how do I fix it?  
**A:** That's an installation problem, certify that you dropped the **"*Scripts*"** folder directly in the **"*RBot.exe*"** folder, this way it will update all previous bots that you downloaded from here. A commom error is your file path be like: *"\*/Rbot/Scripts/**Scripts**/FarmAllDailys.cs"* or even *"\*/Rbot/**Rbot-Scripts-master/Scripts**/FarmAllDailys.cs"* it should be *"\*/Rbot/**Scripts**/FarmAllDailys.cs"*.
> **Note:** If after you follow this answer the error persists, open the script you got an error and certify that the first lines with `//cs_include` have the right path/file name, typos happen.

**Q:** Even after doing the solutions above my script doesn't run, what now?  
**A:** Then I might have messed up something. In this case you can reach out to me in Discord: **Breno_Henrike#6959** and tell me which script you are having trouble with.
