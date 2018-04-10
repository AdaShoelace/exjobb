using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UniRx;

namespace Pierre.Unidux
{
  public class Bottle : VRTK_InteractableObject
  {
    private Vector3 SPAWN_ANGLE_UPPER = new Vector3(240.0f, 0, 240.0f);
    private Vector3 SPAWN_ANGLE_LOWER = new Vector3(110.0f, 0, 110.0f);

    private GameObject pillSpawner;
    private GameObject bottle;
    private Material poisonLabel;

    public void Start()
    {
      pillSpawner = GameObject.Find("PillSpawner");
      poisonLabel = Resources.Load("BottleLabelPoison", typeof(Material)) as Material;
      bottle = GameObject.Find("bottle").transform.Find("bottle/bottle8 label").gameObject;

      Unidux.Subject
          .TakeUntilDisable(this)
          .StartWith(Unidux.State)
          .Subscribe(state =>
          {
            if (state.spawnPill && !state.pillHasSpawned)
            {
              StartCoroutine(SpawnPill());
              Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.PillHasSpawned));
              bottle.GetComponent<Renderer>().material = poisonLabel;
            }
          })
          .AddTo(this);
    }

    protected override void Update()
    {
      base.Update();
      float x = pillSpawner.transform.rotation.eulerAngles.x;
      float z = pillSpawner.transform.rotation.eulerAngles.z;

      if (((x > SPAWN_ANGLE_LOWER.x && x < SPAWN_ANGLE_UPPER.x) || (z > SPAWN_ANGLE_LOWER.z && z < SPAWN_ANGLE_UPPER.z)) && !Unidux.Store.State.pillHasSpawned)
      {
        Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.BottleTilted));
      }
    }

    IEnumerator SpawnPill()
    {
      for (int i = 0; i < 10; i++)
      {
        GameObject temp = Instantiate(Resources.Load("Pill") as GameObject) as GameObject;
        temp.transform.SetPositionAndRotation(pillSpawner.transform.position, pillSpawner.transform.rotation);
        yield return new WaitForSecondsRealtime(.1f);
      }
    }
  }
}
