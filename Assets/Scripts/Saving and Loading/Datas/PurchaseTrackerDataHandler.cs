using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseTrackerDataHandler : MonoBehaviour
{
    public static PurchaseTrackerDataHandler instance;
    [SerializeField] private PurchaseTrackerData purchaseTrackerData;
    private void Awake()
    {
        instance = this;

        purchaseTrackerData = SaveLoadManager.LoadData<PurchaseTrackerData>();
        if (purchaseTrackerData == null)
        {
            purchaseTrackerData = new PurchaseTrackerData();
        }
    }

    #region Return All Data
    public PurchaseTrackerData ReturnSavedValues()
    {
        return purchaseTrackerData;
    }
    #endregion

    #region Saving
    public void SaveUserData()
    {
        SaveLoadManager.SaveData(purchaseTrackerData);
    }
    private void OnDisable()
    {
        SaveUserData();
    }
    #endregion
}

[System.Serializable]
public class PurchaseTrackerData
{
    public bool hasPurchasedAdBlock = false;
}
