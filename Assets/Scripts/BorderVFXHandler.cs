using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderVFXHandler : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem BorderVFX = null;
    [SerializeField]
    private DistrictReference Reference = null;
    [SerializeField]
    private MeshFilter BorderMesh = null;

    void Start()
    {
        DisableVFX();
    }

    private void Update()
    {
        MouseInput();
    }
    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Districts")))
            {
                if (Reference.Districts.Contains(hit.transform.gameObject))
                {
                    SwitchDistrict(Reference.Districts.IndexOf(hit.transform.gameObject));
                }

            }
            else
            {
                DisableVFX();
            }
        }
    }
    void SwitchDistrict(int i)
    {
        DisableVFX();
        var borderShape = BorderVFX.shape;
        borderShape.mesh = Reference.RefList[i].Border.GetComponent<MeshFilter>().sharedMesh;
        BorderMesh.mesh = Reference.RefList[i].Border.GetComponent<MeshFilter>().sharedMesh;
        BorderVFX.gameObject.SetActive(true);
    }

    void DisableVFX()
    {
        BorderVFX.gameObject.SetActive(false);
    }

}
