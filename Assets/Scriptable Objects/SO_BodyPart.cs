using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Body Part", menuName="Body Part")]
public class SO_BodyPart : ScriptableObject
{
    // Body Part Details
    public string bodyPartName;
    public int bodyPartAnimationID;

    // List Containing All Body Part Animations
    public List<AnimationClip> allBodyPartAnimations = new List<AnimationClip>();
}