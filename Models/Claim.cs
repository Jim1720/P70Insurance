
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A70Insurance.Models
{
    public partial class ClaimsHistory
    {
        public List<Claim> HistoryClaims;

        public ClaimsHistory()
        {
            HistoryClaims = new List<Claim>();
        }

        public List<Claim> GetList()
        {
            return HistoryClaims;
        }

        public Claim GetClaim(int Index)
        {
            return HistoryClaims[Index];
        }
    } 

    public partial class Claim
    {


        public int Id { get; set; }
        public string ClaimIdNumber { get; set; }

        [RegularExpression("^\\s|[a-zA-Z0-9#.\\s]*$", ErrorMessage = "Notes must contains letters or numbers . #  if entered")]

        public string ClaimDescription { get; set; }
        public string CustomerId { get; set; }
        public string PlanId { get; set; }

        [Required,
       RegularExpression("^[a-zA-Z0-9.\\s]*$", ErrorMessage = " First Name must only contains letters or numbers and is required.")]

        public string PatientFirst { get; set; }

        [Required,
       RegularExpression("^[a-zA-Z0-9.\\s]*$", ErrorMessage = " Last Name must only contains letters or numbers and is required.")]

        public string PatientLast { get; set; }
        public string Diagnosis1 { get; set; }
        public string Diagnosis2 { get; set; }
        public string Procedure1 { get; set; }
        public string Procedure2 { get; set; }
        public string Procedure3 { get; set; }
        public string Physician { get; set; }
        public string Clinic { get; set; }
        public DateTime? DateService { get; set; }
        public string Service { get; set; } 

        public string ServiceItem { get; set; } 

        public string PaymentPlan { get; set; }
        public string Location { get; set; }

        public decimal TotalCharge { get; set; }
        public decimal CoveredAmount { get; set; }
        public decimal BalanceOwed { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? DateAdded { get; set; }
        public string AdjustedClaimId { get; set; }
        public string AdjustingClaimId { get; set; }
        public DateTime? AdjustedDate { get; set; }
        public string AppAdjusting { get; set; }
        public string ClaimStatus { get; set; }

        [RegularExpression("^\\s|[a-zA-Z0-9#.\\s]*$", ErrorMessage = "Referral must contains letters or numbers . #  if entered")]

        public string Referral { get; set; }
        public string PaymentAction { get; set; }
        public string ClaimType { get; set; }
        public DateTime? DateConfine { get; set; }
        public DateTime? DateRelease { get; set; }
 
        public int ToothNumber { get; set; }
     
        public string DrugName { get; set; }
      
        public string Eyeware { get; set; }

        // edited fields - not updated

        [NotMapped]
        public string ScreenDateService { get; set; }
        [NotMapped]
        public string ScreenDateConfine { get; set; }
        [NotMapped]
        public string ScreenDateRelease { get; set; }
         

       




    }
}
