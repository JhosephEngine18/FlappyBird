using UnityEngine;
using UnityEngine.SceneManagement;

class PauseMenu : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
    }
    public void ResumeGame()
    {
        if (!gameObject.GetComponent<GameLogic>().isAlive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
