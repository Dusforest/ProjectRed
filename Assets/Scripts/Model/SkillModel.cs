using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{
	public interface ISkillModel : IModel
	{
		SkillData GetSkillData();
	}

	public class SkillModel : AbstractModel, ISkillModel
	{
		private ISkillModel skillModelImpl;

		protected override void OnInit()
		{

		}

		SkillData ISkillModel.GetSkillData()
		{
			return new SkillData(false, null);
		}
	}

	public class SkillData
	{
		public BindableProperty<bool> IsCloneAlreadyExist;
		public BindableProperty<CloneController> CloneController;

		public SkillData(bool isCloneAlreadyExist, CloneController cloneController)
		{
			IsCloneAlreadyExist = new BindableProperty<bool>(isCloneAlreadyExist);
			CloneController = new BindableProperty<CloneController>(cloneController);
		}
	}
}

