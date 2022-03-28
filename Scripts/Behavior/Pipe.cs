using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pipe : MonoBehaviour
{
    // Code for managing a pair of pipes, contains references to top and bottom pipe,
    // allows for movement and despawning

    public float speed = 5f;
    public GameObject pipeCenter;
    public GameObject topPipe;
    public GameObject bottomPipe;

  
    

    private void FixedUpdate()
    {
        switch (GameManager.Instance.getCurrentState())
        {
            // Moves the pipe to the left while playing, destroys it when off camera
            case GameManager.State.PLAY:
                transform.position += Vector3.left * speed * Time.deltaTime;
                if (transform.position.x < GameManager.Instance.bottomLeft.x - 1)
                {
                    Destroy(gameObject);
                    Destroy(this);
                }
                break;
            case GameManager.State.DEATH:
                Destroy(gameObject);
                Destroy(this);
                break;
        }
    }
}
