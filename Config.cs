using BepInEx.Configuration;
using System;

namespace GoonHW_WOA;

public partial class GoonHW_WOA
{
    public static ConfigEntry<bool> enableDebugMenu;
    public static ConfigEntry<bool> skipIntro;
    public static ConfigEntry<bool> showItemIdInInventory;
    public static ConfigEntry<bool> showItemResourceInInventory;

    public static ConfigEntry<bool> showFavorabilityInNpcNote;

    public static ConfigEntry<bool> showGiftedAndTalkedInNpcNote;

    private void InitConfig()
    {
        enableDebugMenu = Config.Bind<bool>("General", "Enable Debug Menu", true, "Enable debug menu.");
        skipIntro = Config.Bind<bool>("General", "Skip Intro", true, "Skip the intro.");

        showFavorabilityInNpcNote = Config.Bind<bool>("NpcMenu", "Show Favorability", true, "Show favorability in NPC description.");
        showGiftedAndTalkedInNpcNote = Config.Bind<bool>("NpcMenu", "Show Gifted and Talked", true, "Show gifted and talked status in NPC description.");
        
        showItemIdInInventory = Config.Bind<bool>("Inventory", "Show Item ID", true, "Show item ID in inventory. (after the item name)");
        showItemResourceInInventory = Config.Bind<bool>("Inventory", "Show Item Resource ID", false, "Show an additional item resource ID in inventory. (after the Item ID)");
    }
}