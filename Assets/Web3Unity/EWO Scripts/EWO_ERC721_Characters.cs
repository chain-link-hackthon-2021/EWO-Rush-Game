using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System;
using UnityEngine.UI;
/*
0: "3"
1: "41"
2: "15"
3: "85"
4: "56"
5: "78"
6: "Eren"
agility: "15"
charisma: "85"
intelligence: "78"
name: "Eren"
power: "56"
speed: "3"
strength: "41"
*/
[JsonObject]
public class JsonItem
{
    [JsonProperty("0")]
    public string value0 { get; set; }

    [JsonProperty("1")]
    public string value1 { get; set; }

    [JsonProperty("2")]
    public string value2 { get; set; }

    [JsonProperty("3")]
    public string value3 { get; set; }

    [JsonProperty("4")]
    public string value4 { get; set; }

    [JsonProperty("5")]
    public string value5 { get; set; }
}
 public class StatCollection
 {
    /*
    speed   uint256 :  3
    strength   uint256 :  41
    agility   uint256 :  15
    charisma   uint256 :  85
    power   uint256 :  56
    intelligence   uint256 :  78
    name: Eren  
    */
     public string speed;
     public string strength;
     public string agility;
     public string charisma;
     public string power;
     public string intelligence;
     public string name;

 }
public class EWO_ERC721_Characters : MonoBehaviour
{
    public Text Speed;
    public Text Strength;
    public Text Agility;

    public GameObject PlayerStatDisplay;
    public GameObject PlayerModelDisplay;
    public CharacterInputController characterInputController;
        string chain = "ethereum";
        string network = "rinkeby";
        string contract = "0x8B7995c357592Ee93FD88bA16e146E619FcCFCD0";
        // string account = "0xbd53Ef09C8e5C31004d57E7D297f221C08560FF1";
        string account="";

        string method = "tokenOfOwnerByIndex";
        // abi in json format
        string abi = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"_VRFCoordinator\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"_LinkToken\",\"type\":\"address\"},{\"internalType\":\"bytes32\",\"name\":\"_keyhash\",\"type\":\"bytes32\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"approved\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"Approval\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"ApprovalForAll\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"Transfer\",\"type\":\"event\"},{\"inputs\":[],\"name\":\"LinkToken\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"VRFCoordinator\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"approve\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"baseURI\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"characters\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"speed\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"strength\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"agility\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"charisma\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"power\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"intelligence\",\"type\":\"uint256\"},{\"internalType\":\"string\",\"name\":\"name\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"getApproved\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"getCharacterOverView\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"},{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"getCharacterStats\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getNumberOfCharacters\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"getTokenURI\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"}],\"name\":\"isApprovedForAll\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"ownerOf\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"randomResult\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes32\",\"name\":\"requestId\",\"type\":\"bytes32\"},{\"internalType\":\"uint256\",\"name\":\"randomness\",\"type\":\"uint256\"}],\"name\":\"rawFulfillRandomness\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"userProvidedSeed\",\"type\":\"uint256\"},{\"internalType\":\"string\",\"name\":\"name\",\"type\":\"string\"}],\"name\":\"requestNewRandomCharacter\",\"outputs\":[{\"internalType\":\"bytes32\",\"name\":\"\",\"type\":\"bytes32\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"safeTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"_data\",\"type\":\"bytes\"}],\"name\":\"safeTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"setApprovalForAll\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"},{\"internalType\":\"string\",\"name\":\"_tokenURI\",\"type\":\"string\"}],\"name\":\"setTokenURI\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes4\",\"name\":\"interfaceId\",\"type\":\"bytes4\"}],\"name\":\"supportsInterface\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"index\",\"type\":\"uint256\"}],\"name\":\"tokenByIndex\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"index\",\"type\":\"uint256\"}],\"name\":\"tokenOfOwnerByIndex\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"tokenURI\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"totalSupply\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"transferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";

        string response0, response1;
        bool charactersLoaded = false;

    private void Start()
    {
        //HideCharacterSelectUI();
        account = PlayerPrefs.GetString("Account"); //get player account
    }
    public void OnBalanceOf()
    {
        BalanceOf();
        GetTokenOfOwnerByIndex();
    }

    public void getCharacter0()
    {
        if(!charactersLoaded){
            print("Character0 not loaded or is not available");
        }
        else CallGetCharacterStats(response0);
    }

    public void getCharacter1()
    {
        if(!charactersLoaded){
            print("Character1 not loaded or is not available");
        }
        else CallGetCharacterStats(response1);
    }
    async void BalanceOf()
    {

        int balance = await ERC721.BalanceOf(chain, network, contract, account);
        print("Balance: " + balance);
    }

    async void GetTokenOfOwnerByIndex() //removed parameter int i
    {
        string index0 = "0";
        string index1 = "1";
        string owner = account;
        string method = "tokenOfOwnerByIndex";

        // array of arguments for contract
        // string args0 = "[\"0xbd53Ef09C8e5C31004d57E7D297f221C08560FF1\", 0]"; //
        // string args1= "[\"0xbd53Ef09C8e5C31004d57E7D297f221C08560FF1\", 1]";

        string[] obj0 = { owner, index0 };
        string args0 = JsonConvert.SerializeObject(obj0);
        response0 = await EVM.Call(chain, network, contract, abi, method, args0);
        print("TokenId at Dynamic index : " + response0);

        string[] obj1 = { owner, index1 };
        string args1 = JsonConvert.SerializeObject(obj1);
        response1 = await EVM.Call(chain, network, contract, abi, method, args1);
        print("TokenId at Dynamic index : " + response1);

        charactersLoaded = true;
    }

    async void CallGetCharacterStats(string tokenid)
    {
        string method = "getCharacterStats"; 
        string[] obj3 = { tokenid};
        string args3 = JsonConvert.SerializeObject(obj3);
        string res = await EVM.Call(chain, network, contract, abi, method, args3);
        // print(res);
        JsonItem jsonText = JsonConvert.DeserializeObject<JsonItem>(res);
        print("Speed: " + jsonText.value0);
        print("Strength: " + jsonText.value1);
        print("Agility: " + jsonText.value2);
        print("Charisma: " + jsonText.value3);
        print("Power: " + jsonText.value4);
        print("Intelligence: " + jsonText.value5);
    }

    //stats are populated depending on the character selection screen
    public async void PopulateStats()
    {
        string method = "getCharacterStats";
        string[] obj3 = { PlayerData.instance.usedCharacter.ToString()};
        string args3 = JsonConvert.SerializeObject(obj3);
        string res = await EVM.Call(chain, network, contract, abi, method, args3);
        // print(res);

        JsonItem jsonText = JsonConvert.DeserializeObject<JsonItem>(res);

        Speed.text = "Speed: " + jsonText.value0;
        Strength.text = "Strength: " + jsonText.value1;
        Agility.text = "Agility: " + jsonText.value2;

        /*
        unit = 100/actual_range
        Actual_value_for_game = Value/unit + Minimum range

        agility = 11
        Minimum = 2 to 22
        unit = 100/(22-2)
        unit = 100/20
        unit = 5

        Actual = 11/5 + 2

        Actual: 4

        jump : 2-6 = 4; 100/4 =
        speed : 14-42 = 28; 100/28 =
        */
        //speed
        characterInputController.laneChangeSpeed = (int.Parse(jsonText.value0) / (100 - 28)) + 14;
        //strength
        characterInputController.jumpLength = (float.Parse(jsonText.value1) / (100 / 4)) + 2;
        //agility
        characterInputController.slideLength = (float.Parse(jsonText.value2) / (100 / 20)) + 2;
        //charisma
        //power
        //intelligence

        
    }


    void UpdateStats()
    {
        Speed.text = characterInputController.laneChangeSpeed.ToString();
    }

    public void ShowCharacterSelectUI()
    {
        PlayerStatDisplay.SetActive(true);
        PlayerModelDisplay.SetActive(true);
    }

    public void HideCharacterSelectUI()
    {
        PlayerStatDisplay.SetActive(false);
        PlayerModelDisplay.SetActive(false);
    }
}


