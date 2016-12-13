using AIFramework;
using AIFramework.Actions;
using AIFramework.Entities;
using GoalAgent.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAgent
{
    class GoalBasedAgent : Agent
    {

        Goal<GoalBasedAgent> currentGoal;

        IAction nextAction;
        List<IEntity> viewedEntities;

        public List<IEntity> ViewedEntities
        {
            get { return viewedEntities; }
        }

        public IAction NextAction
        {
            get { return nextAction; }
            set { nextAction = value; }
        }



        public GoalBasedAgent(IPropertyStorage propertyStorage)
            : base(propertyStorage)
        {
            MovementSpeed = 50;
            Strength = 35;
            Health = 40;
            Eyesight = 35;
            Endurance = 35;
            Dodge = 55;
            currentGoal = new ThinkGoal(this);
        }


        public override IAction GetNextAction(List<IEntity> otherEntities)
        {
            viewedEntities = otherEntities;

            nextAction = null;
            while (nextAction == null)
            {
                currentGoal.Process();
            }
            return nextAction;
        }

        public override void ActionResultCallback(bool success)
        {
            //throw new NotImplementedException();
        }


    }
}
