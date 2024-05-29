using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    public class EnvironmentSpawner : MonoBehaviour
    {
        [SerializeField] private List<Building> trees;
        [SerializeField] private List<Building> rocks;
        [SerializeField] private List<Building> grassList;
        [SerializeField] private List<Building> grassesWithStones;
        [SerializeField] private List<Building> grassesWithTrees;

        private void Start() {
            for (int x = 0; x < GridManager.Instance.width; x++)
            {
                for (int z = 0; z < GridManager.Instance.height ; z++)
                {
                    if (GridManager.Instance._grid[x,z].building != null)
                        continue;

                    Vector3 spawnPosition = GridManager.Instance._grid.GridToWorldPosition(new Vector2Int(x, z));
                    if (GridManager.Instance._grid[x,z].tileType == TileType.Grass)
                    {
                        Building grass = Instantiate(grassList[Random.Range(0, grassList.Count)], spawnPosition, Quaternion.identity);

                        Tile tile = new Tile()
                        {
                            building = grass,
                            tileType = GridManager.Instance._grid[new Vector2Int(x, z)].tileType,
                        };

                        GridManager.Instance._grid[new Vector2Int(x, z)] = tile;

                        int probability = Random.Range(0, 100);
                        
                        // int randomRotation = Quaternion.Euler()
                        // switch (probability)
                        // {
                        //     case < 70:
                        //         Instantiate(grasses[Random.Range(0, grasses.Count)], spawnPosition, Quaternion.identity);
                        //     break;
                            
                        //     case  < 90:
                        //         Instantiate(grassesWithStones[Random.Range(0, grassesWithStones.Count)], spawnPosition, Quaternion.identity);
                        //     break;
                            
                        //     default:
                        //         Instantiate(grassesWithTrees[Random.Range(0, grassesWithTrees.Count)], spawnPosition, Quaternion.identity);
                        //     break;
                        // }
                    }        

                    if (GridManager.Instance._grid[x,z].tileType == TileType.Wood)
                    {
                        // GameObject tree = Instantiate(trees[Random.Range(0, trees.Count)], spawnPosition, Quaternion.identity);
                    }

                    if (GridManager.Instance._grid[x,z].tileType == TileType.Rock)
                    {
                        // GameObject rock = Instantiate(trees[Random.Range(0, rocks.Count)], spawnPosition, Quaternion.identity);                        
                    } 
                }
            }
        }
    }
}
