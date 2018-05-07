using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UniRx;

namespace Pierre.Unidux
{
    public class CerealBoxScript : VRTK_InteractableObject
    {
        private Vector3 SPAWN_ANGLE_UPPER = new Vector3(240.0f, 0, 240.0f);
        private Vector3 SPAWN_ANGLE_LOWER = new Vector3(110.0f, 0, 110.0f);
        private GameObject cerealSpawner;
        public void Start()
        {
            cerealSpawner = GameObject.Find("CerealSpawner");

            Unidux.Subject
                .TakeUntilDisable(this)
                .StartWith(Unidux.State)
                .Subscribe(state =>
                {
                    if (state.spawnCereal && !state.cerealHasSpawned)
                    {
                        print("Spawning cereal");
                        StartCoroutine(SpawnCereal());
                        Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.CerealHasSpawned));
                    }
                })
                .AddTo(this);
        }
        protected override void Update()
        {
            base.Update();
            float x = cerealSpawner.transform.rotation.eulerAngles.x;
            float z = cerealSpawner.transform.rotation.eulerAngles.z;

            if (((x > SPAWN_ANGLE_LOWER.x && x < SPAWN_ANGLE_UPPER.x) || (z > SPAWN_ANGLE_LOWER.z && z < SPAWN_ANGLE_UPPER.z)) && !Unidux.Store.State.pillHasSpawned)
            {
                Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.CerealBoxTilted));
            }
        }

        IEnumerator SpawnCereal()
        {
            for (int i = 0; i < 200; i++)
            {
                GameObject temp = Instantiate(Resources.Load("Cereal") as GameObject) as GameObject;
                temp.transform.SetPositionAndRotation(cerealSpawner.transform.position, cerealSpawner.transform.rotation);
                yield return new WaitForSecondsRealtime(.1f);
            }
        }
    }
}