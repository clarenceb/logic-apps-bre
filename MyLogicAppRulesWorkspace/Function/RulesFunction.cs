//------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
//------------------------------------------------------------

namespace Contoso.Enterprise
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Azure.Functions.Extensions.Workflows;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.Workflows.RuleEngine;
    using Microsoft.Extensions.Logging;
    using System.Xml;
    using ContosoNamespace;
    using System.IO;

    /// <summary>
    /// Represents the RulesFunction flow invoked function.
    /// </summary>
    public class RulesFunction
    {
        private readonly ILogger<RulesFunction> logger;

        public RulesFunction(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<RulesFunction>();
        }

        /// <summary>
        /// Executes the logic app workflow.
        /// </summary>
        /// <param name="ruleSetName">The rule set name.</param>
        /// <param name="documentType">document type of input xml.</param>
        /// <param name="inputXml">input xml type fact</param>
        /// <param name="purchaseAmount">purchase amount, value used to create .NET fact </param>
        /// <param name="zipCode">zip code value used to create .NET fact .</param>
        [FunctionName("RulesFunction")]
        public Task<RuleExecutionResult> RunRules(
            [WorkflowActionTrigger] string ruleSetName, 
            string documentType, 
            string inputXml, 
            int purchaseAmount, 
            string zipCode)
        {
            /***** Summary of steps below *****
             * 1. Get the rule set to Execute 
             * 2. Check if the rule set was retrieved successfully
             * 3. create the rule engine object
             * 4. Create TypedXmlDocument facts for all xml document facts
             * 5. Initialize .NET facts
             * 6. Execute rule engine
             * 7. Retrieve relevant updates facts and send them back
             */
            
            try
            {
                // Get the ruleset based on ruleset name
                var ruleExplorer = new FileStoreRuleExplorer();
                var ruleSet = ruleExplorer.GetRuleSet(ruleSetName);

                // Check if ruleset exists
                if(ruleSet == null)
                {
                    // Log an error in finding the rule set
                    this.logger.LogCritical($"RuleSet instance for '{ruleSetName}' was not found(null)");
                    throw new Exception($"RuleSet instance for '{ruleSetName}' was not found.");
                }             

                // Create rule engine instance
                var ruleEngine = new RuleEngine(ruleSet: ruleSet);
                
                // Create a tracking interceptor to log the rule execution
                LocalDebugTrackingInterceptor interceptor = null;
                
                if (IsDebugTracking())
                {
                    interceptor = new LocalDebugTrackingInterceptor("TrackingOutput.txt");
                    ruleEngine.TrackingInterceptor = interceptor;
                }   

                // Create a typedXml Fact(s) from input xml(s)
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(inputXml);
                var typedXmlDocument = new TypedXmlDocument(documentType, doc);

                // Initialize .NET facts
                var currentPurchase = new ContosoNamespace.ContosoPurchase(purchaseAmount, zipCode);

                if (IsDebugTracking())
                {
                    interceptor.TrackDebugMessage("Debug Initial state .NET Fact", "currentPurchase", currentPurchase);
                }

                // Provide facts to rule engine and run it
                ruleEngine.Execute(new object[] { typedXmlDocument, currentPurchase });

                // Send the relevant results(facts) back
                var updatedDoc = typedXmlDocument.Document as XmlDocument;

                if (IsDebugTracking())
                {
                    interceptor.TrackDebugMessage("Debug .NET Fact", "currentPurchase.PurchaseAmount", currentPurchase.PurchaseAmount);
                }

                var ruleExectionOutput = new RuleExecutionResult()
                {
                    // XML Fact document with updated values
                    XmlDoc = IsDebugTracking() ? PrettyPrintXml(updatedDoc) : updatedDoc.OuterXml,
                    // .NET Facts returned to the Logic App
                    PurchaseAmountPostTax = currentPurchase.PurchaseAmount + currentPurchase.GetSalesTax()
                };

                if (IsDebugTracking())
                {
                    interceptor.TrackDebugMessage("Debug Final state .NET Fact", "currentPurchase", currentPurchase);
                    interceptor.TrackDebugMessage("Debug Final Response", "RuleExecutionResult", ruleExectionOutput);
                }

                return Task.FromResult(ruleExectionOutput);
            }
            catch(RuleEngineException ruleEngineException)
            {
                // Log any rule engine exceptions
                this.logger.LogCritical(ruleEngineException.ToString());
                throw;
            }
        }

        private string PrettyPrintXml(XmlDocument xmlDoc)
        {
            var stringWriter = new StringWriter();
            var xmlTextWriter = new XmlTextWriter(stringWriter)
            {
                Formatting = Formatting.Indented
            };
            xmlDoc.WriteTo(xmlTextWriter);
            return stringWriter.ToString();
        }

        private bool IsDebugTracking()
        {
            var debugTracking = Environment.GetEnvironmentVariable("DEBUG_TRACKING");
            return debugTracking != null && debugTracking.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Results of the rule execution
        /// </summary>
        public class RuleExecutionResult
        {
            /// <summary>
            /// Rules updated xml document
            /// </summary>
            public string XmlDoc { get; set;}

            /// <summary>
            /// Purchase amount post tax
            /// </summary>
            public int PurchaseAmountPostTax { get; set;}

            /// <summary>
            /// Override ToString() method to serialize all properties.
            /// </summary>
            public override string ToString()
            {
                return $"XmlDoc:\n{XmlDoc}\nPurchaseAmountPostTax: {PurchaseAmountPostTax}";
            }
        }
    }
}