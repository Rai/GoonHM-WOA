using BepInEx;
using BepInEx.Unity.Mono;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine.InputSystem;
using debug;

namespace GoonHW_WOA;

[BepInPlugin("org.bepinex.plugins.goonhmwoa", "GoonHM-WOA", "1.0.0.0")]
[BepInProcess("Harvest Moon The Winds of Anthos.exe")]
public partial class GoonHW_WOA : BaseUnityPlugin
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
