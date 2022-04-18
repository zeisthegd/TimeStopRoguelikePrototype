using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using NaughtyAttributes;

namespace Penwyn.Game
{
    public class LevelBuilder : MonoBehaviour
    {
        public Tilemap Tilemap;
        public TileBase Tile;
        protected LevelGenerator _generator;

        public virtual void BuildMap(LevelGenerator generator)
        {
            _generator = generator;
            SetWallTiles();
        }

        public virtual void SetWallTiles()
        {
            int[,] walls = _generator.Map;
            Vector3Int tilePos;
            for (int x = 0; x < _generator.MapData.Width; x++)
            {
                for (int y = 0; y < _generator.MapData.Height; y++)
                {
                    if (walls[x, y] == 1)
                    {
                        tilePos = new Vector3Int(-_generator.MapData.Width / 2 + x, -_generator.MapData.Height / 2 + y);
                        Tilemap.SetTile(tilePos, Tile);
                    }
                }
            }
        }
    }
}

