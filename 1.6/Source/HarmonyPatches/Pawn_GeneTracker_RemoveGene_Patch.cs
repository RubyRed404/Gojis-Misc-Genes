using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(Pawn_GeneTracker), nameof(Pawn_GeneTracker.RemoveGene))]
    public static class Pawn_GeneTracker_RemoveGene_Patch
    {
        public static void Postfix(Gene gene, Pawn_GeneTracker __instance)
        {
            if (gene.def.exclusionTags?.Contains("Tail") is not true) return;
            var nineLives = __instance.GetGene(DefsOf.Goji_NineLives);
            if (nineLives?.Active is not false) return;
            var deathRefusal = __instance.pawn.health.hediffSet.GetFirstHediff<Hediff_DeathRefusal>();
            if (deathRefusal?.UsesLeft > 0)
            {
                deathRefusal.SetUseAmountDirect(0);
            }
        }
    }
}
