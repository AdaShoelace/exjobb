using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unidux;
using UniRx;

namespace Pierre.Unidux
{
    public class ApartmentDoor : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Apartment_key")
            {
				Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.EndScene));
            }
        }
    }
}
