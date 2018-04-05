using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UniRx;

namespace Pierre.Unidux
{
	public class PhoneScript : VRTK_InteractableObject {
	public ActionType ToggleRadio = ActionType.ToggleRadio;
			
	public void Start() {
			AudioSource phone = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
			(gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody).constraints = RigidbodyConstraints.FreezeAll;
			
			Unidux.Subject
				.TakeUntilDisable(this)
				.StartWith(Unidux.State)
				.Subscribe(state => {
						if(phone.isPlaying && !state.phonePlay) {
							phone.Stop();
						} else if(!phone.isPlaying && state.phonePlay) {
							phone.Play();
						}
					})
				.AddTo(this);
		}

		public override void Grabbed(VRTK_InteractGrab ob) {
			base.Grabbed(ob);
			Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.PlayPhoneSound));
		}	

		public override void Ungrabbed(VRTK_InteractGrab ob) {
			base.Ungrabbed(ob);
			Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.StopPhoneSound));
		}

	}
}