using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Model
{
    public class Query : IQuery
    {
        List<string> _content;

        public Query()
        {
            _content = new List<string>();
        }

        public List<string> Content
        {
            get { return _content; }
        }

        public string QueryText
        {
            get
            {
                string text = "";
                foreach (var item in _content)
                {
                    text+=item;
                }
                return text;
            }            
        }

        public void SetContent(string member)
        {
            _content.Add(member);
        }
    }
}
