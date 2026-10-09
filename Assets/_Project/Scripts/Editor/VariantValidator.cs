using System;
using System.Collections.Generic;
using Data.Core;
using Data.Variant;
using Playable.Core;
using UnityEngine;
using Utilities;

namespace Playable.Editor
{
    public static class VariantValidator
    {
        public static List<string> Validate(PlayableVariantConfig variant)
        {
            List<string> errors = new List<string>();
            if (variant == null) { errors.Add("Select a variant."); return errors; }
            if (string.IsNullOrWhiteSpace(variant.variantId)) errors.Add("Variant ID is required.");
            if (variant.visualTheme == null || variant.visualTheme.sharedMaterial == null) errors.Add("Theme and shared material are required.");
            if (variant.adFlowConfig == null) errors.Add("Ad flow is required.");
            if (variant.moveSpeed <= 0 || variant.exitDuration <= 0) errors.Add("Animation timings must be positive.");
            var level = variant.levelConfig;
            if (level == null) { errors.Add("Level is required."); return errors; }
            if (level.cells == null || level.blocks == null || level.exits == null) { errors.Add("Level lists cannot be null."); return errors; }
            try { new GridBoard(level.width, level.height, level.cells.ToArray(), level.blocks.ToArray(), level.exits.ToArray()); }
            catch (ArgumentException exception) { errors.Add(exception.Message); }
            HashSet<string> ids = new HashSet<string>();
            for (int i = 0; i < level.blocks.Count; i++)
            {
                var block = level.blocks[i];
                if (block == null) continue;
                if (string.IsNullOrWhiteSpace(block.id) || !ids.Add(block.id)) errors.Add("Block IDs must be nonempty and unique: " + i);
                if (BlockShapeUtility.HasDuplicateCells(block) || !BlockShapeUtility.IsConnected(block)) errors.Add("Block must be a connected shape without duplicate cells: " + block.id);
            }
            HashSet<Vector2Int> overrides = new HashSet<Vector2Int>();
            for (int i = 0; i < level.cells.Count; i++)
                if (level.cells[i] != null && !overrides.Add(level.cells[i].position)) errors.Add("Duplicate cell override: " + level.cells[i].position);
            ids.Clear();
            for (int i = 0; i < level.exits.Count; i++)
            {
                var gate = level.exits[i];
                if (gate == null) continue;
                if (string.IsNullOrWhiteSpace(gate.id) || !ids.Add(gate.id)) errors.Add("Gate IDs must be nonempty and unique: " + i);
                for (int j = 0; j < i; j++)
                {
                    var other = level.exits[j];
                    if (other != null && other.side == gate.side && gate.startIndex < other.startIndex + other.length && other.startIndex < gate.startIndex + gate.length)
                        errors.Add("Gates overlap: " + other.id + " / " + gate.id);
                }
            }
            if (variant.visualTheme != null && variant.visualTheme.palette != null)
            {
                HashSet<ColorId> palette = new HashSet<ColorId>();
                foreach (var entry in variant.visualTheme.palette)
                    if (!palette.Add(entry.colorId)) errors.Add("Duplicate palette color: " + entry.colorId);
            }
            var flow = variant.adFlowConfig;
            if (flow != null)
            {
                if (flow.endCardDelay < 0 || flow.ctaDelay < 0 || flow.tutorialDelay < 0) errors.Add("Delays cannot be negative.");
                if (flow.endCondition == EndCondition.OnTimerExpired && flow.timer <= 0) errors.Add("Timer must be positive.");
                if (flow.endCondition == EndCondition.OnMoveCountReached && flow.moveLimit < 1) errors.Add("Move limit must be positive.");
                if (flow.endCondition == EndCondition.OnTargetBlocksCleared && (flow.targetBlocksToClear < 1 || flow.targetBlocksToClear > level.blocks.Count)) errors.Add("Target clear count must be within the block count.");
            }
            return errors;
        }
    }
}
