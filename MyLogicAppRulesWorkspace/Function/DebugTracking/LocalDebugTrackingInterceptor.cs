using System;
using System.Globalization;
using System.IO;
using Microsoft.Azure.Workflows.RuleEngine;

namespace ContosoNamespace
{
    /// <summary>
    /// LocalDebugTrackingInterceptor class is a concrete implementation of the IRuleSetTrackingInterceptor interface.
    /// It's essentially a copy of the DebugTrackingInterceptor class with the exception of the TrackDebugMessage method.
    /// The TrackDebugMessage method is used to add arbitrary debug messages to the trace file.
    /// 
    /// Without this class, the TrackDebugMessage method would not be available to the RulesFunction class.
    /// You could similuate a less readable debug message by using the TrackConditionEvaluation method like so:
    /// <code>
    /// DebugTrackingInterceptor interceptor = new DebugTrackingInterceptor("TrackingOutput.txt");
    /// ruleEngine.TrackingInterceptor = interceptor;
    ///
    /// AppendToTrackingOutput(interceptor, "Debug Final state .NET Fact", "currentPurchase", currentPurchase);
    /// AppendToTrackingOutput(interceptor, "Debug Final Response", "RuleExecutionResult", ruleExectionOutput);
    ///
    /// AppendToTrackingOutput(interceptor, "Debug Initial state .NET Fact", "currentPurchase", currentPurchase);
    /// AppendToTrackingOutput(interceptor, "Debug .NET Fact", "currentPurchase.PurchaseAmount", currentPurchase.PurchaseAmount);
    ///
    /// private void AppendToTrackingOutput(DebugTrackingInterceptor interceptor, string title, string message, object value)
    /// {
    ///     interceptor.TrackConditionEvaluation($"\n\n>>>> DEBUG TRACKING <<<<\n{title}", message, 0, value.ToString(), "", 0, "", true);
    /// }
    /// </code>
    /// </summary>
    public class LocalDebugTrackingInterceptor : IRuleSetTrackingInterceptor, IDisposable
    {
        private string m_ruleEngineGuid;

        private string m_ruleSetName;

        private string m_directoryName;

        private string m_traceFileName;

        private FileStream m_traceFile;

        private StreamWriter m_traceStream;

        private bool disposed;

        private object m_lockObject = new object();

        private static string m_traceHeaderTrace = Messages.traceHeader;

        private static string m_workingMemoryUpdateTrace = Messages.workingMemoryUpdate;

        private static string m_operationTypeTrace = Messages.operationType;

        private static string m_objectTypeTrace = Messages.objectType;

        private static string m_objectInstanceTrace = Messages.objectInstance;

        private static string m_conditionEvaluationTrace = Messages.conditionEvaluation;

        private static string m_testExpressionTrace = Messages.testExpression;

        private static string m_leftOperandValueTrace = Messages.leftOperandValue;

        private static string m_rightOperandValueTrace = Messages.rightOperandValue;

        private static string m_testResultTrace = Messages.testResult;

        private static string m_agendaUpdateTrace = Messages.agendaUpdate;

        private static string m_ruleNameTrace = Messages.ruleName;

        private static string m_conflictResolutionCriteriaTrace = Messages.conflictResolutionCriteria;

        private static string m_ruleFiredTrace = Messages.ruleFired;

        private static string m_assertOperationTrace = Messages.assertOperation;

        private static string m_updateOperationTrace = Messages.updateOperation;

        private static string m_retractOperationTrace = Messages.retractOperation;

        private static string m_assertUnrecognizedOperationTrace = Messages.assertUnrecognizedOperation;

        private static string m_updateUnrecognizedOperationTrace = Messages.updateUnrecognizedOperation;

        private static string m_retractUnrecognizedOperationTrace = Messages.retractUnrecognizedOperation;

        private static string m_updateNotPresentOperationTrace = Messages.updateNotPresentOperation;

        private static string m_retractNotPresentOperationTrace = Messages.retractNotPresentOperation;

        private static string m_unrecognizedOperationTrace = Messages.unrecognizedOperation;

        private static string m_testResultTrueTrace = Messages.testResultTrue;

        private static string m_testResultFalseTrace = Messages.testResultFalse;

        private static string m_ruleEngineInstanceTrace = Messages.ruleEngineInstance;

        private static string m_rulesetNameTrace = Messages.ruleSetName;

        private static string m_addOperationTrace = Messages.addOperation;

        private static string m_removeOperationTrace = Messages.removeOperation;

        public LocalDebugTrackingInterceptor()
        {
            m_directoryName = Path.GetTempPath();
        }

        public LocalDebugTrackingInterceptor(string traceFile)
        {
            if (traceFile == null)
            {
                throw new RuleEngineArgumentNullException(string.Format(CultureInfo.CurrentCulture, Messages.nullArgument, "traceFile"), GetType().FullName, "traceFile");
            }

            m_directoryName = string.Empty;
            m_traceFileName = traceFile;
            m_traceFile = new FileStream(m_traceFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
            m_traceStream = new StreamWriter(m_traceFile);
            m_traceStream.BaseStream.Seek(0L, SeekOrigin.End);
        }

        public void SetTrackingConfig(TrackingConfiguration trackingConfig)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        public string GetTraceOutput()
        {
            string result = null;
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            lock (m_lockObject)
            {
                if (m_traceStream != null)
                {
                    StreamReader streamReader = new StreamReader(m_traceFile);
                    streamReader.BaseStream.Seek(0L, SeekOrigin.Begin);
                    result = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                else
                {
                    result = string.Empty;
                }
            }

            return result;
        }

        public void DeleteTraceFile()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            lock (m_lockObject)
            {
                if (m_traceFileName != null)
                {
                    CloseTraceFile();
                    new FileInfo(m_traceFileName).Delete();
                }
            }
        }

        public void CloseTraceFile()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            lock (m_lockObject)
            {
                if (m_traceFileName != null)
                {
                    if (m_traceFile != null)
                    {
                        m_traceFile.Close();
                    }

                    m_traceFile = null;
                    m_traceStream = null;
                }
            }
        }

        public void TrackRuleSetEngineAssociation(RuleSetInfo ruleSetInfo, Guid ruleEngineGuid)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (ruleSetInfo == null)
            {
                throw new RuleEngineArgumentNullException(string.Format(CultureInfo.CurrentCulture, Messages.nullArgument, "strClassName"), GetType().FullName, "ruleSetInfo");
            }

            lock (m_lockObject)
            {
                m_ruleSetName = ruleSetInfo.Name;
                m_ruleEngineGuid = ruleEngineGuid.ToString();
                if (m_traceStream == null)
                {
                    if (m_traceFileName == null)
                    {
                        m_traceFileName = m_directoryName + ruleSetInfo.Name + "_" + DateTime.Now.ToFileTime().ToString(CultureInfo.CurrentCulture) + ".trace";
                    }

                    m_traceFile = new FileStream(m_traceFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
                    m_traceStream = new StreamWriter(m_traceFile);
                    m_traceStream.BaseStream.Seek(0L, SeekOrigin.End);
                }

                m_traceStream.WriteLine(m_traceHeaderTrace + " " + m_ruleSetName + " " + DateTime.Now.ToString(CultureInfo.CurrentCulture));
                m_traceStream.Flush();
            }
        }

        public void TrackFactActivity(FactActivityType activityType, string classType, int classInstanceId)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (classType == null)
            {
                throw new RuleEngineArgumentNullException(string.Format(CultureInfo.CurrentCulture, Messages.nullArgument, "strClassName"), GetType().FullName, "objectType");
            }

            lock (m_lockObject)
            {
                if (m_traceStream == null)
                {
                    throw new RuleEngineTrackingException(Messages.noAssociation);
                }

                PrintHeader(m_workingMemoryUpdateTrace);
                switch (activityType)
                {
                    case FactActivityType.Assert:
                        m_traceStream.WriteLine(m_operationTypeTrace + " " + m_assertOperationTrace);
                        break;
                    case FactActivityType.Retract:
                        m_traceStream.WriteLine(m_operationTypeTrace + " " + m_retractOperationTrace);
                        break;
                    case FactActivityType.Update:
                        m_traceStream.WriteLine(m_operationTypeTrace + " " + m_updateOperationTrace);
                        break;
                    case FactActivityType.AssertUnrecognized:
                        m_traceStream.WriteLine(m_operationTypeTrace + " " + m_assertUnrecognizedOperationTrace);
                        break;
                    case FactActivityType.RetractUnrecognized:
                        m_traceStream.WriteLine(m_operationTypeTrace + " " + m_retractUnrecognizedOperationTrace);
                        break;
                    case FactActivityType.UpdateUnrecognized:
                        m_traceStream.WriteLine(m_operationTypeTrace + " " + m_updateUnrecognizedOperationTrace);
                        break;
                    case FactActivityType.RetractNotPresent:
                        m_traceStream.WriteLine(m_operationTypeTrace + " " + m_retractNotPresentOperationTrace);
                        break;
                    case FactActivityType.UpdateNotPresent:
                        m_traceStream.WriteLine(m_operationTypeTrace + " " + m_updateNotPresentOperationTrace);
                        break;
                    default:
                        m_traceStream.WriteLine(m_operationTypeTrace + " " + m_unrecognizedOperationTrace);
                        break;
                }

                m_traceStream.WriteLine(m_objectTypeTrace + " " + classType);
                m_traceStream.WriteLine(m_objectInstanceTrace + " " + classInstanceId.ToString(CultureInfo.CurrentCulture));
                m_traceStream.Flush();
            }
        }

        public void TrackConditionEvaluation(string testExpression, string leftClassType, int leftClassInstanceId, object leftValue, string rightClassType, int rightClassInstanceId, object rightValue, bool result)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (testExpression == null)
            {
                throw new RuleEngineArgumentNullException(string.Format(CultureInfo.CurrentCulture, Messages.nullArgument, "strClassName"), GetType().FullName, "testExpression");
            }

            lock (m_lockObject)
            {
                if (m_traceStream == null)
                {
                    throw new RuleEngineTrackingException(Messages.noAssociation);
                }

                PrintHeader(m_conditionEvaluationTrace);
                m_traceStream.WriteLine(m_testExpressionTrace + " " + testExpression);
                if (leftValue == null)
                {
                    m_traceStream.WriteLine(m_leftOperandValueTrace + " null");
                }
                else if (leftValue.GetType().IsClass && Type.GetTypeCode(leftValue.GetType()) != TypeCode.String)
                {
                    m_traceStream.WriteLine(m_leftOperandValueTrace + " " + m_objectInstanceTrace + " " + leftValue.GetHashCode().ToString(CultureInfo.CurrentCulture));
                }
                else
                {
                    m_traceStream.WriteLine(m_leftOperandValueTrace + " " + leftValue.ToString());
                }

                if (rightValue == null)
                {
                    m_traceStream.WriteLine(m_rightOperandValueTrace + " null");
                }
                else if (rightValue.GetType().IsClass && Type.GetTypeCode(rightValue.GetType()) != TypeCode.String)
                {
                    m_traceStream.WriteLine(m_rightOperandValueTrace + " " + m_objectInstanceTrace + " " + rightValue.GetHashCode().ToString(CultureInfo.CurrentCulture));
                }
                else
                {
                    m_traceStream.WriteLine(m_rightOperandValueTrace + " " + rightValue.ToString());
                }

                if (result)
                {
                    m_traceStream.WriteLine(m_testResultTrace + " " + m_testResultTrueTrace);
                }
                else
                {
                    m_traceStream.WriteLine(m_testResultTrace + " " + m_testResultFalseTrace);
                }

                m_traceStream.Flush();
            }
        }

        public void TrackAgendaUpdate(bool isAddition, string ruleName, object conflictResolutionCriteria)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (ruleName == null)
            {
                throw new RuleEngineArgumentNullException(string.Format(CultureInfo.CurrentCulture, Messages.nullArgument, "strClassName"), GetType().FullName, "ruleName");
            }

            lock (m_lockObject)
            {
                if (m_traceStream == null)
                {
                    throw new RuleEngineTrackingException(Messages.noAssociation);
                }

                PrintHeader(m_agendaUpdateTrace);
                if (isAddition)
                {
                    m_traceStream.WriteLine(m_operationTypeTrace + " " + m_addOperationTrace);
                }
                else
                {
                    m_traceStream.WriteLine(m_operationTypeTrace + " " + m_removeOperationTrace);
                }

                m_traceStream.WriteLine(m_ruleNameTrace + " " + ruleName);
                if (conflictResolutionCriteria == null)
                {
                    m_traceStream.WriteLine(m_conflictResolutionCriteriaTrace + " null");
                }
                else
                {
                    m_traceStream.WriteLine(m_conflictResolutionCriteriaTrace + " " + conflictResolutionCriteria.ToString());
                }

                m_traceStream.Flush();
            }
        }

        public void TrackRuleFiring(string ruleName, object conflictResolutionCriteria)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (ruleName == null)
            {
                throw new RuleEngineArgumentNullException(string.Format(CultureInfo.CurrentCulture, Messages.nullArgument, "ruleName"), GetType().FullName, "ruleName");
            }

            lock (m_lockObject)
            {
                if (m_traceStream == null)
                {
                    throw new RuleEngineTrackingException(Messages.noAssociation);
                }

                PrintHeader(m_ruleFiredTrace);
                m_traceStream.WriteLine(m_ruleNameTrace + " " + ruleName);
                if (conflictResolutionCriteria == null)
                {
                    m_traceStream.WriteLine(m_conflictResolutionCriteriaTrace + " null");
                }
                else
                {
                    m_traceStream.WriteLine(m_conflictResolutionCriteriaTrace + " " + conflictResolutionCriteria.ToString());
                }

                m_traceStream.Flush();
            }
        }

        /// <summary>
        /// Add arbitrary debug message to the trace file.
        /// </summary>
        /// <param name="title">Debug title field</param>
        /// <param name="message">Debug message field</param>
        /// <param name="value">debug value field</param>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="RuleEngineTrackingException"></exception>
        /// <example>
        /// <code>
        /// var currentPurchase = new ContosoNamespace.ContosoPurchase(1100, "98052");
        /// interceptor.TrackDebugMessage("Debug Initial state .NET Fact", "currentPurchase", currentPurchase);
        /// </code>
        /// </example>
        public void TrackDebugMessage(string title, string message, object value)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            lock (m_lockObject)
            {
                if (m_traceStream == null)
                {
                    throw new RuleEngineTrackingException(Messages.noAssociation);
                }

                PrintHeader("------------- DEBUG MESSAGE");
                m_traceStream.WriteLine($"Title: {title}: {title}");
                m_traceStream.WriteLine($"Message: {message}");
                m_traceStream.WriteLine($"Value: {value.ToString()}");
                m_traceStream.Flush();
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            lock (m_lockObject)
            {
                disposed = true;
                if (m_traceFile != null)
                {
                    m_traceFile.Close();
                    m_traceFile = null;
                }

                if (m_traceStream != null)
                {
                    m_traceStream.Close();
                    m_traceStream = null;
                }
            }
        }

        private void PrintHeader(string hdr)
        {
            m_traceStream.WriteLine();
            m_traceStream.WriteLine(hdr + " " + DateTime.Now.ToString(CultureInfo.CurrentCulture));
            m_traceStream.WriteLine(m_ruleEngineInstanceTrace + " " + m_ruleEngineGuid);
            m_traceStream.WriteLine(m_rulesetNameTrace + " " + m_ruleSetName);
        }
    }
}