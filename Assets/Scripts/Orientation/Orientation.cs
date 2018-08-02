using UnityEngine;

public abstract class Orientation
{
    private static GameObject currentEnableCanvas;

    protected GameObject canvasPortait, canvasLandscape;

    public Orientation(GameObject canvasPortait, GameObject canvasLandscape)
    {
        this.canvasPortait = canvasPortait;
        this.canvasLandscape = canvasLandscape;
    }

    public abstract void CheckOrientation();

    protected void ChangeCanvas(GameObject canvas)
    {
        Debug.Log("@ Change canvas to: " + canvas.name);

        if (currentEnableCanvas != null)
            currentEnableCanvas.SetActive(false);

        currentEnableCanvas = canvas;
        currentEnableCanvas.SetActive(true);
    }
}
