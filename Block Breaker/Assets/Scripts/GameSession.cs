using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {

    public static GameSession instance;

    private int level;
    private bool gameOver;
    private float initialPaddleSpeed;
    private float initialBallSpeed;

    // config params
    [SerializeField] bool isAutoPlayEnabled;
    [SerializeField] int pointsPerBlockDestroyed = 83;
    [SerializeField] float ballSpeedPerLevel;
    [SerializeField] float paddleSpeedPerLevel;
    [SerializeField] AudioSource menuAudio;
    [SerializeField] AudioSource playingAudio;
    [SerializeField] Text gameOverScoreText;
    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;
    [SerializeField] Text levelText;
    [SerializeField] GameObject ball;
    public float paddleMvtSpeed = 15f;
    public float ballInitialXPosition = 10f;
    public float ballInitialYPosition = 0.71f;
    public float ballSpeed = 15f; // Also magnitude
    public float NonDropChance = 0.90f; // means drop chance = 0.1 = 10%

    public int numberOfActiveBalls { get; private set; }
    public int lives { get; private set; }

    // state variables
    [SerializeField] int currentScore = 0;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Game Over"))
        {
            gameOver = true;
            InGameUIToggle(!gameOver);
            BGAudioToggle();
            SetGameOverScoreText();
            gameOverScoreText.enabled = true;
        }
        else if (scene.name.Equals("Start Menu"))
        {
            gameOver = true;
            gameOverScoreText.enabled = false;
            BGAudioToggle();
            InGameUIToggle(!gameOver);
        }
        else if(scene.name.Contains("Level 1"))
        {
            gameOver = false;
            AddBallToScene();
            BGAudioToggle();
            InGameUIToggle(!gameOver);
        }
        else
        {
            for(int i = 0; i < numberOfActiveBalls; i++)
            {
                Instantiate(ball, new Vector3(ballInitialXPosition, ballInitialYPosition, 0), Quaternion.identity);
            }
        }
    }
    private void Start()
    {
        initialBallSpeed = ballSpeed;
        initialPaddleSpeed = paddleMvtSpeed;
        ResetGame();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        SetScoreText();
    }

    public void ResetGame()
    {
        numberOfActiveBalls = 0;
        lives = 3;
        currentScore = 0;
        level = 0;
        ballSpeed = initialBallSpeed;
        paddleMvtSpeed = initialPaddleSpeed;
        
        SetLivesText();
        SetLevelText();
        SetScoreText();
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    public void AddBallToScene()
    {
        numberOfActiveBalls++;
        Instantiate(ball, new Vector3(ballInitialXPosition, ballInitialYPosition, 0), Quaternion.identity);
    }
    public void DestroyBallFromScene(GameObject ball)
    {
        numberOfActiveBalls--;
        Destroy(ball);
    }
    public void AddLife()
    {
        lives++;
        SetLivesText();
    }
    public void SubLife()
    {
        lives--;
        SetLivesText();
    }
    private void SetLivesText()
    {
        livesText.text = "Lives : " + lives.ToString();
    }

    private void SetLevelText()
    {
        levelText.text = "Level : " + level.ToString();
    }

    private void SetScoreText()
    {
        scoreText.text = currentScore.ToString();
    }

    private void SetGameOverScoreText()
    {
        gameOverScoreText.text = "Your score is :\n" + currentScore.ToString();
    }

    private void InGameUIToggle(bool value)
    {
        scoreText.enabled = value;
        livesText.enabled = value;
        levelText.enabled = value;
        
    }

    private void BGAudioToggle()
    {
        if(!gameOver)
        {
            menuAudio.Stop();
            playingAudio.Play();
        }
        else
        {
            menuAudio.Play();
            playingAudio.Stop();
        }
    }

    public void LevelUp()
    {
        level++;
        SetLevelText();
        ballSpeed += ballSpeedPerLevel;
        paddleMvtSpeed += paddleSpeedPerLevel;
    }

}