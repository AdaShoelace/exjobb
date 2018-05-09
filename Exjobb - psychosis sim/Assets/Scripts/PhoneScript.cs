using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UniRx;

namespace Pierre.Unidux
{
    public class PhoneScript : VRTK_InteractableObject
    {
        public ActionType ToggleRadio = ActionType.ToggleRadio;
        public AudioClip currentClip;
        private AudioSource phone;
        private AudioClip dialTone, ringTone;
        public void Start()
        {
            phone = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
            dialTone = Resources.Load("Sound clips/america-dial-tone", typeof(AudioClip)) as AudioClip;
            ringTone = Resources.Load("Sound clips/phone-ringing", typeof(AudioClip)) as AudioClip;
            //(gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody).constraints = RigidbodyConstraints.FreezeAll;
            Unidux.Subject
                .TakeUntilDisable(this)
                .StartWith(Unidux.State)
                .Subscribe(state =>
                {
                    if (state.ringPhone && !state.phoneHasRung)
                    {
                        phone.clip = ringTone;
                        phone.loop = true;
                        phone.Play();
                    }
                    if (phone.isPlaying && !state.phonePlay)
                    {
                        phone.clip = dialTone;
                        phone.Stop();
                    }
                    else if (!phone.isPlaying && state.phonePlay)
                    {
                        phone.clip = dialTone;
                        phone.Play();
                    }
                })
                .AddTo(this);
        }
        public override void Grabbed(VRTK_InteractGrab ob)
        {
            base.Grabbed(ob);
            Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.PlayPhoneSound));
        }

        public override void Ungrabbed(VRTK_InteractGrab ob)
        {
            base.Ungrabbed(ob);
            Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.StopPhoneSound));
        }
    }
}