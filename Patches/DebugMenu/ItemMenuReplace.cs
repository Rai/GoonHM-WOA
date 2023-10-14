using HarmonyLib;
using UnityEngine;
using debug;
using ui;
using Table;
using item;
using gimmick;
using System.Collections.Generic;

namespace GoonHW_WOA;

[HarmonyPatch]
public class ItemMenuReplace {
    [HarmonyPatch(typeof(DebugMenuConstructorMain), "Item")]
    [HarmonyPrefix]
    public static unsafe void Item(DebugMenuConstructorMain __instance, ref bool __runOriginal)
    {
        IDebugMenuWindow debugMenuWindow = DebugMenuWindowConstructorBaseMethods.OpenDebugSubWindow(__instance);
        ItemParam_accessor itemParam = TableAccessor.ItemParam;
        debugMenuWindow.InitWindow("Item (improved)");
        debugMenuWindow.AddButton("ItemRecordAll", delegate
        {
            SingletonMonoBehaviour<GameInfo>.Instance.ItemInfo.ItemRecordAll();
            SingletonMonoBehaviour<GameInfo>.Instance.AnimalHosueInfo.AnimalAllNew();
        });
        debugMenuWindow.AddInput("RemoveItem", 0f, delegate(float value)
        {
            SingletonMonoBehaviour<GameInfo>.Instance.ItemInfo.RemoveItem((ITEMPARAM.Defs)Mathf.FloorToInt(value), 1, ItemInfo.InventoryType.Itembag);
        });
        ItemInfo.InventoryType targetInventory = ItemInfo.InventoryType.Itembag;
        debugMenuWindow.AddToggle("AddStorage", false, delegate(bool value)
        {
            if (value)
            {
                targetInventory = ItemInfo.InventoryType.Storage;
                return;
            }
            targetInventory = ItemInfo.InventoryType.Itembag;
        });
        int addNum = 1;
        debugMenuWindow.AddToggle("AddItem+99", false, delegate(bool value)
        {
            if (value)
            {
                addNum = 99;
                return;
            }
            addNum = 1;
        });
        debugMenuWindow.AddInput("ItemId", 0f, delegate(float value)
        {
            int num4 = (int)value;
            if (num4 <= 0)
            {
                return;
            }
            if (!TableAccessor.ItemParam.HasUIDRecord((uint)num4))
            {
                return;
            }
            ITEMPARAM.Defs defs = (ITEMPARAM.Defs)num4;
            int num5;
            if (!SingletonMonoBehaviour<GameInfo>.Instance.ItemInfo.AddHaveItem(defs, addNum, out num5, targetInventory, true) && targetInventory == ItemInfo.InventoryType.Itembag)
            {
                SingletonMonoBehaviour<DropIconItemManager>.Instance.DropItem(defs, num5, 1f, 2.5f, true);
            }
            if (num5 != addNum && targetInventory == ItemInfo.InventoryType.Itembag)
            {
                SingletonMonoBehaviour<UIIngame>.Instance.LogContainer.AddItemLog((uint)defs, addNum - num5);
            }
        });
        uint num = itemParam.GetSize() / 100U;
        int num2 = (int)(itemParam.GetSize() % 100U);
        int num3 = 0;
        while ((long)num3 < (long)((ulong)num))
        {
            int baseIndex = num3 * 100;
            List<string> list = new List<string>();
            int target = baseIndex;
            for (int i = 0; i < 100; i++)
            {
                int index3 = baseIndex + i;
                string name = Msg.MessageManager.Instance.GetMessage((int)itemParam.get_name((uint)(ITEMPARAM.Defs)TableAccessor.ItemParam.GetDataFromIndex(index3).uid));
                int quality = (int)(ITEMPARAM.Defs)TableAccessor.ItemParam.GetDataFromIndex(index3).quality + 1;
                list.Add($"{name} Lv.{quality}");
            }
            debugMenuWindow.AddDropDown(string.Format("ItemId:{0} - {1}", TableAccessor.ItemParam.GetDataFromIndex(baseIndex).uid, TableAccessor.ItemParam.GetDataFromIndex(baseIndex + 99).uid), list.ToArray(), delegate(int index)
            {
                target = index + baseIndex;
            }, 0, delegate()
            {
                ITEMPARAM.Defs uid = (ITEMPARAM.Defs)TableAccessor.ItemParam.GetDataFromIndex(target).uid;
                int num4;
                if (!SingletonMonoBehaviour<GameInfo>.Instance.ItemInfo.AddHaveItem(uid, addNum, out num4, targetInventory, true) && targetInventory == ItemInfo.InventoryType.Itembag)
                {
                    SingletonMonoBehaviour<DropIconItemManager>.Instance.DropItem(uid, num4, 1f, 2.5f, true);
                }
                if (num4 != addNum && targetInventory == ItemInfo.InventoryType.Itembag)
                {
                    SingletonMonoBehaviour<UIIngame>.Instance.LogContainer.AddItemLog((uint)uid, addNum - num4);
                }
            });
            num3++;
        }
        int remainBase = (int)(num * 100U);
        List<string> list2 = new List<string>();
        for (int j = 0; j < num2; j++)
        {
            int index2 = remainBase + j;
            string name = Msg.MessageManager.Instance.GetMessage((int)itemParam.get_name((uint)(ITEMPARAM.Defs)TableAccessor.ItemParam.GetDataFromIndex(index2).uid));
            int quality = (int)(ITEMPARAM.Defs)TableAccessor.ItemParam.GetDataFromIndex(index2).quality + 1;
            list2.Add($"{name} Lv.{quality}");
        }
        int target2 = remainBase;
        debugMenuWindow.AddDropDown(string.Format("ItemId:{0} - {1}", TableAccessor.ItemParam.GetDataFromIndex(remainBase).uid, TableAccessor.ItemParam.GetDataFromIndex(remainBase + num2 - 1).uid), list2.ToArray(), delegate(int index)
        {
            target2 = index + remainBase;
        }, 0, delegate()
        {
            ITEMPARAM.Defs uid = (ITEMPARAM.Defs)TableAccessor.ItemParam.GetDataFromIndex(target2).uid;
            int num4;
            if (!SingletonMonoBehaviour<GameInfo>.Instance.ItemInfo.AddHaveItem(uid, addNum, out num4, targetInventory, true) && targetInventory == ItemInfo.InventoryType.Itembag)
            {
                SingletonMonoBehaviour<DropIconItemManager>.Instance.DropItem(uid, num4, 1f, 2.5f, true);
            }
            if (num4 != addNum && targetInventory == ItemInfo.InventoryType.Itembag)
            {
                SingletonMonoBehaviour<UIIngame>.Instance.LogContainer.AddItemLog((uint)uid, addNum - num4);
            }
        });
        debugMenuWindow.AddButton("HarvestCountAll", delegate
        {
            SingletonMonoBehaviour<GameInfo>.Instance.ItemInfo.ItemHarvestCountAll();
        });
        debugMenuWindow.AddButton("HarvestCountTest", delegate
        {
            SingletonMonoBehaviour<GameInfo>.Instance.ItemInfo.ItemHarvestCountTest();
        });
        debugMenuWindow.SetActionToBackButton(true, ()=>{DebugMenuWindowConstructorBaseMethods.CloseDebugSubWindow(__instance);}, "");
        debugMenuWindow.FinalizeWindow();
        __runOriginal = false;
    }
}