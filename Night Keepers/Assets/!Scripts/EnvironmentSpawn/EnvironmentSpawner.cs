using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    public class EnvironmentSpawner : MonoBehaviour
    {
        [Header("Grass")]
        [SerializeField] private Building[] grassBigRocks;
        [SerializeField] private Building[] grassTree;
        [SerializeField] private Building[] grassStones;
        [SerializeField] private Building[] grassLavenders;
        [SerializeField] private Building[] grassDefault;
        [Header("Wood")]
        [SerializeField] private Building[] woodBigStone;
        [SerializeField] private Building[] woodStone;
        [SerializeField] private Building[] woodDefault;
        [Header("Stone")]
        [SerializeField] private Building[] stoneBig;
        [SerializeField] private Building[] stoneDefault;


        private void Start() {
            for (int x = 0; x < GridManager.Instance.width; x++)
            {
                for (int z = 0; z < GridManager.Instance.height ; z++)
                {
                    if (GridManager.Instance._grid[x,z].building != null)
                        continue;
                    Vector2Int gridPosition = new Vector2Int(x, z);

                    Vector3 spawnPosition = GridManager.Instance._grid.GridToWorldPosition(gridPosition);
                    Quaternion randomRotation = Quaternion.Euler(0, (float)Random.Range(0, 360), 0);
                    
                    int probability = Random.Range(0, 1000);
                    
                    if (GridManager.Instance._grid[x,z].tileType == TileType.Grass)
                    {
                        
                        Building grass = null;
                        
                        int nullProbability = Random.Range(0, 100);
                        if (nullProbability < 15)
                        {
                            continue;
                        }
                        
                        switch (probability)
                        {
                            case < 4: 
                                grass = Instantiate(grassBigRocks[Random.Range(0, grassBigRocks.Length)], spawnPosition, randomRotation);
                            break;
                            case  < 9: 
                                grass = Instantiate(grassTree[Random.Range(0, grassTree.Length)], spawnPosition, randomRotation);
                            break;
                            case < 109:
                                grass = Instantiate(grassStones[Random.Range(0, grassStones.Length)], spawnPosition, randomRotation);
                            break;
                            case < 209:
                                grass = Instantiate(grassLavenders[Random.Range(0, grassLavenders.Length)], spawnPosition, randomRotation);
                            break;

                            default:
                                grass = Instantiate(grassDefault[Random.Range(0, grassDefault.Length)], spawnPosition, randomRotation);
                            break;
                        }
                        grass.buildingType = BuildingData.BuildingType.Environment;
                        Tile tile = new Tile()
                        {
                            building = grass,
                            tileType = GridManager.Instance._grid[gridPosition].tileType,
                        };
                        GridManager.Instance._grid[gridPosition] = tile;
                    }        

                    if (GridManager.Instance._grid[x,z].tileType == TileType.Wood)
                    {
                        Building wood = null;
                        switch (probability)
                        {
                            case < 50:
                                wood = Instantiate(woodBigStone[Random.Range(0, woodBigStone.Length)], spawnPosition, randomRotation);
                            break;
                            case < 200:
                                wood = Instantiate(woodStone[Random.Range(0, woodStone.Length)], spawnPosition, randomRotation);
                            break;
                            default:
                                wood = Instantiate(woodDefault[Random.Range(0, woodDefault.Length)], spawnPosition, randomRotation);
                            break;
                        }
                        Tile tile = new Tile()
                        {
                            building = wood,
                            tileType = GridManager.Instance._grid[gridPosition].tileType,
                        };
                        GridManager.Instance._grid[gridPosition] = tile;
                    }

                    if (GridManager.Instance._grid[x,z].tileType == TileType.Rock)
                    {
                        Building stone = null;
                        switch (probability)
                        {
                            case < 300:
                                stone = Instantiate(stoneBig[Random.Range(0, stoneBig.Length)], spawnPosition, randomRotation);
                            break;
                            default:
                                stone = Instantiate(stoneDefault[Random.Range(0, stoneDefault.Length)], spawnPosition, randomRotation);
                            break;
                        }
                        Tile tile = new Tile()
                        {
                            building = stone,
                            tileType = GridManager.Instance._grid[gridPosition].tileType,
                        };
                        GridManager.Instance._grid[gridPosition] = tile;
                    } 
                }
            }
        }
    }
}
