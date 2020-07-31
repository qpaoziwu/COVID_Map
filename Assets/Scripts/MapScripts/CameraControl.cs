using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private GameObject activeCamera = null;
    public enum ControlMode
    {
        FullMap,
        Zoomed,
        Freelook
    };
    [Range (0.1f, 1f), SerializeField]
    private float rotateSpeed = 1f;
    [SerializeField]
    private BorderVFXHandler VFXHandler = null;
    [SerializeField]
    private ControlMode currentMode = ControlMode.FullMap;
    [SerializeField]
    private GameObject targetLocation = null;
    [SerializeField]
    private GameObject cameraFullMap = null;
    [SerializeField]
    private GameObject cameraZoomed = null;
    [SerializeField]
    private GameObject zoomedCameraRig = null;
    [SerializeField]
    private GameObject TheCamera = null;
    [SerializeField]
    private Vector3 cameraMaxZoom = new Vector3(0, 60, -80); 
    public bool resetCam;
    [Range (0f, 1f), SerializeField]
    public float zoomLerp = 1f;
    [SerializeField]
    private AnimationCurve Curve = null; 
    private float cameraLerpTimer = 0f;

    [SerializeField]
    private Quaternion targetRotation;
    private Quaternion eighty = Quaternion.Euler(80,0,0);
    private Quaternion fourfive = Quaternion.Euler(45,0,0);
    private Quaternion zero = Quaternion.Euler(0,0,0);
    void Update()
    {
    MoveCamera();
    }

    void MoveCamera()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
        {
            zoomLerp +=0.175f;
        }             
        if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
        {
            zoomLerp -=0.175f;
        }
        zoomLerp = Mathf.Clamp(zoomLerp, 0, 1);

        if((int)currentMode == 1)
        {
            if(resetCam)
            {
                //targetLocation = cameraFullMap;
                //targetRotation = eighty;
                zoomLerp = 0f;
                cameraLerpTimer = 0f;
                resetCam = false;
            }

            if(Input.GetMouseButton(1))
            {
                if(Input.GetAxis("Mouse X")>0 ||Input.GetAxis("Mouse X")<0)
                {
                cameraZoomed.transform.Rotate(0, (Input.GetAxis("Mouse X")) * rotateSpeed, 0, Space.Self);
                }
            }
            if(Input.GetMouseButton(2))
            {
                //targetLocation = 
            }
        }

        if((int)currentMode == 2)
        {

        }

        cameraLerpTimer += Time.deltaTime;
        zoomedCameraRig.transform.localPosition = Vector3.Slerp(cameraMaxZoom, Vector3.zero, Curve.Evaluate(zoomLerp));
        TheCamera.transform.localRotation = Quaternion.Lerp(TheCamera.transform.localRotation,targetRotation, Curve.Evaluate(cameraLerpTimer));
        activeCamera.transform.position = Vector3.Lerp(activeCamera.transform.position,targetLocation.transform.position,Curve.Evaluate(cameraLerpTimer));
    }

    public void SwitchToFullMap()
    {
        cameraZoomed.transform.rotation = zero;
        targetLocation = cameraFullMap;
        targetRotation = eighty;
        cameraLerpTimer = 0f;
        zoomLerp = 0f;
        currentMode = ControlMode.FullMap;
    }
    public void SwitchToZoom()
    {
        targetLocation = VFXHandler.LastClickedObject;
        targetRotation = fourfive;
        cameraLerpTimer = 0f;
        currentMode = ControlMode.Zoomed;
    }


    
}
    //[SerializeField]
    //public event Action onSwitchCamera;
    // ControlMode CheckCurrentCamera ()
    // {
    //     activeCamera.SetActive(false); 

    //         if((int)currentMode == 0)
    //         {
    //             activeCamera = cameraFullMap;
    //         }
    //         if((int)currentMode == 1)
    //         {
    //             if(VFXHandler.LastClickedObject == null)
    //             {
    //             currentMode = ControlMode.FullMap;
    //             }else {
    //             activeCamera = cameraZoomed;
    //             }
    //         }
    //         if((int)currentMode == 2)
    //         {
    //             activeCamera = cameraFreelook;
    //         }
    //     activeCamera.SetActive(true); 

    //     return currentMode;
    // }