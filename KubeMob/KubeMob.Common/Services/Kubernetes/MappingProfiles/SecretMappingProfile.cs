using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class SecretMappingProfile : Profile
    {
        public SecretMappingProfile()
        {
            this.CreateMap<k8s.Models.V1Secret, ObjectSummary>()
                .ConstructUsing((r) => new ObjectSummary(
                    r.Metadata.Name,
                    r.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1Secret, SecretDetail>()
                .ConstructUsing((s) =>
                {
                    List<string> labels = s.Metadata.Labels?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                          new List<string>();
                    List<string> annotations = s.Metadata.Annotations?.Select(kvp => $"{kvp.Key}: {kvp.Value}").ToList() ??
                                               new List<string>();

                    string creationTime = s.Metadata.CreationTimestamp.HasValue
                        ? $"{s.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<SecretData> data = s.Data.Select(pair => new SecretData(pair.Key, Encoding.UTF8.GetString(pair.Value))).ToList();

                    return new SecretDetail(
                        s.Metadata.Name,
                        s.Metadata.NamespaceProperty,
                        labels.AsReadOnly(),
                        annotations.AsReadOnly(),
                        creationTime,
                        s.Type,
                        data.AsReadOnly());
                });
        }
    }
}
