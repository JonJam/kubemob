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

            this.CreateMap<k8s.Models.V1Event, ObjectSummary>()
                .ConstructUsing((e) =>
                {
                    Status status = Status.None;

                    switch (e.Type)
                    {
                        case "Warning":
                            status = Status.Warning;
                            break;
                        default:
                            break;
                    }

                    string lastSeen = $"{e.LastTimestamp.Value.ToUniversalTime():s} UTC";

                    return new ObjectSummary(
                        lastSeen,
                        string.Empty,
                        e.Message,
                        status);
                });
        }
    }
}
