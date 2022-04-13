using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using System;
using TMPro;

public class MyCode : MonoBehaviour
{
    public TextMeshProUGUI DataText;
    string Data;
    public void Start()
    {
        if(PlayerPrefs.GetInt("Genesis") ==0)
        {
            CreateGenesisBlock();
            PlayerPrefs.SetInt("Genesis", 1);
        }
    }

    public void AddNewBlock()
    {
        Data = DataText.text;
        DataText.text = null;
        if(Data != null)
        {
            int PreviousBlockNo = PlayerPrefs.GetInt("BlockNo") - 1;
            PlayerPrefs.SetString("BlockData" + PlayerPrefs.GetInt("BlockNo"), Data);
            PlayerPrefs.SetString("DateTime" + PlayerPrefs.GetInt("BlockNo"), (DateTime.Now).ToString());

            string blockHase = GetHash(PlayerPrefs.GetString("BlockData" + PlayerPrefs.GetInt("BlockNo")) +
                         PlayerPrefs.GetString("DateTime" + PlayerPrefs.GetInt("BlockNo")) +
                         PlayerPrefs.GetString("Hash" + PreviousBlockNo));

            PlayerPrefs.SetString("Hash" + PlayerPrefs.GetInt("BlockNo"), blockHase);

            Block(PreviousBlockNo + 1);
            PlayerPrefs.SetInt("BlockNo", PlayerPrefs.GetInt("BlockNo") + 1);
            Data = null;
        }    
    }

    void CreateGenesisBlock()
    {
        PlayerPrefs.SetString("BlockData" + PlayerPrefs.GetInt("BlockNo"), "What you get today that is your past result");
        PlayerPrefs.SetString("DateTime" + PlayerPrefs.GetInt("BlockNo"),(DateTime.Now).ToString());
        PlayerPrefs.SetString("GenesisHash", GetHash("Genesis Block"));

        string blockHase = GetHash(PlayerPrefs.GetString("BlockData" + PlayerPrefs.GetInt("BlockNo")) +
                     PlayerPrefs.GetString("DateTime" + PlayerPrefs.GetInt("BlockNo")) +
                     PlayerPrefs.GetString("GenesisHash" + PlayerPrefs.GetInt("BlockNo")));

        PlayerPrefs.SetString("Hash" + PlayerPrefs.GetInt("BlockNo"),blockHase);


        PlayerPrefs.SetInt("BlockNo", PlayerPrefs.GetInt("BlockNo") + 1);

        Block(0);
    }

    public void Block(int blockNo)
    {
        if(blockNo ==0)
        {       
            print("Block no:-" + blockNo + "\n");
            print("Data: " + PlayerPrefs.GetString("BlockData" + blockNo));
            print("Date: " + PlayerPrefs.GetString("DateTime" + blockNo));
            print("BlockHash: " + PlayerPrefs.GetString("Hash" + blockNo));
            print("GenesisHash : " + PlayerPrefs.GetString("GenesisHash"));
        }
        else
        {
            int PreviousBlockNo = blockNo - 1;
            print("Block no:-" + blockNo + "\n");
            print("Data: " + PlayerPrefs.GetString("BlockData" + blockNo));
            print("Date: " + PlayerPrefs.GetString("DateTime" + blockNo));
            print("BlockHash: " + PlayerPrefs.GetString("Hash" + blockNo));
            print("BlockHash: " + PlayerPrefs.GetString("Hash" + PreviousBlockNo));
        }

    }

    string GetHash(string data)
    {
        Byte[] stringBytes = Encoding.UTF8.GetBytes(data);
        StringBuilder sb = new StringBuilder();

        using (SHA256Managed sha256 = new SHA256Managed())
        {
            byte[] hash = sha256.ComputeHash(stringBytes);
            foreach (Byte b in hash)
                sb.Append(b.ToString("x"));
        }

        return sb.ToString();
    }

    public void ShowAllBlock()
    {
        for(int i=0;i<PlayerPrefs.GetInt("BlockNo");i++)
        {
            Block(i);
            print("--------------------------------------------------------------------------------------");
        }
    }
}
    