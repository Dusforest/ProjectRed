using Framework;

namespace Elvenwood
{
    public class ObtainCloneSkillCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<IPlayerModel>().Data.hasCloneSkill.Value = true;
        }
    }
}