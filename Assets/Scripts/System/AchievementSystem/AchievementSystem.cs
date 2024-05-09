using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework;
using UnityEngine;

namespace Elvenwood
{
    public interface IAchievementSystem  : ISystem
    {
        BindableProperty<int> AchievementCount { get; set; }
    }
    

    public class AchievementSystem : AbstractSystem, IAchievementSystem
    {
        public BindableProperty<int> AchievementCount { get; set; } = new BindableProperty<int>();

        private List<AchievementItem> mItems;

        private bool mNoHurt = false; 
        protected override void OnInit()
        {
            mItems = this.GetModel<IAchievementModel>().Data.Items;
            this.RegisterEvent<OnPlayerHurt>(e =>
            {
                mNoHurt = false;
            });

            this.RegisterEvent<GameStartEvent>(e =>
            {
                mNoHurt = true;
            });

            AchievementCount.Register(f =>
            {
                //TODO 当AchievementCount的值改变时会有什么操作

            }).UnRegister();

            this.RegisterEvent<CleanAchievementEvent>(e =>
            {
                Clean();
            });
            
            mItems.Add(new AchievementItem()
            {
                Name = "百杀",
                CheckComplete = () => this.GetModel<AchievementModel>().Data.KillEnemyCount.Value > 100
            });
            
            mItems.Add(new AchievementItem()
            {
                Name = "十杀",
                CheckComplete = () => this.GetModel<AchievementModel>().Data.KillEnemyCount.Value > 10
            });
            
            mItems.Add(new AchievementItem()
            {
                Name = "完美无缺",
                CheckComplete = () => mNoHurt
            });
            
            mItems.Add(new AchievementItem()
            {
                Name = "全成就达成",
                CheckComplete = () => mItems.Count(item => item.Unlocked) >= 3
            });
            
            
            var storage = this.GetUtility<IStorage>();
  
            foreach (var myAchievementItem in mItems)
            {
                myAchievementItem.Unlocked = storage.LoadBool(myAchievementItem.Name, false);
            }

            this.RegisterEvent<UpdateAchievementEvent>(async e =>
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1f));

                foreach (var myAchievementItem in mItems)
                {
                    if (!myAchievementItem.Unlocked && myAchievementItem.CheckComplete())
                    {
                        myAchievementItem.Unlocked = true;
                        storage.SaveBool(myAchievementItem.Name, true);
                        AchievementCount.Value++;
                    }
                }
            });
        }

        private void Clean()
        {
            foreach (var myAchievementItem in mItems)
            {
                myAchievementItem.Unlocked = false;
                AchievementCount.Value = 0;
            }
        }

    }
}