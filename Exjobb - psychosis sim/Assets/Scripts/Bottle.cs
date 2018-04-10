using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UniRx;

namespace Pierre.Unidux
{
    public class Bottle : VRTK_InteractableObject
    {
        private Vector3 SPAWN_ANGLE_THRESHOLD = new Vector3(110.0f, 0, 110.0f);
        private GameObject pillSpawner;
        public void Start()
        {
            pillSpawner = GameObject.Find("PillSpawner");
            Unidux.Subject
                .TakeUntilDisable(this)
                .StartWith(Unidux.State)
                .Subscribe(state =>
                {
                    if (state.spawnPill && (pillSpawner.transform.rotation.eulerAngles.x > SPAWN_ANGLE_THRESHOLD.x ||
                    		pillSpawner.transform.rotation.z > SPAWN_ANGLE_THRESHOLD.z))
                    {
                        (Instantiate(Resources.Load("Pill")) as GameObject)
                            .transform.SetPositionAndRotation(pillSpawner.transform.position, pillSpawner.transform.rotation);
                    }
                })
                .AddTo(this);
        }

        public override void Grabbed(VRTK_InteractGrab ob)
        {
            base.Grabbed(ob);
            Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.EnablePillSpawn));
        }

        public override void Ungrabbed(VRTK_InteractGrab ob)
        {
            base.Ungrabbed(ob);
            Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.DisablePillSpawn));
        }

    }
}
