using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPos : MonoBehaviour
{
    public bool check()
    {
        Vector3 down = transform.TransformDirection(Vector3.down);

        return (Physics.Raycast(transform.position, down, 5f));
    }
}
