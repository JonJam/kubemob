using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using k8s.Models;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes.Extensions;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class DaemonSetMappingProfile : Profile
    {
        public const string PodsKey = "Pods";
        public const string EventsKey = "Events";

        public DaemonSetMappingProfile()
        {
            this.CreateMap<V1DaemonSet, ObjectSummary>()
                .ConstructUsing((d, rc) =>
                {
                    IList<V1Pod> pods = (IList<V1Pod>)rc.Items[DaemonSetMappingProfile.PodsKey];
                    IList<V1Event> events = (IList<V1Event>)rc.Items[DaemonSetMappingProfile.EventsKey];

                    IEnumerable<V1Pod> relatedPendingPods = pods.FilterPodsForOwner(d.Metadata.Name).FilterPendingPods();
                    IEnumerable<V1Event> relatedWarningEvents = events.FilterEventsForInvolvedObject(d.Metadata.Uid).FilterWarningEvents();

                    Status status = Status.Success;

                    if (relatedWarningEvents.Any())
                    {
                        status = Status.Warning;
                    }
                    else if (relatedPendingPods.Any())
                    {
                        status = Status.Pending;
                    }

                    return new ObjectSummary(
                        d.Metadata.Name,
                        d.Metadata.NamespaceProperty,
                        status);
                });

            this.CreateMap<V1DaemonSet, DaemonSetDetail>()
                .ConstructUsing((d) =>
                {
                    List<string> labels = d.Metadata.Labels.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList();
                    List<string> annotations = d.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                               new List<string>();

                    string creationTime = d.Metadata.CreationTimestamp.HasValue
                        ? $"{d.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<string> images = d.Spec.Template.Spec.Containers.Select(c => c.Image).ToList();

                    string pods = string.Format(
                        AppResources.Detail_Pods,
                        d.Status.CurrentNumberScheduled);

                    string relatedSelector = d.Spec.Selector.ToRelatedSelector();

                    return new DaemonSetDetail(
                        d.Metadata.Name,
                        d.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        images.AsReadOnly(),
                        pods,
                        relatedSelector);
                });
        }
    }
}
