using Zeebe.Client.Api.Responses;

namespace CamundaSampleWorkFlowApp.Models.Core
{
    public interface IZeebeClientService
    {
        Task<long> StartProcessInstance(string bpmnProcessId);
        Task PublishMessage(string messageName, string correlationKey, IDictionary<string, string> variables);
        Task<ITopology> GetTopology();
        Task SetVariables(IDictionary<string, string> variables, long processDefinitionKey);
        Task SetVariables(Expense expense, long processDefinitionKey);
    }
}
