using AIFramework;
using AIFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAgent.Goals
{
    class EatGoalEvaluator : IGoalEvaluator<GoalBasedAgent>
    {

        public float CalculateDesirability(GoalBasedAgent entity)
        {
            return Weight *(1f - (entity.Hitpoints / entity.Health));
        }

        public Goal<GoalBasedAgent> CreateGoal(GoalBasedAgent entity)
        {
            return new EatGoal(entity);
        }


        public float Weight
        {
            get; set;
        }
    }
}
