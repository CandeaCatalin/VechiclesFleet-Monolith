using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VehiclesFleet.DataAccess.Entities;
using VehiclesFleet.Domain.Dtos.IdentityControllerDtos;
using VehiclesFleet.Repository.Contracts;
using VehiclesFleet.Repository.Contracts.Mappers;
using VehiclesFleet.Services.Contracts;

namespace VehiclesFleet.Repository;

public class UserRepository : IUserRepository
    {

        private readonly UserManager<User> userManager;
        private readonly IUserMapper userMapper;
        private readonly IJwtService jwtService;
        public UserRepository(UserManager<User> userManager,IUserMapper userMapper,IJwtService jwtService)
        {
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.userMapper = userMapper;
        }
        public async Task<string> Login(LoginDto loginDto)
        {
            var existingUser = await GetUserByEmailAsync(loginDto.Email);
            if (existingUser is null)
            {
                throw new Exception("Invalid credentials!");
            }
            await CheckIfPwdsMatchesAsync(existingUser, loginDto.Password);
            var tokenAsString = jwtService.GenerateToken(existingUser);
            return tokenAsString;
        }

        public async Task Register(RegisterDto registerDto)
        {
            var newUser = new User()
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                EmailConfirmed = true,
                UserName = registerDto.Email,
                CreatedAtTimeUtc = DateTime.Now,
            };
            var addResult = await userManager.CreateAsync(newUser, registerDto.Password);
            if (addResult.Errors.Count() != 0)
            {
                throw new Exception($"Account already exists!");
            }
        }
        private async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user;
        }
        private async Task CheckIfPwdsMatchesAsync(User existingUser, string password)
        {
            var isValidPassword = await userManager.CheckPasswordAsync(existingUser, password);
            if (!isValidPassword)
                throw new Exception("Invalid credentials!");
        }
        
        public async Task<List<Domain.Models.Identity.User>> GetAllUsers()
        {
            var allUsers = await userManager.Users.ToListAsync();
            var domainUsers = new List<Domain.Models.Identity.User>();
            foreach(var user in allUsers)
            {
                domainUsers.Add(userMapper.DataAccessToDomain(user));
            }
            return domainUsers;
        }
    }