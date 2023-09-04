using UnityEngine;
using UnityEngine.UI;

public class GR_CashUpdate : MonoBehaviour
{
    public static GR_CashUpdate instance;
    public Text CashText;
    public Text GemsText;

    public bool cash;
    public bool gems;

    private GR_SaveData database;

    public void Awake()
    {
        instance = this;
        database = GR_SaveData.instance;
    }

    private void OnEnable()
    {
        if (cash)
        {
            GR_SaveData.allCashUpdate += updateCash;
        }
        else if (gems)
        {
            GR_SaveData.allGemsUpdate += updateCash;
        }
    }

    private void OnDisable()
    {
        if (cash)
        {
            GR_SaveData.allCashUpdate -= updateCash;
        }
        else if (gems)
        {
            GR_SaveData.allGemsUpdate -= updateCash;
        }

    }

    public void Start()
    {
        updateCash();
    }

    public void updateCash()
    {
        if (cash)
        {
            CashText.text = database.Coins.ToString();
        }
        else if (gems)
        {
            GemsText.text = database.Gems.ToString();
        }
    }


}
