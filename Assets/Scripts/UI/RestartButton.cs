using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button _button;

	private void Start()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		_button.onClick.RemoveListener(OnClick);

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
