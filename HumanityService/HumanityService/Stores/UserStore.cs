using AutoMapper;
using HumanityService.DataContracts;
using HumanityService.Stores.Interfaces;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using HumanityService.Exceptions;

namespace HumanityService.Stores
{
    public class UserStore : IUserStore
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IMapper _mapper;

        private static readonly string[] UsersTableColumns =
        {
            nameof(User.Username),
            nameof(User.Email),
            nameof(User.FirstName),
            nameof(User.LastName),
            nameof(User.PhoneNumber),
            nameof(User.Password)
        };

        private static readonly string[] NgosTableColumns =
{
            nameof(Ngo.Name),
            nameof(Ngo.Username),
            nameof(Ngo.Email),
            nameof(Ngo.Password),
            nameof(Ngo.PhoneNumber),
            nameof(Ngo.RegistrationNumber),
            nameof(Ngo.WebsiteAddress)
        };

        private static readonly string[] LocationsTableColumns =
        {
            nameof(Location.Coordinates),
            nameof(Location.Description)
        };

        public UserStore(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserInfo>();
                cfg.CreateMap<Ngo, NgoInfo>();
            });
            _mapper = configuration.CreateMapper();
        }

        public async Task AddNgo(Ngo ngo)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();
            var sql = new QueryBuilder().InsertInto("ngos", NgosTableColumns).Build();
            await connection.ExecuteAsync(sql, ngo);
        }

        public async Task AddUser(User user)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();
            var sql = new QueryBuilder().InsertInto("users", UsersTableColumns).Build();
            await connection.ExecuteAsync(sql, user);
        }

        public async Task DeleteNgo(string ngoUsername)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder().DeleteFrom("ngos")
                .Where("Username = @Username").Build();

            await connection.ExecuteAsync(sql, new
            {
                Username = ngoUsername
            });
        }

        public async Task DeleteUser(string username)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder().DeleteFrom("users")
                .Where("Username = @Username").Build();

            await connection.ExecuteAsync(sql, new
            {
                Username = username
            });
        }

        public async Task<Ngo> GetNgo(string ngoUsername)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder()
                .SelectColumns("ngos", UsersTableColumns)
                .Where("Username = @Username")
                .Build();

            var ngo = await connection.QueryFirstOrDefaultAsync<Ngo>(sql, new { Username = ngoUsername });
            if (ngo == null)
            {
                throw new StorageErrorException($"Ngo entity with username {ngoUsername} was not found", 404);
            }

            return ngo;
        }

        public async Task<User> GetUser(string username)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder()
                .SelectColumns("users", UsersTableColumns)
                .Where("Username = @Username")
                .Build();

            var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
            if (user == null)
            {
                throw new StorageErrorException($"User entity with username {username} was not found", 404);
            }

            return user;
        }

        public async Task UpdateNgo(Ngo ngo)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder().Update("ngos", UsersTableColumns)
                .Where($"Username = @Username").Build();


            int rowsAffected = await connection.ExecuteAsync(sql, ngo);
            if (rowsAffected == 0)
            {
                if (!await NgoExists(ngo.Username))
                {
                    throw new StorageErrorException($"Ngo entity with username {ngo.Username} was not found", 404);
                }
                throw new StorageErrorException($"The entity you are trying to update has changed, reload the entity and try again", 412);
            }
        }

        public async Task UpdateUser(User user)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder().Update("users", UsersTableColumns)
                .Where($"Username = @Username").Build();

           
            int rowsAffected = await connection.ExecuteAsync(sql, user);
            if (rowsAffected == 0)
            {
                if (!await UserExists(user.Username))
                {
                    throw new StorageErrorException($"User entity with username {user.Username} was not found", 404);
                }
                throw new StorageErrorException($"The entity you are trying to update has changed, reload the entity and try again", 412);
            }
        }


        public async Task<bool> UserExists(string username)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder()
                .Count("users")
                .Where("Username = @Username")
                .Build();

            int count = await connection.QuerySingleAsync<int>(sql, new { Username = username });
            return count > 0;
        }

        public async Task<bool> NgoExists(string ngoUsername)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder()
                .Count("ngos")
                .Where("Username = @Username")
                .Build();

            int count = await connection.QuerySingleAsync<int>(sql, new { Username = ngoUsername });
            return count > 0;
        }

        private UserInfo ToUserInfo(UserEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            var user = _mapper.Map<UserInfo>(entity);
            return user;
        }
    }
}
