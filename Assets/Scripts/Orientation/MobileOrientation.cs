using UnityEngine;

public class MobileOrientation : Orientation
{

    public MobileOrientation(GameObject canvasPortait, GameObject canvasLandscape):base(canvasPortait, canvasLandscape)
    {
        Debug.Log("@ Orientatnion: Mobile");
    }

    public override void CheckOrientation()
    {
        if (Input.deviceOrientation == DeviceOrientation.Portrait && canvasPortait.activeInHierarchy == false)
        {
            ChangeCanvas(canvasPortait);
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight && canvasLandscape.activeInHierarchy == false)
        {
            ChangeCanvas(canvasLandscape);
        }
    }
}
