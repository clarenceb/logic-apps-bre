# Logic Apps BRE

A simple example of building and deploying a Logic App with Business Rules Engine (BRE).

## Deploy infrastructure

Creates the Logic Apps Standard Single Tenant plan.

```bash
RESOURCE_GROUP_NAME=la-bre-cicd
LOCATION=australiaeast
SUBSCRIPTION_ID=xxxx-xxxx-xxxx-xxxx
LOGIC_APP_NAME=bre-logicappstd

az login
az account set --subscription $SUBSCRIPTION_ID

az group create --name $RESOURCE_GROUP_NAME --location $LOCATION

az bicep upgrade
az deployment group create --resource-group $RESOURCE_GROUP_NAME --template-file infra/bicep/deploy.bicep [--parameters @deploy.parameters.json]
```

# Build Logic App with BRE

Builds the Logic App with Business Rules Engine (BRE) assemblies bundled into a ZIP file.

Logic Apps with BRE require compiling and then adding any of your custom code libraries, including your .NET facts assemblies, to the `lib/custom/net472` folder in your logic app project where workflows look for custom functions to run.

```bash
# Build the Function App as a custom code library for Logic Apps
cd MyLogicAppRulesWorkspace/Function
dotnet restore ./RulesFunction.csproj
dotnet build ./RulesFunction.csproj

cd ../MyLogicAppRulesWorkspace/LogicApp
zip -r logicapps.zip . -x *local.settings.json -x *appsettings.json -x *__azurite_db_*.json -x *storage__ -x *.zip
```

# Deploy Logic App with BRE

Deploys the Logic App with Business Rules Engine (BRE) assemblies bundled into a ZIP file to a Logic Apps Standard plan.

```bash
az functionapp deploy --resource-group $RESOURCE_GROUP_NAME --name $LOGIC_APP_NAME --src-path logicapps.zip --type zip

# If your Logic App using Connections (connections.json) or Parameters (parameters.json) files, you should include them in the ZIP file.

# If your Logic App uses App Settings for configuration, you should create a `appsettings.json` file, e.g.
#
# {
#     "setting1": "value1",
#     "setting2": "value2"
# }

# Then apply the appsettings.json file to the Logic App
az functionapp config appsettings set -g $RESOURCE_GROUP_NAME -n $LOGIC_APP_NAME --settings @appsettings.json
```

# Azure Pipleines automation

Create a new Azure DevOps project and import the `azure-pipelines.yml` file to automate the build and deployment of the Logic App with BRE.

This pipeline is using the new YAML-based multi-stage pipelines feature in Azure DevOps.

It deploys infrastructure, builds the Logic App with BRE, and deploys the Logic App to a Logic Apps Standard plan.

You can change this to segregate the infrastructure deployment and Logic App deployment into separate pipelines.
