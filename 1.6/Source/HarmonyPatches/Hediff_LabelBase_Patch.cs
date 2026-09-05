using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Hediff), nameof(Hediff.LabelBase), MethodType.Getter)]
    public static class Hediff_LabelBase_Patch
    {
        public static void Postfix(Hediff __instance, ref string __result)
        {
            if (__instance.def == HediffDefOf.Deathrest && __instance.pawn.HasActiveGene(DefsOf.Goji_PeaceRest))
            {
                __result = "Goji_PeaceResting".Translate();
            }
        }
    }
}
