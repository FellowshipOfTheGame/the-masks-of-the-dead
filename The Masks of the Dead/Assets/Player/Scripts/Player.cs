using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

namespace FoG.TMOTD {
	public class Player : MonoBehaviour {
        [SerializeField]
        private PlayerState state;
        public PlayerState State {
            get {
                return state;
            }
        }
	}
}
