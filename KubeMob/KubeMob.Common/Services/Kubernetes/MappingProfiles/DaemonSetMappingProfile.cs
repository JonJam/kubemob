using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using k8s.Models;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes.Extensions;
using KubeMob.Common.Services.Kubernetes.Model;
using Xamarin.Forms.Internals;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class DaemonSetMappingProfile : Profile
    {
        public const string PodsKey = "Pods";
        public const string EventsKey = "Events";

        public DaemonSetMappingProfile()
        {
            this.CreateMap<k8s.Models.V1DaemonSet, ObjectSummary>()
                .ConstructUsing((d, rc) =>
                {
                    var pods = (IList<V1Pod>)rc.Items[DaemonSetMappingProfile.PodsKey];
                    var events = (IList<V1Event>)rc.Items[DaemonSetMappingProfile.EventsKey];

                    var filteredPods = pods.FilterPods(d.Metadata.Name);
                    var filteredEvents = events.Where(e => e.InvolvedObject.Uid == d.Metadata.Uid);

                    int running = 0;
                    int pending = 0;
                    int failed = 0;
                    int succeeded = 0;
                    int warnings = 0;

                    filteredPods.ForEach(p =>
                    {
                        switch (p.Status.Phase)
                        {
                            case "Running":
                                running++;
                                break;
                            case "Pending":
                                pending++;
                                break;
                            case "Failed":
                                failed++;
                                break;
                            case "Succeeded":
                                succeeded++;
                                break;
                        }
                    });
                     //Warnings ??


                    // TODO work out status

                    // NEED TO lookup pods here to get status
                    // See card_component.js and card.html in daemonset > list
                    // See podinfo.go  in common
                    // See list.go in daemonset
                    return new ObjectSummary(
                        d.Metadata.Name,
                        d.Metadata.NamespaceProperty);
                });

            this.CreateMap<k8s.Models.V1DaemonSet, DaemonSetDetail>()
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
