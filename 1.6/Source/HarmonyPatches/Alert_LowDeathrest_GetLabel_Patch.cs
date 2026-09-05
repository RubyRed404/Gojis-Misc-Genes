using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Alert_LowDeathrest), nameof(Alert_LowDeathrest.GetLabel))]
    public static class Alert_LowDeathrest_GetLabel_Patch
    {
        public static void Postfix(Alert_LowDeathrest __instance, ref string __result)
        {
            if (__instance.targets.All(t => t.Thing is Pawn pawn && pawn.HasActiveGene(DefsOf.Goji_PeaceRest)))
            {
                var labels = __instance.targetLabels;
                __result = labels.Count == 1
                    ? "Goji_AlertLowPeaceRestPawn".Translate(labels[0].Named("PAWN"))
                    : "Goji_AlertLowPeaceRestPawns".Translate(labels.Count.ToStringCached().Named("NUMCULPRITS"));
            }
        }
    }
}
