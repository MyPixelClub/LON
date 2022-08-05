using Infrastructure.Services;
using UnityEngine;

public class CristalWallet : Wallet
{    
    private void OnEnable()
    {
        RefreshText();
    }
}
