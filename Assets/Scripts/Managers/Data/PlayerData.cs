using System;
[Serializable]
public struct PlayerData
{

    #region PlayerData
    public int playerHp;
    public float playerSpeed;
    public float playerAcceleration;
    public float playerMaxFuel;

    #endregion

    #region Weapon Data
    public float playerGunDamage;
    public float playerGunShootingSpeed;
    public float playerChargerCapacity;
    #endregion


    #region PlayerScore

    public float playerScore;

    #endregion


}
