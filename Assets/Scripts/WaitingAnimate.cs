using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class WaitingAnimate : MonoBehaviour
{
    private const string START_TEXT = "Waiting...";

    private Text animatedText;
    private int currentID = START_TEXT.Length;
    private int direction = 1;

    private void OnEnable()
    {
        animatedText = GetComponent<Text>();
        InvokeRepeating("InvokeChange", 0f, 0.5f);
    }

    private void OnDisable()
    {
        CancelInvoke("InvokeChange");
    }

    private void InvokeChange()
    {
        if (currentID >= START_TEXT.Length || currentID <= START_TEXT.Length - 3) direction *= -1;

        string end = START_TEXT.Substring(currentID, START_TEXT.Length - currentID);
        string start = START_TEXT.Substring(0, currentID);
        animatedText.text = end + start;

        currentID += direction;
    }
}
