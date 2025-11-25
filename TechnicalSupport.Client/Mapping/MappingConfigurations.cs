namespace TechnicalSupport.Client.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserViewModel, UserExportViewModel>()
            .Map(dest => dest.Roles, src => string.Join(" - ", src.Roles));
    }
}
