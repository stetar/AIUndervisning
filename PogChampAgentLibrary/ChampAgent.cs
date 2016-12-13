using AIFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIFramework.Actions;
using AIFramework;

namespace PogChampAgentLibrary
{
    public class ChampAgent : Agent
    {
        public ChampAgent(IPropertyStorage propertyStorage) : base(propertyStorage)
        {
            MovementSpeed = 65;
            Strength = 100;
            Health = 40;
            Eyesight = 30;
            Endurance = 15;
            Dodge = 0;
        }

        public override IAction GetNextAction(List<IEntity> otherEntities)
        {

            List<IEntity> NearbyPlants = otherEntities.FindAll(x => x.GetType() == typeof(Plant) && AIVector.Distance(Position, x.Position) < Eyesight);

            if (Hunger >= 100)
            {
                List<float> plantDistances = new List<float>();
                foreach (Plant plant in NearbyPlants)
                {
                    plantDistances.Add(AIVector.Distance(Position, plant.Position));
                }
                float lowestValue = plantDistances.Min();
                foreach (Plant item in NearbyPlants)
                {
                    if (lowestValue == AIVector.Distance(Position, item.Position))
                    {
                        if (lowestValue <= AIModifiers.maxFeedingRange)
                        {
                            return new Feed(item);
                        }
                        else
                        {
                            return new Move(new AIVector(this.Position.X - item.Position.X, this.Position.Y - item.Position.Y));
                        }
                    }
                }
            }
        }

        public override void ActionResultCallback(bool success)
        {
            
        }
    }
}
