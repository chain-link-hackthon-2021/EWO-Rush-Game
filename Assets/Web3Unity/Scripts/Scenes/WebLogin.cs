using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WebLogin : MonoBehaviour
{
    public GameObject LoginUI;
#if UNITY_EDITOR
    private void Start()
    {
        LoginUI.SetActive(false);
    }
#else
    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    private int expirationTime;
    private string account;

    private void Start()
    {
        LoginUI.SetActive(true);
    }

    public void OnLogin()
    {
        Web3Connect();
        OnConnected();
    }

    async private void OnConnected()
    {
        account = ConnectAccount();
        while (account == "") {
            await new WaitForSeconds(1f);
            account = ConnectAccount();
        };
        // save account for next scene
        PlayerPrefs.SetString("Account", account);
        // reset login message
        SetConnectAccount("");

        LoginUI.SetActive(false);
        // load next scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnSkip()
    {
        // burner account for skipped sign in screen
        PlayerPrefs.SetString("Account", "");

        LoginUI.SetActive(false);
        // move to next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  
#endif
}

