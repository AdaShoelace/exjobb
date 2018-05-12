using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unidux;
using UniRx;


namespace Pierre.Unidux
{

    public class KnockingHallucination : MonoBehaviour
    {
		void OnTriggerEnter(Collider coll) 
		{
			Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.Knock));
		}
    }

}
