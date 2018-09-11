/// <summary>
/// Interface for a generic Query this gives the
/// facility to have different types of queries
/// with different ways to set the content and get its text form
/// </summary>
namespace SearchFight.Model.Interfaces
{
    public interface IQuery
    {
        string QueryText { get; }
        void SetContent(string member);
    }
}
