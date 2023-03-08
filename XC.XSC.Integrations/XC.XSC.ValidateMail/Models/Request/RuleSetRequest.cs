namespace XC.XSC.ValidateMail.Models.Request
{
    /// <summary>
    /// Request for calling GetRuleSetList api
    /// </summary>
    public class RuleSetRequest
    {
        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the page no
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the page size
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Gets or sets the list of FilterConfig
        /// </summary>
        public List<FilterConfig> Filter { get; set; }

        /// <summary>
        /// Gets or sets the order by of rule set
        /// </summary>
        public string OrderBy { get; set; }
    }

    /// <summary>
    /// initialize the filterconfig
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Gets or sets the Columnname for field
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the operator for field
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Gets or sets the value for field
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the logicaloperator for fiels
        /// </summary>
        public string LogicalOperator { get; set; }
    }
}
