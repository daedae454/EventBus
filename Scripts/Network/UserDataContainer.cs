using UnityEngine;

/// <summary>
/// 서버에서 받은 유저의 데이터를 보관합니다. 데이터를 직접 수정하지 마세요.
/// </summary>
public class UserDataContainer
{
    #region Singleton

    public static UserDataContainer I
    {
        get
        {
            if (_inst == null)
                _inst = new UserDataContainer();
            return _inst;
        }
    }

    private static UserDataContainer _inst;

    #endregion Singleton

    public UserEquipmentDataContainer Equipment;
    public UserCharacterDataContainer Character;

    private UserDataContainer()
    {
        Equipment = new UserEquipmentDataContainer();
        Character = new UserCharacterDataContainer();

        #region TEST_DATA

        SetTestData();

        #endregion TEST_DATA
    }

    #region TEST_DATA

    private void SetTestData()
    {
        Equipment.Add(new UserEquipmentData() { UUID = 1001, Lv = 1 });
        Equipment.Add(new UserEquipmentData() { UUID = 1002, Lv = 10 });
        Character.Add(new UserCharacterData() { UUID = 1001, Lv = 1, EquipUUID = 1001, Exp = 0 });
    }

    #endregion TEST_DATA
}