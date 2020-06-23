using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LetterPositionTracker : MonoBehaviour
{
	public static LetterPositionTracker Instance;
	public Text textComp;
	public int charIndex;
	public Canvas canvas;
	[HideInInspector]public Vector3 worldPos;

	private void Awake()
	{
		if (!Instance) Instance = this;
	}
	public void FindPos ()
	{
		//charIndex = textComp.text.Length - 1 ;
		string text = textComp.text;
		Debug.Log("Text Length : " + text.Length);
		if (charIndex >= text.Length)
			return;

		TextGenerator textGen = new TextGenerator (text.Length);
		Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
		textGen.Populate (text, textComp.GetGenerationSettings (extents));

		int newLine = text.Substring(0, charIndex).Split('\n').Length - 1;
		Debug.Log("new Line " + newLine);
		int whiteSpace = text.Substring(0, charIndex).Split(' ').Length - 1;
		int indexOfTextQuad = (charIndex * 4) + (newLine * 4) - 4;
		if (indexOfTextQuad < textGen.vertexCount)
		{
			Vector3 avgPos = (textGen.verts[indexOfTextQuad].position +
				textGen.verts[indexOfTextQuad + 1].position +
				textGen.verts[indexOfTextQuad + 2].position +
				textGen.verts[indexOfTextQuad + 3].position) / 4f;

			print(avgPos);
			FindWorldPos(avgPos);
		}
		else {
			Debug.LogError ("Out of text bound");
		}
	}

	void FindWorldPos (Vector3 testPoint)
	{
		worldPos = textComp.transform.TransformPoint (testPoint);
		UserInput.Instance.LoadPrefab(worldPos);
		print (worldPos);


		/*new GameObject ("point").transform.position = worldPos;
		Debug.DrawRay (worldPos, Vector3.up, Color.red, 50f);*/
	}


}
