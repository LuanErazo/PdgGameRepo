using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene(3); // 3 = gameMOdeLuan
    }
}
