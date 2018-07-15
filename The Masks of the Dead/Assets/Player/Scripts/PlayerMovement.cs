using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

namespace FoG.TMOTD {
    [RequireComponent(typeof(Player))]
    public class PlayerMovement : MonoBehaviour {
        public float forwardVelocity = 100f;
        public float steeringVelocity = 10f;

        public State state = State.FORWARD;

        private new Transform transform;
        private new Rigidbody rigidbody;
        private Player player;


        private void Start() {
            state = State.FORWARD;

            transform = GetComponent<Transform>();
            rigidbody = GetComponent<Rigidbody>();
            player = GetComponent<Player>();


            Quaternion rotation = Quaternion.Euler(player.State.euler);
            rigidbody.MoveRotation(rotation);
        }

        private void Update() {
            float forward = forwardVelocity * Input.GetAxis("Vertical");
            float steer = steeringVelocity * Input.GetAxis("Horizontal");
            player.State.euler.y += steer;
            Quaternion rotation = Quaternion.Euler(player.State.euler);
            Vector3 position = transform.position + rotation * Vector3.forward * forward;




            if(steer != 0f) {
                state = State.STEERING;
                rigidbody.MoveRotation(rotation);
            } else {
                if (state == State.FORWARD)
                    rigidbody.MovePosition(position);
                else if (state == State.STEERING) 
                    state = State.RECENTER;
            }
        }

        public enum State {
            FORWARD,
            STEERING,
            RECENTER
        }
    }
}
