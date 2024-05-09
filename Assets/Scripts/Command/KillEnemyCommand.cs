using Framework;
using UnityEngine;

namespace Elvenwood
{
    public class KillEnemyCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<IAchievementModel>().Data.KillEnemyCount.Value++;
            var playerModel = this.GetModel<IPlayerModel>();
            if (!playerModel.Data.isWolfState && playerModel.Data.hasWolfMode)
            {
                playerModel.Data.bloodFury.Value++;
            }
        }
    }
}