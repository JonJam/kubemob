using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            this.CreateMap<k8s.Models.V1OwnerReference, ObjectReference>()
                .ConstructUsing((o) => new ObjectReference(
                    o.Name,
                    o.Kind));

            this.CreateMap<k8s.Models.V1ObjectReference, ObjectReference>()
                .ConstructUsing((o) => new ObjectReference(
                    o.Name,
                    o.NamespaceProperty,
                    o.Kind));

            this.CreateMap<k8s.Models.V1CrossVersionObjectReference, ObjectReference>()
                .ConstructUsing((o) => new ObjectReference(
                    o.Name,
                    o.Kind));

            this.CreateMap<k8s.Models.V1Event, Event>()
                .ConstructUsing((e) =>
                {
                    string firstSeen = $"{e.FirstTimestamp.Value.ToUniversalTime():s} UTC";
                    string lastSeen = $"{e.LastTimestamp.Value.ToUniversalTime():s} UTC";

                    return new Event(
                        e.Message,
                        e.Source.Component,
                        e.InvolvedObject.FieldPath,
                        e.Count.GetValueOrDefault(0),
                        firstSeen,
                        e.LastTimestamp.Value,
                        lastSeen);
                });

            // Create selector for Replica Set and Daemon Set to related Services.
            // Cannot find exact logic to match this to related Services, this is best effort.
            this.CreateMap<k8s.Models.V1LabelSelector, string>()
                .ConstructUsing((l) =>
                {
                    const string kubernetesAppLabelKey = "k8s-app";

                    // Ignoring pod-template-hash as auto-generated: https://kubernetes.io/docs/concepts/workloads/controllers/deployment/#pod-template-hash-label
                    IList<KeyValuePair<string, string>> filteredSelectors = l.MatchLabels.Where(ml => ml.Key != "pod-template-hash").ToList();

                    if (filteredSelectors.Any(kvp => kvp.Key == kubernetesAppLabelKey))
                    {
                        // For a Kubernetes app e.g. Dashboard, DNS, selectors seem to only use this label so remove all other selectors.
                        filteredSelectors = filteredSelectors.Where(kvp => kvp.Key == kubernetesAppLabelKey).ToList();
                    }

                    return string.Join(",", filteredSelectors.Select(kvp => $"{kvp.Key}={kvp.Value}"));
                });
        }
    }
}
