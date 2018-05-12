using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UniRx;

namespace Pierre.Unidux
{
    public class PhoneScript : VRTK_InteractableObject
    {
        private AudioSource phone;
        private bool initAccidentHallucination = false;
        public void Start()
        {
            phone = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
            Unidux.Subject
                .TakeUntilDisable(this)
                .StartWith(Unidux.State)
                .Subscribe(state =>
                {
                    if (state.ringPhone && !state.phoneHasRung)
                    {
                        print("Phone is ringing");
                        phone.clip = Resources.Load("Sound clips/phone-ringing", typeof(AudioClip)) as AudioClip;
                        initAccidentHallucination = true;
                        phone.Play();
                    }
                    else if (phone.isPlaying && !state.phonePlay)
                    {
                        phone.Stop();
                    }
                    else if (!phone.isPlaying && state.phonePlay)
                    {
                        phone.clip = Resources.Load("Sound clips/america-dial-tone", typeof(AudioClip)) as AudioClip;
                        phone.Play();
                    }
                })
                .AddTo(this);
        }
        public override void Grabbed(VRTK_InteractGrab ob)
        {
            base.Grabbed(ob);
            Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.PhonePickedUp));
        }

        public override void Ungrabbed(VRTK_InteractGrab ob)
        {
            base.Ungrabbed(ob);
            Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.PhoneDropped));
        }
    }
}