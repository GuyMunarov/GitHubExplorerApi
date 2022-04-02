using AutoMapper;
using Core.Entities;
using GitHubExplorerApi.Dtos;

namespace GitHubExplorerApi.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterDto, AppUser>();
            CreateMap<LoginDto, AppUser>();
            CreateMap<RepositoryToMailDto, GitHubRepository>()
                .ForMember(x => x.AvatarUrl, o => o.MapFrom(s => s.AvatarUrl))
                .ForMember(x => x.name, o => o.MapFrom(s => s.Name))
                .ForMember(x => x.description, o => o.MapFrom(s => s.Description));


            CreateMap<GitHubRepositoryDto, GitHubRepository>().ForMember(x => x.GitHubId, o => o.MapFrom(s => s.id)).ForMember(x => x.Id, o => o.Ignore());
            CreateMap<GitHubRepository, GitHubRepositoryDto>().ForMember(x => x.id, o => o.MapFrom(s => s.GitHubId));

            CreateMap<RepositoryToTransferDto, GitHubRepository>()
                .ForMember(x => x.name, o => o.MapFrom(s => s.name))
                .ForMember(x => x.description, o => o.MapFrom(s => s.description))
                .ForMember(x => x.AvatarUrl, o => o.MapFrom(s => s.avatar_url))
                .ForMember(x => x.GitHubId, o => o.MapFrom(s => s.id)).ForMember(x => x.Id, o => o.Ignore());
            CreateMap<GitHubRepository, RepositoryToTransferDto>().ForMember(x => x.id, o => o.MapFrom(s => s.GitHubId));

        }
    }
}
