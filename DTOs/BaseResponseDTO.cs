using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DTOs
{
    public class BaseResponseDTO
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
    }
}
