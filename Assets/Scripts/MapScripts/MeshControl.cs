using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class MeshControl : MonoBehaviour
{
    [SerializeField]
    private GameObject DistrictFolder = null; //Districts from scene
    [SerializeField]
    private GameObject BorderFolder = null; //Border from scene
    [SerializeField]
    private GameObject PointFolder = null; //Border from scene

    [SerializeField]
    private Material DistrictMaterial = null;
    [SerializeField]
    private Material BorderMaterial = null;
    [SerializeField]
    private Material PointMaterial = null;
    public bool DistrictMesh = true;
    public bool BorderMesh = true;
    public bool PointMesh = true;


    void ChangeMaterial()
    {
        if (DistrictMaterial && BorderMaterial && PointMaterial)
        {
            if (DistrictMaterial != DistrictFolder.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial)
            {
                foreach (Transform d in DistrictFolder.transform)
                {
                    if (d.GetComponent<MeshRenderer>())
                    {
                        d.GetComponent<MeshRenderer>().sharedMaterial = DistrictMaterial;
                    }
                }
            }
            if (BorderMaterial != BorderFolder.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial)
            {
                foreach (Transform d in BorderFolder.transform)
                {
                    if (d.GetComponent<MeshRenderer>())
                    {
                        d.GetComponent<MeshRenderer>().sharedMaterial = BorderMaterial;
                    }
                }
            }
            if (PointFolder.transform.childCount > 0)
            {
                if (PointMaterial != PointFolder.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial)
                {
                    foreach (Transform d in PointFolder.transform)
                    {
                        if (d.GetComponent<MeshRenderer>())
                        {
                            d.GetComponent<MeshRenderer>().sharedMaterial = PointMaterial;
                        }
                    }
                }
            }
        }
    }


    void Update()
    {
        if (DistrictFolder && BorderFolder && PointFolder)
        {
            if (!DistrictMesh)
            {
                if (DistrictFolder.transform.GetChild(0).GetComponent<MeshRenderer>().enabled)
                {
                    foreach (Transform d in DistrictFolder.transform)
                    {
                        if (d.GetComponent<MeshRenderer>())
                        {
                            d.GetComponent<MeshRenderer>().enabled = false;
                        }
                    }
                }
            }
            else
            {
                if (!DistrictFolder.transform.GetChild(0).GetComponent<MeshRenderer>().enabled)
                {
                    foreach (Transform d in DistrictFolder.transform)
                    {
                        if (d.GetComponent<MeshRenderer>())
                        {
                            d.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                }
            }

            if (!BorderMesh)
            {
                if (BorderFolder.transform.GetChild(0).GetComponent<MeshRenderer>().enabled)
                {
                    foreach (Transform d in BorderFolder.transform)
                    {
                        if (d.GetComponent<MeshRenderer>())
                        {
                            d.GetComponent<MeshRenderer>().enabled = false;
                        }
                    }
                }
            }
            else
            {
                if (!BorderFolder.transform.GetChild(0).GetComponent<MeshRenderer>().enabled)
                {
                    foreach (Transform d in BorderFolder.transform)
                    {
                        if (d.GetComponent<MeshRenderer>())
                        {
                            d.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                }
            }
            if (!PointMesh && PointFolder.transform.childCount > 0)
            {
                if (PointFolder.transform.GetChild(0).GetComponent<MeshRenderer>().enabled)
                {
                    foreach (Transform d in PointFolder.transform)
                    {
                        if (d.GetComponent<MeshRenderer>())
                        {
                            d.GetComponent<MeshRenderer>().enabled = false;
                        }
                    }
                }
            }
            else if (PointMesh && PointFolder.transform.childCount > 0)
            {
                if (!PointFolder.transform.GetChild(0).GetComponent<MeshRenderer>().enabled)
                {
                    foreach (Transform d in PointFolder.transform)
                    {
                        if (d.GetComponent<MeshRenderer>())
                        {
                            d.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                }
            }
            ChangeMaterial();
        }
    }

}


