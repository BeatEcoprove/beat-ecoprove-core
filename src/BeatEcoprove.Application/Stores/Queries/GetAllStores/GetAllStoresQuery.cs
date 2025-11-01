using BeatEcoprove.Application.Shared;
using BeatEcoprove.Domain.StoreAggregator.Entities;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Queries.GetAllStores;

public record GetAllStoresQuery
(
    Guid ProfileId,
    string? Search,
    string? Services,
    string? Color,
    string? Brand,
    string? OrderBy,
    int Page = 1,
    int PageSize = 10) : IQuery<ErrorOr<List<Order>>>;