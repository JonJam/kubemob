using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using k8s.Models;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.Extensions
{
    public static class KubernetesExtensions
    {
        public const string PodsKey = "Pods";
        public const string EventsKey = "Events";

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

        // TODO Change OwnerReferences to use UUID not name
        public static IEnumerable<V1Pod> FilterPodsForOwner(this IEnumerable<V1Pod> pods, string ownerName)
            => pods.Where(p => p.Metadata.OwnerReferences.Any(o => o.Name == ownerName));

        public static Status GetStatus(
            this ResolutionContext rc,
            string uid,
            string name)
        {
            IList<V1Pod> pods = (IList<V1Pod>)rc.Items[KubernetesExtensions.PodsKey];
            IList<V1Event> events = (IList<V1Event>)rc.Items[KubernetesExtensions.EventsKey];

            IEnumerable<V1Pod> relatedPendingPods = pods.FilterPodsForOwner(name).FilterPendingPods();
            IEnumerable<V1Event> relatedWarningEvents =
                events.FilterEventsForInvolvedObject(uid).FilterWarningEvents();

            Status status = Status.Success;

            if (relatedWarningEvents.Any())
            {
                status = Status.Warning;
            }
            else if (relatedPendingPods.Any())
            {
                status = Status.Pending;
            }

            return status;
        }

        private static IEnumerable<V1Pod> FilterPendingPods(this IEnumerable<V1Pod> pods)
            => pods.Where(p => p.Status.Phase == "Pending");

        private static IEnumerable<V1Event> FilterEventsForInvolvedObject(this IEnumerable<V1Event> events, string uid)
            => events.Where(e => e.InvolvedObject.Uid == uid);

        private static IEnumerable<V1Event> FilterWarningEvents(this IEnumerable<V1Event> events)
            => events.Where(e => e.Type == "Warning");
    }
}
