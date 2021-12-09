using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class WAM
{
    private static string abi = 
        "[{\"inputs\":[{\"internalType\":\"string\",\"name\":\"token_name\",\"type\":\"string\"},{\"internalType\":\"string\",\"name\":\"short_symbol\",\"type\":\"string\"},{\"internalType\":\"uint8\",\"name\":\"token_decimals\",\"type\":\"uint8\"},{\"internalType\":\"uint256\",\"name\":\"token_totalSupply\",\"type\":\"uint256\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"value\",\"type\":\"uint256\"}],\"name\":\"Transfer\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"}],\"name\":\"balanceOf\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"burn\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"decimals\",\"outputs\":[{\"internalType\":\"uint8\",\"name\":\"\",\"type\":\"uint8\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"account\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"mint\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"name\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"symbol\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"totalSupply\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"recipient\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transfer\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";

    public static string smartContract = "0xED462C1DFefc28E16513BB69eAAab7087F467956";
    public static async Task<int> BalanceOf(string _chain, string _network, string _contract, string _account, string _rpc="")
    {
        string method = "balanceOf";
        string[] obj = { _account };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
        try 
        {
            return int.Parse(response);
        } 
        catch 
        {
            Debug.LogError(response);
            throw;
        }
    }

    public static async Task<string> OwnerOf(string _chain, string _network, string _contract, string _tokenId, string _rpc="")
    {
        string method = "ownerOf";
        string[] obj = { _tokenId };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
        return response; 
    }

    public static async Task<List<string>> OwnerOfBatch(string _chain, string _network, string _contract, string[] _tokenIds, string _multicall = "", string _rpc="")
    {
        string method = "ownerOf";
        // build array of args
        string[][] obj = new string[_tokenIds.Length][];
        for (int i = 0; i < _tokenIds.Length; i++)
        {
            obj[i] = new string[1] { _tokenIds[i] };
        };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.MultiCall(_chain, _network, _contract, abi, method, args, _multicall, _rpc);
        try 
        {
            string[] responses = JsonConvert.DeserializeObject<string[]>(response);
            List<string> owners = new List<string>();
            for (int i = 0; i < responses.Length; i++)
            {
                // clean up address
                string address = "0x" + responses[i].Substring(responses[i].Length - 40);
                owners.Add(address);
            }
            return owners;
        } 
        catch 
        {
            Debug.LogError(response);
            throw;
        }  
    }

    public static async Task<string> URI(string _chain, string _network, string _contract, string _tokenId, string _rpc="")
    {
        string method = "tokenURI";
        string[] obj = { _tokenId };
        string args = JsonConvert.SerializeObject(obj);
        string response = await EVM.Call(_chain, _network, _contract, abi, method, args, _rpc);
        return response; 
    }
    
    public static async void Mint(int tokenEarn, Action updateBalance = null)
    {
        // smart contract method to call
        string method = "mint";
        // address of contract

        // array of arguments for contract
        string args = "[\"0x2aF598ed8104483776661643BCD4995036205760\",\""+tokenEarn+"\"]";
        // value in wei
        string value = "0";
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // connects to user's browser wallet (metamask) to update contract state
        try {
            string response = await Web3GL.SendContract(method, abi, smartContract, args, value, gasLimit, gasPrice);
            Debug.Log(response);
            updateBalance?.Invoke();
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }
    
}
