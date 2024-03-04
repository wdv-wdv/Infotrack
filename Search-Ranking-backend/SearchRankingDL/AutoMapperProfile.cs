using AutoMapper;
using SearchRankingBL;
using SearchRankingDL.EFmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRankingDL
{
    /// <summary>
    /// Used by AutoMapper for mapping configuration for datalayer 
    /// </summary>
    public class AutoMapperProfileDL : Profile
    {
        public AutoMapperProfileDL() {

            //## Search engine mapping
            CreateMap<SearchEngineCss_EF, String>()
                .ConvertUsing(s => s.Cssselector);
            CreateMap<SearchEngine_EF, SearchEngine>()
                .ForMember(x => x.CSSselectors, opt => opt.MapFrom((src, dest, destMember, context) => src.SearchEngineCsses_EF));

            //## Search object mapping
            CreateMap<Url_EF, String>()
                .ConvertUsing(u => u.Url1);
            CreateMap<String, Url_EF>()
                .ForMember(x => x.Url1, opt => opt.MapFrom(s => s))
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<SearchTerm_EF, Search>()
                .ForMember(x => x.SearchTerm, opt => opt.MapFrom(s => s.SearchTerm1))
                .ForMember(x => x.SearchTermID, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.MaxResults, opt => opt.Ignore())
                .ForMember(x => x.URLs, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Active, opt => opt.MapFrom(s => true));
            CreateMap<string[], Search>()
                .ForMember(x => x.URLs, opt => opt.MapFrom(s => s))
                .ForMember(x => x.SearchTermID, opt => opt.Ignore())
                .ForMember(x => x.MaxResults, opt => opt.Ignore())
                .ForMember(x => x.SearchTerm, opt => opt.Ignore());

            //## Search result mapping
            CreateMap<SearchEngine_EF, String>()
                .ConvertUsing(s => s.Name);
            CreateMap<SearchTerm_EF, String>()
                .ConvertUsing(s => s.SearchTerm1);
            CreateMap<SearchResult_EF, SearchResult>()
                .ForMember(x => x.Ranking, opt => opt.MapFrom(r => r.Count))
                .ForMember(x => x.Time, opt => opt.MapFrom(r => r.Date))
                .ForMember(x => x.SearchEngine, opt => opt.MapFrom((src, dest, destMember, context) => src.SearchEngine_EF))
                .ForMember(x => x.SearchTerm, opt => opt.MapFrom((src, dest, destMember, context) => src.SearchTerm_EF));

        }
    }
}
