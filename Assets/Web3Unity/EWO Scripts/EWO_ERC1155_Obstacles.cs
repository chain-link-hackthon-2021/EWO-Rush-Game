using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

#if UNITY_WEBGL
public class EWO_ERC1155_Obstacles : MonoBehaviour
{
    public int[] obstacleIdValues; // [3, 5, 3]
    public int landIdValues; // [5]
    public int[] obstacleIdType;   // [1, 2, 0]
    public string obstacleowner1, obstacleowner2, obstacleowner3, landOwner;

    //////////////////////////////////// For Testing ////////////////////////////////////////////

    public void generateObstacleList()
    {
        generateDUMMYObstacleHitList();
        fetchDUMMYObstacleIdType(); // This is 0 for GREEN_DUSTBIN, 1 for RED_BAR and 2 for BLUE_BAR
    }
    
    public void generateDUMMYObstacleHitList()
    {
        UnityEngine.Random.InitState(42);
        obstacleIdValues = new int[3];
        for (int i = 0; i < 3; i++)
        {
            int randomNumber = (int)UnityEngine.Random.Range(0, 9);
            obstacleIdValues[i] = randomNumber;
            Debug.Log(obstacleIdValues[i]);
        }
    }

    public void fetchDUMMYObstacleIdType()
    {
        UnityEngine.Random.InitState(53);
        obstacleIdType = new int[3];
        Debug.Log("Fetching obstacle id types...");
        for (int i = 0; i < 3; i++)
        {
            int randomNumber = (int)UnityEngine.Random.Range(0, 3);
            obstacleIdType[i] = randomNumber;
            Debug.Log(obstacleIdType[i]);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////

    public void fetchObstacleHitList(int obHit1, int obHit2, int obHit3)
    {
        obstacleIdValues = new int[3];
        obstacleIdValues[0] = obHit1;
        obstacleIdValues[1] = obHit2;
        obstacleIdValues[2] = obHit3;
    }

    public void fetchObstacleIdType(int obType1, int obType2, int obType3)
    {
        obstacleIdType = new int[3];
        obstacleIdType[0] = obType1;
        obstacleIdType[1] = obType2;
        obstacleIdType[2] = obType3;
    }

    // set chain: ethereum, moonbeam, polygon etc
    string chain = "ethereum";
    // set network mainnet, testnet
    string network = "rinkeby";
    // smart contract method to call
    string method = "ownerOf";
    // abi in json format
    string abi = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"tokenAddress\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"_feeRecipient\",\"type\":\"address\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"ApprovalForAll\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256[]\",\"name\":\"ids\",\"type\":\"uint256[]\"},{\"indexed\":false,\"internalType\":\"uint256[]\",\"name\":\"values\",\"type\":\"uint256[]\"}],\"name\":\"TransferBatch\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"TransferSingle\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"string\",\"name\":\"value\",\"type\":\"string\"},{\"indexed\":true,\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"URI\",\"type\":\"event\"},{\"inputs\":[],\"name\":\"BLUE_BAR\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"GREEN_DUSTBIN\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"LAND\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"RED_BAR\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint8\",\"name\":\"\",\"type\":\"uint8\"}],\"name\":\"_sold\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint8\",\"name\":\"\",\"type\":\"uint8\"},{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"_tokenid\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address[]\",\"name\":\"accounts\",\"type\":\"address[]\"},{\"internalType\":\"uint256[]\",\"name\":\"ids\",\"type\":\"uint256[]\"}],\"name\":\"balanceOfBatch\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"\",\"type\":\"uint256[]\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"buyLand\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint8\",\"name\":\"obstacleId\",\"type\":\"uint8\"}],\"name\":\"buyObstacle\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"exists\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"feeRecipient\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"}],\"name\":\"isApprovedForAll\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint8\",\"name\":\"obstacle_id\",\"type\":\"uint8\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"ownerOf\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"prices\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256[]\",\"name\":\"ids\",\"type\":\"uint256[]\"},{\"internalType\":\"uint256[]\",\"name\":\"amounts\",\"type\":\"uint256[]\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"safeBatchTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"},{\"internalType\":\"bytes\",\"name\":\"data\",\"type\":\"bytes\"}],\"name\":\"safeTransferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"operator\",\"type\":\"address\"},{\"internalType\":\"bool\",\"name\":\"approved\",\"type\":\"bool\"}],\"name\":\"setApprovalForAll\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"bytes4\",\"name\":\"interfaceId\",\"type\":\"bytes4\"}],\"name\":\"supportsInterface\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"id\",\"type\":\"uint256\"}],\"name\":\"totalSupply\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"uri\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"obstacle_id\",\"type\":\"uint256\"}],\"name\":\"withdrawObstacles\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
    // address of contract
    string contract = "0x4c6329444C47Fad0B1187a1e839D4645C991308B"; //EWO_Nfts contract

    public void OngetAddressofOwner()
    {
        getAddressofOwner();
    }

    public void OndistributeRewards()
    {
        distributeRewards();
    }

    public void OnconnectPayFees()
    {
        connectPayFees();
    }

    async void getAddressofOwner()
    {
        // Assigning obstacle Owners Addresses
        string[] obj0 = { obstacleIdType[0].ToString(), obstacleIdValues[0].ToString() }; //obstacleIdValues ~ tokenId
        string args0 = JsonConvert.SerializeObject(obj0);
        obstacleowner1 = await EVM.Call(chain, network, contract, abi, method, args0);

        string[] obj1 = { obstacleIdType[1].ToString(), obstacleIdValues[1].ToString() }; //obstacleIdValues ~ tokenId
        string args1 = JsonConvert.SerializeObject(obj1);
        obstacleowner2 = await EVM.Call(chain, network, contract, abi, method, args1);

        string[] obj2 = { obstacleIdType[2].ToString(), obstacleIdValues[2].ToString() }; //obstacleIdValues ~ tokenId
        string args2 = JsonConvert.SerializeObject(obj2);
        obstacleowner3 = await EVM.Call(chain, network, contract, abi, method, args2);

        Debug.Log(obstacleowner1);
        Debug.Log(obstacleowner2);
        Debug.Log(obstacleowner3);

        // Assigning Land Owner Address
        string landIdType = "3";
        string[] obj3 = { landIdType, landIdValues.ToString() }; //obstacleIdValues ~ tokenId
        string args3 = JsonConvert.SerializeObject(obj3);
        landOwner = await EVM.Call(chain, network, contract, abi, method, args3);
        Debug.Log(landOwner);

    }

    //GamePool abi
    string GamePoolABI = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"tokenAddress\",\"type\":\"address\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"string\",\"name\":\"data\",\"type\":\"string\"}],\"name\":\"payingFees\",\"type\":\"event\"},{\"inputs\":[],\"name\":\"claimLandNFTRewards\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"claimObstacleNFTRewards\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getContractBalance\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"landNFTRewards\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"obstacleNFTRewards\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"data\",\"type\":\"string\"}],\"name\":\"payFees\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"playerFeesPaid\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner1\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"owner2\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"owner3\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"landOwner\",\"type\":\"address\"}],\"name\":\"updateRewards\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
    string GamePoolContract = "0x74C9e31eaAd2F6531e9f52849a0a8D1fFbD52623"; //GamePool contract address
    string abiToken = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Approval\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Transfer\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"}],\"name\":\"allowance\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"approve\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"decimals\",\"outputs\":[{\"internalType\":\"uint8\",\"name\":\"\",\"type\":\"uint8\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"subtractedValue\",\"type\":\"uint256\"}],\"name\":\"decreaseAllowance\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"addedValue\",\"type\":\"uint256\"}],\"name\":\"increaseAllowance\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"mint\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"totalSupply\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"recipient\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transfer\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"recipient\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transferFrom\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
    string contractToken = "0xC3ef707460f56d16c0c158C56a777EaD13Fa31c0";
    string gasLimit = "8000000";
    string gasPrice = "";
    string value1 = "0";

    async void distributeRewards()
    {
        method = "updateRewards";
        string[] obj4 = { obstacleowner1, obstacleowner2, obstacleowner3, landOwner }; //obstacleIdValues ~ tokenId
        string args4 = JsonConvert.SerializeObject(obj4);
        string res = await Web3GL.SendContract(method, GamePoolABI, GamePoolContract, args4, value1, gasLimit, gasPrice);
        Debug.Log(res);
    }

    async public void connectPayFees()
    {
        string account = PlayerPrefs.GetString("Account");
        print(account);

        // account to send to
        // string to = "0xE68F72B760f4AdcDf72Dcab4c018a955abf3E23C"; // account 2 in wallet
        // amount in wei to send
        string value = "100000000000000";
        // gas limit OPTIONAL
        // string gasLimit = "";
        // gas price OPTIONAL
        // string gasPrice = "";
        // connects to user's browser wallet (metamask) to send a transaction
        try {
            /////////////////////////////////
            // Increase Allowance
            string method = "increaseAllowance";
            string spender = GamePoolContract;
            string addedValue = "100000000000000000000";
            string value1 = "0";
            string gasLimit = "8000000";
            string gasPrice = "";
            string[] obj6 = {spender, addedValue}; //obstacleIdValues ~ tokenId
            string args6 = JsonConvert.SerializeObject(obj6);
            string res = await Web3GL.SendContract(method, abiToken, contractToken, args6, value1, gasLimit, gasPrice);
            Debug.Log("Res " + res);

            // Pay Fees
            await new WaitForSeconds(15f);
            method = "payFees";
            // string value = "0";
            // string gasLimit = "";
            // // gas price OPTIONAL
            // string gasPrice = "";
            string data = "optionalData";
            string[] obj5 = {data}; //obstacleIdValues ~ tokenId
            string args5 = JsonConvert.SerializeObject(obj5);
            string response = await Web3GL.SendContract(method, GamePoolABI, GamePoolContract, args5, value1, gasLimit, gasPrice);
            // Debug.Log("Res " + res);
            // await new WaitForSeconds(10f);

            /////////////////////////////////
            // string response = await Web3GL.SendTransaction(to, value, gasLimit, gasPrice);
            Debug.Log("Response: " + response);

            string txConfirmed = "";
            txConfirmed = await EVM.TxStatus(chain, network, response); // success, fail, pending

            Debug.Log("txConfirmed: " + txConfirmed);

            while(txConfirmed == "pending"){
                await new WaitForSeconds(2f);
                txConfirmed = await EVM.TxStatus(chain, network, response); // success, fail, pending
            }
            if(txConfirmed == "success"){
                Debug.Log("txConfirmed must be success: " + txConfirmed);
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            // await new WaitForSeconds(10f);

        } catch (Exception e) {
            Debug.LogException(e, this);
        }
    }


}
#endif

