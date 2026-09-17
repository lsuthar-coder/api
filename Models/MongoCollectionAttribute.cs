namespace Trial.Server.Models
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class MongoCollectionAttribute : Attribute
    {
        public string SettingsPropertyName { get; }

        public MongoCollectionAttribute(string settingsPropertyName)
        {
            SettingsPropertyName = settingsPropertyName;
        }
    }
}