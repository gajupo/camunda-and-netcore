using CamundaSampleWorkFlowApp.Models;
using Zeebe.Client.Impl.Builder;
using Zeebe.Client;
using CamundaSampleWorkFlowApp.Models.Core;
using Zeebe.Client.Api.Responses;
using System.Text.Json;

namespace CamundaSampleWorkFlowApp.Services
{
    public class ZeebeClientService : IZeebeClientService
    {
        private readonly IZeebeClient _zeebeClient;

        public ZeebeClientService(CamundaSettings camundaSettings)
        {
            // Ideally, use configuration settings instead of hardcoding credentials and URLs
            _zeebeClient = CamundaCloudClientBuilder
                .Builder()
                .UseClientId(camundaSettings.ClientId)
                .UseClientSecret(camundaSettings.ClientSecret)
                .UseContactPoint(camundaSettings.ContactPoint)
                .Build();
        }

        public async Task<long> StartProcessInstance(string bpmnProcessId)
        {
            var response = await _zeebeClient.NewCreateProcessInstanceCommand()
                .BpmnProcessId(bpmnProcessId)
                .LatestVersion()
                .Send();
            return response.ProcessInstanceKey;
        }

        public async Task PublishMessage(string messageName, string correlationKey, IDictionary<string, string> variables)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(variables, options);

            await _zeebeClient.NewPublishMessageCommand()
                .MessageName(messageName)
                .CorrelationKey(correlationKey)
                .Variables(jsonString)
                .Send();
        }

        public async Task<ITopology> GetTopology()
        {
           var topology = await _zeebeClient.TopologyRequest().Send();

            return topology;
        }

        public async Task SetVariables(IDictionary<string, string> variables, long processDefinitionKey)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(variables, options);
            await _zeebeClient.NewSetVariablesCommand(processDefinitionKey)
                .Variables(jsonString)
                .Send();
        }

        public async Task SetVariables(Expense expense, long processDefinitionKey)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(expense, options);
            await _zeebeClient.NewSetVariablesCommand(processDefinitionKey)
                .Variables(jsonString)
                .Send();
        }
    }
}
