using UnityEngine;

public class ChangeCanvas : MonoBehaviour
{
    public GameObject canvasLandscape, canvasPortait;

    private GameObject currentEnableCanvas;
    private Orientation orientation;
    private Vector2 screenSize;

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
            orientation = new MobileOrientation(canvasPortait, canvasLandscape);
        else
            orientation = new OtherOrientation(canvasPortait, canvasLandscape);

        canvasLandscape.SetActive(false);
        canvasPortait.SetActive(false);
    }

    private void Start()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        orientation.CheckOrientation();
    }

    private void Update()
    {
        if (!screenSize.Equals(getScreenSize))
        {
            orientation.CheckOrientation();
            screenSize = new Vector2(Screen.width, Screen.height);
        }
    }

    private Vector2 getScreenSize
    {
        get
        {
            return new Vector2(Screen.width, Screen.height);
        }
    }
}
