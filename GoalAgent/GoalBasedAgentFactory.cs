using AIFramework;
using AIFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAgent
{
    class GoalBasedAgentFactory : AgentFactory
    {
        public override Agent CreateAgent(IPropertyStorage propertyStorage)
        {
            return new GoalBasedAgent(propertyStorage);
        }

        public override Agent CreateAgent(Agent parent1, Agent parent2, IPropertyStorage propertyStorage)
        {
            return new GoalBasedAgent(propertyStorage);
        }

        public override Type ProvidedAgentType
        {
            get { return typeof(GoalBasedAgent); }
        }

        public override string Creators
        {
            get { return "Palle Ehmsen"; }
        }

        public override void RegisterWinners(List<Agent> sortedAfterDeathTime)
        {
            //Do data collection - Perhaps used to evolutionary algoritmen
        }
    }
}
