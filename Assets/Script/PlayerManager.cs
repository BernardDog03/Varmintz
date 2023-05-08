using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Character chara;
    [SerializeField] List<Terrain> terrainList;
    [SerializeField] int initialLandCount = 5;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewsDistance = -4;
    [SerializeField] int frontViewsDistance = 15;
    [SerializeField, Range (0, 1)] float cactusProbability;

    Dictionary<int, Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);
    [SerializeField] private int travelDistance;
    [SerializeField] private int coin;

    public UnityEvent<int, int> OnUpdateTerrainLimit;
    public UnityEvent <int> OnScoreUpdate;

    private void Start()
    {
        //generate start position & backward
        for (int zPos = backViewsDistance; zPos < initialLandCount; zPos++)
        {
            var terrain = Instantiate(terrainList[0]);
            terrain.transform.position = new Vector3(0,0,zPos);

            if(terrain is Land land)
                land.setCactusPercentage(zPos < -1 ? 1 : 0);

            terrain.Generate(horizontalSize);

            activeTerrainDict[zPos] = terrain;
        }


        //generate forward postion
        for (int zPos = initialLandCount; zPos < frontViewsDistance; zPos++)
        {
            SpawnRandomTerrain(zPos);
        }

        OnUpdateTerrainLimit.Invoke(horizontalSize, travelDistance + backViewsDistance);
        
    }


    private Terrain SpawnRandomTerrain(int zPos)
    {
        Terrain terrainCheck = null;
        int randomIndex;

        for (int z = -1; z >= -3; z--)
        {
            var checkPos = zPos + z;
            if(terrainCheck == null)
            {
                terrainCheck = activeTerrainDict[checkPos];
                continue;
            }
            else if (terrainCheck.GetType() != activeTerrainDict[checkPos].GetType())
            {
                randomIndex = Random.Range(0, terrainList.Count);
                return SpawnTerrain(terrainList[randomIndex], zPos);
            }
            else
            {
                continue;
            }
        }
        var candidateTerrain = new List<Terrain>(terrainList);

        for (int i = 0; i < candidateTerrain.Count; i++)
        {
            if(terrainCheck.GetType() == candidateTerrain[i].GetType())
            {
                candidateTerrain.Remove(candidateTerrain[i]);
                break;
            }
        }

        randomIndex = Random.Range(0, candidateTerrain.Count);
        return SpawnTerrain(candidateTerrain[randomIndex], zPos);
    }

    public Terrain SpawnTerrain(Terrain terrain, int zPos)
    {
        terrain = Instantiate(terrain);
        terrain.transform.position = new Vector3(0,0,zPos);

        terrain.Generate(horizontalSize);
        activeTerrainDict[zPos] = terrain;
        return terrain;
    }

    public void UpdateTravelDistance(Vector3 targetPosition)
    {
        if(targetPosition.z > travelDistance)
        {
            travelDistance = Mathf.CeilToInt(targetPosition.z);
            UpdateTerrain();
            OnScoreUpdate.Invoke(GetScore());
        }
    }

    public void AddCoin(int value = 1)
    {
        this.coin += value;
        OnScoreUpdate.Invoke(GetScore());
    }

    private int GetScore()
    {
        return travelDistance + coin*3;
    }

    public void UpdateTerrain()
    {
        var destroyPos = travelDistance - 1 + backViewsDistance;
        Destroy(activeTerrainDict[destroyPos].gameObject);
        activeTerrainDict.Remove(destroyPos);
        
        var spawnPosition = travelDistance - 1 + frontViewsDistance;
        SpawnRandomTerrain(spawnPosition);

        OnUpdateTerrainLimit.Invoke(horizontalSize, travelDistance + backViewsDistance);
    }
}