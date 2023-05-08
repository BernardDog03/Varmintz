using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{    
    [SerializeField] Train trainPrefabs;
    [SerializeField] float minIntervalSpawn;
    [SerializeField] float maxIntervalSpawn;

    float timer;

    Vector3 spawnPositionTrain;

    Quaternion trainRotation;

    private void Start()
    {
        if(Random.value > 0.5f)
        {
            spawnPositionTrain = new Vector3(horizontalSize/2 + 3, 0, this.transform.position.z);
            trainRotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            spawnPositionTrain = new Vector3(-(horizontalSize/2 + 3), 0, this.transform.position.z);
            trainRotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void Update()
    {
        if(timer <= 0)
        {
            timer = Random.Range(
                minIntervalSpawn,
                maxIntervalSpawn);
            
            var train = Instantiate(trainPrefabs,
                        spawnPositionTrain,
                        trainRotation);

            train.SetUpDistanceLimitTrain(horizontalSize + 6);
            return;
        }

        timer = Time.deltaTime;
    }
}
