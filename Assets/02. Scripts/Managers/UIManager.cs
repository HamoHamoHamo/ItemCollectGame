using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    [Header("In-Game UI")]
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Game End Panel")]
    [SerializeField] private GameObject gameEndPanel;
    [SerializeField] private TextMeshProUGUI clearTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private GameManager gameManager;

    private void Awake()
    {
        instance = this;
        Debug.Log($"UIManager Awake - ID: {GetInstanceID()}");
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
        instance = null;
    }

    private void Start()
    {
        Debug.Log($"UIManager Start - ID: {GetInstanceID()}");
        gameManager = GameManager.Instance;
        Debug.Log($"GameManager ID: {gameManager.GetInstanceID()}");
        
        SubscribeToEvents();
        SetupUI();
        
        // 초기 UI 업데이트 강제 호출
        UpdateItemCountUI(gameManager.CurrentItemCount, gameManager.TargetItemCount);
        UpdateTimerUI(gameManager.GameTime);
    }

    private void SetupUI()
    {
        if (gameEndPanel != null)
        {
            gameEndPanel.SetActive(false);
        }

        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners(); // 기존 리스너 제거
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        if (quitButton != null)
        {
            quitButton.onClick.RemoveAllListeners(); // 기존 리스너 제거
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }
    }

    private void SubscribeToEvents()
    {
        if (gameManager != null)
        {
            Debug.Log("이벤트 구독 중...");
            gameManager.OnItemCountChanged += UpdateItemCountUI;
            gameManager.OnTimeUpdated += UpdateTimerUI;
            gameManager.OnGameEnd += ShowGameEndPanel;
        }
        else
        {
            Debug.LogError("GameManager가 null입니다!");
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (gameManager != null)
        {
            gameManager.OnItemCountChanged -= UpdateItemCountUI;
            gameManager.OnTimeUpdated -= UpdateTimerUI;
            gameManager.OnGameEnd -= ShowGameEndPanel;
        }
    }

    private void UpdateItemCountUI(int current, int target)
    {
        Debug.Log($"UI 업데이트: {current} / {target}");
        if (itemCountText != null)
        {
            itemCountText.text = $"{current} / {target}";
        }
        else
        {
            Debug.LogError("itemCountText가 null입니다!");
        }
    }

    private void UpdateTimerUI(float time)
    {
        if (timerText != null)
        {
            timerText.text = gameManager.GetFormattedTime(time);
        }
    }

    private void ShowGameEndPanel(float clearTime, float bestTime)
    {
        if (gameEndPanel != null)
        {
            gameEndPanel.SetActive(true);
        }

        if (clearTimeText != null)
        {
            clearTimeText.text = $"Clear Time: {gameManager.GetFormattedTime(clearTime)}";
        }

        if (bestTimeText != null)
        {
            if (bestTime == float.MaxValue)
            {
                bestTimeText.text = "Best Time: --:--";
            }
            else
            {
                bestTimeText.text = $"Best Time: {gameManager.GetFormattedTime(bestTime)}";
            }
        }
    }

    private void OnRestartButtonClicked()
    {
        Debug.Log("Restart 버튼 클릭");
        gameManager.RestartGame();
    }

    private void OnQuitButtonClicked()
    {
        gameManager.QuitGame();
    }
}
