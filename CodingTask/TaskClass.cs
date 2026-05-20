using System;
using System.IO;
using System.Threading.Tasks;
using CodingTask.Data;
using CodingTask.Data.Infrastructure;
using CodingTask.Data.Seeding;
using Microsoft.EntityFrameworkCore;

namespace CodingTask
{
    public static class TaskClass
    {
        private const string SeededMessage = "Database seeded!";

        public static async Task<int> Main()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data Source=../CodingTask.Data/codingTask.db")
                .Options;
            var factory = new DefaultDataFactory(
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\CodingTask")));

            using var context = new AppDbContext(options);
            using var uow = new UnitOfWork(context);
            var seeder = new DatabaseSeeder(uow, factory);
            await seeder.SeedAsync().ConfigureAwait(false);
            Console.WriteLine(SeededMessage);
            return 0;
        }

        public static bool ChessMasterBishop(byte a, byte b, byte c, byte d)
        {
            throw new NotImplementedException();
        }

        public static bool ChessMasterCastle(byte a, byte b, byte c, byte d)
        {
            throw new NotImplementedException();
        }

        public static int CountDigitOccuresInNumber(int number, int digit)
        {
            throw new NotImplementedException();
        }

        public static int[][] JaggedArray(int n)
        {
            throw new NotImplementedException();
        }

        public static int[] SortArray(int[] inputArray)
        {
            throw new NotImplementedException();
        }
    }
}