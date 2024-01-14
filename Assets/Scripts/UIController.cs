using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private TMP_Text scoreText, finalScoreText;
    [SerializeField] private Image fadeImage;
    [SerializeField] private Button settingButton, closeButton;
    [SerializeField] private GameObject soundPanel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Sprite resumeSprite, pauseSprite;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    private Image pauseButtonImage;
    private Blade blade;
    private Spawner spawner;
    private int score;
    private bool isPaused;
    private SoundManager soundManager;
    #endregion

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
        pauseButtonImage = pauseButton.GetComponent<Image>();
        soundManager = FindObjectOfType<SoundManager>();
    }
 
    #region MonoBheviour methods
    private void Start()
    {
        NewGame();
    }
    private void OnEnable()
    {
        settingButton.onClick.AddListener(EnableSoundSettings);
        closeButton.onClick.AddListener(DisableSoundSettings);
        pauseButton.onClick.AddListener(PauseGame);
    }
    private void OnDisable()
    {
        settingButton?.onClick.RemoveAllListeners();
    }
    #endregion
    #region Functionality methods
    private void NewGame()
    {
        Time.timeScale = 1f;
        blade.enabled = true;
        spawner.enabled = true;
        score = 0;
        scoreText.text = score.ToString();
        ClearScene();
        soundManager.musicAudioSource.Play();
    }
    private void ClearScene()
    {
        FruitController[] fruits = FindObjectsOfType<FruitController>();
        foreach (FruitController fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }
        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }
    public void IncreaseScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }
    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;
        StartCoroutine(ExplodeSequence());
    }
    #endregion

    #region Coroutines
    private IEnumerator ExplodeSequence()
    {
        yield return new WaitForSeconds(1);
        soundManager.musicAudioSource.Stop();
        soundManager.PlaySFX("GameOver");
        float elapsed = 0f;
        float duration = 0.5f;
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.black, t);
            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        //NewGame();
        elapsed = 0f;
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.black, Color.clear, t);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        EnableGameOver();
    }
    private void EnableGameOver()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Score: " + score;
        Timer.Instance.ResetTimer();
    }
    public void EnableSoundSettings()
    {
        blade.enabled = false;
        spawner.enabled = false;
        soundPanel.SetActive(true);
    }
    public void DisableSoundSettings()
    {
        blade.enabled = true;
        spawner.enabled = true;
        soundPanel.SetActive(false);
    }
    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            pauseButtonImage.sprite = pauseSprite;
            isPaused = false;
            pausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            pauseButtonImage.sprite = resumeSprite;
            isPaused = true;
            pausePanel.SetActive(true);
        }
    }
   
    
    #endregion
}
