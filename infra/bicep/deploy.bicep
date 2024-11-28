@description('The location all resources will be deployed to')
param location string = resourceGroup().location

@description('A prefix to add to the start of all resource names')
param prefix string = 'bre'

@description('Random value to ensure unique suffix')
param randomValue string = newGuid()

@description('Tags to apply to all deployed resources')
param tags object = {}

var uniqueSuffix = uniqueString(resourceGroup().id, prefix, randomValue)
var storageKind = 'StorageV2'
var storageSkuName = 'Standard_LRS'
var storageMinimumTlsVersion = 'TLS1_2'
var logRetentionInDays= 30
var logAnalyticsSkuName = 'PerGB2018'
var logicAppStdName = '${prefix}-logicappstd-${uniqueSuffix}'
var appServicePlanName = '${prefix}-appserviceplan'
var storageName = '${prefix}${uniqueSuffix}'
var logAnalyticsName = '${prefix}-logs${uniqueSuffix}'
var appInsName = '${prefix}-appins${uniqueSuffix}'

resource storage 'Microsoft.Storage/storageAccounts@2022-05-01' = {
  name: storageName
  location: location
  sku: {
    name: storageSkuName
  }
  kind: storageKind
  tags: tags
  properties: {
    supportsHttpsTrafficOnly: true
    minimumTlsVersion: storageMinimumTlsVersion
    defaultToOAuthAuthentication: true
    allowBlobPublicAccess: false
  }
}

resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: logAnalyticsName
  location: location
  tags: tags
  properties: {
    retentionInDays: logRetentionInDays
    sku: {
      name: logAnalyticsSkuName
    }
  }
}

resource appInsights 'Microsoft.Insights/components@2020-02-02-preview' = {
  name: appInsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalytics.id
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2018-02-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'WS1'
    tier: 'WorkflowStandard'
  }
  kind: 'windows'
  tags: tags
}

resource logicAppStd 'Microsoft.Web/sites@2022-03-01' = {
  name: logicAppStdName
  location: location
  kind: 'functionapp,workflowapp'
  identity: {
    type: 'SystemAssigned'
  }
  tags: tags
  properties: {
    siteConfig: {
      appSettings: [
        {
          name: 'APP_KIND'
          value: 'workflowApp'
        }
        {
          name: 'AzureFunctionsJobHost__extensionBundle__id'
          value: 'Microsoft.Azure.Functions.ExtensionBundle.Workflows'
        }
        {
          name: 'AzureFunctionsJobHost__extensionBundle__version'
          value: '[1.*, 2.0.0)'
        }
        {
          name: 'AzureWebJobsStorage'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storage.name};AccountKey=${storage.listKeys().keys[0].value};EndpointSuffix=core.windows.net'
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~4'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet'
        }
        {
          name: 'WEBSITE_NODE_DEFAULT_VERSION'
          value: '~18'
        }
        {
          name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
          value: 'DefaultEndpointsProtocol=https;AccountName=${storage.name};AccountKey=${storage.listKeys().keys[0].value};EndpointSuffix=core.windows.net'
        }
        {
          name: 'WEBSITE_CONTENTSHARE'
          value: logicAppStdName
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsights.properties.ConnectionString
        }
        {
          name: 'AzureWebJobsFeatureFlags'
          value: 'EnableMultiLanguageWorker'
        }
      ]
      netFrameworkVersion: 'v6.0'
      use32BitWorkerProcess: false
      cors: {
        supportCredentials: false
      }
    }
    clientAffinityEnabled: false
    publicNetworkAccess: 'Enabled'
    httpsOnly: true
    serverFarmId: appServicePlan.id
  }

  resource basicPublishingCredentialsPoliciesScm 'basicPublishingCredentialsPolicies@2022-09-01' = {
    name: 'scm'
    properties: {
      allow: false
    }
  }

  resource basicPublishingCredentialsPoliciesFtp 'basicPublishingCredentialsPolicies@2022-09-01' = {
    name: 'ftp'
    properties: {
      allow: false
    }
  }
}

output logicAppName string = logicAppStd.name
output logicAppUrl string = logicAppStd.properties.defaultHostName
