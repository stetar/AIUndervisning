using AIFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAgent.Goals
{
    class AttackGoalEvaluator : IGoalEvaluator<GoalBasedAgent>
    {
        public float CalculateDesirability(GoalBasedAgent entity)
        {
            float totalEnemyPower = 0;
            int enemyCount = 0;
            float totalFriendsPower = 0;
            int friendsCount = 0;

            List<IEntity> agents = entity.ViewedEntities.FindAll(x => x is Agent);
            foreach (Agent agent in agents)
            {
                if (agent is GoalBasedAgent)
                {
                    friendsCount++;
                    totalFriendsPower += agent.Strength + agent.Hitpoints;
                }
                else
                {
                    enemyCount++;
                    totalEnemyPower += agent.Strength + agent.Hitpoints;
                }
            }

            if (enemyCount > 0)
            {
                return Weight * ((totalFriendsPower - totalEnemyPower) / (totalEnemyPower + totalFriendsPower));
            }
            return -1000;
        }

        public Goal<GoalBasedAgent> CreateGoal(GoalBasedAgent entity)
        {
            return new AttackGoal(entity);
        }

        public float Weight
        {
            get;
            set;
        }
    }
}
