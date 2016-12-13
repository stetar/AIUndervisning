using AIFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAgent.Goals
{

    enum GoalStatus {NotStarted, Active, Completed, Failed}
    abstract class Goal<T>
    {

        protected T entity;

        protected Stack<Goal<T>> subGoals = new Stack<Goal<T>>();

        protected GoalStatus status = GoalStatus.NotStarted;

        public GoalStatus Status
        {
            get { return status; }
        }

        public Goal(T entity)
        {
            this.entity = entity;
        }

        public virtual GoalStatus Process()
        {

            if (subGoals.Count > 0 && subGoals.Peek().Status == GoalStatus.Completed)
            {
                Goal<T> goal = subGoals.Pop();
                goal.Terminate();
            }

            if (subGoals.Count > 0)
            {
                status = subGoals.Peek().Process();

                if (status == GoalStatus.Completed && subGoals.Count > 1)
                {
                    status = GoalStatus.Active;
                }
            }
            else
            {
                status = GoalStatus.Completed;
            }

            return status;

        }

        public virtual void Terminate()
        {
            while (subGoals.Count > 0)
            {
                Goal<T> goal = subGoals.Pop();
                goal.Terminate();
            }
        }




    }
}
