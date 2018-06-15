namespace KubeMob.Common.Services.Navigation
{
    public class ObjectId
    {
        public ObjectId(
            string name)
            : this(name, string.Empty)
        {
        }

        public ObjectId(
            string name,
            string namespaceName)
        {
            this.Name = name;
            this.NamespaceName = namespaceName;
        }

        public string Name
        {
            get;
        }

        public string NamespaceName
        {
            get;
        }
    }
}
