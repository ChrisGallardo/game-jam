using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIrenderer : MonoBehaviour
{
    private Text text;
    private PlayerMovement playerscript;

    private void Awake() {
        text = GetComponent<Text>();
        playerscript = GameObject.Find("player").GetComponent(typeof(PlayerMovement)) as PlayerMovement;
    }

    private void Update() {
        text.text = "Souls: " + playerscript.souls + " Fire Flys: " + playerscript.fireflys + " Light: " + playerscript.playerLight;
    }
}
