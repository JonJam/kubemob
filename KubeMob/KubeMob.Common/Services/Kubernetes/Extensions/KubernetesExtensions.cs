using System.Collections.Generic;
using System.Linq;
using k8s.Models;

namespace KubeMob.Common.Services.Kubernetes.Extensions
{
    public static class KubernetesExtensions
    {
        public static string ToRelatedSelector(this V1LabelSelector l) => l.MatchLabels.ToRelatedSelector();

        public static string ToRelatedSelector(this IDictionary<string, string> selectors)
        {
            // Create selector for Daemon Set, Replica Set and Replication Controllers to related Services.
            // Cannot find exact logic to match this to related Services, this is best effort.
            const string kubernetesAppLabelKey = "k8s-app";

            // Ignoring pod-template-hash as auto-generated: https://kubernetes.io/docs/concepts/workloads/controllers/deployment/#pod-template-hash-label
            IList<KeyValuePair<string, string>> filteredSelectors =
                selectors.Where(ml => ml.Key != "pod-template-hash").ToList();

            if (filteredSelectors.Any(kvp => kvp.Key == kubernetesAppLabelKey))
            {
                // For a Kubernetes app e.g. Dashboard, DNS, selectors seem to only use this label so remove all other selectors.
                filteredSelectors = filteredSelectors.Where(kvp => kvp.Key == kubernetesAppLabelKey).ToList();
            }

            return string.Join(",", filteredSelectors.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }
    }
}
