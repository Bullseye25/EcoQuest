using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformOperator : MonoBehaviour
{
    [SerializeField] private Transform actor;
    [SerializeField] private Transform endPoint;

    private Vector3 originPoint;

    private void Start()
    {
        originPoint = actor.position;
    }

    public void SetPosition()
    {
        actor.position = endPoint.position;
    }

    public void SetDefault()
    {
        actor.position = originPoint;
    }
}
