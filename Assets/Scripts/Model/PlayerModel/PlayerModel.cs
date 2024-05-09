using Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elvenwood
{
	public interface IPlayerModel : IJsonModel<PlayerModelData>
	{

	}

	[System.Serializable]
	public class PlayerModelData
	{
		public PlayerModelData(int maxHp, int curHp, int bloodFury, bool hasWolfMode, bool isWolfState,
			bool hasMagicArrow, bool hasCloneSkill, bool isCloneReleased, int atk,
			float speedScale, float jumpForceScale, int keyFragmentCount)
		{
			this.maxHp = new BindableProperty<int>(maxHp);
			this.curHp = new BindableProperty<int>(curHp);
			this.atk = new BindableProperty<int>(atk);
			this.bloodFury = new BindableProperty<int>(bloodFury);
			this.hasWolfMode = new BindableProperty<bool>(hasWolfMode);
			this.isWolfState = new BindableProperty<bool>(isWolfState);
			this.hasMagicArrow = new BindableProperty<bool>(hasMagicArrow);
			this.hasCloneSkill = new BindableProperty<bool>(hasCloneSkill);
			this.isCloneReleased = new BindableProperty<bool>(isCloneReleased);
			this.speedScale = new BindableProperty<float>(speedScale);
			this.jumpForceScale = new BindableProperty<float>(jumpForceScale);
			this.keyFragmentCount = new BindableProperty<int>(keyFragmentCount);
		}

		public BindableProperty<int> maxHp;
		public BindableProperty<int> curHp;
		public BindableProperty<int> bloodFury;
		public BindableProperty<int> atk;
		public BindableProperty<bool> hasWolfMode;
		public BindableProperty<bool> isWolfState;
		public BindableProperty<bool> hasMagicArrow;
		public BindableProperty<bool> hasCloneSkill;
		public BindableProperty<bool> isCloneReleased;
		public BindableProperty<float> speedScale;
		public BindableProperty<float> jumpForceScale;
		public BindableProperty<int> keyFragmentCount;
	}

	public class PlayerModel : AbstractJsonModel<PlayerModelData>, IPlayerModel
	{
		protected override void OnInit()
		{
			Data = new PlayerModelData(5, 5, 5, false, false, false, false, false, 1, 5, 5,
				0);
			Data.curHp.Register(e => { this.SendEvent<OnUpdateUI>(); });
			Data.keyFragmentCount.Register(e => { this.SendEvent<OnUpdateUI>(); });
			Data.hasMagicArrow.Register(e => { this.SendEvent<OnGetSkill>(); });
			Data.hasCloneSkill.Register(e =>
			{
				this.SendEvent<OnGetSkill>();
			});
			Data.maxHp.Register(e => { this.SendEvent<OnUpdateUI>(); });
		}

		public override PlayerModelData Data { get; set; }

		protected override void LoadData(PlayerModelData saveData)
		{
			Data.maxHp = saveData.maxHp;
			Data.curHp = saveData.curHp;
			Data.bloodFury = saveData.bloodFury;
			Data.atk = saveData.atk;
			Data.hasWolfMode = saveData.hasWolfMode;
			Data.isWolfState = saveData.isWolfState;
			Data.hasMagicArrow = saveData.hasMagicArrow;
			Data.hasCloneSkill = saveData.hasCloneSkill;
			Data.isCloneReleased = saveData.isCloneReleased;
			Data.speedScale = saveData.speedScale;
			Data.jumpForceScale = saveData.jumpForceScale;
			Data.keyFragmentCount = saveData.keyFragmentCount;
		}
	}
}