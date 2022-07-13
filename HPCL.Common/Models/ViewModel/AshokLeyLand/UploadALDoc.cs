using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCL.Common.Models.ViewModel.AshokLeyLand
{
    public class UploadALDoc : BaseEntity
    {
        public string CustomerID { get; set; }
        public string IdProofFront { get; set; }

        [Required(ErrorMessage = "Valid File is required")]
        public IFormFile IDProof { get; set; }

        public string AddressProofFront { get; set; }

        [Required(ErrorMessage = "Valid File is required")]
        public IFormFile AddressProof { get; set; }

        public string PanCardFront { get; set; }

        [Required(ErrorMessage = "Valid File is required")]
        public IFormFile PanCardProof { get; set; }

        public string VehicleDetailsFront { get; set; }

        [Required(ErrorMessage = "Valid File is required")]
        public IFormFile VehicleDetail { get; set; }

        public string CustomerFormProofFront { get; set; }

        [Required(ErrorMessage = "Valid File is required")]
        public IFormFile SignedCustomerForm { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }

    }
}