# Logic Apps BRE

A simple example of building and deploying a Logic App with Business Rules Engine (BRE).

## Deploy infrastructure

Creates the Logic Apps Standard Single Tenant plan.

```bash
RESOURCE_GROUP_NAME=la-bre-cicd
LOCATION=australiaeast
SUBSCRIPTION_ID=xxxx-xxxx-xxxx-xxxx

az login
az account set --subscription $SUBSCRIPTION_ID

az group create --name $RESOURCE_GROUP_NAME --location $LOCATION

az bicep upgrade
az deployment group create --resource-group $RESOURCE_GROUP_NAME --template-file infra/bicep/deploy.bicep [--parameters @deploy.parameters.json]
```

## Build Logic App with BRE

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

## Deploy Logic App with BRE

Deploys the Logic App with Business Rules Engine (BRE) assemblies bundled into a ZIP file to a Logic Apps Standard plan.

```bash
LOGIC_APP_NAME="$(az deployment group show --resource-group $RESOURCE_GROUP_NAME --name deploy --query properties.outputs.logicAppName.value -o tsv)"

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

## Azure Pipelines automation

Create a new Azure DevOps project and import the `azure-pipelines.yml` file to automate the build and deployment of the Logic App with BRE.

This pipeline is using the new YAML-based multi-stage pipelines feature in Azure DevOps.

It deploys infrastructure, builds the Logic App with BRE, and deploys the Logic App to a Logic Apps Standard plan.

You can change this to segregate the infrastructure deployment and Logic App deployment into separate pipelines.

![azdo-pipelines](img/azdo-pipelines.png)

## Editing, testing, and debugging locally

Assumes a Windows OS and Visual Studio Code (VSCode) with Azure Functions and Azure Logic Apps (Standard) extensions installed.  You'll also need the Azurite extension for Functions local storage.  If you received storage errors, you may need to start Azurite manually (CTRL+SHIFT+P, `Azurite: Start Blob Service`, repeat for `Azurite: Start Table Service`, etc.)

Start VSCode and then open the Workspace file:

* Navigate to the menu option **File** / **Open Workspace from File**
* Select the file `MyLogicAppRulesWorkspace.code-workspace`
* **or** start VSCode with `code MyLogicAppRulesWorkspace.code-workspace` from the terminal
* Open a Terminal window in VSCode:

```powershell
# Build the Function App as a custom code library for Logic Apps
cd MyLogicAppRulesWorkspace\Function
dotnet restore .\RulesFunction.csproj
dotnet build .\RulesFunction.csproj
```

* Select the **Run and Debug** icon on the left-hand side of the VSCode window (or `CTRL+SHIFT+D`)
* From the **Run and Debug** menu, select **Attach to Logic App (Logic App)**
* Click the **Start Debugging** button (or `F5`)
* From the **Run and Debug** menu, select **Attach to .NET Functions (Functions)**
* Click the **Start Debugging** button (or `F5`)
* Validate that both Logic App and Function App are running locally
* In the Workspace Explorer, navigate to the Logic App file `MyRulesWorkflow\workflow.json`
* Right-click on the file and select **Open Designer** -- it should render with no errors
* Right-click on the file and select **Overview** -- it should render with no errors
* Click the **Run trigger** button to test the Logic App -- it should execute and succeed
* Click the latest run to view the run details
* Click the action **Call a local rules function in this logic app** to view the action inputs and outputs
* Stop the Function and Logic App debugging sessions (`SHIFT+F5`)


## Resources

* [Logic Apps (Standard) – Azure DevOps sample](https://github.com/Azure/logicapps/tree/master/azure-devops-sample)
