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
                        int probability = Random.Range(0, 100);
                        
                        switch (probability)
                        {
                            case < 70:
                                Debug.Log("grass");
                            break;
                            
                            case  < 90:
                                Debug.Log("grassWithStone");
                            break;
                            
                            default:
                                Debug.Log("grassWithTree");
                            break;
                        }
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
