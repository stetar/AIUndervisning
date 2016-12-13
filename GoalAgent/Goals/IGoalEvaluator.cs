using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAgent.Goals
{
    interface IGoalEvaluator<T>
    {
        float CalculateDesirability(T entity);
        Goal<T> CreateGoal(T entity);

        float Weight { get; set; } 
    }
}
