using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	[SerializeField] private RectTransform _healthBar;
	[SerializeField] private GameObject _heartImagePrefab;
	[SerializeField] private Health _health;

	private Image[] _healthImages;

	private void OnEnable()
	{
		_health.HealthChanged += UpdateHealth;
	}

	private void OnDisable()
	{
		_health.HealthChanged -= UpdateHealth;
	}

	private void Start()
	{
		int _maxHealth = _health.HealthPoints;

		_healthImages = new Image[_maxHealth];

		for (int i = 0; i < _maxHealth; i++)
		{
			GameObject healthObj = Instantiate(_heartImagePrefab, _healthBar);
			_healthImages[i] = healthObj.GetComponent<Image>();
		}
	}

	private void UpdateHealth()
	{
		for (int i = 0; i < _healthImages.Length; i++)
		{
			if (i < _health.HealthPoints)
			{
				_healthImages[i].enabled = true;
			}
			else
			{
				_healthImages[i].enabled = false;
			}
		}
	}
}
