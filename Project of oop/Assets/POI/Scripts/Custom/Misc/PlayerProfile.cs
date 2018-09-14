using UnityEngine;

/// <summary>
/// Generic player profile class capable of saving and loading PlayerPrefs data associated with the player.
/// As an example, the player profile data contains how much experience they've earned, 
/// as well as what kind of options they've chosen.
/// You should modify this class to store any kind of data you need using the provided functions as a template.
/// </summary>
/// add username and get rid of nickname****

public static class PlayerProfile
{
	static string mName;
    static int mHostId = -1;
    static float mHostLat = 0;
    static float mHostLong = 0;
    static float mLat = 0;
    static float mLong = 0;
	static int mFull = -1;
	static int mHints = -1;
	static int mWifi = -1;
	static int mExp = -1;
    static int mLob = -1;
	static int mPowerSaving = -1;

	/// <summary>
	/// Whether the player should have full access to all of the game's features.
	/// </summary>

	static public bool fullAccess
	{
		get
		{
			if (mFull == -1) mFull = PlayerPrefs.GetInt("Full", 0);
			return (mFull == 1);
		}
		set
		{
			int val = value ? 1 : 0;

			if (mFull != val)
			{
				mFull = val;
				PlayerPrefs.SetInt("Full", val);
				NGUITools.Broadcast("OnAccessLevelChanged");
			}
		}
	}

	/// <summary>
	/// Player's chosen name.
	/// </summary>

	static public string playerName
	{
		get
		{
			if (string.IsNullOrEmpty(mName)) mName = PlayerPrefs.GetString("Name", "Guest");
			return mName;
		}
		set
		{
			if (string.IsNullOrEmpty(value.Trim()))
				value = "Guest";

			if (mName != value)
			{
				mName = value;
				PlayerPrefs.SetString("Name", value);
			}
		}
	}

	/// <summary>
	/// Player's experience.
	/// </summary>

	static public int experience
	{
		get
		{
			if (mExp == -1) mExp = PlayerPrefs.GetInt("Experience", 0);
			return mExp;
		}
		set
		{
			if (mExp != value)
			{
				mExp = value;
				PlayerPrefs.SetInt("Experience", value);
			}
		}
	}

    /// <summary>
    /// Player's shared lobby ID.
    /// </summary>

    static public int lobbyId
    {
        get
        {
            if (mLob == -1) mLob = PlayerPrefs.GetInt("Lobby", 0);
            return mLob;
        }
        set
        {
            if (mLob != value)
            {
                mLob = value;
                PlayerPrefs.SetInt("Lobby", value);
            }
        }
    }

    /// <summary>
    /// Whether the player wants hints in single player mode.
    /// </summary>

    static public bool hints
	{
		get
		{
			if (mHints == -1) mHints = PlayerPrefs.GetInt("Hints", 1);
			return mHints == 1;
		}
		set
		{
			int val = value ? 1 : 0;

			if (val != mHints)
			{
				mHints = val;
				PlayerPrefs.SetInt("Hints", mHints);
			}
		}
	}

	/// <summary>
	/// Whether the data will be restricted to a wifi-only connection. If off, 3G/4G/LTE traffic will be allowed.
	/// </summary>

	static public bool allow3G
	{
		get
		{
			if (mWifi == -1) mWifi = PlayerPrefs.GetInt("Wifi", 0);
			return mWifi == 1;
		}
		set
		{
			int i = value ? 1 : 0;

			if (i != mWifi)
			{
				mWifi = i;
				PlayerPrefs.SetInt("Wifi", mWifi);
			}
		}
	}

	/// <summary>
	/// Whether the application is allowed to access the internet.
	/// </summary>

	static public bool allowedToAccessInternet
	{
		get
		{
#if UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
			return allow3G;
#else
			return (allow3G || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork);
#endif
		}
	}

	/// <summary>
	/// Power-saving mode limits framerate to 30 instead of the usual 60.
	/// </summary>

	static public bool powerSavingMode
	{
		get
		{
			if (mPowerSaving == -1)
			{
#if UNITY_ANDROID || UNITY_IPHONE
				Application.targetFrameRate = PlayerPrefs.GetInt("FPS", 30);
#else
				Application.targetFrameRate = PlayerPrefs.GetInt("FPS", 60);
#endif
				mPowerSaving = (Application.targetFrameRate == 30) ? 1 : 0;
			}
			return (mPowerSaving == 1);
		}
		set
		{
			int val = value ? 1 : 0;
			
			if (mPowerSaving != val)
			{
				mPowerSaving = val;
				Application.targetFrameRate = value ? 30 : 60;
				PlayerPrefs.SetInt("FPS", Application.targetFrameRate);
			}
		}
	}

    /// <summary
    /// Host ID to let database know if a user is host.
    /// </summary>
    
    static public bool HostId
    {
        get
        {
            if (mHostId == -1) mHostId = PlayerPrefs.GetInt("Host", 0);
            return mHostId == 1;
        }
        set
        {
            int i = value ? 1 : 0;

            if (mHostId != i)
            {
                mHostId = i;
                PlayerPrefs.GetInt("Host", i);
            }
        }
    }

    /// <summary
    ///
    /// </summary>

    static public float HostLat
    {
        get
        {
            if (mHostId == 1)
            {
                mHostLat = PlayerPrefs.GetFloat("HLat", 0);
            }
            return mHostLat;
        }
        set
        {
            if (mHostLat != value)
            {
                mHostLat = value;
                PlayerPrefs.SetFloat("HLat", value);
            }
        }
    }

    /// <summary
    ///
    /// </summary>

    static public float HostLong
    {
        get
        {
            if (mHostId == 1)
                mHostLong = PlayerPrefs.GetFloat("HLong", 0);
            return mHostLong;
        }
        set
        {
            if (mHostLong != value)
            {
                mHostLong = value;
                PlayerPrefs.SetFloat("HLong", value);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    
    static public float PlayerLat
    {
        get
        {
            if (mLat == 0)
                mLat = PlayerPrefs.GetFloat("PLat", 0);
            return mLat;

        }
        set
        {
            if (mLat != value)
            {
                mLat = value;
                PlayerPrefs.SetFloat("PLat", 0);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>

    static public float PlayerLong
    {
        get
        {
            if (mLong == 0)
                mLong = PlayerPrefs.GetFloat("PLong", 0);
            return mLong;

        }
        set
        {
            if (mLong != value)
            {
                mLong = value;
                PlayerPrefs.SetFloat("PLong", 0);
            }
        }
    }

    /// <summary>
    /// Experience points it takes to obtain each level, mainly here for example purposes.
    /// You can change it to be an experience curve, or some kind of a formula if you wish.
    /// </summary>

    public const int expPerLevel = 50000;

	/// <summary>
	/// Total maximum number of obtainable experience points.
	/// </summary>

	static public int maxExp { get { return expPerLevel * 69; } }

	/// <summary>
	/// Player's ability point cap.
	/// </summary>

	static public int abilityPoints { get { return 12; } }

	/// <summary>
	/// Player's current level.
	/// </summary>

	static public int level { get { return experience / expPerLevel; } }

	/// <summary>
	/// Player's progress toward the next level.
	/// </summary>

	static public float progressToNextLevel { get { return GetProgressToNextLevel(experience); } }

	/// <summary>
	/// Calculate the progress toward next level, given the experience.
	/// </summary>

	static public float GetProgressToNextLevel (int exp)
	{
		int lvl = GetLevelByExp(exp) - 1;
		return (float)(exp - lvl * expPerLevel) / expPerLevel;
	}

	/// <summary>
	/// Retrieve a title associated with the specified level.
	/// </summary>

	static public string GetTitle (int lvl) { return Localization.Get("Title " + (lvl / 5)); }

	/// <summary>
	/// Given the experience amount, return the level.
	/// </summary>

	static public int GetLevelByExp (int exp)
	{
		if (exp > maxExp) exp = maxExp;
		return 1 + exp / expPerLevel;
	}

	/// <summary>
	/// Retrieve the title associated with the specified amount of experience.
	/// </summary>

	static public string GetTitleByExp (int exp) { return GetTitle(GetLevelByExp(exp) - 1); }
}
