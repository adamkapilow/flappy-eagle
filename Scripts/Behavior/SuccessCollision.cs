using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SuccessCollision : MonoBehaviour
{ 
    /* Manages when the player flies through a pipe pair successfully.
     * This plays a success sound, increases the score, and spawns in a new pipe.
     */
    public AudioSource successSound;

    private void Awake()
    {
        successSound.volume = PlayerPrefs.GetFloat("effects volume", 0.6f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collision with the player character
        if (collision.gameObject.layer == 6)
        {
            GameManager.Instance.Score += 1;
            successSound.Play();
            float newX = GameManager.Instance.topRight.x + 1;
            float edge = GameManager.Instance.pipeSeparation + 0.5f;
            float newY = Random.Range(GameManager.Instance.bottomLeft.y + edge  , GameManager.Instance.topRight.y - edge);
            GameObject nextPipe = Instantiate(GameManager.Instance.pipe, new Vector3(newX, newY, 0), Quaternion.identity);
            nextPipe.SetActive(true);
            Destroy(this);
        }

    }
}