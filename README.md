# Rbot-Scripts

If any of my scripts helped you and you want to donate:  
[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/donate?hosted_button_id=QVQ4Q7XSH9VBY)  
Pix: bhenrike@protonmail.com

- [Versão em Português Brasileiro](README.pt-br.md)

- [RBot Docs](https://brenohenrike.github.io/Rbot-Scripts/)

- [Rbot-Scripts](#rbot-scripts)
  - [To do](#to-do)
  - [Customizing CoreBots](#customizing-corebots)
  - [Core Skill Plugin](#core-skill-plugin)
  - [FAQ](#faq)

## To do

- Make a script assistant interface like Grimoire (easy to use) ([$85/200](https://www.paypal.com/donate?hosted_button_id=QVQ4Q7XSH9VBY))

## Customizing CoreBots

Inside the **CoreBots.cs** you can find various properties you can change to your preferences, their default values are listed as bellow:

```csharp
// [Can Change] Delay between commom actions, 700 is the safe number
public int ActionDelay { get; set; } = 700;
// [Can Change] Delay used to get out of combat, 1600 is the safe number
public int ExitCombatDelay { get; set; } = 1600;
// [Can Change] Delay between jumping rooms after hunting a monster, increase if you think it is jumping too much
public int HuntDelay { get; set; } = 1000;
// [Can Change] How many tries to accept/complete the quest will be sent
public int AcceptandCompleteTries { get; set; } = 20;
// [Can Change] Whether the bots will use private rooms
public bool PrivateRooms { get; set; } = true;
// [Can Change] Whether the player should rest after killing a monster
public bool ShouldRest { get; set; } = false;
// [Can Change] Whether you want anti lag features (lag killer, invisible monsters, set to 10 FPS)
public bool AntiLag { get; set; } = true;
// [Can Change] The interval, in milliseconds, at which to use skills, if they are available.
public int SkillTimer { get; set; } = 100;
// [Can Change] Name of your soloing class
public string SoloClass { get; set; } = "Generic";
// [Can Change] (Use the Skills > Advanced window) Skill sequence string
public string SoloClassSkills { get; set; } = "1 | 2 | 3 | 4 | Mode Optimistic";
// [Can Change] (Use the Skills > Advanced window if unsure) SkillTimeout of the soloing class
public int SoloClassSkillTimeout { get; set; } = 150;
// [Can Change] Name of your farming class
public string FarmClass { get; set; } = "Generic";
// [Can Change] (Use the Skills > Advanced window) Skill sequence string
public string FarmClassSkills { get; set; } = "1 | 2 | 3 | 4 | Mode Optimistic";
// [Can Change] (Use the Skills > Advanced window if unsure) SkillTimeout of the farming class
public int FarmClassSkillTimeout { get; set; } = 1;
// [Can Change] Some Sagas use the hero alignment to give extra reputation, change to your desired rep (Alignment.Evil or Alignment.Good).
public int HeroAlignment { get; set; } = (int)Alignment.Evil;
```

## Core Skill Plugin

> **In RBot 3.6.2 and beyond Core Skills are branded as Advanced skills so you don't need the plugin anymore.**
> **The guide was [moved to here](https://brenohenrike.github.io/Rbot-Scripts/Skills#advanced-skills)**

## FAQ

**Q:** How do I download the scripts?  
**A:**

- Go to the [Releases link](https://github.com/BrenoHenrike/Rbot-Scripts/releases);
- From the latest upload, download the **Scripts.zip** folder under _Assets_;
- Place the **Scripts.zip** inside your _RBot Scripts folder_;
- Right click **Scripts.zip** and click **"_Extract here_"**;
- If prompted to replace your files, click **_Yes_**;
- All should be good to use now!

> **Note:** The current latest RBot version (as long as I update it) can be [downloaded here](https://github.com/BrenoHenrike/RBot/releases).

**Q:** I try to run CoreBots/Dailys/Farms and get an error, what do I do?  
**A:** All files starting with **"*Core*"** aren't bots, they are used by bots.

**Q:** I'm running a bot and I get an error like *"The type or namespace 'CoreBots' could not be found"*, how do I fix it?  
**A:** That's an installation problem, certify that you dropped the **"*Scripts*"** folder directly in the **"*RBot.exe*"** folder, this way it will update all previous bots that you downloaded from here. A commom error is your file path be like: *"\*/Rbot/Scripts/**Scripts**/FarmAllDailys.cs"* or even *"\*/Rbot/**Rbot-Scripts-master/Scripts**/FarmAllDailys.cs"* it should be *"\*/Rbot/**Scripts**/FarmAllDailys.cs"*.
> **Note:** If after you follow this answer the error persists, open the script you got an error and certify that the first lines with `//cs_include` have the right path/file name, typos happen.

**Q:** Even after doing the solutions above my script doesn't run, what now?  
**A:** Then I might have messed up something. In this case you can reach out to me in Discord: **Breno_Henrike#6959** and tell me which script you are having trouble with.
