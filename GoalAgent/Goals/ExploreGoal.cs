using AIFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAgent.Goals
{
    class ExploreGoal :Goal<GoalBasedAgent>
    {
        DateTime startTime;
        Random rnd = new Random();
        public ExploreGoal(GoalBasedAgent agent)
            : base(agent)
        {
            startTime = DateTime.Now;


            subGoals.Push(new MoveGoal(entity, entity.Position + new AIVector(rnd.Next(-1, 2), rnd.Next(-1, 2)) * entity.MovementSpeed * 2));
        }

        public override GoalStatus Process()
        {
            
            base.Process();
            if (DateTime.Now - startTime > TimeSpan.FromSeconds(3))
            {
                status = GoalStatus.Completed;
            }
            else
            {
                if (status == GoalStatus.Completed)
                {
                    subGoals.Push(new MoveGoal(entity, entity.Position + new AIVector(rnd.Next(-1, 2), rnd.Next(-1, 2)) * entity.MovementSpeed * 2));
                }
                status = GoalStatus.Active;
            }
            return status;
        }

    }
}
