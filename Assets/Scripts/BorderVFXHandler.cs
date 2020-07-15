using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderVFXHandler : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem BorderVFX;
    [SerializeField]
    private DistrictReference Reference;
    
    void Start()
    {
        DisableVFX();
    }

    private void Update() {
        MouseInput();
    }
    void MouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {   
            LayerMask DistrictLayer = 8;
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            if(Physics.Raycast (ray, out hit, 1000f, LayerMask.GetMask("Districts")))
            {
                if(Reference.Districts.Contains(hit.transform.gameObject))
                {
                    print(hit.transform.gameObject);
                    print(Reference.Districts.IndexOf(hit.transform.gameObject));
                    SwitchDistrict(Reference.Districts.IndexOf(hit.transform.gameObject));

                }

            }else
            {
                DisableVFX();
            }
        }
    }
    void SwitchDistrict(int i)
    {
        DisableVFX();
        var borderShape = BorderVFX.shape;
        borderShape.mesh = Reference.RefList[i].Border.GetComponent<MeshFilter>().mesh;
        BorderVFX.gameObject.SetActive(true);
    }

    void DisableVFX()
    {
        BorderVFX.gameObject.SetActive(false);
    }

}
