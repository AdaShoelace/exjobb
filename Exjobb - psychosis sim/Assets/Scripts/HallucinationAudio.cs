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
        public void Start()
        {
            GameObject temp = GameObject.Find("SDKManager/SDKSetups/SteamVR/[CameraRig]/Camera (head)/AudioParent");
            foreach (AudioSource go in temp.GetComponentsInChildren<AudioSource>())
            {
                AudioSource aso = go.GetComponent("AudioSource") as AudioSource;
                aso.enabled = true;
                playerSources.Add(go.transform.name, aso);
            }

            Unidux.Subject
                        .TakeUntilDisable(this)
                        .StartWith(Unidux.State)
                        .Subscribe(state =>
                        {
                            //test
                            if (!state.crowdWhisperIsPlaying)
                            {
                                //PlayCloseProximityAudio(Resources.Load("Voices/CrowdWhisper", typeof(AudioClip)) as AudioClip);
                                PlayCloseProximityAmbientWhisper();
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

        private void PlayCloseProximityAmbientWhisper()
        {
            AudioClip clip = Resources.Load("Voices/CrowdWhisper", typeof(AudioClip)) as AudioClip;
            AudioSource left = playerSources["AudioLeft"];
            left.clip = clip;
            AudioSource right = playerSources["AudioRight"];
            right.clip = clip;
            left.Play();
            right.Play();
        }
    }
}
