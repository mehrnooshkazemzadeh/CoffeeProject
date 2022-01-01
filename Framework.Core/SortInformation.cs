using System.Linq.Expressions;

namespace Framework.Core
{
    public class SortInformation
    {
        public string PropertyName { get; set; }
        public SortDirection SortDirection { get; set; }
        public int OrderNumber { get; set; }
        public Expression CustomExpression { get; set; }
    }
}
