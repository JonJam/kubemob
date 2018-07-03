using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using k8s.Models;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes.Extensions;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class DeploymentMappingProfile : Profile
    {
        public DeploymentMappingProfile()
        {
            this.CreateMap<V1Deployment, ObjectSummary>()
                   .ConstructUsing((d, rc) =>
                {
                    Status status = rc.GetStatus(d.Metadata.Uid, d.Metadata.Name);

                    return new ObjectSummary(
                        d.Metadata.Name,
                        d.Metadata.NamespaceProperty,
                        status);
                });

            this.CreateMap<V1Deployment, DeploymentDetail>()
                .ConstructUsing((d) =>
                {
                    List<string> labels = d.Metadata.Labels.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList();
                    List<string> annotations = d.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                               new List<string>();

                    List<string> selectors = d.Spec.Selector.MatchLabels.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList();

                    string creationTime = d.Metadata.CreationTimestamp.HasValue
                        ? $"{d.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    int minReadySeconds = d.Spec.MinReadySeconds.GetValueOrDefault(0);
                    string revisionHistoryLimit = d.Spec.RevisionHistoryLimit?.ToString() ?? AppResources.ObjectDetail_NotSet;
                    string rollingUpdateStrategy = string.Format(
                        AppResources.DeploymentDetail_RollingUpdateStrategy,
                        d.Spec.Strategy.RollingUpdate.MaxSurge.Value,
                        d.Spec.Strategy.RollingUpdate.MaxUnavailable.Value);
                    string status = string.Format(
                        AppResources.DeploymentDetail_Status,
                        d.Status.UpdatedReplicas.GetValueOrDefault(0),
                        d.Status.Replicas.GetValueOrDefault(0),
                        d.Status.AvailableReplicas.GetValueOrDefault(0),
                        d.Status.UnavailableReplicas.GetValueOrDefault(0));

                    return new DeploymentDetail(
                        d.Metadata.Name,
                        d.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        selectors.AsReadOnly(),
                        d.Spec.Strategy.Type,
                        minReadySeconds,
                        revisionHistoryLimit,
                        rollingUpdateStrategy,
                        status);
                });
        }
    }
}
