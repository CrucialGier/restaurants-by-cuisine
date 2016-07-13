// using Xunit;
// using System.Collections.Generic;
// using System;
// using System.Data;
// using System.Data.SqlClient;
//
// namespace CuisineFinder.Objects
// {
//   public class ReviewTest : IDisposable
//   {
//     public ReviewTest()
//     {
//       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cuisine_database_test;Integrated Security=SSPI;";
//     }
//
//     public void Dispose()
//     {
//       Restaurant.DeleteAll();
//     }
//
//     [Fact]
//     public void Test_DatabaseEmptyAtFirst()
//     {
//       //Arrange, Act
//       int result = Review.GetAll().Count;
//
//       //Assert
//       Assert.Equal(0, result);
//     }
//   }
// }
