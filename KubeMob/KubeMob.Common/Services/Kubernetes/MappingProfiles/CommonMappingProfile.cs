using AutoMapper;
using KubeMob.Common.Services.Kubernetes.Model;

namespace KubeMob.Common.Services.Kubernetes.MappingProfiles
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            this.CreateMap<k8s.Models.V1OwnerReference, OwnerReference>()
                .ConstructUsing((o) => new OwnerReference(
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
        }
    }
}
