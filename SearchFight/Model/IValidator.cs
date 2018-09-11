using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Model
{
    interface IValidator
    {
        string Pattern { get; set; }
        bool Validate(string input);
    }
}
