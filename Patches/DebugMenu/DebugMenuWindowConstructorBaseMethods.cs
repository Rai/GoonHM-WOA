using System;
using HarmonyLib;
using debug;

namespace GoonHM_WOA;

[HarmonyPatch]
public static class DebugMenuWindowConstructorBaseMethods {
    [HarmonyReversePatch]
    [HarmonyPatch(typeof(DebugMenuWindowConstructorBase), "OpenSubWindow", argumentTypes: new Type[] { })]
    public static IDebugMenuWindow OpenDebugSubWindow(object instance) { throw new NotImplementedException(); }

    [HarmonyReversePatch]
    [HarmonyPatch(typeof(DebugMenuWindowConstructorBase), "CloseSubWindow")]
    public static void CloseDebugSubWindow(object instance) { }

    [HarmonyReversePatch]
    [HarmonyPatch(typeof(DebugMenuWindowConstructorBase), "AddButton")]
    public static void AddDebugButton(object instance, string label, Action action) { }
}