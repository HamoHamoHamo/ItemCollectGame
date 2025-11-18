using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    [SerializeField] private int targetItemCount = 10;

    private int currentItemCount = 0;
    private float gameTime = 0f;
    private bool isGameActive = false;
    private float bestTime = float.MaxValue;

    private const string BEST_TIME_KEY = "BestTime";

    public event Action<int, int> OnItemCountChanged;
    public event Action<float> OnTimeUpdated;
    public event Action<float, float> OnGameEnd;

    public int CurrentItemCount => currentItemCount;
    public int TargetItemCount => targetItemCount;
    public float GameTime => gameTime;
    public bool IsGameActive => isGameActive;

    protected override void Awake()
    {
        base.Awake();
        LoadBestTime();
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (isGameActive)
        {
            gameTime += Time.deltaTime;
            OnTimeUpdated?.Invoke(gameTime);
        }
    }

    public void StartGame()
    {
        currentItemCount = 0;
        gameTime = 0f;
        isGameActive = true;

        OnItemCountChanged?.Invoke(currentItemCount, targetItemCount);
        OnTimeUpdated?.Invoke(gameTime);
    }

    public void CollectItem()
    {
        if (!isGameActive) return;

        currentItemCount++;
        OnItemCountChanged?.Invoke(currentItemCount, targetItemCount);

        if (currentItemCount >= targetItemCount)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        isGameActive = false;

        if (gameTime < bestTime)
        {
            bestTime = gameTime;
            SaveBestTime();
        }

        OnGameEnd?.Invoke(gameTime, bestTime);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartGame();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void LoadBestTime()
    {
        if (PlayerPrefs.HasKey(BEST_TIME_KEY))
        {
            bestTime = PlayerPrefs.GetFloat(BEST_TIME_KEY);
        }
    }

    private void SaveBestTime()
    {
        PlayerPrefs.SetFloat(BEST_TIME_KEY, bestTime);
        PlayerPrefs.Save();
    }

    public string GetFormattedTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float GetBestTime()
    {
        return bestTime;
    }
}
