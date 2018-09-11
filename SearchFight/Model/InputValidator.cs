using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchFight.Model
{
    public class InputValidator : IValidator
    {
        private string _pattern;

        public string Pattern
        {
            get
            {
                return _pattern;
            }

            set
            {
                if (value == _pattern || string.IsNullOrEmpty(value)) return;
                _pattern = value;
            }
        }

        public bool Validate(string input)
        {
            Regex rgx = new Regex(_pattern);

            if (rgx.IsMatch(input)) return true;

            return false;
        }
    }
}
