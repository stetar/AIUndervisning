using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAgent.Goals
{
    class ExploreGoalEvaluator : IGoalEvaluator<GoalBasedAgent>
    {

        public float CalculateDesirability(GoalBasedAgent entity)
        {
            return Weight * 0.001f;
        }

        public Goal<GoalBasedAgent> CreateGoal(GoalBasedAgent entity)
        {
            return new ExploreGoal(entity);
        }


        public float Weight
        {
            get;
            set;
        }

    }
}
