For this project I'm using Camunda 8 and gRPC client to connect and create an instance process. This a basic expense report process but it includes a service task to send a email notification, this service can executed outside camunda engine.

**Camunda BPM Model/Diagram**

You can import the diagram "Expense Reimbursement.bpmn" into camunda modeler and the deploy it, this step is required to obtaind the BpmProcessId.

***Development***

Before to execute the proyect to need to configure the appsettings.json file like this:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "CamundaSettings": {
    "ClientId": "camunda client id",
    "ClientSecret": "client secret",
    "ContactPoint": camunda cluster endpoint"
  },
  "BpmProccessSettings": {
    "BpmProcessId": "put here the bpm process id to be used to created the instance process"
  }
}
```
