using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

namespace FoG.TMOTD {
    [CreateAssetMenu(fileName = "Player", menuName = "TMOTD/Player State")]
	public class PlayerState : ScriptableObject {
        public Vector3 euler;
    }
}
