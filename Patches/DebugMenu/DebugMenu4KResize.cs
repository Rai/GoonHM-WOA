using HarmonyLib;
using debug;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace GoonHM_WOA;

[HarmonyPatch]
public class DebugMenu4KResize {
    [HarmonyPatch(typeof(DebugMenuWindow), "InitWindow")]
    [HarmonyPostfix]
    public static void InitWindowDoubler(ref float ____maxHeight)
    {
        ____maxHeight = 80f;
    }

    [HarmonyPatch(typeof(DebugMenuWindow), "FinalizeWindow")]
    [HarmonyPostfix]
    public static void FinalizeWindowDoubler(ref GridLayoutGroup ____gridLayout, ref float ____maxHeight, ref int ____contentCount, ref DebugMenuItemButton ____backButton)
    {
        ____gridLayout.cellSize = new Vector2(250f, ____maxHeight);
        float num = ____maxHeight + ____gridLayout.spacing.y;
        int num2 = (int)(800f / num);
        ____gridLayout.constraintCount = ((____contentCount <= num2) ? ____contentCount : num2);

        ____backButton.GetComponentInChildren<Text>().text = "Back";
    }

    [HarmonyPatch(typeof(DebugMenuRoot), "OpenSubWindow")]
    [HarmonyPostfix]
    public static void OpenSubWindow(ref DebugMenuWindow ____mainWindow, ref DebugMenuWindow ____subWindow)
    {
        ____subWindow.RectTransform.anchoredPosition = new Vector2(____mainWindow.RectTransform.sizeDelta.x * 7f * ____mainWindow.RectTransform.localScale.x, -20f);
    }

    [HarmonyPatch(typeof(DebugMenuItemDropdown), "Initialize", argumentTypes: new Type[] { typeof(string), typeof(Action<int>) })]
    [HarmonyPostfix]
    public static void DropdownUpdateTextSize(ref Text ____label, ref Dropdown ____dropdown)
    {
        ____label.fontSize = 20;
        
        // TODO: find out how to properly resize dropdowns
        // RectTransform rectTransform = ____dropdown.transform as RectTransform;
        // RectTransform rectTransform2 = ____dropdown.itemText.transform as RectTransform;
        // rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 30f);
        // rectTransform2.sizeDelta = new Vector2(rectTransform2.sizeDelta.x, 30f);
        // ____dropdown.captionText.fontSize = 20;
        // ____dropdown.itemText.fontSize = 20;
    }

    [HarmonyPatch(typeof(DebugMenuItemButton), "Initialize", argumentTypes: new Type[] { typeof(string), typeof(Action) })]
    [HarmonyPostfix]
    public static void ButtonUpdateTextSize(ref Text ____text)
    {
        ____text.fontSize = 20;
    }

    [HarmonyPatch(typeof(DebugMenuItemSlider), "SetUp")]
    [HarmonyPostfix]
    public static void SliderUpdateTextSize(ref Text ____label, ref Slider ____slider)
    {
        ____label.fontSize = 20;
    }

    [HarmonyPatch(typeof(DebugMenuItemInputNumeric), "SetInputType")]
    [HarmonyPatch(typeof(DebugMenuItemInputString), "SetInputType")]
    [HarmonyPostfix]
    public static void InputUpdateTextSize(ref Text ____label)
    {
        ____label.fontSize = 20;
    }
}