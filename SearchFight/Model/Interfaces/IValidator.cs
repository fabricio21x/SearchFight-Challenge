namespace SearchFight.Model.Interfaces
{
    interface IValidator
    {
        string Pattern { get; set; }
        bool Validate(string input);
    }
}
