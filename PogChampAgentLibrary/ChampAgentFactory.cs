using AIFramework;
using AIFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PogChampAgentLibrary
{
    class ChampAgentFactory : AgentFactory
    {
        public override Agent CreateAgent(Agent parent1, Agent parent2, IPropertyStorage propertyStorage)
        {
            return new ChampAgent(propertyStorage);
        }

        public override void RegisterWinners(List<Agent> sortedAfterDeathTime)
        {
            
        }

        public override Agent CreateAgent(IPropertyStorage propertyStorage)
        {
            return new ChampAgent(propertyStorage);
        }

        public override Type ProvidedAgentType
        {
            get
            {
                return typeof(ChampAgent);
            }
        }

        public override string Creators
        {
            get
            {
                return "Steen & Peter";
            }
        }
    }
}
