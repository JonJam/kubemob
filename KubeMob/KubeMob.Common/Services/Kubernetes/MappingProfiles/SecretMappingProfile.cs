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
                .ConstructUsing((s) => new ObjectSummary(
                    s.Metadata.Name,
                    s.Metadata.NamespaceProperty));

            this.CreateMap<k8s.Models.V1Secret, SecretDetail>()
                .ConstructUsing((s) =>
                {
                    List<MetadataItem> labels = s.Metadata.Labels?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                new List<MetadataItem>();
                    List<MetadataItem> annotations = s.Metadata.Annotations?.Select(kvp => new MetadataItem(kvp.Key, kvp.Value)).ToList() ??
                                                     new List<MetadataItem>();

                    string creationTime = s.Metadata.CreationTimestamp.HasValue
                        ? $"{s.Metadata.CreationTimestamp.Value.ToUniversalTime():s} UTC"
                        : string.Empty;

                    List<SecretData> data = s.Data.Select(pair => new SecretData(pair.Key, Encoding.UTF8.GetString(pair.Value))).ToList();

                    return new SecretDetail(
                        s.Metadata.Uid,
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
