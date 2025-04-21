using AuctionService.Data;
using AuctionService.Entities;
using Grpc.Core;
using MassTransit;

namespace AuctionService;

public class GrpcAuctionService : GrpcAuction.GrpcAuctionBase
{
  private readonly AuctionDbContext _dbContext;
  public GrpcAuctionService(AuctionDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public override async Task<GrpcAuctionResponse> GetAuction(GetAuctionRequest request, ServerCallContext context)
  {
    Console.WriteLine("==> Received Grpc request for auction");

    var auction = await _dbContext.Auctions.FindAsync(Guid.Parse(request.Id));

    if (auction == null) throw new RpcException(new Grpc.Core.Status(StatusCode.NotFound, "Not found"));

    var reponse = new GrpcAuctionResponse
    {
      Auction = new GrpcAuctionModel
      {
        AuctionEnd = auction.AuctionEnd.ToString(),
        Id = auction.Id.ToString(),
        ReservePrice = auction.ReservePrice,
        Seller = auction.Seller
      }
    };

    return reponse;
  }
}