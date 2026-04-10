using InventoryManagementSystem_BusinessLayer.UserServices; // تأكد من الـ Namespace
using InventoryManagementSystem_Model.Interfaces;
using InventoryManagementSystem_Model.Models;
using Moq;

namespace InventoryManagementSystem.Tests.BusinessLogic
{
	public class AddServiceTests
	{
		
		[Theory]
		[InlineData("", "ali@test.com", "123", 1)]    
		[InlineData("Ali", "", "123", 1)]             
		[InlineData("Ali", "ali@test.com", "", 1)]    
		[InlineData("Ali", "ali@test.com", "123", 0)] 
		public async Task AddAsync_ShouldReturnFalse_WhenDataIsInvalid(string name, string email, string pass, int role)
		{
			// --- Arrange ---
			// لا نحتاج لبرمجة الموك هنا لأن الكود سيتوقف عند الـ Validation قبل الوصول للريبويتوري
			var mockRepo = new Mock<IAddRepository<User>>();
			var service = new AddUserService(mockRepo.Object);

			var invalidUser = new User
			{
				FullName = name,
				Email = email,
				Password = pass,
				RollID = role
			};

			// --- Act ---
			var result = await service.AddAsync(invalidUser);

			// --- Assert ---
			Assert.False(result);

			// تحقق إضافي: نتأكد أن الدالة لم تقم باستدعاء الريبويتوري أبداً
			// هذا يثبت أن الـ Validation يعمل كحائط صد
			mockRepo.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Never);
		}


		[Fact]
		public async Task AddAsync_ShouldReturnTrue_WhenDataIsValid_And_RepoSucceeds()
		{
			// --- Arrange ---
			var mockRepo = new Mock<IAddRepository<User>>();

			// نبرمج الموك ليرجع ID صحيح (مثلاً 50)
			mockRepo.Setup(r => r.AddAsync(It.IsAny<User>()))
					.ReturnsAsync(50);

			var service = new AddUserService(mockRepo.Object);

			var validUser = new User
			{
				FullName = "Hassan",
				Email = "hassan@test.com",
				Password = "123",
				RollID = 1
			};

			// --- Act ---
			var result = await service.AddAsync(validUser);

			// --- Assert ---
			Assert.True(result); // نتوقع نجاح العملية
		}


		[Fact]
		public async Task AddAsync_ShouldReturnFalse_WhenRepoFails()
		{
			// --- Arrange ---
			var mockRepo = new Mock<IAddRepository<User>>();

			// نبرمج الموك ليرجع -1 (دلالة الفشل)
			mockRepo.Setup(r => r.AddAsync(It.IsAny<User>()))
					.ReturnsAsync(-1);

			var service = new AddUserService(mockRepo.Object);

			var validUser = new User
			{
				FullName = "Hassan",
				Email = "hassan@test.com",
				Password = "123",
				RollID = 1
			};

			// --- Act ---
			var result = await service.AddAsync(validUser);

			// --- Assert ---
			Assert.False(result); // العملية فشلت لأن الداتا أرجعت -1
		}
	}
}