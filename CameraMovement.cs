using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3? basePointerPosition = null;
    public float CameraSpeed = 0.03f;
    private int CameraXmin, CameraZmin, CameraXmax, CameraZmax;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCamera(Vector3 Pointerposition)
    {
        if (basePointerPosition.HasValue == false)
        {
            basePointerPosition = Pointerposition;
        }
        Vector3 newPosition = Pointerposition - basePointerPosition.Value;
        newPosition = new Vector3(newPosition.x, 0, newPosition.y);
        transform.Translate(newPosition * CameraSpeed);
        CameraInBounds();
    }

    private void CameraInBounds()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, CameraXmin, CameraXmax)
                    , 0, Mathf.Clamp(transform.position.z, CameraZmin, CameraZmax));
    }

    public void StopCameraMovement()
    {
        basePointerPosition= null;
    }

    public void SetCameraBounds(int CameraXmin, int CameraXmax, int CameraZmin, int CameraZmax)
    {
        this.CameraXmax = CameraXmax;
        this.CameraXmin = CameraXmin;
        this.CameraZmax= CameraZmax;
        this.CameraZmin= CameraZmin;
        
    }
}
