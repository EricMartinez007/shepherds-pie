using AutoMapper;
using ShepherdsPie.Models;
using ShepherdsPie.Models.DTOs;

namespace ShepherdsPie;

// AutoMapper reads this class at startup to learn how to turn entities into DTOs.
// Each CreateMap<TSource, TDest>() teaches it one conversion; properties with the
// same name map automatically, and we only spell out the ones that don't line up.
// .ReverseMap() also registers the DTO -> entity direction for create/update use.
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Email/UserName are NOT mapped here: AuthController sets them from the auth
        // claims, and profile.IdentityUser usually isn't loaded (mapping it would
        // null-ref). Reverse direction just round-trips FirstName/LastName/Address.
        CreateMap<UserProfile, UserProfileDTO>().ReverseMap();

        CreateMap<Order, OrderDTO>().ReverseMap();

        // The entity stores toppings through the PizzaTopping join table, but the
        // DTO wants a flat list of toppings. Pull the Topping off each join row;
        // AutoMapper then maps each Topping -> ToppingDTO using the map below.
        // NOTE: ReverseMap cannot un-flatten this projection — going DTO -> Pizza
        // will leave PizzaToppings empty, so set the join rows manually when saving.
        CreateMap<Pizza, PizzaDTO>()
            .ForMember(
                dest => dest.Toppings,
                opt => opt.MapFrom(src => src.PizzaToppings.Select(pt => pt.Topping)))
            .ReverseMap();

        CreateMap<Size, SizeDTO>().ReverseMap();
        CreateMap<Cheese, CheeseDTO>().ReverseMap();
        CreateMap<Sauce, SauceDTO>().ReverseMap();
        CreateMap<Topping, ToppingDTO>().ReverseMap();
    }
}
