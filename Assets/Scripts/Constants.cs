using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EmojiName
{
    happyFace = 1,
    loveEyed = 2,
    glassEyed = 3,
}
public class Constants : MonoBehaviour
{
    public static string happyEmojiCode = "<sprite=5>";
    public static string loveEyedEmojiCode = "<sprite=2>";
    public static string glassEyedEyedEmojiCode = "<sprite=3>";
}

[Serializable]
public class EmojiInfo
{
    public string emojiCode;
    public GameObject emojiPrefab;
    public EmojiName emojiName;
}
