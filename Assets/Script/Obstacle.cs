using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    static HashSet<Vector3> positionSet = new HashSet<Vector3>();
    public static HashSet<Vector3> Position { get => new HashSet<Vector3>(positionSet); }

    private void onEnable()
    {
        positionSet.Add(this.transform.position);
    }

    private void onDisable()
    {
        positionSet.Remove(this.transform.position);
    }
}
