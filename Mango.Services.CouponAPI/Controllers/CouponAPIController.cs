using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace Mango.Services.CouponAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CouponAPIController : ControllerBase
	{
		private readonly AppDbContext _db;
		private ResponseDto _response;
		private IMapper _mapper;

		public CouponAPIController(AppDbContext db, IMapper mapper) { 
			_db = db;
			_response = new ResponseDto();
			_mapper = mapper;
		}

		//get all
		[HttpGet]
		public ResponseDto Get()
		{
			try { 
				IEnumerable<Coupon> objList = _db.Coupons.ToList();

				//Convert IEnumerable Coupon to IEnumerable CouponDto
				_response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
				
			}catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}


			return _response;
		}

		//get one by id
		[HttpGet]
		[Route("{id:int}")]  //parameter, search condition 
		public ResponseDto Get(int id)
		{
			try
			{
				Coupon obj = _db.Coupons.First(u=>u.CouponId == id);
				_response.Result = _mapper.Map<CouponDto>(obj);

			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}


			return _response;
		}

		//get one by code
		[HttpGet]
		[Route("GetByCode/{code}")]  //parameter, search condition 
		public ResponseDto GetByCode(string code)
		{
			try
			{
				Coupon obj = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
				_response.Result = _mapper.Map<CouponDto>(obj);
				
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}


			return _response;
		}

		[HttpPut]
		public ResponseDto Put([FromBody] CouponDto couponDto)
		{
			try
			{
				Coupon obj = _mapper.Map<Coupon>(couponDto);
				_db.Coupons.Update(obj);
				_db.SaveChanges();


				
				_response.Result = _mapper.Map<CouponDto>(obj);
				
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}


			return _response;
		}

		[HttpDelete]
		public ResponseDto Delete(int id)
		{
			try
			{
				Coupon obj = _db.Coupons.First(u => u.CouponId == id);
				
				_db.Coupons.Remove(obj);
				_db.SaveChanges();


			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}


			return _response;
		}
	}
}
