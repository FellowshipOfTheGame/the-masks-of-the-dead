using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Cinemachine;

using System;
using System.Collections;
using System.Collections.Generic;

namespace FoG.TMOTD {
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerCamera : MonoBehaviour {

        public CinemachineFreeLook freeLook;

        private PlayerMovement movement;

        private void Start() {
            movement = GetComponent<PlayerMovement>();
        }

        private void Update() {
            if(movement.state == PlayerMovement.State.RECENTER) {
                StartCoroutine(Recenter());
                movement.state = PlayerMovement.State.FORWARD;
            }
        }

        private IEnumerator Recenter() {
            float delay = freeLook.m_RecenterToTargetHeading.m_RecenteringTime;
            freeLook.m_RecenterToTargetHeading.m_enabled = true;
            yield return new WaitForSeconds(delay);
            freeLook.m_RecenterToTargetHeading.m_enabled = false;
        }
    }
}
