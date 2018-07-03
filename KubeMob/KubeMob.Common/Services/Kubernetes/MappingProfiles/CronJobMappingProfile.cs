using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using k8s.Models;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class CronJobMappingProfile : Profile
    {
        public CronJobMappingProfile()
        {
            this.CreateMap<V1beta1CronJob, ObjectSummary>()
                .ConstructUsing((c) => new ObjectSummary(
                    c.Metadata.Name,
                    c.Metadata.NamespaceProperty,
                    c.Spec.Suspend.GetValueOrDefault(false) ? Status.Error : Status.Success));

            this.CreateMap<V1beta1CronJob, CronJobDetail>()
                .ConstructUsing((c) =>
                {
                    List<string> labels = c.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<string> annotations = c.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                               new List<string>();

                    string creationTime = c.Metadata.CreationTimestamp.HasValue
                        ? $"{c.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    IList<V1ObjectReference> activeJobs = c.Status.Active ?? new List<V1ObjectReference>();

                    bool suspend = c.Spec.Suspend.GetValueOrDefault(false);
                    string lastSchedule = c.Status.LastScheduleTime.HasValue
                        ? $"{c.Status.LastScheduleTime.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    string concurrencyPolicy = c.Spec.ConcurrencyPolicy;
                    string startingDeadlineSeconds = c.Spec.StartingDeadlineSeconds?.ToString() ?? "-";

                    return new CronJobDetail(
                        c.Metadata.Name,
                        c.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        c.Spec.Schedule,
                        suspend,
                        lastSchedule,
                        concurrencyPolicy,
                        startingDeadlineSeconds,
                        activeJobs.Count);
                });
        }
    }
}
