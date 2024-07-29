using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }
    private int totalCoins;

    public void AddCoins(int coins)
    {
        totalCoins += coins;
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }

    public void ResetLevelCoins()
    {
        totalCoins = 0;
    }
}
