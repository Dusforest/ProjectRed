using System.Collections.Generic;
using Framework;

namespace Elvenwood.Test
{
    public struct TestSaveData
    {
        public int MaxHp;
        public int CurHp;
        public int Defence;
        public int Atk;
        public float SpeedScale;
        public float JumpForceScale;
        public int TypeKeys;
    }
    public class JsonTestQuery : AbstractQuery<TestSaveData>
    {
        protected override TestSaveData OnDo()
        {
            var model = this.GetModel<IPlayerModel>();
            model.Load();
            TestSaveData saveData;
            saveData.MaxHp = model.Data.maxHp.Value;
            saveData.CurHp = model.Data.curHp.Value;
            saveData.Defence = model.Data.bloodFury.Value;
            saveData.Atk = model.Data.atk.Value;
            saveData.SpeedScale = model.Data.speedScale.Value;
            saveData.JumpForceScale = model.Data.jumpForceScale.Value;
            saveData.TypeKeys = model.Data.keyFragmentCount.Value;
            return saveData;
        }
    }
}