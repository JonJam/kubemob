using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using k8s.Models;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes.Extensions;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class JobMappingProfile : Profile
    {
        public JobMappingProfile()
        {
            this.CreateMap<V1Job, ObjectSummary>()
                .ConstructUsing((j, rc) =>
                {
                    Status status = rc.GetStatus(j.Metadata.Uid, j.Metadata.Name);

                    return new ObjectSummary(
                        j.Metadata.Name,
                        j.Metadata.NamespaceProperty,
                        status);
                });

            this.CreateMap<V1Job, JobDetail>()
                .ConstructUsing((j) =>
                {
                    List<string> labels = j.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<string> annotations = j.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                               new List<string>();

                    string creationTime = j.Metadata.CreationTimestamp.HasValue
                        ? $"{j.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<string> images = j.Spec.Template.Spec.Containers.Select(c => c.Image).ToList();

                    int parallelism = j.Spec.Parallelism.Value;
                    int completions = j.Status.Succeeded.GetValueOrDefault(0);

                    string status = string.Format(
                        AppResources.JobDetail_Status,
                        j.Status.Active.GetValueOrDefault(0),
                        j.Status.Succeeded.GetValueOrDefault(0),
                        j.Status.Failed.GetValueOrDefault(0));

                    return new JobDetail(
                        j.Metadata.Name,
                        j.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        images.AsReadOnly(),
                        parallelism,
                        completions,
                        status);
                });
        }
    }
}
