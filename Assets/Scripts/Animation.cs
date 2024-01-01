using System.Collections.Generic;
using UnityEngine;

namespace CustomAnimation.Animation
{
    [System.Serializable]
    public struct Keyframe
    {
        [Header("General Info")] 
        public Sprite sprite;
        [Range(0f, 1f)] public float duration;
    }
    
    [CreateAssetMenu(menuName = "Custom Animation System/Animation/New Animation")]
    public class Animation : ScriptableObject
    {
        public int KeyframeCount => keyframes.Count;
        
        [Header("Settings")] 
        public bool loop;
        public int loopFrame;

        [Header("Frame Data")] 
        public List<Keyframe> keyframes = new List<Keyframe>();

        public int CalculateTotalFrameCount()
        {
            int frameCount = 0;
            int frameRate = (int)(1f / Time.unscaledDeltaTime);

            foreach (var keyframe in keyframes)
            {
                frameCount += (int)(frameRate * keyframe.duration);
            }

            return frameCount;
        }
    }
}
