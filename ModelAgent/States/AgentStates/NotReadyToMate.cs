﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIFramework.Entities;
using AIFramework.Actions;
using AIFramework;

namespace ModelAgent.States.AgentStates
{
    class NotReadyToMate : IState<ModelBasedAgent>
    {
        private static NotReadyToMate _instance;

        Random rnd;

        public static NotReadyToMate Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotReadyToMate();
                }
                return _instance;
            }
        }

        private NotReadyToMate()
        {
            rnd = new Random();
        }

        public void Enter(ModelBasedAgent obj)
        {

        }

        public void Execute(ModelBasedAgent obj)
        {

            //ProcreationCountDown <=0
            if (obj.ProcreationCountDown <= 0)
            {
                obj.Fsm.ChangeState(Mature.Instance);
                return;
            }

            //Range to another agent < AIModfiers.maxMeleeAttackRange and other agent Strength + Health > Strength + Health
            List<IEntity> inRange = obj.Model.FindAll(x => x is Agent && x.EntityType != typeof(ModelBasedAgent).Name && AIVector.Distance(obj.Position, x.Position) < AIModifiers.maxMeleeAttackRange && (((Agent)x).Strength + ((Agent)x).Health) > (obj.Strength + obj.Health));
            if (inRange.Count > 0)
            {
                obj.Fsm.ChangeState(Scared.Instance);
                return;
            }


            //A agent of another type with range < AIModifiers.maxMeleeAttackRange and other agent Strength + Health < Strength + Health
            inRange = obj.ViewedEntities.FindAll(x => x is Agent && x.EntityType != typeof(ModelBasedAgent).Name && AIVector.Distance(obj.Position, x.Position) < AIModifiers.maxMeleeAttackRange && (((Agent)x).Strength + ((Agent)x).Health) < (obj.Strength + obj.Health));
            if (inRange.Count > 0)
            {
                obj.NextAction = new Attack((Agent) inRange[0]);
                return;
            }

            //Default
            obj.NextAction = new Move(new AIVector(rnd.Next(-1, 2), rnd.Next(-1, 2)));

        }

        public void Exit(ModelBasedAgent obj)
        {
                
        }
    }
}
