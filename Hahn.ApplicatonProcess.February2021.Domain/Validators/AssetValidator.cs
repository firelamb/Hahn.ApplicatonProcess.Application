using System;
using FluentValidation;
using Hahn.ApplicatonProcess.February2021.Data.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Models;

namespace Hahn.ApplicatonProcess.February2021.Domain.Validators
{
    public class AssetValidator : AbstractValidator<AssetModel>
    {
        private readonly IHttpDataAccessService _httpDataAccessService;

        public AssetValidator(IHttpDataAccessService httpDataAccessService)
        {
            _httpDataAccessService = httpDataAccessService;

            //Checking Required
            RuleFor(x => x.AssetName).NotEmpty().WithMessage("Asset name is required");
            RuleFor(x => x.Department).NotEmpty().WithMessage("Department is required");
            RuleFor(x => x.CountryOfDepartment).NotEmpty().WithMessage("Country of department is required");
            RuleFor(x => x.EMailAdressOfDepartment).NotEmpty().WithMessage("EMail adress of department is required");
            RuleFor(x => x.PurchaseDate).NotEmpty().WithMessage("Purchase date is required");

            // Checking Constraints 
            RuleFor(x => x.AssetName).MinimumLength(5).WithMessage("Asset name – at least 5 characters");
            RuleFor(x => x.PurchaseDate).Must(ValidatePurchaseDate)
                .WithMessage("Purchase date – must not be older then one yea");
            RuleFor(x => x.CountryOfDepartment).Must(ValidateCountry)
                .WithMessage("Country of department – must be a valid Country");
            RuleFor(x => x.EMailAdressOfDepartment).EmailAddress().WithMessage(
                "EMail adress of department – must be an valid email (only check for valid syntax *@*.[valid topleveldomain])");
        }

        private bool ValidateCountry(string countryName)
        {
            return _httpDataAccessService
                .ValidateCountry(countryName)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        private bool ValidatePurchaseDate(DateTime purchaseDate)
        {
            return purchaseDate >= DateTime.Now.AddYears(-1);
        }
    }
}