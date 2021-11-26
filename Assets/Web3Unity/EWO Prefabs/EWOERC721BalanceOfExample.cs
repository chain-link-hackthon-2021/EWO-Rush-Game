using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;

public class EWOERC721BalanceOfExample : MonoBehaviour
{
    public void OnBalanceOf()
    {
        BalanceOf();
    }
    async void BalanceOf()
    {
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = "0x8B7995c357592Ee93FD88bA16e146E619FcCFCD0";
        string account = "0xbd53Ef09C8e5C31004d57E7D297f221C08560FF1";

        int balance = await ERC721.BalanceOf(chain, network, contract, account);
        print(balance);
    }
}
