using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Models;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class CreateVoucher
{
    private static readonly string host = "https://eth-lisbon.onrender.com";

    string account;

    Constants constants;

    public static async Task<GetVoucher.GetVoucherResponse> CreateVoucherTokens(string _amount)
    {
        
        GetVoucher.Voucher newVoucher = new GetVoucher.Voucher
        {
            amount = _amount
        };

        string url = host + "/createVoucher";

        string jsonData = JsonUtility.ToJson(newVoucher);

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                GetVoucher.GetVoucherResponse data = JsonConvert.DeserializeObject<GetVoucher.GetVoucherResponse>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
                Debug.Log(data.amount);
                return data;
            }
            else
            {
                Debug.LogError($"Error creating voucher: {webRequest.error}");
                Debug.LogError($"Server response: {webRequest.downloadHandler.text}");
                return null;
            }
        }

       
    }
}
