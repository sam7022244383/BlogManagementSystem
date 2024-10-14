﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class BaseResponse
    {
        public BaseResponse() { Success = true; }
        public BaseResponse(string message) { Success = true; message = message; }
        public BaseResponse(string message , bool success) { message = message; success = success; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public List<string> ValidationErrors { get; set; }
    }
}