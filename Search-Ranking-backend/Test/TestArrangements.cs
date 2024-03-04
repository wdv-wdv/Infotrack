using AutoMapper;
using Microsoft.Extensions.Configuration;
using ScarperSelenium;
using SearchRankingAPI.Models;
using SearchRankingBL;
using SearchRankingDL;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class TestArrangements
    {
        private Logger _logger;

        public Logger logger { get
            {

                if (_logger == null)
                {
                    _logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.Debug()
                    .CreateLogger();
                }

                return _logger;
            }
        }

        private IDatalayer _datalayer;
        public IDatalayer datalayer { get
            {
                if(_datalayer == null)
                {
                    _datalayer = new Datalayer();
                }

                return _datalayer;
            }
        }

        private ISearchWorker _searchWorker;
        public ISearchWorker searchWorker { get
            {
                if(_searchWorker == null)
                {
                    _searchWorker = new SearchWorker(datalayer, scarper);
                }
                return _searchWorker;
            }
        }

        private IScarper _scarper;

        public IScarper scarper { get 
            {
                if(_scarper == null)
                {
                    _scarper = new Scarper(logger);
                }
                return _scarper;
            } 
        }

        private IMapper _autoMapperAPI;
        public IMapper autoMapperAPI { get
            {
                if (_autoMapperAPI == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<AutoMapperProfileAPI>();
                    });
                    _autoMapperAPI = config.CreateMapper();
                }
                return _autoMapperAPI;
            } 
        }

    }
}
