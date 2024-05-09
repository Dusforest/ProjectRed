using Framework;

namespace Elvenwood
{
    public struct PlayerSkill
    {
        public bool hasMagicArrow;
        public bool hasCloneSkill;
    }
    
    public class PlayerSkillState : AbstractQuery<PlayerSkill>
    {
        protected override PlayerSkill OnDo()
        {
            PlayerSkill tmpPlayerSkill;

            tmpPlayerSkill.hasMagicArrow = this.GetModel<IPlayerModel>().Data.hasMagicArrow.Value;
            tmpPlayerSkill.hasCloneSkill = this.GetModel<IPlayerModel>().Data.hasCloneSkill.Value;

            return tmpPlayerSkill;
        }
    }
}