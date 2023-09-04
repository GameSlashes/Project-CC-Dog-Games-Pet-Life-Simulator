using UnityEngine;

public class GR_IAPStoreManager : MonoBehaviour
{
    public void removeAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
        if (FindObjectOfType<Handler>())
        {
            FindObjectOfType<Handler>().Show_SmallBanner1();
            FindObjectOfType<Handler>().Show_SmallBanner2();
        }
            
    }

    public void unlockAllLevels()
    {
        GR_SaveData.Instance.Level = 20;
    }

    public void unlockAllPlayers()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetString("UnlockedPlayer" + i, "Purchased");
        }
    }

    public void unlockEverything()
    {
        addCoins(1000);
        removeAds();
        unlockAllLevels();
        unlockAllPlayers();
    }

    public void addCoins(int coins)
    {
        GR_SaveData.instance.Coins += coins;

    }

    public void addGems(int gems)
    {
        GR_SaveData.instance.Gems += gems;

    }
}
