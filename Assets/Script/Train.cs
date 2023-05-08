using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField, Range (0,max: 10)] float speed;

    Vector3 initialPositionTrain;
    float distanceLimitTrain = float.MaxValue;

    public void SetUpDistanceLimitTrain(float limitTrain)
    {
        this.distanceLimitTrain = limitTrain;
    }

    private void Start()
    {
        initialPositionTrain = this.transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if((byte)Vector3.Distance(initialPositionTrain, this.transform.position) >= this.distanceLimitTrain)
            Destroy(this.gameObject);
    }
}
