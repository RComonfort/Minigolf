using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string nextLevelName = "scene";
    [SerializeField] private int coinsCollected = 0;
    [SerializeField] private int coinsNeeded = 0;

    public int CoinsNeeded {get => coinsNeeded;}
    public int CoinsCollected {get => coinsCollected;}

    public TextMeshProUGUI timeGUI;
    public TextMeshProUGUI strokesGUI;

    public bool isGameOver {get; private set;} = false;

    public GameObject pauseGUI; 

    public static GameManager instance = null;

    Player player;
    bool isPaused = false;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Debug.LogError("GameManager found a second instance in level");
    }

    // Start is called before the first frame update
    void Start()
    {
        coinsNeeded = FindObjectsOfType<Coin>().Length;
        player = FindObjectOfType<Player>();
    }

    private void Update() {
        int minutes = (int)(Time.timeSinceLevelLoad / 60);
        int seconds = (int)Time.timeSinceLevelLoad % 60;

        timeGUI.SetText(string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00")));

        strokesGUI.SetText(player.numberOfShots.ToString());

        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame(!isPaused);
    }


    public void AddCoinCollected() {
        coinsCollected++;
    }

    public void GameOver() {
       
       if (isGameOver)
        return;

        isGameOver = true;

        Invoke("LoadNextLevel", 3f);
    }

    void LoadNextLevel() {
        SceneManager.LoadScene(nextLevelName);
    }

    public void Restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetPlayer() {
        player.ResetToStart();
    }

    public void PauseGame(bool enabled) {
        pauseGUI.SetActive(enabled);

        Time.timeScale = enabled ? 0 : 1;

        isPaused = enabled;
    }
}
