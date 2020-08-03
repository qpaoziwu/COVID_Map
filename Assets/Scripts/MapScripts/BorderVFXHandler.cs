using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BorderVFXHandler : MonoBehaviour
{
    /*This Script handles mouse inputs like clicks and mouse overing
    It needs to have the reference script to know what it's clicking on
    and CameraControl script to switch camera focus 
    */
    [Header("Raycast Results")]
    [SerializeField]
    [Tooltip("The GameObject detected on hovering")]
    private GameObject LastRaycastHit = null;
    [Tooltip("The GameObject detected on clicking")]
    public GameObject LastClickedObject = null;
    [SerializeField]
    [Tooltip("The Ray for cursor clicking")]
    private Ray clickRay;
    [SerializeField]
    [Tooltip("The Ray for cursor hovering")]
    private Ray hoverRay;
    [SerializeField]
    [Tooltip("The raycast result for cursor clicking")]
    private RaycastHit clickHit;
    [SerializeField]
    [Tooltip("The raycast result for cursor hovering")]
    private RaycastHit hoverHit;
    [Header("Material Settings")]
    [SerializeField]
    [Tooltip("The default material on borders")]
    private Material BorderMaterial = null;
    [SerializeField]
    [Tooltip("The hovering material on borders")]
    private Material MouseOverMaterial = null;

    [Header("References")]
    [SerializeField]
    [Tooltip("The BorderVFX")]
    private ParticleSystem BorderVFX = null;
    [SerializeField]
    [Tooltip("Reference script that stores all the mesh info")]
    private DistrictReference Reference = null;
    [SerializeField]
    [Tooltip("Camera script that controls the camera")]
    private CameraControl CameraControl = null;
    [SerializeField]
    [Tooltip("The highlighting border")]
    private MeshFilter BorderMesh = null;
    void Start()
    {
        DisableVFX();//Reset VFX 
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
            if (EventSystem.current.IsPointerOverGameObject())//If cursor is on UI, return
                return;
            //Raycast logic
            clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(clickRay, out clickHit, 1000f, LayerMask.GetMask("Districts")))
            {
                if (Reference.Districts.Contains(clickHit.transform.gameObject))//If the object is in the reference
                {
                    SwitchDistrict(Reference.Districts.IndexOf(clickHit.transform.gameObject));// Switch VFX to the object clicked

                    LastClickedObject = clickHit.transform.gameObject;// Change camera focus to clicked district
                    CameraControl.SwitchToZoom();// Swtich camera mode to zoomed
                }
            }
            else
            {
                DisableVFX(); //Stop VFX particles
                CameraControl.SwitchToFullMap(); // Swtich camera mode to full map
                CameraControl.resetCam = true; // Reset camera position and rotation
            }
        }
        if (Physics.Raycast(hoverRay, out hoverHit, 1000f, LayerMask.GetMask("Districts")))
        {
            if (EventSystem.current.IsPointerOverGameObject())//If cursor is on UI, return
                return;
            if (hoverHit.transform.gameObject)
            {
                if (Reference.Districts.Contains(hoverHit.transform.gameObject))//If the object is in the reference
                {
                    MouseOverBorderVFX(hoverHit.transform.gameObject);// Switch VFX to the object hovered
                }
            }
        }
        else
        {// Reset last hovered object to default material
            Reference.RefList[Reference.Districts.IndexOf(LastRaycastHit)].Border.GetComponent<MeshRenderer>().material = BorderMaterial;
        }

    }

    void MouseOverBorderVFX(GameObject raycastObj)
    {//when cursor is on an obj, reset previous, set current, then store current as previous
        if (LastRaycastHit != null)
        {
            Reference.RefList[Reference.Districts.IndexOf(LastRaycastHit)].Border.GetComponent<MeshRenderer>().material = BorderMaterial;
            Reference.RefList[Reference.Districts.IndexOf(raycastObj)].Border.GetComponent<MeshRenderer>().material = MouseOverMaterial;
            LastRaycastHit = raycastObj;
        }
    }
    void SwitchDistrict(int i)
    {//Turn off current VFX, change particle mesh to new, then play particles
        DisableVFX();
        var borderShape = BorderVFX.shape;
        BorderMesh.mesh = Reference.RefList[i].Border.GetComponent<MeshFilter>().sharedMesh;
        borderShape.mesh = Reference.RefList[i].Border.GetComponent<MeshFilter>().sharedMesh;
        BorderMesh.gameObject.SetActive(true);
        BorderVFX.gameObject.SetActive(true);
        BorderVFX.Play();
    }

    void DisableVFX()
    {//Turn off particles
        BorderVFX.Stop();
        BorderMesh.gameObject.SetActive(false);
        BorderVFX.gameObject.SetActive(false);
    }

}
