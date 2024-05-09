using Framework;

namespace Elvenwood
{
    public class AchievementCountQuery : AbstractQuery<int>
    {
        protected override int OnDo()
        {
            return this.GetSystem<IAchievementSystem>().AchievementCount.Value;
        }
    }
}