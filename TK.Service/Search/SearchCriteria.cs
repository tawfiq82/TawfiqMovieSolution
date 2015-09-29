namespace TK.Service.Search
{
    public enum OperatorType
    {
        Equals,
        NotEquals,
        Contains,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual
    }

    public class SearchCriteria
    {
        public string Name { get; set; }
        public OperatorType Operator { get; set; }
        public string Value { get; set; }
    }
}
