using UnityEngine;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject pauseButton;

     [SerializeField]
    private GameObject contunieButton;

    [SerializeField]
    private GameObject startPanel;
    [SerializeField]
    private GameObject succesPanel;
    [SerializeField]
    private GameObject failPanel;
    [SerializeField]
    private GameObject inGamePanel;

    [SerializeField]
    private TextMeshProUGUI playTime;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public TextMeshProUGUI PlayTime { get{return playTime; } set{playTime=value;} }
    public TextMeshProUGUI ScoreText { get{return scoreText; } set{scoreText=value;} }

    private void Awake() 
    {
        GameManager.Instance.GameStateChanged+=OnGameStateChanged;
    }


    void Start()
    {
    }

    void Update()
    {
        
    }

    private void CloseAllPanels()
    {
        pausePanel.SetActive(false);
        startPanel.SetActive(false);
        inGamePanel.SetActive(false);
        succesPanel.SetActive(false);
        failPanel.SetActive(false);
        
    }

    public void StartGame()
    {
        GameManager.Instance.UpdateGameState(GameState.InGame);
    }
    public void ReStartGame()
    {
        GameManager.Instance.ReloadScene();
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        contunieButton.SetActive(true);
        Time.timeScale=0;
    }

    public void ResumeGame()
    { 
        pausePanel.SetActive(false);
        contunieButton.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale=1;
    }

    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            
            case GameState.Start:
                CloseAllPanels();
                startPanel.SetActive(true);
                break;
            case GameState.InGame:
                CloseAllPanels();
                contunieButton.SetActive(false);
                inGamePanel.SetActive(true);
                break;
            case GameState.Fail:
                CloseAllPanels();
                LeanTween.delayedCall(1f,()=>{failPanel.SetActive(true);});
                
                break;
            case GameState.Succes:
                CloseAllPanels();
                LeanTween.delayedCall(2f,()=>{succesPanel.SetActive(true);});
                break;
        }
    }
}
