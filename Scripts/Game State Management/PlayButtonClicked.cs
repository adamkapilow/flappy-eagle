using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonClicked : MonoBehaviour
{
    public void playButtonClicked()
    {
        GameManager.Instance.SwitchState(GameManager.State.INIT);
    }
}
