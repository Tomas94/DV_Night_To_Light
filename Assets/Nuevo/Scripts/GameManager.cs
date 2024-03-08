using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] UI_InGame _uInterface;
    [SerializeField] PlayerLogic _player;
    [SerializeField] Flashlight _flashlight;
    [SerializeField] Checkpoint _checkpoint;
    [SerializeField] Notas_UI _noteBox;


    public PlayerLogic Player { get { return _player; } }
    public UI_InGame UI { get { return _uInterface; } }
    public Flashlight Flashlight { get { return _flashlight; } }
    public Checkpoint Checkpoint { get { return _checkpoint; } set { _checkpoint = value; } }
    public Notas_UI NoteBox { get {  return _noteBox; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else Destroy(gameObject);
    }

    public void SetPlayerRef(PlayerLogic player)
    {
        _player = player;
    }

    public void SetUIRef(UI_InGame ui)
    {
        _uInterface = ui;
    }

    public void SetFlashlightRef(Flashlight flashlight)
    {
        _flashlight = flashlight;
    }

    public void SetTextUIBox(Notas_UI textBox)
    {
        _noteBox = textBox;
    }

    #region Lo anterior
    /*[SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject uiPlayer;

    private bool isPaused;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            UpdateGameState();
            ShowPausePanel();
            NotShowUIPlayer();
        }
    }

    private void UpdateGameState()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;

        }
        else
        {
            Time.timeScale = 1f;

        }
    }

    private void ShowPausePanel()
    {
        if (isPaused)
        {
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
        }
    }

    private void NotShowUIPlayer()
    {
        if (isPaused)
        {
            uiPlayer.SetActive(false);
        }
        else
        {
            uiPlayer.SetActive(true);
        }
    }
    */
    #endregion
}
