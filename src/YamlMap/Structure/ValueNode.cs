namespace YamlMap
{
    /// <summary>
    /// 
    /// </summary>
    public class ValueNode : IYamlNode
    {
        private readonly string _value;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public ValueNode(string value)
        {
            _value = value;
        }
        
        /// <summary>
        /// Gets the value of the Node
        /// </summary>
        public object Value => _value;

        /// <summary>
        /// ValueNodes don't have children, so this always returns null
        /// </summary>
        /// <param name="key"></param>
        public IYamlNode this[string key] => null;
        
        /// <summary>
        /// ValueNodes don't have children, so this always returns null
        /// </summary>
        /// <param name="index"></param>
        public IYamlNode this[int index] => null;

        /// <summary>
        /// ValueNodes don't have children, so this does nothing
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string key, IYamlNode value)
        {
            // no children allowed in a ValueNode
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static implicit operator string(ValueNode c) => c.Value?.ToString();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _value;
        }
    }
}