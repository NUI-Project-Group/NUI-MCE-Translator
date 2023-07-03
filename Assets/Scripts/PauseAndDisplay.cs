using TMPro;
using UnityEngine;

public class PauseAndDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private float pauseDuration = 2f; // 2 seconds pause
    private float lastDetectedTime = 0;

    // This method would be called by your detection script
    public void OnLetterDetected(string detectedLetter)
    {
        // Set the new text only if more than pauseDuration sections passed since last detection
        if (Time.time - lastDetectedTime > pauseDuration)
        {
            displayText.text = detectedLetter;
            lastDetectedTime = Time.time;
        }
    }
}
