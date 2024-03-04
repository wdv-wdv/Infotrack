using AutoMapper;
using SearchRankingBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRankingAPI.Models
{
    /// <summary>
    /// Used by AutoMapper for mapping configuration for API 
    /// </summary>
    public class AutoMapperProfileAPI : Profile
    {
        public AutoMapperProfileAPI() {

            CreateMap<SearchDto, Search>()
                .ForMember(x => x.SearchTerm, opt => opt.MapFrom(s => s.SearchTerm))
                .ForMember(x => x.SearchTermID, opt => opt.MapFrom(s => 0))
                .ForMember(x => x.MaxResults, opt => opt.Ignore())
                .ForMember(x => x.URLs, opt => opt.MapFrom(s => new String[] { s.URL }));
        }
    }
}
