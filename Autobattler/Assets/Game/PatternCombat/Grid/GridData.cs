using System;
using UnityEngine;

namespace Game.PatternCombat.Grid
{
    [Serializable]
    public class GridData
    {
        public int x, y;
        public float worldX, worldY;
        public Vector2 worldPosition;
        public bool isWalkable = true;
    }
}