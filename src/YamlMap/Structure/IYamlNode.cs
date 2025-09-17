namespace YamlMap
{
    /// <summary>
    /// a node element of the yaml object
    /// </summary>
    public interface IYamlNode
    {
        /// <summary>
        /// Gets the YamlNode associated with the key
        /// </summary>
        /// <param name="key"></param>
        IYamlNode this[string key] { get; }

        /// <summary>
        /// Gets the YamlNode at the indicated index
        /// </summary>
        /// <param name="index"></param>
        IYamlNode this[int index] { get; }

        /// <summary>
        /// Gets the value of the Node
        /// </summary>
        object Value { get; }

        /// <summary>
        /// Set the child value of the node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetValue(string key, IYamlNode value);
    }
}