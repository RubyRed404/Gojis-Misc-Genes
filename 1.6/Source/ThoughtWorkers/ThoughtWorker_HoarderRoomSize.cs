using RimWorld;
using Verse;

namespace GojisMiscGenes
{
    public class ThoughtWorker_HoarderRoomSize : ThoughtWorker
    {
        public override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (!p.HasActiveGene(DefsOf.Goji_Hoarder)) return ThoughtState.Inactive;
            return p.needs.roomsize.CurCategory switch {
                RoomSizeCategory.VeryCramped => ThoughtState.ActiveAtStage(0),
                RoomSizeCategory.Cramped => ThoughtState.ActiveAtStage(1),
                RoomSizeCategory.Normal => ThoughtState.Inactive, // this line could be removed.
                RoomSizeCategory.Spacious => ThoughtState.ActiveAtStage(2),
                _ => ThoughtState.Inactive,
            }
        }
    }
}
