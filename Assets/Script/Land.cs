using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : Terrain
{
    [SerializeField] List<GameObject> cactusPrefabsList;
    [SerializeField, Range(0, 1)] float cactusProbability;

    public void setCactusPercentage(float newProbability)
    {
        this.cactusProbability = Mathf.Clamp01(newProbability);
    }

    public override void Generate(int size)
    {
        base.Generate(size);

        var limit = Mathf.FloorToInt((float)size/2);
        var catusCount = Mathf.FloorToInt((float) size * cactusProbability);
        
        List<int> emptyPosition = new List<int>();
        for (int i = -limit; i <= limit; i++)
        {
            emptyPosition.Add(i);            
        }

        
        for (int i = 0; i < catusCount; i++)
        {
            var randomIndex = Random.Range(0, emptyPosition.Count);
            var pos = emptyPosition[randomIndex];

            emptyPosition.RemoveAt(randomIndex);

            SpawnRandomCactus(pos);
        }
        SpawnRandomCactus(-limit -1);
        SpawnRandomCactus(limit + 1);
    }

    private void SpawnRandomCactus(int xPos)
    {
        var randomIndex = Random.Range(0, cactusPrefabsList.Count);
        var prefab = cactusPrefabsList[randomIndex];

        var cactus = Instantiate(
            prefab,
            new Vector3(xPos, 0, z: this.transform.position.z),
            Quaternion.identity,
            transform);
    }
}
