using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unidux;
using UniRx;

namespace Pierre.Unidux
{
    public class HallucinationAudio : MonoBehaviour
    {
        Dictionary<string, AudioSource> playerSources = new Dictionary<string, AudioSource>();
        List<AudioSource> ambientSources = new List<AudioSource>();
        public void Awake()
        {
            GameObject temp = GameObject.Find("AudioParent");
            foreach (AudioSource go in temp.GetComponentsInChildren<AudioSource>())
            {
                playerSources.Add(go.transform.parent.name.ToString(), go);
                print("Parent name:" + go.transform.parent.name);
            }
            Unidux.Subject
                        .TakeUntilDisable(this)
                        .StartWith(Unidux.State)
                        .Subscribe(state =>
                        {
                            //test
                            if (state.playPillHallucinationAudio)
                            {
                                PlayCloseProximityAudio(Resources.Load("Voices/Crowd Whispering") as AudioClip);
                            }
                        })
                        .AddTo(this);
        }
        private void PlayCloseProximityAudio(AudioClip audioClip)
        {
            AudioSource left = playerSources["AudioLeft"];
            left.clip = audioClip;
            AudioSource right = playerSources["AudioRight"];
            right.clip = audioClip;
            left.Play();
            right.Play();
        }
    }
}
