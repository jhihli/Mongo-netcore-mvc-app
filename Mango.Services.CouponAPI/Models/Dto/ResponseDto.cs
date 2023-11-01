namespace Mango.Services.CouponAPI.Models.Dto
{
	public class ResponseDto
	{
		//use for any diff type of object of table
		public object? Result { get; set; }
		public bool IsSuccess { get; set; } = true;
		public string Message { get; set; } = "";
	}
}
