using CamundaSampleWorkFlowApp.Models;
using CamundaSampleWorkFlowApp.Models.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace CamundaSampleWorkFlowApp.Controllers
{
    public class CamundaController : Controller
    {
        private readonly IZeebeClientService _zeebeClientService;
        private readonly ILogger<CamundaController> _logger;
        private readonly BpmProcessSettings _bpmProcessSettings;

        public CamundaController(IZeebeClientService zeebeClientService, ILogger<CamundaController> logger, BpmProcessSettings bpmProcessSettings)
        {
            _zeebeClientService = zeebeClientService;
            _logger = logger;
            _bpmProcessSettings = bpmProcessSettings;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> StartInstance (string bpmnProcessId)
        {
            var _processInstanceKey = await _zeebeClientService.StartProcessInstance(bpmnProcessId);

            TempData["_processInstanceKey"] = _processInstanceKey.ToString();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SetInstanceVariables(long _processInstanceKey)
        {
            var instanceVariables = new Dictionary<string, string>
            {
                { "firstName", "Gaudencio" },
                { "lastName", "Jurado" },
                { "email", "john.doe@camunda.com" },
                { "department", "finance" },
                { "supervisorEmail", "jane.doe@camunda.com" },
                { "expenseDate", "2023-06-09" },
                { "expenseType", "travel" },
                { "travelStart", "2023-06-06" },
                { "travelEnd", "2023-06-09" },
                { "expenseAmount", "500" }
            };

            await _zeebeClientService.SetVariables(instanceVariables, _processInstanceKey);
            TempData.Keep();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SendMessage()
        {
            var instanceVariables = new Dictionary<string, string>
            {
                { "managerReviewOutcome", "approved" }
            };
            await _zeebeClientService
                .PublishMessage("Message_0b3847a", "Gaudencio", instanceVariables);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CaptureExpenses()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {

                var _processInstanceKey = await _zeebeClientService.StartProcessInstance(_bpmProcessSettings.BpmProcessId);

                await _zeebeClientService.SetVariables(expense, _processInstanceKey);

                return RedirectToAction("Success"); // Redirect to a success action/view
            }

            // If we got this far, something failed; redisplay form
            return View("CaptureExpenses");
        }

        public IActionResult Success()
        {
            // Return a view that informs the user of successful submission
            return View();
        }
    }
}
