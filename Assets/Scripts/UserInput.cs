using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class UserInput : MonoBehaviour
{
    public static UserInput Instance;
    public TextMeshPro titleText;
    public TMP_InputField inputField;
    public List<EmojiInfo> emojiInfos;
    public Transform messageParentTransform;
    public float emojiOffsetX;
    public Vector3 emojiScale;
    public Vector3 emojiRotation;
    GameObject emoji;
    [HideInInspector]public List<GameObject> emojisOnScene;
    public List<Vector3> letterCenterPositions;
    void Start()
    {
        if (!Instance) Instance = this;
    }


    public void OnSendButtonPressed()
    {
        
        titleText.text = inputField.text;
        titleText.ForceMeshUpdate();
        inputField.text = "";
        

        if (emojisOnScene.Count > 0)
        {
            for (int k = 0; k < emojisOnScene.Count; k++)
            {
                Destroy(emojisOnScene[k]);
            }
            emojisOnScene.Clear();
        }

        
        if (titleText.text.Contains("<sprite="))
        {
            StartCoroutine(ShowEmoji());
        }
    }

    public IEnumerator ShowEmoji()
    {

        string emojiCOde = "";
        int index = 0;
        EmojiName emojiName = EmojiName.happyFace;

        yield return new WaitForSeconds(0);

        //RecordCenterPositionOfEveryLetter();
        int totalEmojiInText = Regex.Matches(titleText.text, "<sprite=").Count;
        for (int j = 0; j < totalEmojiInText; j++)
        {
            
            for (int i = 0; i < titleText.text.Length; i++)
            {
                if (titleText.text[i] == '<' && titleText.text[i + 1] == 's' && titleText.text[i + 2] == 'p')
                {
                    index = i;
                    break;
                }
            }

            emojiCOde = titleText.text.Substring(index, 10);
            for (int l = 0; l < emojiInfos.Count; l++)
            {
                if (emojiInfos[l].emojiCode == emojiCOde)
                {
                    emojiName = emojiInfos[l].emojiName;
                    emoji = emojiInfos[l].emojiPrefab;
                }
            }

            Vector3 bottomRightPosition = titleText.textInfo.characterInfo[index].bottomRight;
            Vector3 bottomLeftPosition = titleText.textInfo.characterInfo[index].bottomLeft;
            Vector3 topRightPosition = titleText.textInfo.characterInfo[index].topRight;
            Vector3 topLeftosition = titleText.textInfo.characterInfo[index].topLeft;
            float centerX = (bottomLeftPosition.x + topRightPosition.x) / 2;
            float centerY = (bottomLeftPosition.y + topRightPosition.y) / 2;
            Vector3 centerPosition = new Vector3(centerX, centerY, titleText.transform.position.z);

            LoadPrefab(centerPosition);
            titleText.text = titleText.text.Remove(titleText.text.IndexOf(emojiCOde), emojiCOde.Length).Insert(titleText.text.IndexOf(emojiCOde), "          ");
            titleText.ForceMeshUpdate();
        }
    }
    
    public void OnEmojiClicked(string emojiText)
    {
        inputField.text += emojiText;
    }
    public void LoadPrefab(Vector3 worldPos)
    {
         emoji = Instantiate(
             emoji, 
             new Vector3(worldPos.x+ titleText.transform.position.x+emojiOffsetX, worldPos.y + titleText.transform.position.y, worldPos.z),
             Quaternion.identity
             );

         emoji.transform.localScale = emojiScale;
         emoji.transform.localRotation = Quaternion.Euler(emojiRotation);
        emoji.transform.parent = messageParentTransform;
         emojisOnScene.Add(emoji);

    }

    int SpaceCounterInString(int index)
    {
        int spaceCounter = 0;
        string strTemp;

        for (int i = 0; i <=index; i++)
        {
            strTemp = titleText.text.Substring(i, 1);
            if (strTemp == " ")
                spaceCounter++;
        }

        return spaceCounter;
    }
}
