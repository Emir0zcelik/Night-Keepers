using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    public class EnvironmentSpawner : MonoBehaviour
    {
        [Header("-----------------------------------Probabilities Out Of 1000-----------------------------------")]
        [Header("----------Grass----------")]
        [SerializeField] private float grassBigRocksPercantage;
        [SerializeField] private float grassTreePercantage;
        [SerializeField] private float grassStonesPercantage;
        [SerializeField] private float grassLavendersPercantage;
        [Header("----------Wood----------")]
        [SerializeField] private float woodBigStonePercantage;
        [SerializeField] private float woodStonePercantage;
        [Header("----------Stone----------")]
        [SerializeField] private float stoneBigPercantage;
        [Header("-------------------------------------------------PREFABS-------------------------------------------------")]
        [Header("-----Grass-----")]
        [SerializeField] private Building[] grassBigRocks;
        [SerializeField] private Building[] grassTree;
        [SerializeField] private Building[] grassStones;
        [SerializeField] private Building[] grassLavenders;
        [SerializeField] private Building[] grassDefault;
        [Header("-----Wood-----")]
        [SerializeField] private Building[] woodBigStone;
        [SerializeField] private Building[] woodStone;
        [SerializeField] private Building[] woodDefault;
        [Header("-----Stone-----")]
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
                    
                    Building environment = null;

                    if (GridManager.Instance._grid[x,z].tileType == TileType.Grass)
                    {                        
                        int nullProbability = Random.Range(0, 100);

                        if (nullProbability < 15)
                        {
                            continue;
                        }

                        if (probability < grassBigRocksPercantage)
                        {
                            environment = Instantiate(grassBigRocks[Random.Range(0, grassBigRocks.Length)], spawnPosition, randomRotation);
                        }
                        else if (probability > grassBigRocksPercantage && probability < grassBigRocksPercantage + grassTreePercantage)
                        {
                            environment = Instantiate(grassTree[Random.Range(0, grassTree.Length)], spawnPosition, randomRotation);
                        }
                        else if (probability > grassBigRocksPercantage + grassTreePercantage && probability < grassBigRocksPercantage + grassTreePercantage + grassStonesPercantage)
                        {
                            environment = Instantiate(grassStones[Random.Range(0, grassStones.Length)], spawnPosition, randomRotation);
                        }
                        else if (probability > grassBigRocksPercantage + grassTreePercantage + grassStonesPercantage && probability < grassBigRocksPercantage + grassTreePercantage + grassStonesPercantage + grassLavendersPercantage)
                        {
                            environment = Instantiate(grassLavenders[Random.Range(0, grassLavenders.Length)], spawnPosition, randomRotation);
                        }
                        else
                        {
                            environment = Instantiate(grassDefault[Random.Range(0, grassDefault.Length)], spawnPosition, randomRotation);
                        }
                    }        

                    if (GridManager.Instance._grid[x,z].tileType == TileType.Wood)
                    {
                        if (probability < woodBigStonePercantage)
                        {
                            environment = Instantiate(woodBigStone[Random.Range(0, woodBigStone.Length)], spawnPosition, randomRotation);
                        }
                        else if (probability > woodBigStonePercantage && probability < woodBigStonePercantage + woodStonePercantage)
                        {
                            environment = Instantiate(woodStone[Random.Range(0, woodStone.Length)], spawnPosition, randomRotation);
                        }
                        else
                        {
                            environment = Instantiate(woodDefault[Random.Range(0, woodDefault.Length)], spawnPosition, randomRotation);
                        }
                    }

                    if (GridManager.Instance._grid[x,z].tileType == TileType.Rock)
                    {
                        if (probability < stoneBigPercantage)
                        {
                            environment = Instantiate(stoneBig[Random.Range(0, stoneBig.Length)], spawnPosition, randomRotation);
                        }
                        else
                        {
                            environment = Instantiate(stoneDefault[Random.Range(0, stoneDefault.Length)], spawnPosition, randomRotation);
                        }
                    } 

                    Tile tile = new Tile()
                    {
                        building = environment,
                        tileType = GridManager.Instance._grid[gridPosition].tileType,
                    };
                    GridManager.Instance._grid[gridPosition] = tile;
                }
            }
        }
    }
}
