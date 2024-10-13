using learning.Data.Entities;
using learning.Models;
using Mapster;

namespace learning.Data
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TimeBillModel, TimeBill>()
                .TwoWays()
                .Map(dest => dest.BillingRate, src => src.Rate)
                .Map(dest => dest.WorkedPerformed, src => src.Work)
                .Map(dest => dest.Hours, src => src.HoursWorked);


        }
    }
}
