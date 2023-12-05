using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationList : MonoBehaviour
{
    public List<AnimationClip> animationclip = new List<AnimationClip>();
    private void Start()
    {
        Debug.LogError(animationclip.Count);
    }
}
