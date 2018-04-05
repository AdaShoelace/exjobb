using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRTK;
using UniRx;

namespace Pierre.Unidux
{
	[InitializeOnLoad]
	public class Radio : VRTK_InteractableObject {
	public ActionType ToggleRadio = ActionType.ToggleRadio;
			
	public void Awake() {
			AudioSource radio = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
			(gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody).constraints = RigidbodyConstraints.FreezeAll;
			radio.Play();
			Unidux.Subject
				.TakeUntilDisable(this)
				.StartWith(Unidux.State)
				.Subscribe(state => {
						radio.mute = state.radioMute;
					})
				.AddTo(this);
		}

		public override void StartUsing(VRTK_InteractUse ob) {
			base.StartUsing(ob);
			Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.ToggleRadio));
		}	

	}
}

