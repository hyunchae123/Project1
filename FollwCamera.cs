using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollwCamera : MonoBehaviour
{
    [SerializeField] Transform target;

    void LateUpdate()
    {
        Vector3 pos = new Vector3(target.position.x, target.position.y + 2, -10f);
        transform.position = pos;
    }
}
