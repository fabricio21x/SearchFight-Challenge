using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Interface for a generic Query this gives the
/// facility to have different types of queries
/// with different ways to set the content and get its text form
/// </summary>
namespace SearchFight.Model
{
    public interface IQuery
    {
        string QueryText { get; }
        void SetContent(string member);
    }
}
