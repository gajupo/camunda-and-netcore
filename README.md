For this project I'm using Camunda 8 and gRPC client to connect and create an instance process. This basic expense report process, created and instance and the send typed expense information to camunda and pre-load this data into a user review task.

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
