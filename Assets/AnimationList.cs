using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationList : MonoBehaviour
{
    public static AnimationList instance;

    private void Start()
    {
        instance = this;
    }

    public List<RuntimeAnimatorController> animationclip = new List<RuntimeAnimatorController>();
}
