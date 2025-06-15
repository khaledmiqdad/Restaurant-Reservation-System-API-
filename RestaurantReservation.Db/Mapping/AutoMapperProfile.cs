using AutoMapper;
using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Db.Mapping
{
    /// <summary>
    /// Defines mapping configuration between domain entities and DTOs using AutoMapper.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RestaurantModel, RestaurantResponse>();
            CreateMap<RestaurantRequest, RestaurantModel>();

            CreateMap<EmployeeModel, EmployeeResponse>();
            CreateMap<EmployeeRequest, EmployeeModel>();

            CreateMap<TableModel, TableResponse>();
            CreateMap<TableRequest, TableModel>();

            CreateMap<MenuItemModel, MenuItemResponse>();
            CreateMap<MenuItemRequest, MenuItemModel>();

            CreateMap<ReservationModel, ReservationResponse>();
            CreateMap<ReservationRequest, ReservationModel>();

            CreateMap<OrderModel, OrderResponse>();
            CreateMap<OrderRequest, OrderModel>();

            CreateMap<OrderItemModel, OrderItemResponse>();
            CreateMap<OrderItemRequest, OrderItemModel>();

            CreateMap<CustomerModel, CustomerResponse>();
            CreateMap<CustomerRequest, CustomerModel>();

            CreateMap<Restaurant, RestaurantModel>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<Customer, CustomerModel>().ReverseMap();
            CreateMap<MenuItem, MenuItemModel>().ReverseMap();
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemModel>().ReverseMap();
            CreateMap<Reservation, ReservationModel>().ReverseMap();
            CreateMap<Table, TableModel>().ReverseMap();

        }
    }
}