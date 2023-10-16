using HarmonyLib;
using debug;
using npc;
using Table;
using System.IO;
using System.Text;

namespace GoonHW_WOA;

[HarmonyPatch]
public class GoonMenuTest {
    [HarmonyPatch(typeof(DebugMenuConstructorMain), nameof(debug.DebugMenuConstructorMain.CreateWindow))]
    [HarmonyPrefix]
    public static void AddDebugMenuOptions(DebugMenuConstructorMain __instance)
    {
        DebugMenuWindowConstructorBaseMethods.AddDebugButton(__instance, "Goon Testing", () => {
            var goonmenu = DebugMenuWindowConstructorBaseMethods.OpenDebugSubWindow(__instance);
            
            goonmenu.InitWindow("Goon Testing");
            
            NpcNoteParam_accessor npcNoteParam = TableAccessor.NpcNoteParam;
            NpcInfo.NpcData[] npcData = Extensions.GetNpcData(SingletonMonoBehaviour<GameInfo>.Instance.NpcInfo);
            string[] npcNames = new string[npcData.Length+1];
            npcNames[0] = "None";
            for (int i = 1; i < npcData.Length; i++)
            {
                npcNames[i] = Msg.MessageManager.Instance.GetMessage((int)npcNoteParam.get_name_msg(i));
            }

            goonmenu.AddButton("Print NPCData to Console", () => {
                GoonHW_WOA.Log.LogInfo($"== [NPCData] Length: {npcData.Length} ==");
                for (int i = 1; i < npcData.Length; i++)
                {
                    int nameMessageId = (int)npcNoteParam.get_name_msg(i);
                    GoonHW_WOA.Log.LogInfo($"{Msg.MessageManager.Instance.GetMessage(nameMessageId)}\t\tFavorability: {npcData[i]._favorability}\t\tGifted: {(npcData[i]._gift ? "Yes" : "No")}\tTalked: {(npcData[i]._talked ? "Yes" : "No")}");
                }
            });

            int selected = 0;
            goonmenu.AddDropDown("Relation NPC", npcNames, (int i) => {
                GoonHW_WOA.Log.LogInfo($"Selected {i}");
                selected = i;
            }, 0, () => {
                if(selected == 0 || selected > 72) return;
                GoonHW_WOA.Log.LogInfo($"Adding 0.1 to Favorability for {npcNames[selected]}");
                SingletonMonoBehaviour<GameInfo>.Instance.NpcInfo.AddFavorability(selected, 0.1f);
            });

            goonmenu.AddButton("Dump Items", () => {
                DumpItems();
            });

            goonmenu.AddButton("Set Inventory Slots to 25", () => {
                item.ItemInfo ii = SingletonMonoBehaviour<GameInfo>.Instance.ItemInfo;
                ii.ChangeCapacity(ITEMCATEGORYGROUP.Defs.Items, 25);
                ii.ChangeCapacity(ITEMCATEGORYGROUP.Defs.Foods, 25);
                ii.ChangeCapacity(ITEMCATEGORYGROUP.Defs.Materials, 25);
            });

            goonmenu.SetActionToBackButton(true, () => {DebugMenuWindowConstructorBaseMethods.CloseDebugSubWindow(__instance);});
            goonmenu.FinalizeWindow();

            // Populate any data here.
        });
    }

    private static void DumpItems()
    {
        ItemParam_accessor itemParam = TableAccessor.ItemParam;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Name,UID,Quality");
        for(int i = 1; i < itemParam.GetSize(); i++)
        {
            var item = itemParam.GetDataFromIndex(i);
            string name = Msg.MessageManager.Instance.GetMessage((int)item.name);
            sb.AppendLine($"{name},{item.uid},{item.quality+1}");
        }
        File.WriteAllText("HMWOA-Items.csv", sb.ToString());
    }
}