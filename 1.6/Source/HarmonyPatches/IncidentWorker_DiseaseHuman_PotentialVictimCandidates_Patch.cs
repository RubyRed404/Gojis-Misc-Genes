using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    [HarmonyPatch(typeof(IncidentWorker_DiseaseHuman), nameof(IncidentWorker_DiseaseHuman.PotentialVictimCandidates))]
    public static class IncidentWorker_DiseaseHuman_PotentialVictimCandidates_Patch
    {
        public static void Postfix(IncidentWorker_DiseaseHuman __instance, ref IEnumerable<Pawn> __result)
        {
            if (ModsConfig.RoyaltyActive && (__instance.def is DefsOf.Disease_BloodRot or DefsOf.Disease_Abasia))
            {
                __result = __result.Where(p => p.HasActiveGene(DefsOf.Goji_ExoticDiseaseProne));
                return;
            }
            if (__instance.def is not (DefsOf.Disease_FibrousMechanites and DefsOf.Disease_SensoryMechanites))
            {
                return;
            }

            var pawnsToAdd = new List<Pawn>();
            foreach (var pawn in __result)
            {
                if (pawn.HasActiveGene(DefsOf.Goji_MechaniteProne))
                {
                    pawnsToAdd.Add(pawn);
                    pawnsToAdd.Add(pawn);
                }
            }

            if (pawnsToAdd.Any())
            {
                __result = __result.Concat(pawnsToAdd);
            }
        }
    }
}
