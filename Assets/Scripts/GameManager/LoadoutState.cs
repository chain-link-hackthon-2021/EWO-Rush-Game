using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// using UnityEngine;
// using UnityEngine.UI;

#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif

/// <summary>
/// State pushed on the GameManager during the Loadout, when player select player, theme and accessories
/// Take care of init the UI, load all the data used for it etc.
/// </summary>
#if UNITY_WEBGL
public class LoadoutState : AState
{
    //////////////////////////////////////////////////////////////////////////////////////////////
    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    private int expirationTime;
    private string account; 


    string chain = "ethereum"; 
    string network = "rinkeby"; //configure the network as required
    // public bool feesPaid = false; //May need to change in future and check transaction status instead
    //     public void OnPlayGame()
    // {
    //     OnConnectedPayFees();
    // }

    public void OnLogin()
    {
        Web3Connect();
        OnConnected();
    }
    
    //GamePool abi
    string GamePoolABI = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"tokenAddress\",\"type\":\"address\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"previousOwner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"OwnershipTransferred\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"string\",\"name\":\"data\",\"type\":\"string\"}],\"name\":\"payingFees\",\"type\":\"event\"},{\"inputs\":[],\"name\":\"claimLandNFTRewards\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"claimObstacleNFTRewards\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getContractBalance\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"landNFTRewards\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"obstacleNFTRewards\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"owner\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"data\",\"type\":\"string\"}],\"name\":\"payFees\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"playerFeesPaid\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"renounceOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"newOwner\",\"type\":\"address\"}],\"name\":\"transferOwnership\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner1\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"owner2\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"owner3\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"landOwner\",\"type\":\"address\"}],\"name\":\"updateRewards\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
    string GamePoolContract = "0x74C9e31eaAd2F6531e9f52849a0a8D1fFbD52623"; //GamePool contract address
    string abiToken = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Approval\",\"type\":\"event\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Transfer\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"owner\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"}],\"name\":\"allowance\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"approve\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"decimals\",\"outputs\":[{\"internalType\":\"uint8\",\"name\":\"\",\"type\":\"uint8\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"subtractedValue\",\"type\":\"uint256\"}],\"name\":\"decreaseAllowance\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"spender\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"addedValue\",\"type\":\"uint256\"}],\"name\":\"increaseAllowance\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"mint\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"totalSupply\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"recipient\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transfer\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"sender\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"recipient\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transferFrom\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
    string contractToken = "0xC3ef707460f56d16c0c158C56a777EaD13Fa31c0";

    async public void OnConnectedPayFees()
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
                manager.SwitchState("Game");
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else{
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
            // await new WaitForSeconds(10f);

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


    /////////////////////////////////////////////////////////////////////////////////////////////
    public Canvas inventoryCanvas;
    public EWO_ERC721_Characters characterStats;

    [Header("Char UI")]
    public Text charNameDisplay;
	public RectTransform charSelect;
	public Transform charPosition;

	[Header("Theme UI")]
	public Text themeNameDisplay;
	public RectTransform themeSelect;
	public Image themeIcon;

	[Header("PowerUp UI")]
	public RectTransform powerupSelect;
	public Image powerupIcon;
	public Text powerupCount;
    public Sprite noItemIcon;

	[Header("Accessory UI")]
    public RectTransform accessoriesSelector;
    public Text accesoryNameDisplay;
	public Image accessoryIconDisplay;

	[Header("Other Data")]
	public Leaderboard leaderboard;
    public MissionUI missionPopup;
	public Button runButton;

    public GameObject tutorialBlocker;
    public GameObject tutorialPrompt;

	public MeshFilter skyMeshFilter;
    public MeshFilter UIGroundFilter;

	public AudioClip menuTheme;


    [Header("Prefabs")]
    public ConsumableIcon consumableIcon;

    Consumable.ConsumableType m_PowerupToUse = Consumable.ConsumableType.NONE;

    protected GameObject m_Character;
    protected List<int> m_OwnedAccesories = new List<int>();
    protected int m_UsedAccessory = -1;
	protected int m_UsedPowerupIndex;
    protected bool m_IsLoadingCharacter;

	protected Modifier m_CurrentModifier = new Modifier();

    protected const float k_CharacterRotationSpeed = 45f;
    protected const string k_ShopSceneName = "shop";
    protected const float k_OwnedAccessoriesCharacterOffset = -0.1f;
    protected int k_UILayer;
    protected readonly Quaternion k_FlippedYAxisRotation = Quaternion.Euler (0f, 180f, 0f);

    public override void Enter(AState from)
    {
        tutorialBlocker.SetActive(!PlayerData.instance.tutorialDone);
        tutorialPrompt.SetActive(false);

        inventoryCanvas.gameObject.SetActive(true);
        missionPopup.gameObject.SetActive(false);

        charNameDisplay.text = "";
        themeNameDisplay.text = "";

        k_UILayer = LayerMask.NameToLayer("UI");

        skyMeshFilter.gameObject.SetActive(true);
        UIGroundFilter.gameObject.SetActive(true);

        // Reseting the global blinking value. Can happen if the game unexpectedly exited while still blinking
        Shader.SetGlobalFloat("_BlinkingValue", 0.0f);

        if (MusicPlayer.instance.GetStem(0) != menuTheme)
		{
            MusicPlayer.instance.SetStem(0, menuTheme);
            StartCoroutine(MusicPlayer.instance.RestartAllStems());
        }

        runButton.interactable = false;
        runButton.GetComponentInChildren<Text>().text = "Loading...";

        if(m_PowerupToUse != Consumable.ConsumableType.NONE)
        {
            //if we come back from a run and we don't have any more of the powerup we wanted to use, we reset the powerup to use to NONE
            if (!PlayerData.instance.consumables.ContainsKey(m_PowerupToUse) || PlayerData.instance.consumables[m_PowerupToUse] == 0)
                m_PowerupToUse = Consumable.ConsumableType.NONE;
        }

        Refresh();
    }

    public override void Exit(AState to)
    {
        missionPopup.gameObject.SetActive(false);
        inventoryCanvas.gameObject.SetActive(false);

        if (m_Character != null) Addressables.ReleaseInstance(m_Character);

        GameState gs = to as GameState;

        skyMeshFilter.gameObject.SetActive(false);
        UIGroundFilter.gameObject.SetActive(false);

        if (gs != null)
        {
			gs.currentModifier = m_CurrentModifier;
			
            // We reset the modifier to a default one, for next run (if a new modifier is applied, it will replace this default one before the run starts)
			m_CurrentModifier = new Modifier();

			if (m_PowerupToUse != Consumable.ConsumableType.NONE)
			{
				PlayerData.instance.Consume(m_PowerupToUse);
                Consumable inv = Instantiate(ConsumableDatabase.GetConsumbale(m_PowerupToUse));
                inv.gameObject.SetActive(false);
                gs.trackManager.characterController.inventory = inv;
            }
        }
    }

    public void Refresh()
    {
		PopulatePowerup();

        StartCoroutine(PopulateCharacters());
        StartCoroutine(PopulateTheme());
    }

    public override string GetName()
    {
        return "Loadout";
    }

    public override void Tick()
    {
        if (!runButton.interactable)
        {
            bool interactable = ThemeDatabase.loaded && CharacterDatabase.loaded;
            if(interactable)
            {
                runButton.interactable = true;
                runButton.GetComponentInChildren<Text>().text = "Run!";

                //we can always enabled, as the parent will be disabled if tutorial is already done
                tutorialPrompt.SetActive(true);
            }
        }

        if(m_Character != null)
        {
            m_Character.transform.Rotate(0, k_CharacterRotationSpeed * Time.deltaTime, 0, Space.Self);
        }

        charSelect.gameObject.SetActive(PlayerData.instance.characters.Count > 1);
		themeSelect.gameObject.SetActive(PlayerData.instance.themes.Count > 1);
    }

	public void GoToStore()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene(k_ShopSceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
	}

    public void ChangeCharacter(int dir)
    {
        Debug.Log(PlayerData.instance.usedCharacter);

        if (PlayerData.instance.usedCharacter == 0)
        {
            PlayerData.instance.usedCharacter = 1;
        }
        else if (PlayerData.instance.usedCharacter == 1)
        {
            PlayerData.instance.usedCharacter = 0;
        }

        StartCoroutine(PopulateCharacters());
    }

    public void ChangeAccessory(int dir)
    {
        m_UsedAccessory += dir;
        if (m_UsedAccessory >= m_OwnedAccesories.Count)
            m_UsedAccessory = -1;
        else if (m_UsedAccessory < -1)
            m_UsedAccessory = m_OwnedAccesories.Count-1;

        if (m_UsedAccessory != -1)
            PlayerData.instance.usedAccessory = m_OwnedAccesories[m_UsedAccessory];
        else
            PlayerData.instance.usedAccessory = -1;

        SetupAccessory();
    }

    public void ChangeTheme(int dir)
    {
        PlayerData.instance.usedTheme += dir;
        if (PlayerData.instance.usedTheme >= PlayerData.instance.themes.Count)
            PlayerData.instance.usedTheme = 0;
        else if (PlayerData.instance.usedTheme < 0)
            PlayerData.instance.usedTheme = PlayerData.instance.themes.Count - 1;

        StartCoroutine(PopulateTheme());
    }

    public IEnumerator PopulateTheme()
    {
        ThemeData t = null;

        while (t == null)
        {
            t = ThemeDatabase.GetThemeData(PlayerData.instance.themes[PlayerData.instance.usedTheme]);
            yield return null;
        }

        themeNameDisplay.text = t.themeName;
		themeIcon.sprite = t.themeIcon;

		skyMeshFilter.sharedMesh = t.skyMesh;
        UIGroundFilter.sharedMesh = t.UIGroundMesh;
	}

    public IEnumerator PopulateCharacters()
    {
        accessoriesSelector.gameObject.SetActive(false);
        PlayerData.instance.usedAccessory = -1;
        m_UsedAccessory = -1;

        if (!m_IsLoadingCharacter)
        {
            m_IsLoadingCharacter = true;
            GameObject newChar = null;
            while (newChar == null)
            {
                Character c = CharacterDatabase.GetCharacter(PlayerData.instance.characters[PlayerData.instance.usedCharacter]);
                PlayerData.instance.AddCharacter("Rubbish Raccoon");

                if (c != null)
                {
                    m_OwnedAccesories.Clear();
                    for (int i = 0; i < c.accessories.Length; ++i)
                    {
						// Check which accessories we own.
                        string compoundName = c.characterName + ":" + c.accessories[i].accessoryName;
                        if (PlayerData.instance.characterAccessories.Contains(compoundName))
                        {
                            m_OwnedAccesories.Add(i);
                        }
                    }

                    Vector3 pos = charPosition.transform.position;
                    if (m_OwnedAccesories.Count > 0)
                    {
                        pos.x = k_OwnedAccessoriesCharacterOffset;
                    }
                    else
                    {
                        pos.x = 0.0f;
                    }
                    charPosition.transform.position = pos;

                    accessoriesSelector.gameObject.SetActive(m_OwnedAccesories.Count > 0);

                    AsyncOperationHandle op = Addressables.InstantiateAsync(c.characterName);
                    yield return op;
                    if (op.Result == null || !(op.Result is GameObject))
                    {
                        Debug.LogWarning(string.Format("Unable to load character {0}.", c.characterName));
                        yield break;
                    }
                    newChar = op.Result as GameObject;
                    Helpers.SetRendererLayerRecursive(newChar, k_UILayer);
					newChar.transform.SetParent(charPosition, false);
                    newChar.transform.rotation = k_FlippedYAxisRotation;

                    if (m_Character != null)
                        Addressables.ReleaseInstance(m_Character);

                    m_Character = newChar;
                    charNameDisplay.text = c.characterName;

                    m_Character.transform.localPosition = Vector3.right * 1000;
                    //animator will take a frame to initialize, during which the character will be in a T-pose.
                    //So we move the character off screen, wait that initialised frame, then move the character back in place.
                    //That avoid an ugly "T-pose" flash time
                    yield return new WaitForEndOfFrame();
                    m_Character.transform.localPosition = Vector3.zero;

                    SetupAccessory();
                }
                else
                    yield return new WaitForSeconds(1.0f);
            }
            m_IsLoadingCharacter = false;
        }
	}

    void SetupAccessory()
    {
        Character c = m_Character.GetComponent<Character>();
        c.SetupAccesory(PlayerData.instance.usedAccessory);

        if (PlayerData.instance.usedAccessory == -1)
        {
            accesoryNameDisplay.text = "None";
			accessoryIconDisplay.enabled = false;
		}
        else
        {
			accessoryIconDisplay.enabled = true;
			accesoryNameDisplay.text = c.accessories[PlayerData.instance.usedAccessory].accessoryName;
			accessoryIconDisplay.sprite = c.accessories[PlayerData.instance.usedAccessory].accessoryIcon;
        }
    }

	void PopulatePowerup()
	{
		powerupIcon.gameObject.SetActive(true);

        if (PlayerData.instance.consumables.Count > 0)
        {
            Consumable c = ConsumableDatabase.GetConsumbale(m_PowerupToUse);

            powerupSelect.gameObject.SetActive(true);
            if (c != null)
            {
                powerupIcon.sprite = c.icon;
                powerupCount.text = PlayerData.instance.consumables[m_PowerupToUse].ToString();
            }
            else
            {
                powerupIcon.sprite = noItemIcon;
                powerupCount.text = "";
            }
        }
        else
        {
            powerupSelect.gameObject.SetActive(false);
        }
	}

	public void ChangeConsumable(int dir)
	{
		bool found = false;
		do
		{
			m_UsedPowerupIndex += dir;
			if(m_UsedPowerupIndex >= (int)Consumable.ConsumableType.MAX_COUNT)
			{
				m_UsedPowerupIndex = 0; 
			}
			else if(m_UsedPowerupIndex < 0)
			{
				m_UsedPowerupIndex = (int)Consumable.ConsumableType.MAX_COUNT - 1;
			}

			int count = 0;
			if(PlayerData.instance.consumables.TryGetValue((Consumable.ConsumableType)m_UsedPowerupIndex, out count) && count > 0)
			{
				found = true;
			}

		} while (m_UsedPowerupIndex != 0 && !found);

		m_PowerupToUse = (Consumable.ConsumableType)m_UsedPowerupIndex;
		PopulatePowerup();
	}

	public void UnequipPowerup()
	{
		m_PowerupToUse = Consumable.ConsumableType.NONE;
	}
	

	public void SetModifier(Modifier modifier)
	{
		m_CurrentModifier = modifier;
	}

    public void StartGame()
    {
        if (PlayerData.instance.tutorialDone)
        {
            if (PlayerData.instance.ftueLevel == 1)
            {
                PlayerData.instance.ftueLevel = 2;
                PlayerData.instance.Save();
            }
        }
        OnConnectedPayFees();
    }

	public void Openleaderboard()
	{
		leaderboard.displayPlayer = false;
		leaderboard.forcePlayerDisplay = false;
		leaderboard.Open();
    }
}
#endif
