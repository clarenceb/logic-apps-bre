using Microsoft.Azure.Workflows.RuleEngine;
using System;

namespace ContosoNamespace
{
    public class MyFactCreator : IFactCreator
    {
        private object[] myFacts;

        public MyFactCreator()
        {
        }

        public object[] CreateFacts (RuleSetInfo ruleSetInfo)
        {
            myFacts = new object[1];
            myFacts.SetValue(new ContosoPurchase(1100, "98052"), 0);
            return myFacts;
        }

        public Type[] GetFactTypes (RuleSetInfo ruleSetInfo)
        {
            Type[] t = new Type[1];
            t[0] = typeof(ContosoPurchase);
            return t;
        }
    }
}
