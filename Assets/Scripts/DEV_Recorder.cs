using Leap;
using Leap.Unity;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DEV_Recorder : MonoBehaviour
{
    [SerializeField] private LeapProvider leapProvider;
    private Hand hand = new();
    public Chirality handToRecord = Chirality.Left;
    public System.Action<HandPoseScriptableObject> OnPoseSaved;

    [SerializeField] private TMP_Text label, poseLabel, warnings;
    [SerializeField] private Pose pose = Pose.NONE;
    [SerializeField] private Recorder recorder = Recorder.Nobody;
    private string handPoseName, savePath;
    private bool isRecording = false;

    // Start is called before the first frame update
    void Start()
    {
        if (leapProvider == null)
        {
            Debug.Log("Plese select a Leap Provider.");
            EditorApplication.isPlaying = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ToggleRecording();

        if (isRecording)
        {
            SaveCurrentMCEPose();
        }
    }

    /// <summary>
    /// Turns on recording.
    /// </summary>
    public void ToggleRecording()
    {

        if (pose == Pose.NONE)
        {
            Debug.Log("Plese pick a pose.");
            return;
        }

        if (recorder == Recorder.Nobody)
        {
            Debug.Log("Plese pick a name.");
            return;
        }

        isRecording = !isRecording;
        if (isRecording)
        {
            SetupAssetName();
            label.text = "REC...";
        } else
        {
            label.text = "GO!";
        }
    }

    private void SetupAssetName()
    {
        poseLabel.text = pose.ToString();

        handPoseName = recorder.ToString().Capitalize();
        if (handToRecord == Chirality.Left)
        {
            handPoseName += "_L";
        }
        else
        {
            handPoseName += "_R";
        }

        savePath = "HandPoses/" + pose.ToString() + "/";
    }

    private void SaveCurrentMCEPose()
    {
        Hand handToCapture = leapProvider.CurrentFrame.GetHand(handToRecord);

        if (handToCapture == null) return;

        hand = hand.CopyFrom(handToCapture);

        CreateScriptableObject(handPoseName, hand);
    }

    private void CreateScriptableObject(string handPoseName, Hand handData)
    {
#if UNITY_EDITOR
        HandPoseScriptableObject newItem = ScriptableObject.CreateInstance<HandPoseScriptableObject>();
        newItem.name = handPoseName;
        newItem.SaveHandPose(handData);

        if (!Directory.Exists("Assets/" + savePath))
        {
            Directory.CreateDirectory("Assets/" + savePath);
        }

        string fullPath = "Assets/" + savePath + handPoseName + ".asset";

        int fileIterator = 1;
        while (File.Exists(fullPath))
        {
            fullPath = "Assets/" + savePath + handPoseName + " (" + fileIterator + ")" + ".asset";
            fileIterator++;
        }

        AssetDatabase.CreateAsset(newItem, fullPath);
        AssetDatabase.Refresh();

        OnPoseSaved?.Invoke(newItem);
#else
            Debug.LogError("Error saving Hand Pose: You can not save Hand Poses in a built application.");
            return null;
#endif
    }
}
