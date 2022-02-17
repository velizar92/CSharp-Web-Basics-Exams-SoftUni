using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string _errorMessage)
        {
            this.ErrorMessage = _errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}
