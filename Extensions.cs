using HarmonyLib;
using npc;

namespace GoonHM_WOA
{
    public static class Extensions
    {
            public static NpcInfo.NpcData[] GetNpcData(this NpcInfo npcInfo)
            {
                return (NpcInfo.NpcData[])AccessTools.Field(typeof(NpcInfo), "Data").GetValue(npcInfo);
            }
    }
}