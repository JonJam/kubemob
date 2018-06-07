using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KubeMob.Common.Resx;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class ConfigMapMappingProfile : Profile
    {
        public ConfigMapMappingProfile()
        {
            this.CreateMap<k8s.Models.V1ConfigMap, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1ConfigMap, ConfigMapDetail>()
                .ConstructUsing((s) =>
                {
                    //List<string> labels = s.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                    //                      new List<string>();
                    //List<string> annotations = s.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                    //                           new List<string>();

                    //string creationTime = s.Metadata.CreationTimestamp.HasValue
                    //    ? $"{s.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                    //    : string.Empty;

                    //List<string> images = s.Spec.Template.Spec.Containers.Select(c => c.Image).ToList();

                    //// TODO When look up pods, base status of that (see web-0 in kube-system) as this is currently incorrect.??
                    //// TODO Based off previous, change other objects which use pod status in detail.
                    //string pods = string.Format(
                    //    AppResources.Detail_Pods,
                    //    s.Status.CurrentReplicas.GetValueOrDefault(0));

                    return new ConfigMapDetail();
                    //(
                    //    s.Metadata.Name,
                    //    s.Metadata.NamespaceProperty,
                    //    labels.AsReadOnly(),
                    //    annotations.AsReadOnly(),
                    //    creationTime,
                    //    images.AsReadOnly(),
                    //    pods);
                });
        }
    }
}
