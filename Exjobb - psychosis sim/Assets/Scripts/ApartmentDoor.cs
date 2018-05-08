using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unidux;
using UniRx;

namespace Pierre.Unidux
{
    public class ApartmentDoor : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            Unidux.Subject
                      .TakeUntilDisable(this)
                      .StartWith(Unidux.State)
                      .Subscribe(state =>
                      {
						  if(state.isSceneOver)
						  {
							  
						  }
                      })
                      .AddTo(this);
        }

        void OnTriggerEnter(Collider other)
        {
			if(other.gameObject.tag == "Apartment_key")
			{
				Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.EndScene));
			}
        }
    }
}