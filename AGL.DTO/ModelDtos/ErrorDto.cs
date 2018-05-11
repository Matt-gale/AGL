using System;
using System.Collections.Generic;
using System.Text;

namespace AGL.Dto.ModelDtos
{
    public class ErrorDto
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
