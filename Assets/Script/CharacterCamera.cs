using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField, Range (0, 1)] float moveDuration = 0.2f;

    private void Start()
    {
        offset = this.transform.position;
    }

    public void UpdatePosition(Vector3 targetPosition)
    {
        DOTween.Kill(this.transform);
        transform.DOMove(offset + targetPosition, moveDuration);
    }
}
