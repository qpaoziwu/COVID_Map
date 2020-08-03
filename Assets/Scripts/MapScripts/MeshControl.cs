using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class MeshControl : MonoBehaviour
{
    [Header("Show/Hide Mesh")]
    [Tooltip("Enable to show districts")]
    public bool DistrictMesh = true;
    [Tooltip("Enable to show borders")]
    public bool BorderMesh = true;
    [Tooltip("Enable to show heatmap points")]
    public bool PointMesh = true;
    [Header("Material Settings")]
    [SerializeField]
    [Tooltip("The Material for districts")]
    private Material DistrictMaterial = null;
    [SerializeField]
    [Tooltip("The Material for borders")]
    private Material BorderMaterial = null;
    [SerializeField]
    [Tooltip("The Material for points")]
    private Material PointMaterial = null;
    [Header("References")]
    [SerializeField]
    [Tooltip("The GameObject that contains all the district meshes")]
    private GameObject DistrictFolder = null; //Districts from scene
    [SerializeField]
    [Tooltip("The GameObject that contains all the border meshes")]
    private GameObject BorderFolder = null; //Border from scene
    [SerializeField]
    [Tooltip("The GameObject that contains all the heatmap points")]
    private GameObject PointFolder = null; //Border from scene
    void Update()
    {
        CheckAndEnableMesh();
    }
    void CheckAndEnableMesh()
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
    void ChangeMaterial()
    {
        if (DistrictMaterial && BorderMaterial && PointMaterial)//null check
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


}


