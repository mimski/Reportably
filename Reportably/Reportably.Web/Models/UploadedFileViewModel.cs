﻿using System;

namespace Reportably.Web.Models
{
    public class UploadedFileViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public long Lenght { get; set; }

        public byte[] FileContent { get; set; }

        public string ContentType { get; set; }

        public Guid ReportId { get; set; }
    }
}
