using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject winPopupPanel;
    public Text winPopupText;
    public Button rematchButton;
    public Button quitButton;

    [SerializeField] private AudioClip soundClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add this to refresh references when scene reloads
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find references again after scene reload
        winPopupPanel = GameObject.FindGameObjectWithTag("WinPopup");
        winPopupText = GameObject.FindGameObjectWithTag("WinText")?.GetComponent<Text>();

        rematchButton = GameObject.FindGameObjectWithTag("RematchButton")?.GetComponent<Button>();
        quitButton = GameObject.FindGameObjectWithTag("QuitButton")?.GetComponent<Button>();

        if (rematchButton != null)
        {
            rematchButton.onClick.RemoveAllListeners();  // Clear any existing listeners
            rematchButton.onClick.AddListener(RestartGame);  // Add new listener
            rematchButton.onClick.AddListener(soundPlay);
        }

        if (quitButton != null)
        {
            quitButton.onClick.RemoveAllListeners();
            quitButton.onClick.AddListener(QuitToMenu);
            quitButton.onClick.AddListener(soundPlay);
        }

        if (winPopupPanel != null)
        {
            winPopupPanel.SetActive(false);
        }
    }

    public void PlayerDied(int playerNumber)
    {
        Debug.Log("Player died");

        // find both dinos and make invulnerable so they don't continue to take damage
        DinoHealth[] allDinos = FindObjectsOfType<DinoHealth>();
        foreach (DinoHealth dino in allDinos)
        {
            dino.SetInvulnerable(true);

            // TODO::get movement script and disable attack ability
            // maybe
        }

        ShowWinPopupPanel(playerNumber);
    }

    public void ShowWinPopupPanel(int playerNumber)
    {
        if (winPopupPanel == null || winPopupText == null)
        {
            Debug.LogError("Missing references! Check if UI elements have correct tags.");
            return;
        }

        int winNum;

        if (playerNumber == 1)
        {
            winNum = 2;
        } else
        {
            winNum = 1;
        }

        string winner = "Player " + winNum.ToString() + " Wins!";
        winPopupText.text = winner;
        winPopupPanel.SetActive(true);
    }

    public void RestartGame()
    {
        // reset all dinos before restarting
        DinoHealth[] allDinos = FindObjectsOfType<DinoHealth>();
        foreach (DinoHealth dino in allDinos)
        {
            dino.SetInvulnerable(false);


        }
        if (winPopupPanel != null)
        {
            winPopupPanel.SetActive(false);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        Debug.Log("QuitToMenu called");
        StartCoroutine(LoadMenuSceneCoroutine());
    }

    private IEnumerator LoadMenuSceneCoroutine()
    {
        if (winPopupPanel != null)
        {
            winPopupPanel.SetActive(false);
        }

        yield return new WaitForSeconds(0.1f); // Small delay to ensure UI updates

        try
        {
            SceneManager.LoadScene("MenuScene");
            // Optional: Destroy the GameManager when returning to menu
            Destroy(gameObject);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error loading menu scene: " + e.Message);
        }
    }

    public void soundPlay(){
        AudioSource.PlayClipAtPoint(soundClip, transform.position, 1f);
    }
}
