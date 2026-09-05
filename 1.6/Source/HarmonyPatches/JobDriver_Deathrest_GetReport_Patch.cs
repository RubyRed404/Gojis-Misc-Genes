using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(JobDriver_Deathrest), nameof(JobDriver_Deathrest.GetReport))]
    public static class JobDriver_Deathrest_GetReport_Patch
    {
        public static void Postfix(JobDriver_Deathrest __instance, ref string __result)
        {
            if (__instance.pawn.HasActiveGene(DefsOf.Goji_PeaceRest))
            {
                var gene = __instance.pawn.genes?.GetFirstGeneOfType<Gene_Deathrest>();
                if (gene != null)
                {
                    var percent = gene.DeathrestPercent;
                    __result = "Goji_PeaceRestingCap".Translate() + ": " + (percent < 1f ? percent.ToStringPercent("F0") : "Complete".Translate().CapitalizeFirst().Resolve());
                }
            }
        }
    }
}
