
using ConnectMedia.Common.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace ConnectMedia.Common.Helper
{
    public static class CSVtoModel
    {
        public static List<ResgisterUser> CSVFileToModel(string filepath, string teamName, int CreatedBy)
        {
            List<ResgisterUser> resgisterUserList = new List<ResgisterUser>();
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            CsvRegisterMapping csvMapper = new CsvRegisterMapping();
            CsvParser<ResgisterUser> csvParser = new CsvParser<ResgisterUser>(csvParserOptions, csvMapper);
            var result = csvParser.ReadFromFile(filepath, Encoding.ASCII).ToList();
            foreach (var details in result)
            {
                if (details.IsValid)
                {
                    ResgisterUser resgisterUser = new ResgisterUser();
                    resgisterUser.TeamName = teamName;
                    resgisterUser.SerialNo = details.Result.SerialNo;
                    resgisterUser.Name = details.Result.Name;
                    resgisterUser.Email = details.Result.Email;
                    resgisterUser.Phone = details.Result.Phone;
                    resgisterUser.RegistrationNumber = details.Result.RegistrationNumber;
                    resgisterUser.CreatedOn = DateTime.Now;
                    resgisterUser.CreatedBy = CreatedBy;
                    resgisterUserList.Add(resgisterUser);
                }

            }

            return resgisterUserList;
        }
    }
    public class CsvRegisterMapping : CsvMapping<ResgisterUser>
    {
        public CsvRegisterMapping() : base()
        {
            MapProperty(0, x => x.SerialNo);
            MapProperty(1, x => x.Name);
            MapProperty(2, x => x.Email);
            MapProperty(3, x => x.Phone);
            MapProperty(4, x => x.RegistrationNumber);
        }
    }

}
