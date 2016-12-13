using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAgent.Goals
{
    class ThinkGoal : Goal<GoalBasedAgent>
    {
        private List<IGoalEvaluator<GoalBasedAgent>> evaluators = new List<IGoalEvaluator<GoalBasedAgent>>();

        public ThinkGoal(GoalBasedAgent agent)
            : base(agent)
        {
            status = GoalStatus.Active;

            evaluators.Add(new ExploreGoalEvaluator());
            evaluators.Add(new EatGoalEvaluator());
            evaluators.Add(new AttackGoalEvaluator());
            foreach (IGoalEvaluator<GoalBasedAgent> evaluator in evaluators)
            {
                evaluator.Weight = 1;
            }

        }


        public override GoalStatus Process()
        {
            base.Process();
            if (status == GoalStatus.Completed || status == GoalStatus.Failed)
            {
                subGoals.Clear();
                IGoalEvaluator<GoalBasedAgent> currentEvaluator = null;
                float currentDesirability = 0;
                foreach (IGoalEvaluator<GoalBasedAgent> evaluator in evaluators)
                {
                    float tmpDesirability = evaluator.CalculateDesirability(entity);

                    if (currentDesirability < tmpDesirability)
                    {
                        currentEvaluator = evaluator;
                        currentDesirability = tmpDesirability;
                    }
                }

                if (currentEvaluator != null)
                {
                    Goal<GoalBasedAgent> goal = currentEvaluator.CreateGoal(entity);
                    if (goal != null)
                    {
                        subGoals.Push(goal);
                    }
                }
                status = GoalStatus.Active;
            }
            return status;
        }


    }
}
