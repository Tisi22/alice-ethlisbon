using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
//using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;
using TMPro;

public class MintingPanel : MonoBehaviour
{
    [SerializeField] private Constants constants;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private Button MintingButton;

   string response;

   

    public async Task<string> VoucherMint(GetVoucher.GetVoucherResponse _voucher)
    {
        // smart contract method to call
        string method = "redeem";
        // abi in json format
        string abi = constants.AbiAliceTokens;
        // address of contract
        string contract = constants.ContractAliceTokens;
        // array of arguments for contract
        string account = PlayerPrefs.GetString("Account");

        object[] tuple = {
            _voucher.amount,
            _voucher.signature
        };

        object[] obj = { account, tuple };
        string args = JsonConvert.SerializeObject(obj);

        // value in wei
        string value = "0";
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = await EVM.GasPrice(constants.Chain, constants.Network, constants.Rpc);

        // connects to user's browser wallet (metamask) to update contract state
        try {
            response = await Web3GL.SendContract(method, abi, contract, args, value, gasLimit, gasPrice);
            Debug.Log(response);
        } catch (Exception e) {
            Debug.LogException(e, this);
        }

        return response;
    }

    public async void MintTokens(string _amount)
    {
        message.text = "Creating Voucher";
        MintingButton.interactable = false;
        StartCoroutine(MintTokensCoroutine(_amount));
    }

    private IEnumerator MintTokensCoroutine(string _amount)
    {
        var mintingTask = MintTokensFinal(_amount);
        yield return new WaitForSeconds(3f);
        
        while (!mintingTask.IsCompleted)
        {
            yield return null;
        }
    }

    private async Task MintTokensFinal(string _amount)
    {
        
        GetVoucher.GetVoucherResponse voucher = await CreateVoucher.CreateVoucherTokens(_amount);

        message.text = "Please, confirm transaction in your wallet";

        var result1 = await VoucherMint(voucher);

        if (result1 is null)
        {
            message.text = "Transaction failed";
            MintingButton.interactable = true;
            return;
        }

        message.text = "Your ALC tokens have been minted";

    }

    
}
