using Leap;
using Leap.Unity;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public LeapProvider leapProvider;
    public List<Hand> hands = new();

    // Start is called before the first frame update
    void Start()
    {
        //hands = leapProvider.CurrentFrame.Hands;
    }

    // Update is called once per frame
    void Update()
    {
        //hands.Add(leapProvider.CurrentFrame.Hands[0]);
    }
}
