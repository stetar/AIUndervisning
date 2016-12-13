using AIFramework;
using AIFramework.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAgent.Goals
{
    class MoveGoal : Goal<GoalBasedAgent>
    {
        private AIVector target;
        private float slack = 1;

        public MoveGoal(GoalBasedAgent agent, AIVector target)
            : base(agent)
        {
            this.target = target;
        }

        public MoveGoal(GoalBasedAgent agent, AIVector target, float slack)
            : this(agent, target)
        {
            this.slack = slack;
        }

        public override GoalStatus Process()
        {
            status = GoalStatus.Active;
            if (AIVector.Distance(entity.Position, target) <= slack)
            {
                status = GoalStatus.Completed;
            }
            else
            {
                entity.NextAction = new Move(target- entity.Position);
            }
            return status;
        }
    }
}
