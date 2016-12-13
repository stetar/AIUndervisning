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
    class EatGoal : Goal<GoalBasedAgent>
    {
        Plant plant;
        public EatGoal(GoalBasedAgent agent) : base(agent)
        {

        }

        private Plant GetClosestPlant()
        {
            List<IEntity> plants = entity.ViewedEntities.FindAll(x => x is Plant);
            Plant closestPlant = null;
            foreach (Plant p in plants)
            {
                if (closestPlant == null || AIVector.Distance(entity.Position, p.Position) < AIVector.Distance(entity.Position, closestPlant.Position))
                {
                    closestPlant = p;
                }
            }
            return closestPlant;
        }

        public override GoalStatus Process()
        {
            plant = GetClosestPlant();
            base.Process();
            if (status == GoalStatus.Completed)
            {
                if (plant == null)
                {
                    subGoals.Push(new ExploreGoal(entity));
                    status = GoalStatus.Active;
                }
                else if (AIVector.Distance(entity.Position, plant.Position) < AIModifiers.maxFeedingRange)
                {
                    entity.NextAction = new Feed(plant);
                }
                else if (AIVector.Distance(entity.Position, plant.Position) >= AIModifiers.maxFeedingRange)
                {
                    subGoals.Push(new MoveGoal(entity, plant.Position, AIModifiers.maxFeedingRange - 1));
                    status = GoalStatus.Active;
                }
            }

            return status;
        }

    }
}
