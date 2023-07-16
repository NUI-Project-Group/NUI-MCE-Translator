using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseAndDisplay : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioFiles;
    [SerializeField] private TMP_Text displayText, headline;
    [SerializeField] private float pauseDuration = 2f; // 2 seconds pause
    [SerializeField] private string headlineDefault = "Place your hand above Leap!",
        headlineDetected = "Detected MCE letter:", headlineTimeout = "Try again. Make a MCE gesture.";
    private float lastDetectedTime = 0;

    private void Update()
    {
        // Resets Texts after 5s of waiting after each detection.
        if (lastDetectedTime != 0 && (Time.time > lastDetectedTime + 5.0f))
            OnLetterLost(headlineTimeout);
        if (lastDetectedTime != 0 && (Time.time > lastDetectedTime + 30.0f))
            OnLetterLost(headlineDefault);
    }

    /// <summary>
    /// This method would be called by your detection script.
    /// It displays the letter and plays the sound.
    /// </summary>
    /// <param name="detectedLetter">As the name implies.</param>
    public void OnLetterDetected(string detectedLetter)
    {
        /* 
         * Set the new text only if more than pauseDuration seconds
         * passed since last detection and no sound is still playing.
         */
        if (!audioSource.isPlaying && Time.time - lastDetectedTime > pauseDuration)
        {
            headline.text = headlineDetected;
            displayText.text = detectedLetter;
            lastDetectedTime = Time.time;
            PlaySound(detectedLetter);
        }
    }

    /// <summary>
    /// This method would be called by your detection script.
    /// It clears the letter and headline text.
    /// <param name="setText">The text to set.</param>
    /// </summary>
    public void OnLetterLost(string setText)
    {
        displayText.text = "";
        headline.text = setText;
    }

    /// <summary>
    /// This method would be called by your detection script.
    /// It clears the letter and headline text.
    /// </summary>
    public void OnLetterLost()
    {
        displayText.text = "";
        headline.text = headlineDefault;
    }

    /// <summary>
    /// Plays a corresponding sound file from a list.
    /// </summary>
    /// <param name="detectedLetter">The file to pick.</param>
    public void PlaySound(string detectedLetter)
    {
        // Just in case, even if it is obsolete.
        if (!audioSource.isPlaying)
        {
            AudioClip file = audioFiles.Find(obj => obj.name.Equals(detectedLetter));
            audioSource.clip = file;
            audioSource.Play();
        }
    }
}
