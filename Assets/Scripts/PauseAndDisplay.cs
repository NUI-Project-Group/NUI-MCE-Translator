using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseAndDisplay : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioFiles;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private float pauseDuration = 2f; // 2 seconds pause
    private float lastDetectedTime = 0;

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
        if (Time.time - lastDetectedTime > pauseDuration && !audioSource.isPlaying)
        {
            displayText.text = detectedLetter;
            lastDetectedTime = Time.time;
            PlaySound(detectedLetter);
        }
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
