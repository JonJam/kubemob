using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.Model
{
    [Preserve(AllMembers = true)]
    public class Container
    {
        public Container(
            string name,
            string image,
            IReadOnlyList<EnvironmentVariable> environmentVariables,
            IReadOnlyList<string> commands,
            IReadOnlyList<string> arguments)
        {
            this.Name = name;
            this.Image = image;
            this.EnvironmentVariables = environmentVariables;
            this.Commands = commands;
            this.Arguments = arguments;
        }

        public string Name
        {
            get;
        }

        public string Image
        {
            get;
        }

        // TODO Handle links
        public IReadOnlyList<EnvironmentVariable> EnvironmentVariables
        {
            get;
        }

        public IReadOnlyList<string> Commands
        {
            get;
        }

        public IReadOnlyList<string> Arguments
        {
            get;
        }
    }
}
