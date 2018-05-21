﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vrektproject.Models.ManageViewModels
{
    public class UploadPictureViewModel
    {
        public UploadPictureViewModel()
        {
            AvatarImage = new List<IFormFile>();
        }

        public List<IFormFile> AvatarImage { get; set; }
    }
}
