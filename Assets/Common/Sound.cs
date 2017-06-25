using UnityEngine;
using System.Collections;

namespace Common
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        [Range(0f, 1f)]
        public float volume;

        public AudioClip sound;

        [HideInInspector]
        public AudioSource source;
    }
}