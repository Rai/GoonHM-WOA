﻿using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine.InputSystem;
using debug;

namespace GoonHM_WOA;

[BepInPlugin("org.bepinex.plugins.goonhmwoa", "GoonHM-WOA", "1.0.4.0")]
[BepInProcess("Harvest Moon The Winds of Anthos.exe")]
public partial class GoonHM_WOA : BaseUnityPlugin
{
    public static ManualLogSource Log;

    private void Awake()
    {
        Log = base.Logger;
        Log.LogInfo($"Plugin GoonHM-WOA is loaded!");

        InitConfig();

        Harmony.CreateAndPatchAll(typeof(ConveniencePatches)); // Skip intro, etc..
        
        //debug menu
        Harmony.CreateAndPatchAll(typeof(DebugMenuWindowConstructorBaseMethods));
        if(enableDebugMenu4KResize.Value)
        {
            Harmony.CreateAndPatchAll(typeof(DebugMenu4KResize));
        }
        Harmony.CreateAndPatchAll(typeof(GoonMenuTest));
        Harmony.CreateAndPatchAll(typeof(ItemMenuReplace));
    }

    private void Update()
    {
        if(Gamepad.current is Gamepad gamepad)
        {
            if(gamepad.rightStickButton.wasReleasedThisFrame)
            {
                SingletonMonoBehaviour<DebugMenuRoot>.Instance.SwitchingActivateDebugMenu();
            }
        }
    }
}
