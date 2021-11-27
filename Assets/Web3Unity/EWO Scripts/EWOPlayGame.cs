using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_WEBGL
public class EWOPlayGame : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    private int expirationTime;
    private string account; 

    // For printing Scene in UI
    // public Text transactionHash;

    string chain = "ethereum"; 
    string network = "rinkeby"; //configure the network as required
    // public bool feesPaid = false; //May need to change in future and check transaction status instead

    public void OnPlayGame()
    {
        OnConnectedPayFees();
    }

    public void OnLogin()
    {
        Web3Connect();
        OnConnected();
    }
    
    async public void OnConnectedPayFees()
    {
        string account = PlayerPrefs.GetString("Account");
        print(account);

        // account to send to
        string to = "0xE68F72B760f4AdcDf72Dcab4c018a955abf3E23C"; // account 2 in wallet
        // amount in wei to send
        string value = "100000000000000";
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // connects to user's browser wallet (metamask) to send a transaction
        try {
            string response = await Web3GL.SendTransaction(to, value, gasLimit, gasPrice);
            Debug.Log("Response: " + response);

            // string txConfirmed = await EVM.TxStatus(chain, network, transaction); // success, fail, pending

            await new WaitForSeconds(10f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        } catch (Exception e) {
            Debug.LogException(e, this);
        }
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
        // load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
#endif
