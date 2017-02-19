using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class batonik : MonoBehaviour {

    public GameObject panel;
    public GameObject kreds;

	public void Starcik()
    {
        SceneManager.LoadScene("Game");
    }

    public void Krakenik()
    {
        SceneManager.LoadScene("uwolnićkrakena");
    }

    public void Kreds()
    {
        panel.SetActive(false);
        kreds.SetActive(true);
    }

    public void KredReturn()
    {
        kreds.SetActive(false);
        panel.SetActive(true);
    }
}
