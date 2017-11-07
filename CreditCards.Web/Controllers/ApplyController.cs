using CreditCards.Core.Interface;
using CreditCards.Core.Model;
using CreditCards.Web.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CreditCards.Web.Controllers
{
    /// <summary>
    /// Getting Started with the Data Protection APIs
    /// https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/using-data-protection
    /// </summary>
    public class ApplyController : Controller
    {
        private readonly ICreditCardApplicationRepository _applicationRepository;
        private readonly IDataProtector _dataProtector;

        public ApplyController(
            ICreditCardApplicationRepository applicationRepository,
            IDataProtectionProvider dataProtectionProvider)
        {
            _applicationRepository = applicationRepository;
            _dataProtector = dataProtectionProvider.CreateProtector("CreditCards.v1");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(NewCreditCardApplicationDetails applicationDetails)
        {
            if (ModelState.IsValid == false)
            {
                return View(applicationDetails);
            }

            var creditCardApplication = new CreditCardApplication
            {
                FirstName = applicationDetails.FirstName,
                LastName = applicationDetails.LastName,
                Age = applicationDetails.Age.Value,
                GrossAnnualIncome = applicationDetails.GrossAnnualIncome.Value,
            };

            await _applicationRepository.AddAsync(creditCardApplication);

            var applicationId = _dataProtector.Protect(creditCardApplication.Id.ToString());
            return RedirectToAction(nameof(ApplicationComplete), new { id = applicationId });
        }

        public async Task<IActionResult> ApplicationComplete(string id)
        {
            var deCryptedApplicationId = int.Parse(_dataProtector.Unprotect(id));
            var model = await _applicationRepository.FindAsync(deCryptedApplicationId);
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
