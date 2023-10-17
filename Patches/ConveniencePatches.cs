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
        public static void ItemBagItemDetail_ViewItemDetail(ref ItemBagItemDetail __instance, ref TextMeshProEx ____itemName, Table.ITEMCATEGORYGROUP.Defs categoryGroup, ref Table.ITEMPARAM.Defs itemId, Sprite iconSprite, string itemName)
        {
            if(GoonHW_WOA.showItemIdInInventory.Value)
                ____itemName.text = itemName + " - " + ((uint)itemId) + (GoonHW_WOA.showItemResourceInInventory.Value ? " - " + itemId.ToString() : "");
        }

        [HarmonyPatch(typeof(EncyclopediaDetailFlower), "SetId")]
        [HarmonyPostfix]
        public static void SetId_Flower(int idx, ref TextMeshProEx ____name)
        {
            if(GoonHW_WOA.showItemIdInInventory.Value){
                Table.EncyclopediaParam_accessor encyclopediaParam = Table.TableAccessor.EncyclopediaParam;
                int id = encyclopediaParam.get_item_id(idx + 183);
                ____name.text = ____name.text + " - " + id;
            }
        }

        [HarmonyPatch(typeof(EncyclopediaDetailOre), "SetId")]
        [HarmonyPostfix]
        public static void SetId_Ore(int idx, ref TextMeshProEx ____name)
        {
            if(GoonHW_WOA.showItemIdInInventory.Value){
                Table.EncyclopediaParam_accessor encyclopediaParam = Table.TableAccessor.EncyclopediaParam;
                int id = encyclopediaParam.get_item_id(idx + 534);
                ____name.text = ____name.text + " - " + id;
            }
        }

        [HarmonyPatch(typeof(EncyclopediaDetailFish), "SetId")]
        [HarmonyPostfix]
        public static void SetId_Fish(int idx, ref TextMeshProEx ____name)
        {
            if(GoonHW_WOA.showItemIdInInventory.Value){
                Table.EncyclopediaParam_accessor encyclopediaParam = Table.TableAccessor.EncyclopediaParam;
                int id = encyclopediaParam.get_item_id(idx + 467);
                ____name.text = ____name.text + " - " + id;
            }
        }
                
        [HarmonyPatch(typeof(EncyclopediaDetailCrop), "SetId")]
        [HarmonyPostfix]
        public static void SetId_Crop(int idx, ref TextMeshProEx ____name)
        {
            if(GoonHW_WOA.showItemIdInInventory.Value){
                Table.EncyclopediaParam_accessor encyclopediaParam = Table.TableAccessor.EncyclopediaParam;
                int id = encyclopediaParam.get_item_id(idx + 1);
                ____name.text = ____name.text + " - " + id;
            }
        }

        [HarmonyPatch(typeof(EncyclopediaDetailCooking), "SetId")]
        [HarmonyPostfix]
        public static void SetId_Cooking(int idx, ref TextMeshProEx ____name)
        {
            if(GoonHW_WOA.showItemIdInInventory.Value){
                Table.EncyclopediaParam_accessor encyclopediaParam = Table.TableAccessor.EncyclopediaParam;
                int id = encyclopediaParam.get_item_id(idx + 304);
                ____name.text = ____name.text + " - " + id;
            }
        }

        [HarmonyPatch(typeof(EncyclopediaDetailCollection), "SetId")]
        [HarmonyPostfix]
        public static void SetId_Collection(int idx, ref TextMeshProEx ____name)
        {
            if(GoonHW_WOA.showItemIdInInventory.Value){
                Table.EncyclopediaParam_accessor encyclopediaParam = Table.TableAccessor.EncyclopediaParam;
                int id = encyclopediaParam.get_item_id(idx + 265);
                ____name.text = ____name.text + " - " + id;
            }
        }

        [HarmonyPatch(typeof(QuestDetailItem), "SetRequest")]
        [HarmonyPostfix]
        public static void SetRequest(uint tableIdx, int idx, ref TextMeshProEx ____name)
        {
            if(GoonHW_WOA.showItemIdInInventory.Value){
                Table.QuestParam_accessor questParam = Table.TableAccessor.QuestParam;
                uint uid = (uint)questParam.get_Request_item(tableIdx, idx);
                ____name.text = ____name.text + "(" + uid + ")";
            }
        }

        [HarmonyPatch(typeof(NpcNoteFavoriteItem), "SetItem")]
        [HarmonyPostfix]
        public static void NpcNoteFavoriteItem_SetItem(int itemParamIdx, ref TextMeshProEx ____itemName)
        {
            if(GoonHW_WOA.showItemIdInInventory.Value)
                ____itemName.text = ____itemName.text + " - " + itemParamIdx;
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

        // [HarmonyPatch(typeof(AnimalNoteDetail), "UpdateDetails")]
        // [HarmonyPostfix]
        // public static void AnimalNoteDetail_UpdateDetails(int idx, ref TextMeshProEx ____animalDetail)
        // {
        //     // if(!GoonHW_WOA.showFavorabilityInAnimalNote.Value)
        //     //     return;

        //     var fav = SingletonMonoBehaviour<GameInfo>.Instance.AnimalHosueInfo.GetAnimalData(idx).State;
            
        //     ____animalDetail.text = ____animalDetail.text + "\n"
        //     + (GoonHW_WOA.showFavorabilityInAnimalNote.Value ? $"Favorability: {fav}" : "");
        // }
    }