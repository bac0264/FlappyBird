using UnityEngine;
using UnityEngine.UI;

public class WAMBalance : MonoBehaviour
{
    public Text balanceTxt;
    public async void Start()
    {
        string chain = "binance";
        string network = "testnet";

        string account = PlayerPrefs.GetString("Account");

        int balance = await WAM.BalanceOf(chain, network, WAM.smartContract, account);
        balanceTxt.text = "Token: "+balance;
    }
    
}
