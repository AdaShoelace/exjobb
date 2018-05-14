using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pierre.Unidux
{
	public class Actions : MonoBehaviour {

        // ActionCreators creates actions and deliver payloads
        // in redux, you do not dispatch from the ActionCreator to allow for easy testability
        public static class ActionCreator
        {
            public static Action Create(ActionType type)
            {
                return new Action() {ActionType = type};
            }

            public static Action ToggleRadio()
            {
                return new Action() {ActionType = ActionType.ToggleRadio};
            }
        }
	}

	// actions must have a type and may include a payload
	public class Action
	{
		public ActionType ActionType;
	}

	// specify the possible types of actions
	public enum ActionType
	{
		ToggleRadio,
		RingPhone,
		PhoneDropped,	
		PhonePickedUp,
		TurnOffAlarmClock,
		SetLight,
		BottleTilted,
		PillHasSpawned,
		BottleDropped,
		BottleLabelHasChanged,
		PillBottleAudioHallucination,
		CrowdIsWhispering,
		CerealBoxTilted,
		CerealHasSpawned,
		AccidentHallucination,
		EndScene,
		Knock,
		HasKnocked,
		LowerWhisperVolume,
		ResetWhisperVolume,
	}
}

