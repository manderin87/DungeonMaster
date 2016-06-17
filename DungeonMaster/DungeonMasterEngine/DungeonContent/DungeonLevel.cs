﻿using System.Collections.Generic;
using DungeonMasterEngine.DungeonContent.Entity;
using DungeonMasterEngine.DungeonContent.GroupSupport;
using DungeonMasterEngine.DungeonContent.Items;
using DungeonMasterEngine.DungeonContent.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonMasterEngine.DungeonContent
{
    public class DungeonLevel
    {        
        private Dungeon dungeon;

        public Tile StartTile { get; }

        public int LevelIndex { get; }        

        public Texture2D MiniMap { get; }

        public IEnumerable<Tile> Tiles { get; }

        public List<ILiveEntity> Creatures { get; }

        public IReadOnlyDictionary<Point, Tile> TilesPositions { get; }

        public DungeonLevel(Dungeon d, List<ILiveEntity> creatures, int levelIndex, IReadOnlyDictionary<Point,Tile> positions, Tile startTile, Texture2D minimap)
        {
            TilesPositions = positions;

            dungeon = d;
            Tiles = positions.Values;
            StartTile = startTile;
            LevelIndex = levelIndex;
            MiniMap = minimap;
            Creatures = creatures;
        }
    }
}