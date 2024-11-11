using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ContosoNamespace {

    internal class Messages
    {
        private static ResourceManager resourceMan;

        private static CultureInfo resourceCulture;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (resourceMan == null)
                {
                    resourceMan = new ResourceManager("Microsoft.Azure.Workflows.RuleEngine.Messages", typeof(Microsoft.Azure.Workflows.RuleEngine.DebugTrackingInterceptor).Assembly);
                }

                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static string addOperation => ResourceManager.GetString("addOperation", resourceCulture);

        internal static string agendaUpdate => ResourceManager.GetString("agendaUpdate", resourceCulture);

        internal static string agendaUpdateFailed => ResourceManager.GetString("agendaUpdateFailed", resourceCulture);

        internal static string alreadyDeployed => ResourceManager.GetString("alreadyDeployed", resourceCulture);

        internal static string argumentWrongType => ResourceManager.GetString("argumentWrongType", resourceCulture);

        internal static string arithmeticIncompatibleArguments => ResourceManager.GetString("arithmeticIncompatibleArguments", resourceCulture);

        internal static string arithmeticOp => ResourceManager.GetString("arithmeticOp", resourceCulture);

        internal static string assemblyNotFound => ResourceManager.GetString("assemblyNotFound", resourceCulture);

        internal static string assertOperation => ResourceManager.GetString("assertOperation", resourceCulture);

        internal static string assertUnrecognizedOperation => ResourceManager.GetString("assertUnrecognizedOperation", resourceCulture);

        internal static string badRef => ResourceManager.GetString("badRef", resourceCulture);

        internal static string badSchema => ResourceManager.GetString("badSchema", resourceCulture);

        internal static string bindingNotImpl => ResourceManager.GetString("bindingNotImpl", resourceCulture);

        internal static string bindingNull => ResourceManager.GetString("bindingNull", resourceCulture);

        internal static string bindingsKey => ResourceManager.GetString("bindingsKey", resourceCulture);

        internal static string cannotGetLatestPolicyFromUpdtSvc => ResourceManager.GetString("cannotGetLatestPolicyFromUpdtSvc", resourceCulture);

        internal static string cannotRegisterCallbackWithUpdtSvc => ResourceManager.GetString("cannotRegisterCallbackWithUpdtSvc", resourceCulture);

        internal static string cantWrite => ResourceManager.GetString("cantWrite", resourceCulture);

        internal static string castFailed => ResourceManager.GetString("castFailed", resourceCulture);

        internal static string castNotAllowed => ResourceManager.GetString("castNotAllowed", resourceCulture);

        internal static string classMissing => ResourceManager.GetString("classMissing", resourceCulture);

        internal static string classNotFound => ResourceManager.GetString("classNotFound", resourceCulture);

        internal static string classWrong => ResourceManager.GetString("classWrong", resourceCulture);

        internal static string columnIncompatibleArguments => ResourceManager.GetString("columnIncompatibleArguments", resourceCulture);

        internal static string columnNotFound => ResourceManager.GetString("columnNotFound", resourceCulture);

        internal static string comparisonIncompatibleArguments => ResourceManager.GetString("comparisonIncompatibleArguments", resourceCulture);

        internal static string compatibilityCheckError => ResourceManager.GetString("compatibilityCheckError", resourceCulture);

        internal static string compatibilityNoExecute => ResourceManager.GetString("compatibilityNoExecute", resourceCulture);

        internal static string compatibilityNoStoredProc => ResourceManager.GetString("compatibilityNoStoredProc", resourceCulture);

        internal static string conditionEvaluation => ResourceManager.GetString("conditionEvaluation", resourceCulture);

        internal static string conditionEvaluationFailed => ResourceManager.GetString("conditionEvaluationFailed", resourceCulture);

        internal static string conflictResolutionCriteria => ResourceManager.GetString("conflictResolutionCriteria", resourceCulture);

        internal static string constructorNotFound => ResourceManager.GetString("constructorNotFound", resourceCulture);

        internal static string conversionFailed => ResourceManager.GetString("conversionFailed", resourceCulture);

        internal static string createObjectException => ResourceManager.GetString("createObjectException", resourceCulture);

        internal static string dataRowMissing => ResourceManager.GetString("dataRowMissing", resourceCulture);

        internal static string dataRowsExceedLimit => ResourceManager.GetString("dataRowsExceedLimit", resourceCulture);

        internal static string dataRowWrong => ResourceManager.GetString("dataRowWrong", resourceCulture);

        internal static string dataUpdateFailed => ResourceManager.GetString("dataUpdateFailed", resourceCulture);

        internal static string dbConversionError => ResourceManager.GetString("dbConversionError", resourceCulture);

        internal static string dbNotConfigured => ResourceManager.GetString("dbNotConfigured", resourceCulture);

        internal static string deleteArtifactAuthError => ResourceManager.GetString("deleteArtifactAuthError", resourceCulture);

        internal static string deployFailed => ResourceManager.GetString("deployFailed", resourceCulture);

        internal static string deploymentConnectionFailed => ResourceManager.GetString("deploymentConnectionFailed", resourceCulture);

        internal static string deserialize => ResourceManager.GetString("deserialize", resourceCulture);

        internal static string dictionaryBadKey => ResourceManager.GetString("dictionaryBadKey", resourceCulture);

        internal static string divideByZeroError => ResourceManager.GetString("divideByZeroError", resourceCulture);

        internal static string emptyActionList => ResourceManager.GetString("emptyActionList", resourceCulture);

        internal static string errorCount => ResourceManager.GetString("errorCount", resourceCulture);

        internal static string factActivityTrackingFailed => ResourceManager.GetString("factActivityTrackingFailed", resourceCulture);

        internal static string factRetrieverException => ResourceManager.GetString("factRetrieverException", resourceCulture);

        internal static string fieldIncompatibleArguments => ResourceManager.GetString("fieldIncompatibleArguments", resourceCulture);

        internal static string fieldNotFound => ResourceManager.GetString("fieldNotFound", resourceCulture);

        internal static string fileEmptyError => ResourceManager.GetString("fileEmptyError", resourceCulture);

        internal static string fileNotFound => ResourceManager.GetString("fileNotFound", resourceCulture);

        internal static string filePosition => ResourceManager.GetString("filePosition", resourceCulture);

        internal static string functionNotImpl => ResourceManager.GetString("functionNotImpl", resourceCulture);

        internal static string getFailed => ResourceManager.GetString("getFailed", resourceCulture);

        internal static string incompatibleTypes => ResourceManager.GetString("incompatibleTypes", resourceCulture);

        internal static string inconsistentConstantType => ResourceManager.GetString("inconsistentConstantType", resourceCulture);

        internal static string initFailure => ResourceManager.GetString("initFailure", resourceCulture);

        internal static string insufficientPermissions => ResourceManager.GetString("insufficientPermissions", resourceCulture);

        internal static string interfaceNotFound => ResourceManager.GetString("interfaceNotFound", resourceCulture);

        internal static string invalidArtifactTypeError => ResourceManager.GetString("invalidArtifactTypeError", resourceCulture);

        internal static string invalidAuthGroupError => ResourceManager.GetString("invalidAuthGroupError", resourceCulture);

        internal static string invalidAuthIdentityError => ResourceManager.GetString("invalidAuthIdentityError", resourceCulture);

        internal static string invalidColumnName => ResourceManager.GetString("invalidColumnName", resourceCulture);

        internal static string invalidConstantNullType => ResourceManager.GetString("invalidConstantNullType", resourceCulture);

        internal static string invalidExecutor => ResourceManager.GetString("invalidExecutor", resourceCulture);

        internal static string invalidNameCharacter => ResourceManager.GetString("invalidNameCharacter", resourceCulture);

        internal static string invalidPropertyName => ResourceManager.GetString("invalidPropertyName", resourceCulture);

        internal static string invalidRangeArguments => ResourceManager.GetString("invalidRangeArguments", resourceCulture);

        internal static string invalidTableName => ResourceManager.GetString("invalidTableName", resourceCulture);

        internal static string invocationFailed => ResourceManager.GetString("invocationFailed", resourceCulture);

        internal static string itemTypeInvalid => ResourceManager.GetString("itemTypeInvalid", resourceCulture);

        internal static string leftOperandValue => ResourceManager.GetString("leftOperandValue", resourceCulture);

        internal static string logicalOp => ResourceManager.GetString("logicalOp", resourceCulture);

        internal static string loopDetected => ResourceManager.GetString("loopDetected", resourceCulture);

        internal static string memberNotFound => ResourceManager.GetString("memberNotFound", resourceCulture);

        internal static string messageError1 => ResourceManager.GetString("messageError1", resourceCulture);

        internal static string messageError2 => ResourceManager.GetString("messageError2", resourceCulture);

        internal static string methodNotImplemented => ResourceManager.GetString("methodNotImplemented", resourceCulture);

        internal static string missingParm => ResourceManager.GetString("missingParm", resourceCulture);

        internal static string modifyRuleSetAuthorizationError => ResourceManager.GetString("modifyRuleSetAuthorizationError", resourceCulture);

        internal static string modifyVersionError => ResourceManager.GetString("modifyVersionError", resourceCulture);

        internal static string modifyVocabularyAuthorizationError => ResourceManager.GetString("modifyVocabularyAuthorizationError", resourceCulture);

        internal static string noAssociation => ResourceManager.GetString("noAssociation", resourceCulture);

        internal static string noClass => ResourceManager.GetString("noClass", resourceCulture);

        internal static string noDatabase => ResourceManager.GetString("noDatabase", resourceCulture);

        internal static string noDeployedVersions => ResourceManager.GetString("noDeployedVersions", resourceCulture);

        internal static string noSchema => ResourceManager.GetString("noSchema", resourceCulture);

        internal static string noServer => ResourceManager.GetString("noServer", resourceCulture);

        internal static string notAClass => ResourceManager.GetString("notAClass", resourceCulture);

        internal static string notClass => ResourceManager.GetString("notClass", resourceCulture);

        internal static string notComparable => ResourceManager.GetString("notComparable", resourceCulture);

        internal static string notDeployed => ResourceManager.GetString("notDeployed", resourceCulture);

        internal static string notRecognized => ResourceManager.GetString("notRecognized", resourceCulture);

        internal static string notSerializable => ResourceManager.GetString("notSerializable", resourceCulture);

        internal static string noUserObject => ResourceManager.GetString("noUserObject", resourceCulture);

        internal static string nullArgument => ResourceManager.GetString("nullArgument", resourceCulture);

        internal static string nullArguments => ResourceManager.GetString("nullArguments", resourceCulture);

        internal static string objectDisposed => ResourceManager.GetString("objectDisposed", resourceCulture);

        internal static string objectInstance => ResourceManager.GetString("objectInstance", resourceCulture);

        internal static string objectInstantiationFailed => ResourceManager.GetString("objectInstantiationFailed", resourceCulture);

        internal static string objectType => ResourceManager.GetString("objectType", resourceCulture);

        internal static string operationType => ResourceManager.GetString("operationType", resourceCulture);

        internal static string overflowError => ResourceManager.GetString("overflowError", resourceCulture);

        internal static string primaryKeyMissing => ResourceManager.GetString("primaryKeyMissing", resourceCulture);

        internal static string publishEmptyRuleSetError => ResourceManager.GetString("publishEmptyRuleSetError", resourceCulture);

        internal static string publishEmptyVocabularyError => ResourceManager.GetString("publishEmptyVocabularyError", resourceCulture);

        internal static string pubsubError1 => ResourceManager.GetString("pubsubError1", resourceCulture);

        internal static string pubsubError2 => ResourceManager.GetString("pubsubError2", resourceCulture);

        internal static string pubsubError3 => ResourceManager.GetString("pubsubError3", resourceCulture);

        internal static string readOnlyError => ResourceManager.GetString("readOnlyError", resourceCulture);

        internal static string readRuleSetAuthorizationError => ResourceManager.GetString("readRuleSetAuthorizationError", resourceCulture);

        internal static string readVocabularyAuthorizationError => ResourceManager.GetString("readVocabularyAuthorizationError", resourceCulture);

        internal static string regexNotSupported => ResourceManager.GetString("regexNotSupported", resourceCulture);

        internal static string RemoteUpdateServiceNoLocalhost => ResourceManager.GetString("RemoteUpdateServiceNoLocalhost", resourceCulture);

        internal static string RemoteUpdateServiceStartFailure => ResourceManager.GetString("RemoteUpdateServiceStartFailure", resourceCulture);

        internal static string RemoteUpdateServiceStartSuccess => ResourceManager.GetString("RemoteUpdateServiceStartSuccess", resourceCulture);

        internal static string RemoteUpdateServiceStopFailure => ResourceManager.GetString("RemoteUpdateServiceStopFailure", resourceCulture);

        internal static string RemoteUpdateServiceStopSuccess => ResourceManager.GetString("RemoteUpdateServiceStopSuccess", resourceCulture);

        internal static string removeOperation => ResourceManager.GetString("removeOperation", resourceCulture);

        internal static string retractNotPresentOperation => ResourceManager.GetString("retractNotPresentOperation", resourceCulture);

        internal static string retractOperation => ResourceManager.GetString("retractOperation", resourceCulture);

        internal static string retractUnrecognizedOperation => ResourceManager.GetString("retractUnrecognizedOperation", resourceCulture);

        internal static string rightOperandValue => ResourceManager.GetString("rightOperandValue", resourceCulture);

        internal static string rule => ResourceManager.GetString("rule", resourceCulture);

        internal static string ruleEngineException => ResourceManager.GetString("ruleEngineException", resourceCulture);

        internal static string ruleEngineInstance => ResourceManager.GetString("ruleEngineInstance", resourceCulture);

        internal static string ruleFired => ResourceManager.GetString("ruleFired", resourceCulture);

        internal static string ruleFiringTrackingFailed => ResourceManager.GetString("ruleFiringTrackingFailed", resourceCulture);

        internal static string ruleName => ResourceManager.GetString("ruleName", resourceCulture);

        internal static string ruleSet => ResourceManager.GetString("ruleSet", resourceCulture);

        internal static string ruleSetDeployedError => ResourceManager.GetString("ruleSetDeployedError", resourceCulture);

        internal static string ruleSetEngineAssociationFailed => ResourceManager.GetString("ruleSetEngineAssociationFailed", resourceCulture);

        internal static string rulesetExistsDuringImport => ResourceManager.GetString("rulesetExistsDuringImport", resourceCulture);

        internal static string ruleSetFetchFailed => ResourceManager.GetString("ruleSetFetchFailed", resourceCulture);

        internal static string rulesetGuidMismatch => ResourceManager.GetString("rulesetGuidMismatch", resourceCulture);

        internal static string ruleSetName => ResourceManager.GetString("ruleSetName", resourceCulture);

        internal static string ruleSetNotFound => ResourceManager.GetString("ruleSetNotFound", resourceCulture);

        internal static string ruleSetNotPublished => ResourceManager.GetString("ruleSetNotPublished", resourceCulture);

        internal static string ruleSetPublished => ResourceManager.GetString("ruleSetPublished", resourceCulture);

        internal static string ruleSetRenameError => ResourceManager.GetString("ruleSetRenameError", resourceCulture);

        internal static string ruleSetRenameFails => ResourceManager.GetString("ruleSetRenameFails", resourceCulture);

        internal static string ruleSetRenamePub => ResourceManager.GetString("ruleSetRenamePub", resourceCulture);

        internal static string ruleSetTooComplex => ResourceManager.GetString("ruleSetTooComplex", resourceCulture);

        internal static string rulestoreConnectionFailed => ResourceManager.GetString("rulestoreConnectionFailed", resourceCulture);

        internal static string setFailed => ResourceManager.GetString("setFailed", resourceCulture);

        internal static string sqlWrongVersion => ResourceManager.GetString("sqlWrongVersion", resourceCulture);

        internal static string staticMethodFailed => ResourceManager.GetString("staticMethodFailed", resourceCulture);

        internal static string staticNotAllowed => ResourceManager.GetString("staticNotAllowed", resourceCulture);

        internal static string testExpression => ResourceManager.GetString("testExpression", resourceCulture);

        internal static string testResult => ResourceManager.GetString("testResult", resourceCulture);

        internal static string testResultFalse => ResourceManager.GetString("testResultFalse", resourceCulture);

        internal static string testResultTrue => ResourceManager.GetString("testResultTrue", resourceCulture);

        internal static string traceHeader => ResourceManager.GetString("traceHeader", resourceCulture);

        internal static string trackingInterceptorException => ResourceManager.GetString("trackingInterceptorException", resourceCulture);

        internal static string unableToDeleteVocabularyError => ResourceManager.GetString("unableToDeleteVocabularyError", resourceCulture);

        internal static string unableToSaveRuleSetError => ResourceManager.GetString("unableToSaveRuleSetError", resourceCulture);

        internal static string unableToSaveVocabularyError => ResourceManager.GetString("unableToSaveVocabularyError", resourceCulture);

        internal static string unexpectedArgumentType => ResourceManager.GetString("unexpectedArgumentType", resourceCulture);

        internal static string unexpectedArgumentValue => ResourceManager.GetString("unexpectedArgumentValue", resourceCulture);

        internal static string unrecognizedOperation => ResourceManager.GetString("unrecognizedOperation", resourceCulture);

        internal static string unsupportedType => ResourceManager.GetString("unsupportedType", resourceCulture);

        internal static string updateNotPresentOperation => ResourceManager.GetString("updateNotPresentOperation", resourceCulture);

        internal static string updateOperation => ResourceManager.GetString("updateOperation", resourceCulture);

        internal static string updateServiceConnectionFailed => ResourceManager.GetString("updateServiceConnectionFailed", resourceCulture);

        internal static string updateUnrecognizedOperation => ResourceManager.GetString("updateUnrecognizedOperation", resourceCulture);

        internal static string userSwitchFailed => ResourceManager.GetString("userSwitchFailed", resourceCulture);

        internal static string validationError => ResourceManager.GetString("validationError", resourceCulture);

        internal static string vocabRenameError => ResourceManager.GetString("vocabRenameError", resourceCulture);

        internal static string vocabRenameFails => ResourceManager.GetString("vocabRenameFails", resourceCulture);

        internal static string vocabRenamePub => ResourceManager.GetString("vocabRenamePub", resourceCulture);

        internal static string vocabulary => ResourceManager.GetString("vocabulary", resourceCulture);

        internal static string vocabularyExistsDuringImport => ResourceManager.GetString("vocabularyExistsDuringImport", resourceCulture);

        internal static string vocabularyNotFound => ResourceManager.GetString("vocabularyNotFound", resourceCulture);

        internal static string vocabularyPublished => ResourceManager.GetString("vocabularyPublished", resourceCulture);

        internal static string workingMemoryUpdate => ResourceManager.GetString("workingMemoryUpdate", resourceCulture);

        internal static string xmlConversionError => ResourceManager.GetString("xmlConversionError", resourceCulture);

        internal static string xmlMissing => ResourceManager.GetString("xmlMissing", resourceCulture);

        internal static string xmlObjectWriteError => ResourceManager.GetString("xmlObjectWriteError", resourceCulture);

        internal static string xmlSetError => ResourceManager.GetString("xmlSetError", resourceCulture);

        internal static string xmlWrong => ResourceManager.GetString("xmlWrong", resourceCulture);

        internal Messages()
        {
        }
    }
}