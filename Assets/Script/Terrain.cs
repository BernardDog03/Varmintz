using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    private void Start()
    {
        Generate(9);
    }
    [SerializeField] GameObject tilePrefabs;

    protected int horizontalSize;

    public virtual void Generate(int size)
    {
        horizontalSize = size;
        
        if(size == 0 )return;

        if((float)size % 2 == 0) size -= 1;

        int limit = Mathf.FloorToInt((float) size/2);

        for (int i = -limit; i <= limit; i++)
        {
            SpawanTile (i);
        }

        var leftBoundaryTile = SpawanTile(-limit -1);
        var RightBoundaryTile = SpawanTile(limit +1);
        DarkenObject(leftBoundaryTile);
        DarkenObject(RightBoundaryTile);
    }

    private GameObject SpawanTile(int xPos)
    {
        var go = Instantiate(tilePrefabs, transform);
            go.transform.localPosition = new Vector3(xPos, 0, 0);
        
        return go;
    }

    private void DarkenObject(GameObject go)
    {
        var renderers = go.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
        foreach (var rend in renderers)
        {
            rend.material.color = rend.material.color * Color.grey;
        }
    }
}
