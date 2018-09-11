using System.Text.RegularExpressions;
using SearchFight.Model.Interfaces;

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
            var rgx = new Regex(_pattern);

            return rgx.IsMatch(input);
        }
    }
}
