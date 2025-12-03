using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class OperationResult
    {
        // Code: 0 = success, non-zero = business/db error code
        public int Code { get; set; }
        public string Message { get; set; }
        // Useful payload e.g. newly created num_cita
        public object Data { get; set; }
    }
}
