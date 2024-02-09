using UnityEngine;

public class LevelEnd : MonoBehaviour
{
	[SerializeField] private Health _health;
	[SerializeField] private GameObject _losePanel;

	private void OnEnable()
	{
		_health.HealthDownToZero += ShowLosePanel;
	}

	private void OnDisable()
	{
		_health.HealthDownToZero -= ShowLosePanel;
	}

	private void ShowLosePanel()
	{
		_losePanel.SetActive(true);
	}
}
