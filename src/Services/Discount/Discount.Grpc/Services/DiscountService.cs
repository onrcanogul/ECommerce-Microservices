using Discount.Grpc.Models;
using Discount.Grpc.Repository;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext _context, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            Coupon? coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon == null)
                coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount" };

            logger.LogInformation($"Discount is retrieved for ProductName : {coupon.ProductName}, Amount {coupon.Amount}");

            CouponModel couponModel = coupon.Adapt<CouponModel>();
            return couponModel;

        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            Coupon coupon = request.Coupon.Adapt<Coupon>();
            if(coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument"));
            
            await _context.Coupons.AddAsync(coupon);
            await _context.SaveChangesAsync();

            logger.LogInformation($"Coupon is created, ProductName: {coupon.ProductName}, Amount : {coupon.Amount}...");


            CouponModel couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
            //return request.Coupon; id not added!!
        }


        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            Coupon? coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.Id == request.Coupon.Id);
            if(coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument"));
            }
            coupon.ProductName = request.Coupon.ProductName;
            coupon.Amount = request.Coupon.Amount;
            coupon.Description = request.Coupon.Description;
            _context.Update(coupon);
            await _context.SaveChangesAsync();

            logger.LogInformation($"Coupon is updated, ProductName: {coupon.ProductName}, Amount : {coupon.Amount}...");

            CouponModel couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }


        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            Coupon? coupon = await _context.Coupons.FirstOrDefaultAsync(x => x.Id == request.Coupon.Id);
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument"));

            logger.LogInformation($"Coupon is deleted, ProductName: {coupon.ProductName}, Amount : {coupon.Amount}...");

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            return new() { Success = true };            
        }
    }
}
