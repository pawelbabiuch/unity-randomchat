using UnityEngine;

public class OtherOrientation : Orientation
{
    public OtherOrientation(GameObject canvasPortait, GameObject canvasLandscape) : base(canvasPortait, canvasLandscape){ }

    public override void CheckOrientation()
    {
        if (Screen.height > Screen.width)
            ChangeCanvas(canvasPortait);
        else
            ChangeCanvas(canvasLandscape);

    }
}
