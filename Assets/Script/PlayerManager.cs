using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Land landPrefab;
    [SerializeField] Road roadPrefab;
    [SerializeField] int initialLandCount = 5;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewsDistance = -4;
    [SerializeField] int frontViewsDistance = 15;
    [SerializeField, Range (0, 1)] float cactusProbability;
    
    private void Start()
    {
        //generate start position & backward
        for (int zPos = backViewsDistance; zPos < initialLandCount; zPos++)
        {
            var land = Instantiate(landPrefab);
            land.transform.position = new Vector3(0,0,zPos);
            land.setCactusPercentage(zPos < -2 ? 1 : 0);
            land.Generate(horizontalSize);
        }

        //generate forward postion
        for (int zPos = initialLandCount; zPos < frontViewsDistance; zPos++)
        {
            var terrain = Instantiate(roadPrefab);
            
            terrain.transform.position = new Vector3(0,0,zPos);
            
            terrain.Generate(horizontalSize);
        }
    }
}