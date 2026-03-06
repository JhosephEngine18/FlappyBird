using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
public class GameLogic : MonoBehaviour
{   
    [HideInInspector] public bool isAlive = true;
    private int points;
    [Header("AudioClips")]
    [SerializeField] private AudioClip hitAudio;
    [Tooltip("Esto se reproducira cuando el jugador consiga puntos")]
    [SerializeField] private AudioClip pointsAudio;
    [Tooltip("Esto se reproducira cuando el jugador se afectado por un hongo pequeno")]
    [SerializeField] private AudioClip madeSmallAudio;
    [Tooltip("Esto se reproducira cuando el jugador obtenga un escudo")]
    [SerializeField] private AudioClip ShieldAudio;
    [Header("Texts")] 
    [SerializeField] private GameObject GameOverText;
    [SerializeField] private GameObject Image;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI MaxPoints;
    
    private int TimesToRepeat;

    private void OnEnable()
    {
        TriggerDataSender.OnPlayerStateChanged += CallingState;
    }

    private void OnDisable()
    {
        TriggerDataSender.OnPlayerStateChanged -= CallingState;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level")
        {
            StartCoroutine(StartGame());
        }
    }

    private void CallingState(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Killer":
                SaveGameData();
                PlayerDeath();
                break;
            case "Points":
                points++;
                pointsText.text = points.ToString();
                AudioSource.PlayClipAtPoint(pointsAudio, transform.position);
                break;
            case "Gold":
                points += 4;
                pointsText.text = points.ToString();
                AudioSource.PlayClipAtPoint(pointsAudio, transform.position);
                Destroy(collision.gameObject);
                break;
            case "Silver":
                points += 3;
                pointsText.text = points.ToString();
                AudioSource.PlayClipAtPoint(pointsAudio, transform.position);
                Destroy(collision.gameObject);
                break;
            case "Bronze":
                points += 2;
                pointsText.text = points.ToString();
                AudioSource.PlayClipAtPoint(pointsAudio, transform.position);
                Destroy(collision.gameObject);
                break;
            case "SmallPowerUp":
                TimesToRepeat++;
                StartCoroutine(ObjectsEffect(TimesToRepeat));
                Destroy(collision.gameObject);
                break;
            case "Shield":
                break;
        }
    }
    
    private void PlayerDeath()
    {
        //This will happen when player dies
        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
        Time.timeScale = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameOverText.SetActive(true);
        isAlive = false;
        //This waits for players input
        StartCoroutine(WaitForPlayerInput());
    }

    IEnumerator WaitForPlayerInput()
    {
        while (true)
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (Keyboard.current.spaceKey.isPressed || Mouse.current.leftButton.isPressed)
                {
                    Time.timeScale = 1;
                    SceneManager.LoadScene("Level");
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
                    isAlive = true;
                    break;
                }
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                if (Touchscreen.current.press.isPressed)
                {
                    Time.timeScale = 1;
                    SceneManager.LoadScene("Level");
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
                    isAlive = true;
                    break;
                }
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    IEnumerator StartGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        LoadGameData();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);
        Image.SetActive(false);
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
    }
    
    public void SaveGameData()
    {
        if (points > PlayerPrefs.GetInt("MaxScore1"))
        {
            PlayerPrefs.SetInt("MaxScore1", points);
        }
        else if (points > PlayerPrefs.GetInt("MaxScore2") && points <= PlayerPrefs.GetInt("MaxScore1") )
        {
            PlayerPrefs.SetInt("MaxScore2", points);
        }
        else if (points > PlayerPrefs.GetInt("MaxScore3") && points <= PlayerPrefs.GetInt("MaxScore2") )
        {
            PlayerPrefs.SetInt("MaxScore3", points);
        }
    }

    private void LoadGameData()
    {
        MaxPoints.text = "Max Score: " + PlayerPrefs.GetInt("MaxScore1", 0);
        PlayerPrefs.GetInt("MaxScore2", 0);
        PlayerPrefs.GetInt("MaxScore3", 0);
    }

    IEnumerator ObjectsEffect(int _TimesToRepeat)
    {
        AudioSource.PlayClipAtPoint(madeSmallAudio, transform.position);
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size = new Vector2(0.2f, 0.2f);
        GameObject.FindGameObjectWithTag("PlayerTexture").gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
        while (_TimesToRepeat > 0)
        {
            yield return new WaitForSecondsRealtime(5f);
            _TimesToRepeat--;
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().size = new Vector2(0.4f, 0.4f);
        GameObject.FindGameObjectWithTag("PlayerTexture").gameObject.transform.localScale = new Vector2(1f, 1f);
    }
}


