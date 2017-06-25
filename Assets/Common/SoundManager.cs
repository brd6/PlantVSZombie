using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [SerializeField]
        private List<Sound> sounds;

        private List<AudioSource> audioSources;

        private void Awake()
        {
            MakeSingletonInstance();
        }

        #region Singleton
        private void MakeSingletonInstance()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        #endregion

        private void Start()
        {
            audioSources = new List<AudioSource>();
            audioSources.Add(gameObject.AddComponent<AudioSource>());
        }

        public void PlaySound(string soundName, bool looping = false)
        {
            Sound sound = sounds.Find(x => x.name == soundName);
            if (sound == null)
            {
                Debug.LogError("Sound not found : " + soundName);
                return;
            }
            sound.source = InitAudioClipToAudioSource(sound);
            sound.source.loop = looping;
            sound.source.Play();
        }

        public void StopSound(string soundName)
        {
            Sound sound = sounds.Find(x => x.name == soundName);
            if (sound == null)
            {
                Debug.LogError("Sound not found : " + soundName);
                return;
            }
            sound.source.Stop();
        }

        public void StopAllSound()
        {
            foreach (var sound in sounds)
            {
                sound.source.Stop();
            }
        }

        private AudioSource InitAudioClipToAudioSource(Sound sound)
        {
            AudioSource audioSource = GetAudioSourceAvaible();

            audioSource.volume = sound.volume;
            audioSource.clip = sound.sound;
            return audioSource;
        }

        private AudioSource GetAudioSourceAvaible()
        {
            AudioSource avaible = audioSources.Find(x => x.isPlaying == false);
            if (avaible == null)
            {
                avaible = gameObject.AddComponent<AudioSource>();
                audioSources.Add(avaible);
            }
            avaible.playOnAwake = false;
            return avaible;
        }

    }
}