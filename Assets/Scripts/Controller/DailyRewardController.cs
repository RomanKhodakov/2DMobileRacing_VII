using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class DailyRewardController: BaseController
{
   private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/Reward/RewardUI"};
   
   private readonly DailyRewardView _dailyRewardView;
   private readonly ProfilePlayer _profilePlayer;
   
   private List<ContainerSlotRewardView> _slots;
   private bool _isGetReward;

   public DailyRewardController(Transform menuUiTransform, ProfilePlayer profilePlayer)
   {
       _profilePlayer = profilePlayer;
       _dailyRewardView = LoadView(menuUiTransform);
       RefreshView();
   }

   private DailyRewardView LoadView(Transform placeForUi)
   {
       var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
       AddGameObjects(objectView);
        
       return objectView.GetComponent<DailyRewardView>();
   }

   private void RefreshView()
   {
       InitSlots();
      
       _dailyRewardView.StartCoroutine(RewardsStateUpdater());
      
       RefreshUi();
       SubscribeButtons();
   }

   private void InitSlots()
   {
       _slots = new List<ContainerSlotRewardView>();

       for (var i = 0; i < _dailyRewardView.Rewards.Count; i++)
       {
           var instanceSlot = Object.Instantiate(_dailyRewardView.ContainerSlotRewardView,
               _dailyRewardView.MountRootSlotsReward, false);

           _slots.Add(instanceSlot);
       }
   }

   private IEnumerator RewardsStateUpdater()
   {
       while (true)
       {
           RefreshRewardsState();
           yield return new WaitForSeconds(1);
       }
   }

   private void RefreshRewardsState()
   {
       _isGetReward = true;

       if (_dailyRewardView.TimeGetReward.HasValue)
       {
           var timeSpan = DateTime.UtcNow - _dailyRewardView.TimeGetReward.Value;

           if (timeSpan.Seconds > _dailyRewardView.TimeDeadline)
           {
               _dailyRewardView.TimeGetReward = null;
               _dailyRewardView.CurrentSlotInActive = 0;
           }
           else if (timeSpan.Seconds < _dailyRewardView.TimeCooldown)
           {
               _isGetReward = false;
           }
       }

       RefreshUi();
   }

   private void RefreshUi()
   {
       _dailyRewardView.GetRewardButton.interactable = _isGetReward;

       if (_isGetReward)
       {
           _dailyRewardView.TimerNewReward.text = "You can receive Reward";
       }
       else
       {
           if (_dailyRewardView.TimeGetReward != null)
           {
               var nextClaimTime = _dailyRewardView.TimeGetReward.Value.AddSeconds(_dailyRewardView.TimeCooldown);
               var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
               var timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:" +
                                   $"{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";
      
               _dailyRewardView.TimerNewReward.text = $"Time to get the next reward: {timeGetReward}";
           }
       }
       _dailyRewardView.CurrentMoney.text = _profilePlayer.Money.ToString();
       _dailyRewardView.CurrentHealth.text = _profilePlayer.Health.ToString();

       for (var i = 0; i < _slots.Count; i++)
       {
           _slots[i].SetData(_dailyRewardView.Rewards[i],i + 1, i == _dailyRewardView.CurrentSlotInActive);
       }
   }

   private void SubscribeButtons()
   {
       _dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
       _dailyRewardView.ResetButton.onClick.AddListener(ResetTimer);
       _dailyRewardView.StartButton.onClick.AddListener(GoToStart);
   }

   private void ClaimReward()
   {
       if (!_isGetReward)
           return;

       var reward = _dailyRewardView.Rewards[_dailyRewardView.CurrentSlotInActive];

       switch (reward.RewardType)
       {
           case RewardType.Money:
               _profilePlayer.Money += reward.CountCurrency;
               break;
           case RewardType.Health:
               _profilePlayer.Health += reward.CountCurrency;
               break;
       }

       _dailyRewardView.TimeGetReward = DateTime.UtcNow;
       _dailyRewardView.CurrentSlotInActive = (_dailyRewardView.CurrentSlotInActive + 1) % _dailyRewardView.Rewards.Count;

       RefreshRewardsState();
   }

   private void ResetTimer()
   {
       PlayerPrefs.DeleteAll();
       ResetMoneyAndHealth();
   }

   private void ResetMoneyAndHealth()
   {
       _profilePlayer.Money = 0;
       _profilePlayer.Health = 0;
   }

   private void GoToStart()
   {
       _profilePlayer.CurrentState.Value = GameState.Start;
   }

   protected override void OnDispose()
   {
       _dailyRewardView.GetRewardButton.onClick.RemoveAllListeners();
       _dailyRewardView.ResetButton.onClick.RemoveAllListeners();
       _dailyRewardView.StartButton.onClick.RemoveAllListeners();
       _dailyRewardView.StopAllCoroutines();
       
       base.OnDispose();
   }
}

