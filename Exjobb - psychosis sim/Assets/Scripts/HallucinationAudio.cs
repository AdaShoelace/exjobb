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
        private bool executeHallucination = true;
        public void Start()
        {
            GameObject temp =
                GameObject.Find("SDKManager/SDKSetups/SteamVR").gameObject.activeSelf ?
                    temp = GameObject.Find("SDKManager/SDKSetups/SteamVR/[CameraRig]/Camera (head)/AudioParent")
                    : temp = GameObject.Find("SDKManager/SDKSetups/Simulator/VRSimulatorCameraRig/AudioParent");
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
                            if (state.playCrowdWhisper && !state.crowdWhisperIsPlaying)
                            {
                                PlayCloseProximityAmbientWhisper();
                                print("Time to play hallucination");
                                Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.CrowdIsWhispering));
                            }
                            if (state.playAccidentHallucination && executeHallucination)
                            {
                                executeHallucination = false;
                                StartCoroutine(PlayCloseProximityAudioAfterDelay());
                            }
                            if (state.doKnock && !state.hasKnocked)
                            {
                                AudioSource wardrobe = GameObject.Find("WardrobeAudio").GetComponent<AudioSource>() as AudioSource;
                                print(wardrobe == null ? "Wardrobe is null" : "Wardrobe is success!");
                                wardrobe.Play();
                                print("HasKnocked");
                                Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.HasKnocked));
                            }
                        })
                        .AddTo(this);
        }
        private void PlayCloseProximityAudio(AudioClip audioClip)
        {
            AudioSource left = playerSources["AudioLeft"];
            //left.clip = audioClip;
            AudioSource right = playerSources["AudioRight"];
            //right.clip = audioClip;
            print("Left loop: " + left.loop.ToString() + " Right loop: " + right.loop.ToString());
            left.PlayOneShot(audioClip);
            right.PlayOneShot(audioClip);
        }

        private IEnumerator PlayCloseProximityAudioAfterDelay()
        {
            AudioClip[] temp = {
                Resources.Load("Sound clips/EndingHallucinations/accident", typeof(AudioClip)) as AudioClip,
                Resources.Load("Sound clips/EndingHallucinations/dont_care", typeof(AudioClip)) as AudioClip,
                Resources.Load("Sound clips/EndingHallucinations/dont_love_them", typeof(AudioClip)) as AudioClip,
                Resources.Load("Sound clips/EndingHallucinations/theyre_hurt", typeof(AudioClip)) as AudioClip,
                Resources.Load("Sound clips/EndingHallucinations/your_fault", typeof(AudioClip)) as AudioClip,
            };
            for( int i = 0; i < temp.Length; i++) {
                PlayCloseProximityAudio(temp[i]);
                yield return new WaitForSeconds((float)temp[i].length + Random.Range(2, 4));
            }

        }

        private void PlayCloseProximityAmbientWhisper()
        {
            AudioClip clip = Resources.Load("Voices/CrowdWhisper", typeof(AudioClip)) as AudioClip;
            AudioSource front = playerSources["AudioFront"];
            front.clip = clip;
            AudioSource back = playerSources["AudioBack"];
            back.clip = clip;
            front.loop = true;
            back.loop = true;
            front.Play();
            back.Play();
        }
    }
}
