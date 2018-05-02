using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unidux;
using UniRx;

namespace Pierre.Unidux
{
    public class DoorManager : MonoBehaviour
    {


        private GameObject door;
        private bool isOpen = true;
        private Vector3 open = new Vector3(0, 20, 0);
        private Vector3 close = new Vector3(0, -20, 0);
        private float speed = 2f;
        void Start()
        {
            door = GameObject.Find("ClosetDoor");

        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time > 5)
            {
                door.transform.Rotate(axis: Vector3.up, angle: Time.deltaTime * speed);
            }
        }
    }
}