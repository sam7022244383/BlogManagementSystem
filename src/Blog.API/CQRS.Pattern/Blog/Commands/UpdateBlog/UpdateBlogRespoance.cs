﻿using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Blog.Commands.UpdateBlog
{
    public class UpdateBlogRespoance : BaseResponse
    {
        public int Id { get; set; }
    }
}
