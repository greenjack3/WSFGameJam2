using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class batonik : MonoBehaviour {

	public void startcik()
    {
        SceneManager.LoadScene("Game");
    }

    public void krakenik()
    {
        SceneManager.LoadScene("uwolnićkrakena");
    }
}
