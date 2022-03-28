using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButtonClicked : MonoBehaviour
{
    public void optionsButtonClicked()
    {
        GameManager.Instance.SwitchState(GameManager.State.OPTIONS);
    }
}
