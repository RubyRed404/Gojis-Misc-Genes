using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace GojisMiscGenes
{
    public class GeneGizmo_PeaceRestCapacity : GeneGizmo_DeathrestCapacity
    {
        private const float PaddingConst = 6f;
        private const float DivisorConst = 3f;

        public GeneGizmo_PeaceRestCapacity(Gene_Deathrest gene) : base(gene)
        {
        }

        public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
        {
            var rect = new Rect(topLeft.x, topLeft.y, GetWidth(maxWidth), 75f);
            var position = rect.ContractedBy(PaddingConst);
            var num = position.height / DivisorConst;
            Widgets.DrawWindowBackground(rect);
            GUI.BeginGroup(position);
            Widgets.Label(new Rect(0f, 0f, position.width, num), "Goji_PeaceRestCap".Translate());
            if (gene.DeathrestNeed != null)
            {
                gene.DeathrestNeed.DrawOnGUI(new Rect(0f, num, position.width, num + 2f), int.MaxValue, 2f, drawArrows: false, doTooltip: true, new Rect(0f, 0f, position.width, num * 2f), drawLabel: false);
            }
            var rect2 = new Rect(0f, num * 2f, position.width, Text.LineHeight);
            Text.Anchor = TextAnchor.UpperCenter;
            Widgets.Label(rect2, string.Format("{0}: {1} / {2}", "Buildings".Translate().CapitalizeFirst(), gene.CurrentCapacity, gene.DeathrestCapacity));
            Text.Anchor = TextAnchor.UpperLeft;
            if (Mouse.IsOver(rect2))
            {
                Widgets.DrawHighlight(rect2);
                TooltipHandler.TipRegion(rect2, "DeathrestCapacityDesc".Translate() + "\n\n" + "PawnIsConnectedToBuildings".Translate(gene.pawn.Named("PAWN"), gene.CurrentCapacity.Named("CURRENT"), gene.DeathrestCapacity.Named("MAX")));
            }
            GUI.EndGroup();
            return new GizmoResult(GizmoState.Clear);
        }
    }

    public class Gene_PeaceRest : Gene_Deathrest
    {
        private GeneGizmo_PeaceRestCapacity peaceGizmo;

        public override IEnumerable<Gizmo> GetGizmos()
        {
            if (Active is false) yield break;
            peaceGizmo = peaceGizmo ?? new GeneGizmo_PeaceRestCapacity(this);
            if (Find.Selector.SelectedPawns.Count == 1) yield return peaceGizmo;
            foreach (var g in base.GetGizmos())
            {
                if (!(g is GeneGizmo_DeathrestCapacity)) yield return g;
            }
        }
    }
}
