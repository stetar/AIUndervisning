using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAgent.Goals
{
    class AttackGoal : Goal<GoalBasedAgent>
    {

        Agent target;

        public AttackGoal(GoalBasedAgent agent)
            : base(agent)
        {

        }

        private Agent GetClosestAgent()
        {
            List<IEntity> agents = entity.ViewedEntities.FindAll(x => x is Agent && !(x is GoalBasedAgent));
            Agent closestAgent = null;
            foreach (Agent a in agents)
            {
                if (closestAgent == null || AIVector.Distance(entity.Position, a.Position) < AIVector.Distance(entity.Position, closestAgent.Position))
                {
                    closestAgent = a;
                }
            }
            return closestAgent;
        }

        public override GoalStatus Process()
        {
            if (target == null || target.Hitpoints < 1)
            {
                target = GetClosestAgent();
            }

            base.Process();

            if (status == GoalStatus.Completed)
            {
                if (target != null && AIVector.Distance(entity.Position, target.Position) < AIModifiers.maxMeleeAttackRange)
                {
                    entity.NextAction = new Attack(target);
                }
                else if (target != null && AIVector.Distance(entity.Position, target.Position) >= AIModifiers.maxMeleeAttackRange)
                {
                    subGoals.Push(new MoveGoal(entity, target.Position, AIModifiers.maxMeleeAttackRange - 1));
                    status = GoalStatus.Active;
                }
            }

            return status;
        }

    }
}
