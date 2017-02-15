namespace CollectingProductionDataSystem.Web.Areas.MonthlyDataReporting.Models
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using CollectingProductionDataSystem.Infrastructure.Mapping;
    using CollectingProductionDataSystem.Models.Productions;
    using CollectingProductionDataSystem.Models.Productions.Mounthly;
    using Resources = App_GlobalResources.Resources;

    public class UnitsMonthlyConfigDataViewModel : IMapFrom<UnitMonthlyConfig>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Code", ResourceType = typeof(Resources.Layout))]
        public string Code { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "UnitName", ResourceType = typeof(Resources.Layout))]
        public string Name { get; set; }

        public int ProcessUnitId { get; set; }

        public ProcessUnitUnitsMonthlyDataViewModel ProcessUnit { get; set; }

        public int MeasureUnitId { get; set; }

        public MeasureUnitUnitMonthlyDataViewModel MeasureUnit { get; set; }

        [UIHint("Hidden")]
        public int ProductTypeId { get; set; }

        public MonthlyProductTypeViewModel ProductType { get; set; }
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<UnitMonthlyConfig, UnitsMonthlyConfigDataViewModel>()
                .ForMember(p => p.ProcessUnit,opt => opt.MapFrom(p => p.HistorycalProcessUnit?? new ProcessUnit()));
        }
    }
}