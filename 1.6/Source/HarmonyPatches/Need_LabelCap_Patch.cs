using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Need), nameof(Need.LabelCap), MethodType.Getter)]
    public static class Need_LabelCap_Patch
    {
        public static void Postfix(Need __instance, ref string __result)
        {
            if (__instance is Need_Deathrest { pawn: var pawn } && pawn.HasActiveGene(DefsOf.Goji_PeaceRest))
            {
                __result = "Goji_PeaceRestCap".Translate();
            }
        }
    }
}
