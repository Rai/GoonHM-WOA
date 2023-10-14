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
            if(GoonHW_WOA.skipIntro.Value)
                ____timeCount = 10f;
        }

        [HarmonyPatch(typeof(ItemBagItemDetail), "ViewItemDetail")]
        [HarmonyPostfix]
        public static void ItemBagItemDetail_ViewItemDetail(ref ItemBagItemDetail __instance, ref TextMeshProEx ____itemName, Table.ITEMCATEGORYGROUP.Defs categoryGroup, Table.ITEMPARAM.Defs itemId, Sprite iconSprite, string itemName)
        {
            if(GoonHW_WOA.showItemIdInInventory.Value)
                ____itemName.text = itemName + " - " + ((uint)itemId) + (GoonHW_WOA.showItemResourceInInventory.Value ? " - " + itemId.ToString() : "");
        }

        [HarmonyPatch(typeof(NpcNoteDetail), "UpdateDetails")]
        [HarmonyPostfix]
        public static void NpcNoteDetail_UpdateDetails(int idx, ref TextMeshProEx ____npcDetail)
        {
            if(!GoonHW_WOA.showFavorabilityInNpcNote.Value && !GoonHW_WOA.showGiftedAndTalkedInNpcNote.Value)
                return;

            var fav = SingletonMonoBehaviour<GameInfo>.Instance.NpcInfo.GetFavorability(idx);
            var gifted = SingletonMonoBehaviour<GameInfo>.Instance.NpcInfo.GetGift(idx);
            var talked = SingletonMonoBehaviour<GameInfo>.Instance.NpcInfo.GetTalked(idx);
            
            ____npcDetail.text = ____npcDetail.text + "\n"
            + (GoonHW_WOA.showFavorabilityInNpcNote.Value ? $"Favorability: {fav}" : "") 
            + (GoonHW_WOA.showGiftedAndTalkedInNpcNote.Value ? $" Gifted: {(gifted ? "yes" : "no")} Talked: {(talked ? "yes" : "no")}" : "");
        }
    }