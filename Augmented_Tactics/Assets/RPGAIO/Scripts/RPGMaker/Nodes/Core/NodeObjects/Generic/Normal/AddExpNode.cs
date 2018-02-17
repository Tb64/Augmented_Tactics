using System;
using LogicSpawn.RPGMaker.Generic;

namespace LogicSpawn.RPGMaker.Core
{
    [NodeCategory("Player", "")]
    public class AddExpNode : SimpleNode
    {
        public override string Name
        {
            get { return "Add Player Exp"; }
        }

        public override string Description
        {
            get { return "Gives the player experience."; }
        }

        public override string SubText
        {
            get { return ""; }
        }

        public override bool CanBeLinkedTo
        {
            get
            {
                return true;
            }
        }

        public override string NextNodeLinkLabel(int index)
        {
            return "Next";
        }

        protected override void SetupParameters()
        {
            Add("Amount", PropertyType.Int, null, 0, PropertySource.EnteredOrInput);
        }

        protected override void Eval(NodeChain nodeChain)
        {
            var amount = Convert.ToInt32(ValueOf("Amount"));
            GetObject.PlayerCharacter.AddExp(amount);
        }
    }
}