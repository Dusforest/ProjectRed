using Elvenwood.Test;
using Framework;
using UnityEngine;

namespace Elvenwood
{
	public class Elvenwood : Architecture<Elvenwood>
	{
		protected override void Init()
		{
			//注册组件
			RegisterModel<IPlayerModel>(new PlayerModel());
			RegisterModel<ISkillModel>(new SkillModel());
			RegisterModel<IAchievementModel>(new AchievementModel());
			RegisterModel<IUIElementModel>(new UIElementModel());

			RegisterModel<IJsonTestModel>(new JsonTestModel());

			RegisterSystem<IPlayerAttackSystem>(new PlayerAttackSystem());
			RegisterSystem<IAchievementSystem>(new AchievementSystem());
			RegisterSystem<ITimeSystem>(new TimeSystem());
			RegisterSystem<IAttackSystem>(new AttackSystem());
			RegisterSystem<IStorageSystem>(new StorageSystem());

			RegisterUtility<IStorage>(new StorageUtility());
		}
	}
}

