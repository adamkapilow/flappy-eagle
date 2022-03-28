using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailureCollision : MonoBehaviour
{
    // Triggers if the player hits a pipe or level boundary, starts death state.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            GameManager.Instance.SwitchState(GameManager.State.DEATH);
            
        }
    }
}
