using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BorderVFXHandler : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem BorderVFX = null;
    [SerializeField]
    private DistrictReference Reference = null;
        [SerializeField]
    private CameraControl CameraControl = null;
    [SerializeField]
    private MeshFilter BorderMesh = null;
    [SerializeField]
    private RaycastHit clickHit;
    [SerializeField]
    private RaycastHit hoverHit;
    [SerializeField]
    private Material BorderMaterial = null;
    [SerializeField]
    private Material MouseOverMaterial = null;
    [SerializeField]
    private GameObject LastRaycastHit = null;
    
    public GameObject LastClickedObject = null;
    [SerializeField]
    private Ray clickRay;
    
    [SerializeField]
    private Ray hoverRay;

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
        hoverRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject())
            return;
            clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(clickRay, out clickHit, 1000f, LayerMask.GetMask("Districts")))
            {
                if (Reference.Districts.Contains(clickHit.transform.gameObject))
                {
                    SwitchDistrict(Reference.Districts.IndexOf(clickHit.transform.gameObject));
                    LastClickedObject = clickHit.transform.gameObject;
                    CameraControl.SwitchToZoom();
                }
            }
            else
            {
                DisableVFX();
                LastClickedObject = null;
                CameraControl.SwitchToFullMap();
                CameraControl.resetCam = true;
            }
        }
        if (Physics.Raycast(hoverRay, out hoverHit, 1000f, LayerMask.GetMask("Districts")))
        {
            if(EventSystem.current.IsPointerOverGameObject())
            return;
            if(hoverHit.transform.gameObject)
            {
                if (Reference.Districts.Contains(hoverHit.transform.gameObject))
                {
                    MouseOverBorderVFX(hoverHit.transform.gameObject);
                }
            }
        }else
        {
            Reference.RefList[Reference.Districts.IndexOf(LastRaycastHit)].Border.GetComponent<MeshRenderer>().material = BorderMaterial;
        }

    }

    void MouseOverBorderVFX(GameObject raycastObj)
    {
        if(LastRaycastHit != null)
        {
            Reference.RefList[Reference.Districts.IndexOf(LastRaycastHit)].Border.GetComponent<MeshRenderer>().material = BorderMaterial;
            Reference.RefList[Reference.Districts.IndexOf(raycastObj)].Border.GetComponent<MeshRenderer>().material = MouseOverMaterial;
            LastRaycastHit = raycastObj;
        }
    }
    void SwitchDistrict(int i)
    {
        DisableVFX();
        var borderShape = BorderVFX.shape;
        BorderMesh.mesh = Reference.RefList[i].Border.GetComponent<MeshFilter>().sharedMesh;
        borderShape.mesh = Reference.RefList[i].Border.GetComponent<MeshFilter>().sharedMesh;
        BorderMesh.gameObject.SetActive(true);
        BorderVFX.gameObject.SetActive(true);
        BorderVFX.Play();
    }

    void DisableVFX()
    {
        BorderVFX.Stop();
        BorderMesh.gameObject.SetActive(false);
        BorderVFX.gameObject.SetActive(false);
    }

}
