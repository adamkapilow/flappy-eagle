using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Main state manager for the game. Main functionality is moving between the various
 * menus and play states. This is the point of reference for other 
 * single instance entities, e.g. the pipe model and corner positions.
 * 
 */
public class GameManager : MonoBehaviour
{
    //Game objects
    public GameObject pipe;
    public GameObject player;
    private Rigidbody2D playerBody;

    //UI fields
    public Text ScoreText;
    public Text highScoreText;
    public Text deathText;
    public GameObject panelPlay;
    public GameObject panelMenu;
    public GameObject panelDeath;
    public GameObject panelOptions;

    //Corners of the screen, used later for pipe spawning
    public Vector3 topRight;
    public Vector3 bottomLeft;

    //Vertail pipe separation variables
    public float pipeSeparation;
    public int difficulty;

    public AudioSource music;

    public enum State { MENU, INIT, PLAY, DEATH, OPTIONS};
    State currentState;

    public static GameManager Instance { get; private set; }


    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            if (value > highScore[difficulty])
            {
                highScore[difficulty] = value;
            }
            ScoreText.text = "Score: " + score + "\nHigh Score: " + highScore[difficulty];
        }
    }

    //High scores handled separately for three difficulty settings
    private int[] highScore;
    public int[] HighScore { get { return highScore; } set { highScore = value;} }

    // Sets the singleton instance of this, and loads score save data.
    private void Awake()
    {
        
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        ScoreData data = SaveSystem.Load();
        if (data == null)
        {
            highScore = new int[3];
            return;
        }
        highScore = data.highScore;
    }

    // Sets the dimensions of the pipe model given the player difficulty. 
    public void setDimensions(int difficulty)
    {
        PlayerPrefs.SetInt("difficulty", difficulty);
        this.difficulty = difficulty;
        Transform topPipe = pipe.transform.Find("Top Pipe");
        Transform bottomPipe = pipe.transform.Find("Bottom Pipe");
        Transform pipeCenter = pipe.transform.Find("Pipe Center");
        if (difficulty == 0)
        {
            pipeSeparation = 1.8f;
        }
        else if (difficulty == 1)
        {
            pipeSeparation = 1.5f;
        }
        else
        {
            pipeSeparation = 2.3f;
        }
        pipeCenter.localScale.Set(pipeCenter.localScale.x, pipeSeparation, pipeCenter.localScale.z);
        Vector3 displacementVector = (pipeSeparation + topPipe.localScale.y / 2) * Vector3.up;
        topPipe.localPosition = pipeCenter.localPosition + displacementVector;
        bottomPipe.localPosition = pipeCenter.localPosition - displacementVector;
        
    }

    /* Activates menu, initializes fields, loads in player preferences, 
     * and sets up pipe dimensions.
     */
    private void Start()
    {
        playerBody = player.GetComponent<Rigidbody2D>();
        SwitchState(State.MENU);
        topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        difficulty = PlayerPrefs.GetInt("difficulty", 0);
        music.volume = PlayerPrefs.GetFloat("music volume", 1);
        Time.timeScale = PlayerPrefs.GetFloat("time scale", 1);
        setDimensions(difficulty);
        

    }



    // Game state management, used for switching between menus, death, and play states. 
    public State getCurrentState()
    {
        return this.currentState;
    }

    public void SwitchState(State newState)
    {
        EndState();
        currentState = newState;
        BeginState(newState);
    }

    public void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                Cursor.visible = true;
                playerBody.constraints = RigidbodyConstraints2D.FreezeAll;
                panelPlay.SetActive(false);
                panelMenu.SetActive(true);
                music.Play();
                break;
            case State.INIT:
                Score = 0;
                difficulty = PlayerPrefs.GetInt("difficulty", 0);
                Cursor.visible = false;
                player.transform.position = new Vector3(0, 0, 0);
                playerBody.velocity = Vector2.zero;
                playerBody.constraints = RigidbodyConstraints2D.FreezeAll;
                GameObject firstPipe = Instantiate(pipe, new Vector3(1.3f*topRight.x, 0, 0), Quaternion.identity);
                player.SetActive(true);
                panelPlay.SetActive(true);
                firstPipe.SetActive(true);
                break;
            case State.OPTIONS:
                Cursor.visible = true;
                panelOptions.SetActive(true);
                break;
            case State.DEATH:
                SaveSystem.Save();
                Cursor.visible = true;
                playerBody.velocity = Vector2.zero;
                player.SetActive(false);
                panelPlay.SetActive(false);
                highScoreText.text = "Your high score is: " + highScore[difficulty];
                deathText.text = "Your score was: " + score;
                panelDeath.SetActive(true);
                break;
        }
    }

    public void EndState()
    {
        switch (currentState)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.DEATH:
                panelDeath.SetActive(false);
                break;
            case State.OPTIONS:
                panelOptions.SetActive(false);
                break;
        }
    }
}
