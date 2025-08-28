using UnityEngine;
using UnityEngine.UI;

public class UIDemo : MonoBehaviour
{
    [Header("Demo Buttons")]
    [SerializeField] private Button btnCharaLv;
    [SerializeField] private Button btnCharaExp;
    [SerializeField] private Button btnAllEquipLv;

    [Header("Open UI Buttons")]
    [SerializeField] private Button btnOpenChara;
    [SerializeField] private Button btnOpenEquip;

    [Header("UI References")]
    [SerializeField] private UICharacter uiChara;
    [SerializeField] private UIEquipment uiEquip;

    private void Awake()
    {
        btnCharaLv.onClick.AddListener(() =>
        {
            UserDataContainer.I.Character.LevelAdd(1001, 1);
        });
        btnCharaExp.onClick.AddListener(() =>
        {
            UserDataContainer.I.Character.ExpAdd(1001, 1);
        });
        btnAllEquipLv.onClick.AddListener(() =>
        {
            UserDataContainer.I.Equipment.ForEach((data) =>
            {
                UserDataContainer.I.Equipment.LevelAdd(data.UUID, 1);
            });
        });

        btnOpenChara.onClick.AddListener(() =>
        {
            uiChara.Open(new UICharacter.UIPARAM() { UUID = 1001 });
        });
        btnOpenEquip.onClick.AddListener(() =>
        {
            uiEquip.Open();
        });

        UIController.I.RegisterUI(uiChara);
        UIController.I.RegisterUI(uiEquip);
    }
}