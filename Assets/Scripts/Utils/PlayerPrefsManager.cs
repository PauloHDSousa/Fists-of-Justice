using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerPrefsManager
{
    //I'm not sure if this class was necessary, BUT I did it anyway.
    #region Keys

    public enum PrefKeys
    {
        Volume,
        SFX,
    }
    #endregion

    #region Set
    public void SaveFloat(PrefKeys key, float value)
    {
        PlayerPrefs.SetFloat(key.ToString(), value);
    }
    #endregion

    #region Get
    public bool HasKey(PrefKeys key)
    {
        return PlayerPrefs.HasKey(key.ToString());
    }

    public float GetFloat(PrefKeys key)
    {
        return PlayerPrefs.GetFloat(key.ToString());
    }
    #endregion
}

