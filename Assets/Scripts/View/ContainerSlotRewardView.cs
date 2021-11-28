using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerSlotRewardView : MonoBehaviour
{
    [SerializeField] private Image _selectBackgroundImage;
    [SerializeField] private Image _currencyImage;
    
    [SerializeField] private TMP_Text _textDay;
    [SerializeField] private TMP_Text _countReward;

    public void SetData(Reward reward, int countDay, bool isSelected)
    {
        _currencyImage.sprite = reward.IconCurrency;
        _textDay.text = $"Day {countDay}";
        _countReward.text = reward.CountCurrency.ToString();
        _selectBackgroundImage.gameObject.SetActive(isSelected);
    }
}
