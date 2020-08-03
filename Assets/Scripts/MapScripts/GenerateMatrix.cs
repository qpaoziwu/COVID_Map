using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMatrix : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField]
    private int xQuantity = 1;
    [SerializeField]
    private int yQuantity = 1;
    [SerializeField]
    private int zQuantity = 1;
    [SerializeField]
    private float spawnOffset = 1;
    public bool scatter;
    [SerializeField]
    private GameObject SpawnObject = null;
    private List<GameObject> PointList = new List<GameObject>();
    private int spawnTarget;
    private Transform SpawnParent;
    [Header("Spawn Results")]

    [SerializeField]
    private int count = 0;
    [SerializeField]
    private int spawned = 0;

    void Awake()
    {
        spawnTarget = xQuantity * yQuantity * zQuantity;

        for (int x = 0; x < xQuantity; x++)
        {
            for (int y = 0; y < yQuantity; y++)
            {
                for (int z = 0; z < zQuantity; z++)
                {
                    Vector3 checkPos = Points(x, y, z);

                    if (RayBelow(checkPos))
                    {
                        PointList.Add(Instantiate(SpawnObject));
                        PointList[spawned].transform.position = checkPos;
                        PointList[spawned].transform.parent = SpawnParent.transform;
                        PointList[spawned].name = spawned.ToString();
                        spawned++;
                    }
                    count++;
                }
            }
        }
        //StartCoroutine(Generate());
    }
    public bool RayBelow(Vector3 v)
    {
        Vector3 down = Vector3.down;
        bool check = false;
        RaycastHit ray;
        if(Physics.Raycast(v, down, out ray, 5f))
        {
            SpawnParent = ray.transform;
            check = true;
        }
        return check;
    }

    Vector3 Points(int x, int y, int z)
    {
        if (scatter)
        {
            return new Vector3(spawnOffset * x + Random.Range(-spawnOffset * .25f, spawnOffset * .25f), spawnOffset * z, spawnOffset * y + Random.Range(-spawnOffset * .25f, spawnOffset * .25f)) + SpawnObject.transform.position;
        }
        else
            return new Vector3(spawnOffset * x, spawnOffset * z, spawnOffset * y) + SpawnObject.transform.position;
    }
    // private IEnumerator Generate()
    // {
    //     WaitForSeconds wait = new WaitForSeconds(0.0001f);

    //     for (int x = 0; x < xQuantity; x++)
    //     {
    //         for (int y = 0; y < yQuantity; y++)
    //         {
    //             for (int z = 0; z < zQuantity; z++)
    //             {
    //                 Vector3 checkPos = new Vector3(spawnOffset * x + Random.Range(-spawnOffset, spawnOffset), spawnOffset * z, spawnOffset * y + Random.Range(-spawnOffset, spawnOffset)) + Sphere.transform.position;
    //                 if(check(checkPos))
    //                 {
                        // PointList.Add(Instantiate(ReferencePoint));
                        // PointList[spawned].transform.position = checkPos;
                        // PointList[spawned].transform.parent = SpawnParent.transform;
                        // PointList[spawned].name = spawned.ToString();
                        // spawned++;
    //                 }
    //                     count++;

    //                 yield return wait;
    //             }
    //         }
    //     }
    // }
}
