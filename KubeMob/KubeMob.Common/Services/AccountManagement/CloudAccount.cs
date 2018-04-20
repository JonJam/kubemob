using System;
using System.Collections.Generic;

namespace KubeMob.Common.Services.AccountManagement
{
    public class CloudAccount
    {
        public CloudAccount(
            string id,
            IDictionary<string, string> properties)
        {
            this.Id = id;
            this.Properties = properties;
        }

        public CloudAccount(
            string id,
            string name,
            CloudAccountType accountType)
            : this(id, new Dictionary<string, string>())
        {
            this.Name = name;
            this.AccountType = accountType;
        }

        public string Id { get; }

        public IDictionary<string, string> Properties { get; }

        public string Name
        {
            get => this.Properties[nameof(this.Name)];
            set => this.Properties[nameof(this.Name)] = value;
        }

        public CloudAccountType AccountType
        {
            get => (CloudAccountType)Enum.Parse(
                typeof(CloudAccountType),
                this.Properties[nameof(this.AccountType)]);
            set => this.Properties[nameof(this.AccountType)] = value.ToString();
        }
    }
}
