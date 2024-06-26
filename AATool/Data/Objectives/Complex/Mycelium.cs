﻿using System.Xml;
using AATool.Data.Progress;

namespace AATool.Data.Objectives.Complex
{
    public class Mycelium : ComplexObjective
    {
        public const string BlockId = "minecraft:mycelium";

        public Mycelium() : base() 
        {
            this.Icon = "mycelium";
        }

        private bool obtained;
        private bool placed;

        protected override void UpdateAdvancedState(ProgressState progress)
        {
            this.obtained = progress.WasPickedUp(BlockId);
            this.placed = progress.WasUsed(BlockId);
            this.CompletionOverride = this.obtained || this.placed;
        }

        protected override void ClearAdvancedState()
        {
            this.obtained = false;
            this.placed = false;
        }

        protected override string GetShortStatus()
        {
            if (this.placed)
                return "Placed";
            if (this.obtained)
                return "Obtained";         
            return "0";
        }

        protected override string GetLongStatus()
        {
            if (this.placed)
                return "Mycelium\nPlaced";
            if (this.obtained)
                return "Mycelium\nObtained";       
            return "Obtain\nMycelium";
        }
    }
}
