using System.Collections;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
	private Renderer renderer;
	private float flashInterval = 0.3f;
	private float invulnerabilityDuration = 2.0f;

	public Color Color => renderer.material.color;

	private void Awake()
	{
		renderer = GetComponent<Renderer>();
	}

	public void Flash(Color flashColor)
	{
		StartCoroutine(FlashCoroutine(flashColor));
	}

	private IEnumerator FlashCoroutine(Color flashColor)
	{
		bool isEven = true;
		float elapsedTime = 0f;
		Color currentColor = Color;

		while (elapsedTime < invulnerabilityDuration)
		{
			if(isEven) ChangeColor(flashColor);
			else ChangeColor(currentColor);

			isEven = !isEven;
			elapsedTime += flashInterval;
			if(flashInterval > 0.08f) flashInterval -= flashInterval / 8;

			yield return new WaitForSeconds(flashInterval);
		}

		ChangeColor(currentColor);
		flashInterval = 0.3f;
	}

	private void ChangeColor(Color flashColor)
	{
		renderer.material.color = flashColor;
	}
}
