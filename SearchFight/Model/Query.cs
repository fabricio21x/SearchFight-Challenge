using System.Collections.Generic;
using System.Linq;
using SearchFight.Model.Interfaces;

namespace SearchFight.Model
{
    public class Query : IQuery
    {
        public Query()
        {
            Content = new List<string>();
        }

        private List<string> Content { get; }

        public string QueryText
        {
            get
            {
                return Content.Aggregate("", (current, item) => current +" "+ item);
            }
        }

        public void SetContent(string member)
        {
            Content.Add(member);
        }
    }
}
