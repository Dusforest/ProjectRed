using System;
using System.Collections.Generic;
using Framework;

namespace Elvenwood
{
    public interface IAchievementModel : IJsonModel<AchievementModelData>
    {

    }

    public class AchievementItem
    {
        public string Name { get; set; }
        
        public Func<bool> CheckComplete { get; set; }
        
        public bool Unlocked { get; set; }
    }

    public class AchievementModelData
    {
        public AchievementModelData(BindableProperty<int> killEnemyCount, List<AchievementItem> items)
        {
            KillEnemyCount = killEnemyCount;
            Items = items;
        }
        public BindableProperty<int> KillEnemyCount;

        public List<AchievementItem> Items;
    }
    
    public class AchievementModel : AbstractJsonModel<AchievementModelData>, IAchievementModel
    {
        protected override void OnInit()
        {
            Data = new AchievementModelData(new BindableProperty<int>(), new List<AchievementItem>());
        }
        
        public override AchievementModelData Data { get; set; }
        protected override void LoadData(AchievementModelData saveData)
        {
            Data.KillEnemyCount = saveData.KillEnemyCount;
            Data.Items = saveData.Items;
        }
    }
}