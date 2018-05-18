namespace KubeMob.Common.Services.AccountManagement.Model
{
    public class CloudAccount
    {
        public CloudAccount(
            string id,
            string name,
            CloudAccountType accountType)
        {
            this.Id = id;
            this.Name = name;
            this.AccountType = accountType;
        }

        public string Id { get; }
        
        public string Name
        {
            get;
        }

        public CloudAccountType AccountType
        {
            get;
        }
    }
}
