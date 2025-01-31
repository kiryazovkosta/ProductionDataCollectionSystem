﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Resources = App_GlobalResources.Resources;

namespace CollectingProductionDataSystem.Web.Areas.Administration.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessageResourceName = "Password", ErrorMessageResourceType = typeof(Resources.ErrorMessages), MinimumLength = 6)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Layout))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessageResourceName = "Password", ErrorMessageResourceType = typeof(Resources.ErrorMessages), MinimumLength = 6)]
        [Display(Name = "NewPassword", ResourceType = typeof(Resources.Layout))]
        public string NewPassword { get; set; }


        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Layout))]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessageResourceName = "ConfirmPassword", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        public string ConfirmPassword { get; set; }

        public bool UserMustChangePassword { get; set; }
    }
}






