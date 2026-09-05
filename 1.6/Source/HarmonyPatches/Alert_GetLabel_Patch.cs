using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Alert), nameof(Alert.GetLabel))]
    public static class Alert_GetLabel_Patch
    {
        public static void Postfix(Alert __instance, ref string __result)
        {
            if (__instance is Alert_DeathrestComplete complete && complete.targets.All(p => p.HasActiveGene(DefsOf.Goji_PeaceRest)))
            {
                __result = "Goji_AlertPeaceRestComplete".Translate();
            }
        }
    }
}
