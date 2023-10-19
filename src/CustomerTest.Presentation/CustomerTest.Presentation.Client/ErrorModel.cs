﻿using System.Collections.Generic;

namespace CustomerTest.Presentation.Client
{
    public class ErrorModel
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int Status { get; set; }
        public string? TraceId { get; set; }
        public Dictionary<string, string>? Errors { get; set; }
    }

}
