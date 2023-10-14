using HarmonyLib;
using UnityEngine;
using ui;

namespace GoonHW_WOA;

[HarmonyPatch]
public class ConveniencePatches
    {
        [HarmonyPatch(typeof(BootSceneController), "CheckTime")]
        [HarmonyPrefix]
        public static void SkipIntro(ref float ____timeCount)
        {
            ____timeCount = 10f;
        }

        [HarmonyPatch(typeof(ItemBagItemDetail), "ViewItemDetail")]
        [HarmonyPostfix]
        public static void ItemBagItemDetail_ViewItemDetail(ref ItemBagItemDetail __instance, ref TextMeshProEx ____itemName, Table.ITEMCATEGORYGROUP.Defs categoryGroup, Table.ITEMPARAM.Defs itemId, Sprite iconSprite, string itemName)
        {
            ____itemName.text = itemName + " - " + ((uint)itemId) + " - " + itemId.ToString();
        }

        [HarmonyPatch(typeof(NpcNoteDetail), "UpdateDetails")]
        [HarmonyPostfix]
        public static void NpcNoteDetail_UpdateDetails(int idx, ref TextMeshProEx ____npcDetail)
        {
            var fav = SingletonMonoBehaviour<GameInfo>.Instance.NpcInfo.GetFavorability(idx);
            var gifted = SingletonMonoBehaviour<GameInfo>.Instance.NpcInfo.GetGift(idx);
            var talked = SingletonMonoBehaviour<GameInfo>.Instance.NpcInfo.GetTalked(idx);
            ____npcDetail.text = ____npcDetail.text + "\n"
            + $"Favorability: {fav} Gifted: {(gifted ? "yes" : "no")} Talked: {(talked ? "yes" : "no")}";
        }
    }