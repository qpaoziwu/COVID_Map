using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class CameraControl : MonoBehaviour
{
        public enum ControlMode
    {
        FullMap,
        Zoomed,
        Freelook
    };
    [SerializeField]
    private GameObject activeCamera = null;
    [Header("Camera Settings")]
    [Range (0.1f, 5f), SerializeField]
    private float rotateSpeed = 1f;
    [Range (1f, 5f), SerializeField]
    private float panSpeed = 1f;
    [SerializeField]
    private AnimationCurve MotionCurve = null; 
    [SerializeField]
    private Vector3 cameraMaxZoom = new Vector3(0, 120, -120); 

    [Header("References")]
    [SerializeField]
    private BorderVFXHandler VFXHandler = null;
    [SerializeField]
    private GameObject cameraFullMap = null;
    [SerializeField]
    private GameObject cameraZoomed = null;
    [SerializeField]
    private GameObject zoomedCameraRig = null;
    [SerializeField]
    private GameObject TheCamera = null;
    [Header("Debugging")]
    [SerializeField]
    private ControlMode currentMode = ControlMode.FullMap;
    [SerializeField]
    private GameObject targetLocation = null;
    [SerializeField]
    private Vector3 targetPosition;
    public bool resetCam;
    [Range (0f, 1f), SerializeField]
    public float zoomLerp = 1f;

    private float cameraLerpTimer = 0f;

    [SerializeField]
    private Quaternion targetRotation = Quaternion.identity;
    private Quaternion eighty = Quaternion.Euler(80,0,0);
    private Quaternion fourfive = Quaternion.Euler(45,0,0);
    private Quaternion zero = Quaternion.identity;
    void Update()
    {
    MoveCamera();
    }

    void MoveCamera()
    {
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
            if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
            {
                zoomLerp +=0.175f;
            }             
            if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
            {
                zoomLerp -=0.175f;
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
                Vector3 mouseInputOffset = new Vector3(Input.GetAxis("Mouse X"),0,Input.GetAxis("Mouse Y"));
                targetPosition -= cameraZoomed.transform.TransformDirection(mouseInputOffset)*panSpeed;
            }
        }

        if((int)currentMode == 2)
        {

        }

        zoomLerp = Mathf.Clamp(zoomLerp, 0, 1);
        if(cameraLerpTimer<1f){
        cameraLerpTimer += Time.deltaTime*0.5f;
        }
        zoomedCameraRig.transform.localPosition = Vector3.Slerp(cameraMaxZoom, Vector3.zero, MotionCurve.Evaluate(zoomLerp));
        TheCamera.transform.localRotation = Quaternion.Lerp(TheCamera.transform.localRotation,targetRotation, MotionCurve.Evaluate(cameraLerpTimer));
        activeCamera.transform.position = Vector3.Lerp(activeCamera.transform.position,targetPosition,MotionCurve.Evaluate(cameraLerpTimer));
    }

    public void SwitchToFullMap()
    {
        cameraZoomed.transform.rotation = zero;
        targetLocation = cameraFullMap;
        targetPosition = targetLocation.transform.position;
        targetRotation = eighty;
        cameraLerpTimer = 0f;
        zoomLerp = 0f;
        currentMode = ControlMode.FullMap;
    }
    public void SwitchToZoom()
    {
        targetLocation = VFXHandler.LastClickedObject;
        targetPosition = targetLocation.transform.position;
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