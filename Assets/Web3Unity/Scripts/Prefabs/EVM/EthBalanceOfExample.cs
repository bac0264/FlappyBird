using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EthBalanceOfExample : MonoBehaviour
{
    public Text balanceTxt;
    async void Start()
    {
        string chain = "ethereum";
        string network = "rinkeby"; // mainnet ropsten kovan rinkeby goerli
        string account = "0x2aF598ed8104483776661643BCD4995036205760";

        string balance = await EVM.BalanceOf(chain, network, account);
        balanceTxt.text = balance;
    }
}
