using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    public class EnvironmentSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> trees;
        [SerializeField] private List<GameObject> rocks;
        [SerializeField] private List<GameObject> grasses;
        [SerializeField] private List<GameObject> grassesWithStones;
        [SerializeField] private List<GameObject> grassesWithTrees;

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
                        Instantiate(grasses[Random.Range(0, grasses.Count)], spawnPosition, Quaternion.identity);
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
